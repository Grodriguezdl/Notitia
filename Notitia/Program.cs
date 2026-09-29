using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notitia
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Verifica si se pasó el argumento
            if (Environment.GetCommandLineArgs().Contains("--skipLoading"))
            {
                Application.Run(new Login());
            }
            else
            {
                Application.Run(new Carga());
            }
        }
    }
}
