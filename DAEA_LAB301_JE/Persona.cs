using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAEA_LAB301_JE
{
    public partial class Persona : Form
    {
        SqlConnection conn;
        DataTable dataTable = new DataTable();

        public Persona(SqlConnection conn)
        {
            this.conn = conn;
            InitializeComponent();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    List<Usuario> usuarios = new List<Usuario>();


                    String sql = "select * from Usuario";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dgvListado.DataSource = dt;
                    dgvListado.Refresh();
                }
                else
                {
                    MessageBox.Show("La conexión esta cerrada");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Imposible de leer los usuarios: \n" +
                                 ex.ToString());
            }
        }
    }
}
