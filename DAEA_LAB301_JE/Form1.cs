using System;
using System.Data.SqlClient; //ado .net
using System.Data;
using System.Windows.Forms;

namespace DAEA_LAB301_JE
{
    public partial class Form1 : Form
    {
        //Nos permite manejar el acceso al servidor
        SqlConnection conn;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Definimos una cadena de conexión
                String servidor = txtServidor.Text;
                String bd = txtBaseDatos.Text;
                String user = txtUsuario.Text;
                String pwd  = txtPassword.Text;

                String str = "Server=" + servidor + ";Database=" + bd + ";";

                //La cadena de conexión  by checkbox
                if (chkAutenticacion.Checked)
                    str += "Integrated Security=True;";
                else
                    str += "User Id=" + user + ";Password=" + pwd + ";";
                
                //Abrir una conexión con el servicor, usando la cadena de conexión
                try
                {
                    conn = new SqlConnection(str);
                    conn.Open();
                    MessageBox.Show("Conectado satisfactoriamente");
                    btnDesconectar.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar el servidor: \n" + ex.ToString());
                }

                // SqlConnection connection = new SqlConnection("data source= ; initial catalog=Neptuno; Integrated Secutiry=True;");
                //connection.Open();
                //SqlCommand command = new SqlCommand("Select * from empleados", connection);
                //SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                //DataTable dataTable = new DataTable();
                //dataAdapter.Fill(dataTable);
                //connection.Close();//Desconectado
                // dgvEmpleados.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Intentamos obtener el estado de la conexión 
            try
            {
                if (conn.State == ConnectionState.Open)
                    MessageBox.Show("Estado del servidor: " + conn.State +
                        "\nVersión del servidor: " + conn.ServerVersion +
                        "\nBase de datos: " + conn.Database);
                else
                    MessageBox.Show("Estado del servidor: " + conn.State);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Imposible determinar el estado del servidor: \n" +
                                 ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            //Para cerrar la conexión verificamos que no este cerrada
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Close();
                    MessageBox.Show("Conexión cerrada satisfactoriamente");
                }
                else
                    MessageBox.Show("La conexión ya esta cerrada");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cerra la conexión: \n" +
                                ex.ToString());
            }
        }

        private void chkAutenticacion_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutenticacion.Checked)
            {
                txtUsuario.Enabled = false;
                txtPassword.Enabled = false;
            }
            else
            {
                txtUsuario.Enabled = true;
                txtPassword.Enabled = true; 
            }
        }

        private void btnPersona_Click(object sender, EventArgs e)
        {
            Persona persona = new Persona(conn);
            persona.Show();
        }
    }
}
