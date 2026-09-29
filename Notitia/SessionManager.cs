using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notitia
{
    public static class SessionManager
    {
        private static Login loginForm;

        public static void CerrarSesion()
        {
            if (loginForm == null || loginForm.IsDisposed) // AQUI: Verifica si el formulario ya existe o está cerrado
            {
                loginForm = new Login();
                loginForm.ShowDialog();
            }
        }
    }
}