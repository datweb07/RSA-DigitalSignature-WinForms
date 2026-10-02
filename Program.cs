using System;
using System.Windows.Forms;

namespace RSA_DigitalSignature
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}