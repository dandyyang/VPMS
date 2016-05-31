using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Process instance = GetRunningInstance();
            if (instance == null)
            {
                //Application.Run(new WindowUI.PublisherTool.PublisherToolMainForm());
                //Application.Run(new frmYCDeviceTs());
                Application.Run(new MainForm());
                //Application.Run(new frmReInitYZ89());
            }
            else
            {
                MessageBox.Show("程序已经打开！");
            }
        }

        public static Process GetRunningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            string currentFileName = currentProcess.MainModule.FileName;
            Process[] processes = Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (Process process in processes)
            {
                if (process.MainModule.FileName == currentFileName)
                {
                    if (process.Id != currentProcess.Id)
                        return process;
                }
            }
            return null;
        }
    }
}
