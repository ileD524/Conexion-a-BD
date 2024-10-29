using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ConexionBD
{
    public partial class Form1 : Form
    {
        private SqlConnection conn;
        public Form1()
        {
            InitializeComponent();
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            // Obtener valores de los campos de texto
            string servidor = txtServidor.Text; // Campo de texto para servidor
            string bd = txtBD.Text; // Campo de texto para base de datos
            string usuario = txtUsuario.Text; // Campo de texto para usuario
            string clave = txtClave.Text; // Campo de texto para contraseña

            // Crear una instancia de la clase CConexion
            CConexion miConexion = new CConexion(); // Asegúrate de que el nombre de la clase coincida

            // Establecer la conexión usando la instancia
            conn = await miConexion.establecerConexionAsync(servidor, bd, usuario, clave);

            // Cargar la tabla usando la conexión establecida
            await CargarTablaAsync(conn);
        }

        private async Task CargarTablaAsync(SqlConnection conn)
        {
            if (conn != null)
            {
                // Crear un DataAdapter para llenar un DataTable
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Peliculas", conn);
                DataTable dataTable = new DataTable();

                // Llenar el DataTable con datos de la base de datos
                await Task.Run(() => dataAdapter.Fill(dataTable));

                // Asignar el DataTable al DataGridView
                dataGridView1.DataSource = dataTable;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();       // Cierra la conexión
                conn.Dispose();     // Libera todos los recursos de la conexión
                SqlConnection.ClearPool(conn); // Libera el pool de conexiones específico
                conn = null;        // Establece el objeto en null para indicar que no hay conexión activa
                dataGridView1.DataSource = null; // Retira el contenido de la tabla
                MessageBox.Show("Conexión cerrada correctamente.");
            }
            else
            {
                MessageBox.Show("La conexión ya está cerrada o no se ha establecido.");
            }
            
        }
    } 
}
