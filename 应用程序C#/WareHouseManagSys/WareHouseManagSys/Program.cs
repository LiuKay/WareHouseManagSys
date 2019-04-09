using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WareHouseManagSys
{
    static class Program
    {
        public static Form1 f1;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            f1 = new Form1();
            Application.Run(f1);
        }
    }
}
