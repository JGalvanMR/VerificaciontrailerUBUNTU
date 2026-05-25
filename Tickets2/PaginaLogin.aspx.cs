using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;

namespace Tickets2
{
    public partial class PaginaLogin : System.Web.UI.Page
    {
        
        dcTicketsDataContext dcDatos;
        DataVerificacionDataContext Dataver;
        //GAB_Irapuato_LocalDataContext GAB_Irapuato_local;
        

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Iniciada"] != null)
            {
                if (Session["Iniciada"].ToString() == "0")
                {
                    MessageBox.Show("Debe iniciar sesion para realizar acciones en el sistema.");
                }
            }

            dcDatos = new dcTicketsDataContext();
            Dataver = new DataVerificacionDataContext();
            //GAB_Irapuato_local = new GAB_Irapuato_LocalDataContext();
            Session["objUser"] = null;
            Session["objAdmin"] = null;
            Session["objAdminMan"] = null;
        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            if (cmbRol.SelectedValue == "user") //Trabajador
            {
                var consulta = from u in dcDatos.Usuario
                               join p in dcDatos.Persona on u.per_ID equals p.per_ID
                               join t in dcDatos.Trabajador on p.per_ID equals t.per_ID
                               where u.usu_Usuario == txtUsuario.Text
                                    && u.usu_Password == txtPass.Text
                                    && p.per_IsActivo == true
                               select u;

                if (consulta != null)
                {
                    if (consulta.Count() > 0)
                    {
                        Session["objUser"] = consulta.First();
                        Response.Redirect("Usuario.aspx");
                    }
                    else
                    {
                        MessageBoxError.Show("Su usuario y/o contraseña no son validos.");
                    }
                }
                else
                {
                    MessageBoxError.Show("Su usuario y/o contraseña no son validos.");
                }

            }
            else if (cmbRol.SelectedValue == "emb")
            {
                #region Production environment
                var consulta = from u in Dataver.Tb_Autoriza_OdeP
                               where u.usuario == txtUsuario.Text
                                    && u.password == txtPass.Text
                                    && u.clave == "TRAIL"
                               select u;
                #endregion
                #region Local environment
                /*var consulta = from u in GAB_Irapuato_local.Tb_Autoriza_OdeP
                               where u.usuario == txtUsuario.Text
                                    && u.password == txtPass.Text
                                    && u.clave == "TRAIL"
                               select u;*/
                #endregion

                if (consulta != null)
                {
                    if (consulta.Count() > 0)
                    {
                        Session["Iniciada"] = "1";
                        Session["objAdmin"] = consulta.First();
                        Response.Redirect("SeleccionarTrailer.aspx");

                        //logueo correcto, insercion a base de datos - tabla registro de movimientos
                        DateTime fechahoramov = DateTime.Now;
                        tb_registro_vertrai insert = new tb_registro_vertrai();
                        insert.fecha = fechahoramov;
                        insert.nom_compu = "WebEmbarques";
                        insert.nom_usu = txtUsuario.Text;
                        insert.tipo_mov = "I";
                        insert.op_clave = "7.9";
                        insert.folio = "7.9";
                        insert.detalle = "Ingreso al sistema";
                        insert.sistema = "EMBWEB";
                        Dataver.tb_registro_vertrai.InsertOnSubmit(insert);
                        Dataver.SubmitChanges();

                    }
                    else
                    {
                        MessageBoxError.Show("Su usuario y/o contraseña no son validos.");
                    }
                }
                else
                {
                    MessageBoxError.Show("Su usuario y/o contraseña no son validos.");
                }
            }
            else if (cmbRol.SelectedValue == "consulta")
            {
                var consulta = from u in Dataver.tb_cat_usuarios
                               where u.usu_login == txtUsuario.Text
                                    && u.usu_password == txtPass.Text
                                    && u.estatus == "A"
                               select u;

                if (consulta != null)
                {
                    if (consulta.Count() > 0)
                    {
                        Session["Iniciada"] = "1";
                        Session["objAdminMan"] = consulta.First();
                        Response.Redirect("AdminManto.aspx");
                    }
                    else
                    {
                        MessageBoxError.Show("Su usuario y/o contraseña no son validos.");
                    }
                }
                else
                {
                    MessageBoxError.Show("Su usuario y/o contraseña no son validos.");
                }
            }
        }
    }
}