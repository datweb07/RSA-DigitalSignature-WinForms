using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

namespace RSA_DigitalSignature
{
    public partial class Form1 : Form
    {
        private long _n;
        private long _phi;
        private long _eA;
        private long _dA;
        private List<long> _lastSignatures = new List<long>();

        public Form1()
        {
            InitializeComponent();
            this.Text = "Chữ Ký Số RSA";
            SetDefaults();
        }

        private void SetDefaults()
        {
            txtP.Text = "13";
            txtQ.Text = "17";
            txtSecretKey.Text = "11";
        }

        private void btnGenKey_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtP.Text.Trim(), out int p) || !IsPrime(p))
                {
                    ShowError("p phải là số nguyên tố hợp lệ!");
                    txtP.Focus();
                    return;
                }

                if (!int.TryParse(txtQ.Text.Trim(), out int q) || !IsPrime(q))
                {
                    ShowError("q phải là số nguyên tố hợp lệ!");
                    txtQ.Focus();
                    return;
                }

                if (p == q)
                {
                    ShowError("p và q phải khác nhau!");
                    return;
                }

                if (!long.TryParse(txtSecretKey.Text.Trim(), out long eA))
                {
                    ShowError("Khóa bí mật eA phải là số nguyên!");
                    txtSecretKey.Focus();
                    return;
                }

                long n = (long)p * q;
                long phi = (long)(p - 1) * (q - 1);

                if (eA <= 1 || eA >= phi)
                {
                    ShowError($"Điều kiện: 1 < eA < φ(n) = {phi}");
                    txtSecretKey.Focus();
                    return;
                }

                long gcd = GCD(eA, phi);
                if (gcd != 1)
                {
                    ShowError($"GCD({eA}, {phi}) = {gcd} ≠ 1\neA phải nguyên tố cùng nhau với φ(n)!");
                    return;
                }

                long dA = ModInverse(eA, phi);

                _n = n;
                _phi = phi;
                _eA = eA;
                _dA = dA;

                txtPublicKey.Text = dA.ToString();

                AppendLog("--- SINH KHOA ---");
                AppendLog($"  p = {p}");
                AppendLog($"  q = {q}");
                AppendLog($"  n = p x q = {p} x {q} = {n}");
                AppendLog($"  phi(n) = (p-1)(q-1) = {p - 1} x {q - 1} = {phi}");
                AppendLog($"  GCD(eA={eA}, phi(n)={phi}) = {gcd}");
                AppendLog($"  Khoa bi mat  eA = {eA}");
                AppendLog($"  Khoa cong khai dA = {dA}");
                AppendLog($"  Kiem tra: ({eA} x {dA}) mod {phi} = {(eA * dA) % phi}");
                AppendLog("--------------------------------------------------");
            }
            catch (Exception ex)
            {
                ShowError("Lỗi: " + ex.Message);
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            try
            {
                if (_n == 0 || _eA == 0)
                {
                    ShowError("Vui lòng sinh khóa trước!");
                    return;
                }

                string message = txtMessage.Text.Trim().Replace(" ", "").ToUpperInvariant();

                if (string.IsNullOrEmpty(message))
                {
                    ShowError("Văn bản không được rỗng!");
                    txtMessage.Focus();
                    return;
                }

                List<int> asciiValues = new List<int>();
                foreach (char c in message)
                {
                    int ascii = (int)c;
                    if (ascii >= _n)
                    {
                        ShowError($"ASCII của '{c}' = {ascii} >= n = {_n}\nRSA yêu cầu mỗi ký tự < n!");
                        return;
                    }
                    asciiValues.Add(ascii);
                }

                List<long> signatures = new List<long>();
                foreach (int m in asciiValues)
                {
                    long sig = ModPow(m, _eA, _n);
                    signatures.Add(sig);
                }

                _lastSignatures = signatures;
                txtSignature.Text = string.Join(" ", signatures);

                txtVerifyInput.Text = string.Join(" ", signatures);
                txtVerifyMessage.Text = message;   

                AppendLog("--- KY VAN BAN ---");
                AppendLog($"  Van ban M = {message}");
                AppendLog($"  ASCII M = [{string.Join(", ", asciiValues)}]");
                AppendLog($"  Cong thuc: Si = Mi^eA mod n = Mi^{_eA} mod {_n}");
                AppendLog("");
                AppendLog($"  {"Ky tu",-8} {"ASCII",-8} {"Cong thuc",-22} {"Chu ky",-8}");
                AppendLog("  " + new string('-', 50));
                for (int i = 0; i < message.Length; i++)
                {
                    AppendLog($"  {message[i],-8} {asciiValues[i],-8} {asciiValues[i]}^{_eA} mod {_n,-10} {signatures[i],-8}");
                }
                AppendLog("");
                AppendLog($"  Chu ky S = [{string.Join(", ", signatures)}]");
                AppendLog("--------------------------------------------------");
            }
            catch (Exception ex)
            {
                ShowError("Lỗi ký: " + ex.Message);
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                if (_n == 0 || _dA == 0)
                {
                    ShowError("Vui lòng sinh khóa trước!");
                    return;
                }

                string rawInput = txtVerifyInput.Text.Trim();
                if (string.IsNullOrEmpty(rawInput))
                {
                    ShowError("Chữ ký không được rỗng!");
                    return;
                }

                string[] parts = rawInput.Split(new char[] { ' ', ',', '[', ']' },
                    StringSplitOptions.RemoveEmptyEntries);

                List<long> sigs = new List<long>();
                foreach (string part in parts)
                {
                    if (!long.TryParse(part.Trim(), out long val))
                    {
                        ShowError($"Giá trị '{part}' không hợp lệ trong chữ ký!");
                        return;
                    }
                    sigs.Add(val);
                }

                List<long> recovered = new List<long>();
                foreach (long sig in sigs)
                {
                    long val = ModPow(sig, _dA, _n);
                    recovered.Add(val);
                }

                StringBuilder sb = new StringBuilder();
                bool allValid = true;
                foreach (long ascii in recovered)
                {
                    if (ascii < 32 || ascii > 126)
                    {
                        allValid = false;
                        break;
                    }
                    sb.Append((char)ascii);
                }

                string recoveredText = allValid ? sb.ToString() : $"[ASCII: {string.Join(", ", recovered)}]";

                string originalMessage = txtVerifyMessage.Text
                    .Trim()
                    .Replace(" ", "")
                    .ToUpperInvariant();

                AppendLog("--- XAC THUC CHU KY ---");
                AppendLog($"  Van ban M (goc) = {originalMessage}");
                AppendLog($"  Chu ky S = [{string.Join(", ", sigs)}]");
                AppendLog($"  Cong thuc: Mi' = Si^dA mod n = Si^{_dA} mod {_n}");
                AppendLog("");
                AppendLog($"  {"Chu ky Si",-12} {"Cong thuc",-24} {"Mi'",-8} {"Ky tu",-6}");
                AppendLog("  " + new string('-', 54));
                for (int i = 0; i < sigs.Count; i++)
                {
                    string charStr = (recovered[i] >= 32 && recovered[i] <= 126)
                        ? ((char)recovered[i]).ToString() : "?";
                    AppendLog($"  {sigs[i],-12} {sigs[i]}^{_dA} mod {_n,-10} {recovered[i],-8} {charStr}");
                }
                AppendLog("");
                AppendLog($"  ASCII khoi phuc = [{string.Join(", ", recovered)}]");
                AppendLog($"  Van ban khoi phuc = {recoveredText}");
                AppendLog($"  So sanh: '{recoveredText}' == '{originalMessage}' => {recoveredText == originalMessage}");
                AppendLog("--------------------------------------------------");

                bool valid = allValid && recoveredText == originalMessage;

                if (valid)
                {
                    txtVerifyResult.Text = recoveredText;
                    lblVerifyStatus.Text = "CHU KY HOP LE (VALID)";
                    lblVerifyStatus.ForeColor = System.Drawing.Color.Green;
                    AppendLog("  KET QUA: CHU KY HOP LE (VALID)");
                }
                else
                {
                    txtVerifyResult.Text = recoveredText;
                    lblVerifyStatus.Text = "CHU KY KHONG HOP LE (INVALID)";
                    lblVerifyStatus.ForeColor = System.Drawing.Color.Red;
                    AppendLog("  KET QUA: CHU KY KHONG HOP LE (INVALID)");
                }
                AppendLog("══════════════════════════════════════════════════");
            }
            catch (Exception ex)
            {
                ShowError("Lỗi xác thực: " + ex.Message);
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }


        private void AppendLog(string text)
        {
            txtLog.AppendText(text + Environment.NewLine);
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }

        private static void ShowError(string msg)
        {
            MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static void ShowSuccess(string msg)
        {
            MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        static bool IsPrime(int n)
        {
            if (n < 2) return false;
            if (n == 2) return true;
            if (n % 2 == 0) return false;
            for (int i = 3; i * i <= n; i += 2)
                if (n % i == 0) return false;
            return true;
        }

        static long GCD(long a, long b)
        {
            while (b != 0) { long r = a % b; a = b; b = r; }
            return Math.Abs(a);
        }

        static long ExtendedGCD(long a, long b, out long x, out long y)
        {
            if (b == 0) { x = 1; y = 0; return a; }
            long g = ExtendedGCD(b, a % b, out long x1, out long y1);
            x = y1;
            y = x1 - (a / b) * y1;
            return g;
        }

        static long ModInverse(long a, long m)
        {
            long g = ExtendedGCD(a, m, out long x, out _);
            if (g != 1) throw new InvalidOperationException("Không tồn tại nghịch đảo modulo.");
            return (x % m + m) % m;
        }

        static long ModPow(long baseVal, long exp, long mod)
        {
            long result = 1;
            baseVal %= mod;
            while (exp > 0)
            {
                if ((exp & 1) == 1)
                    result = (long)((BigInteger)result * baseVal % mod);
                baseVal = (long)((BigInteger)baseVal * baseVal % mod);
                exp >>= 1;
            }
            return result;
        }
    }
}
