using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionBD
{
    internal class CConexion : IDisposable
    {
        private SqlConnection conex = new SqlConnection();
        private bool disposed = false;
        private const string puerto = "1433";

        public async Task<SqlConnection> establecerConexionAsync(string servidor, string bd, string usuario, string clave)
        {
            try
            {
                string cadenaConexion = "Data source=" + servidor + ";" +
                        "user id=" + usuario + ";" +
                        "password=" + clave + ";" +
                        "Initial Catalog=" + bd + ";" +
                        "Persist security info=true;" +
                        "Pooling=false;";

                conex.ConnectionString = cadenaConexion;
                await conex.OpenAsync();
                MessageBox.Show("Se estableció la conexión con la base de datos.");
            }
            catch (SqlException e)
            {
                MessageBox.Show("No se ha podido conectar a la base de datos." + e.ToString());
            }
            return conex;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Liberar recursos gestionados aquí.
                    if (conex != null)
                    {
                        conex.Dispose();
                    }
                }
                // Liberar recursos no gestionados aquí.
                disposed = true;
            }
        }
    }

}
