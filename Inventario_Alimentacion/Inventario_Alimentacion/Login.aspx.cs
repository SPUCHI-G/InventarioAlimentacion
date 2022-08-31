using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Inventario_Alimentacion.Models;

namespace Inventario_Alimentacion
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ValidarUsuario();
           
        }
        public void ValidarUsuario()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                //para validar usuario
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spValidaUsuario";
                cmd.Parameters.Add("@usuario", SqlDbType.NVarChar).Value = txtUsuario.Text;
                cmd.Parameters.Add("@contrasena", SqlDbType.NVarChar).Value = txtContraseña.Text;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                { 
                    LLenarEntrada();
                    Response.Redirect(@"https://localhost:44370/Home/Index");
                    lblMensaje.Text = "";
                    
                }
                else
                {
                    lblMensaje.Text = "Usuario y o contraseñas incorrectas";
                    lblMensaje.Text = "O verifique si su cuenta esta activada";
                }

            }
            
        }
          public void LLenarEntrada()
        {
            using (SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                        {
                            //para llenar entrada
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.CommandText = "spLlenarEntrada";
                            cmd2.Parameters.Add("@Usuario", SqlDbType.NVarChar).Value = txtUsuario.Text;
                            cmd2.Connection = conn2;
                            conn2.Open();
                            SqlDataReader dr2 = cmd2.ExecuteReader();

                
                        }
        }

        }
       
}
