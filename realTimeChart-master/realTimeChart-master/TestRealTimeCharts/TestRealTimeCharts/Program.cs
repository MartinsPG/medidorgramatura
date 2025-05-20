using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestRealTimeCharts
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static int v_id_produto = 0;
        public static string v_produto = "";
        public static double v_densidadegm3 = 0;
        public static double v_coeficientegm3 = 0;
        public static double v_espessuramm = 0;
        public static double v_gramaturagm2 = 0;
        public static Boolean v_readonly = true;

        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmFichas());
            Application.Run(new FormMedicao());
        }
    }
}
