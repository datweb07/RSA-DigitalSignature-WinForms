namespace RSA_DigitalSignature
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // ===== PANEL SINH KHÓA =====
        private System.Windows.Forms.GroupBox grpGenKey;
        private System.Windows.Forms.Label lblP;
        private System.Windows.Forms.TextBox txtP;
        private System.Windows.Forms.Label lblQ;
        private System.Windows.Forms.TextBox txtQ;
        private System.Windows.Forms.Label lblSecretKey;
        private System.Windows.Forms.TextBox txtSecretKey;
        private System.Windows.Forms.Label lblPublicKey;
        private System.Windows.Forms.TextBox txtPublicKey;
        private System.Windows.Forms.Button btnGenKey;

        // ===== PANEL KÝ VĂN BẢN =====
        private System.Windows.Forms.GroupBox grpSign;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label lblSignatureResult;
        private System.Windows.Forms.TextBox txtSignature;
        private System.Windows.Forms.Button btnSign;

        // ===== PANEL XÁC THỰC =====
        private System.Windows.Forms.GroupBox grpVerify;
        private System.Windows.Forms.Label lblVerifyInput;
        private System.Windows.Forms.TextBox txtVerifyInput;
        private System.Windows.Forms.Label lblVerifyResultLabel;
        private System.Windows.Forms.TextBox txtVerifyResult;
        private System.Windows.Forms.Label lblVerifyStatus;
        private System.Windows.Forms.Button btnVerify;

        // ===== LOG =====
        private System.Windows.Forms.GroupBox grpLog;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Button btnClearLog;

        // Dummy labels (unused but referenced in Form1.cs via out param)
        private System.Windows.Forms.Label lblGenKeyTitle;
        private System.Windows.Forms.Label lblSignTitle;
        private System.Windows.Forms.Label lblVerifyTitle;
        private System.Windows.Forms.Label lblLogTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // ── Form ──────────────────────────────────────────────────
            this.Text = "Chữ Ký Số RSA - TRUONGTHANHDAT";
            this.Size = new System.Drawing.Size(860, 620);
            this.MinimumSize = new System.Drawing.Size(860, 620);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Font = new System.Drawing.Font("Segoe UI", 9f);

            // Dummy labels to satisfy out params from Form1.cs (not added to any container)
            lblGenKeyTitle = new System.Windows.Forms.Label();
            lblSignTitle   = new System.Windows.Forms.Label();
            lblVerifyTitle = new System.Windows.Forms.Label();
            lblLogTitle    = new System.Windows.Forms.Label();

            // ── GroupBox: SINH KHÓA ───────────────────────────────────
            grpGenKey = new System.Windows.Forms.GroupBox();
            grpGenKey.Text = "SINH KHÓA";
            grpGenKey.Location = new System.Drawing.Point(8, 8);
            grpGenKey.Size = new System.Drawing.Size(400, 260);

            // p
            lblP = new System.Windows.Forms.Label();
            lblP.Text = "p (số nguyên tố):";
            lblP.Location = new System.Drawing.Point(12, 30);
            lblP.AutoSize = true;

            txtP = new System.Windows.Forms.TextBox();
            txtP.Text = "13";
            txtP.Location = new System.Drawing.Point(160, 27);
            txtP.Width = 80;

            // q
            lblQ = new System.Windows.Forms.Label();
            lblQ.Text = "q (số nguyên tố):";
            lblQ.Location = new System.Drawing.Point(12, 62);
            lblQ.AutoSize = true;

            txtQ = new System.Windows.Forms.TextBox();
            txtQ.Text = "17";
            txtQ.Location = new System.Drawing.Point(160, 59);
            txtQ.Width = 80;

            // Khóa bí mật eA
            lblSecretKey = new System.Windows.Forms.Label();
            lblSecretKey.Text = "Khóa bí mật eA:";
            lblSecretKey.Location = new System.Drawing.Point(12, 100);
            lblSecretKey.AutoSize = true;

            txtSecretKey = new System.Windows.Forms.TextBox();
            txtSecretKey.Text = "11";
            txtSecretKey.Location = new System.Drawing.Point(160, 97);
            txtSecretKey.Width = 80;

            var lblHintEA = new System.Windows.Forms.Label();
            lblHintEA.Text = "( gcd(eA, φ(n)) = 1 )";
            lblHintEA.Location = new System.Drawing.Point(248, 100);
            lblHintEA.AutoSize = true;

            // Khóa công khai dA
            lblPublicKey = new System.Windows.Forms.Label();
            lblPublicKey.Text = "Khóa công khai dA:";
            lblPublicKey.Location = new System.Drawing.Point(12, 138);
            lblPublicKey.AutoSize = true;

            txtPublicKey = new System.Windows.Forms.TextBox();
            txtPublicKey.Location = new System.Drawing.Point(160, 135);
            txtPublicKey.Width = 80;
            txtPublicKey.ReadOnly = true;

            // Button Sinh khóa
            btnGenKey = new System.Windows.Forms.Button();
            btnGenKey.Text = "Sinh Khóa";
            btnGenKey.Location = new System.Drawing.Point(140, 175);
            btnGenKey.Size = new System.Drawing.Size(120, 28);
            btnGenKey.Click += new System.EventHandler(this.btnGenKey_Click);

            grpGenKey.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblP, txtP, lblQ, txtQ,
                lblSecretKey, txtSecretKey, lblHintEA,
                lblPublicKey, txtPublicKey,
                btnGenKey
            });
            this.Controls.Add(grpGenKey);

            // ── GroupBox: KÝ VĂN BẢN ─────────────────────────────────
            grpSign = new System.Windows.Forms.GroupBox();
            grpSign.Text = "KÝ VĂN BẢN";
            grpSign.Location = new System.Drawing.Point(8, 276);
            grpSign.Size = new System.Drawing.Size(400, 200);

            lblMessage = new System.Windows.Forms.Label();
            lblMessage.Text = "Văn bản M:";
            lblMessage.Location = new System.Drawing.Point(12, 30);
            lblMessage.AutoSize = true;

            txtMessage = new System.Windows.Forms.TextBox();
            txtMessage.Text = "TRUONGTHANHDAT";
            txtMessage.Location = new System.Drawing.Point(110, 27);
            txtMessage.Width = 270;

            lblSignatureResult = new System.Windows.Forms.Label();
            lblSignatureResult.Text = "Chữ ký S:";
            lblSignatureResult.Location = new System.Drawing.Point(12, 68);
            lblSignatureResult.AutoSize = true;

            txtSignature = new System.Windows.Forms.TextBox();
            txtSignature.Location = new System.Drawing.Point(110, 65);
            txtSignature.Size = new System.Drawing.Size(270, 60);
            txtSignature.Multiline = true;
            txtSignature.ReadOnly = true;
            txtSignature.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            btnSign = new System.Windows.Forms.Button();
            btnSign.Text = "Ký Văn Bản";
            btnSign.Location = new System.Drawing.Point(140, 148);
            btnSign.Size = new System.Drawing.Size(120, 28);
            btnSign.Click += new System.EventHandler(this.btnSign_Click);

            grpSign.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblMessage, txtMessage,
                lblSignatureResult, txtSignature,
                btnSign
            });
            this.Controls.Add(grpSign);

            // ── GroupBox: XÁC THỰC CHỮ KÝ ────────────────────────────
            grpVerify = new System.Windows.Forms.GroupBox();
            grpVerify.Text = "XÁC THỰC CHỮ KÝ";
            grpVerify.Location = new System.Drawing.Point(416, 8);
            grpVerify.Size = new System.Drawing.Size(428, 220);

            lblVerifyInput = new System.Windows.Forms.Label();
            lblVerifyInput.Text = "Chữ ký S:";
            lblVerifyInput.Location = new System.Drawing.Point(12, 30);
            lblVerifyInput.AutoSize = true;

            txtVerifyInput = new System.Windows.Forms.TextBox();
            txtVerifyInput.Location = new System.Drawing.Point(90, 27);
            txtVerifyInput.Size = new System.Drawing.Size(326, 55);
            txtVerifyInput.Multiline = true;
            txtVerifyInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            lblVerifyResultLabel = new System.Windows.Forms.Label();
            lblVerifyResultLabel.Text = "Kết quả:";
            lblVerifyResultLabel.Location = new System.Drawing.Point(12, 100);
            lblVerifyResultLabel.AutoSize = true;

            txtVerifyResult = new System.Windows.Forms.TextBox();
            txtVerifyResult.Location = new System.Drawing.Point(90, 97);
            txtVerifyResult.Width = 326;
            txtVerifyResult.ReadOnly = true;

            lblVerifyStatus = new System.Windows.Forms.Label();
            lblVerifyStatus.Text = "— chưa xác thực —";
            lblVerifyStatus.Location = new System.Drawing.Point(90, 130);
            lblVerifyStatus.Size = new System.Drawing.Size(326, 22);
            lblVerifyStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            btnVerify = new System.Windows.Forms.Button();
            btnVerify.Text = "Xác Thực";
            btnVerify.Location = new System.Drawing.Point(154, 162);
            btnVerify.Size = new System.Drawing.Size(120, 28);
            btnVerify.Click += new System.EventHandler(this.btnVerify_Click);

            grpVerify.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblVerifyInput, txtVerifyInput,
                lblVerifyResultLabel, txtVerifyResult,
                lblVerifyStatus,
                btnVerify
            });
            this.Controls.Add(grpVerify);

            // ── GroupBox: NHẬT KÝ TÍNH TOÁN ──────────────────────────
            grpLog = new System.Windows.Forms.GroupBox();
            grpLog.Text = "Nhật Ký Tính Toán";
            grpLog.Location = new System.Drawing.Point(416, 236);
            grpLog.Size = new System.Drawing.Size(428, 240);

            txtLog = new System.Windows.Forms.RichTextBox();
            txtLog.Location = new System.Drawing.Point(8, 20);
            txtLog.Size = new System.Drawing.Size(408, 178);
            txtLog.ReadOnly = true;
            txtLog.Font = new System.Drawing.Font("Consolas", 8.5f);
            txtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;

            btnClearLog = new System.Windows.Forms.Button();
            btnClearLog.Text = "Xóa Log";
            btnClearLog.Location = new System.Drawing.Point(328, 204);
            btnClearLog.Size = new System.Drawing.Size(88, 26);
            btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);

            grpLog.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                txtLog, btnClearLog
            });
            this.Controls.Add(grpLog);
        }
    }
}
