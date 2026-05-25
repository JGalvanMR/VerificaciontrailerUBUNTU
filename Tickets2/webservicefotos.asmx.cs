using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Drawing;

namespace Tickets2
{
    /// <summary>
    /// Descripción breve de webservicefotos
    /// </summary>
    [WebService(Namespace = "http://189.206.160.206:81/verificacionTrailer")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class webservicefotos : System.Web.Services.WebService
    {
        private SqlConnection con;
        private DataTable dt = new DataTable();
        private DataTable dt1 = new DataTable();
        private DataTable dt2 = new DataTable();
        private DataTable dt3 = new DataTable();
        private SqlCommand cmd;

        [WebMethod]
        private void Conectar()
        {
            //con = new SqlConnection("Persist Security Info=False;user id=sa; password=Gabira2026$;Initial Catalog =GAB_Irapuato; server=tcp:192.168.123.6,1433; Connect Timeout = 9999");
            con = new SqlConnection("Persist Security Info=False;user id=sa; password=Gabira2026$;Initial Catalog =GAB_Irapuato; server=tcp:189.206.160.206,2352; Connect Timeout = 9999");
            con.Open();
        }

        private void Desconectar()
        {
            con.Close();
        }

        private void CrearComando(String consulta)
        {
            cmd = new SqlCommand(consulta, con);
        }

        private int EjecutarConsulta()
        {
            int numReg;
            numReg = cmd.ExecuteNonQuery();
            return numReg;
        }

        private string EjecutarConsultatraeregistro()
        {
            string numReg;
            numReg = Convert.ToString(cmd.ExecuteScalar());
            return numReg;
        }

        private void AsignarParametro(string param, SqlDbType tipo, object value)
        {
            cmd.Parameters.Add(param, tipo).Value = value;
        }


        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public string BajarRecibo(byte[] foto, string nombre_archivo, string fechatrailer, string conse, string pos)
        {


            var fechaactual2 = Convert.ToDateTime(fechatrailer).ToString("ddMMyyyy");

            var mes_anio = fechaactual2.Remove(0, 2);

            // Specify the path to save the uploaded file to.
            string savePath = "~/FotoRevisionTrailer/";
            savePath = Server.MapPath(savePath);

            var existe = Directory.Exists(savePath + mes_anio);

            if (!Directory.Exists(savePath + mes_anio))
            {
                System.IO.Directory.CreateDirectory(savePath + mes_anio);
            }

            // Get the name of the file to upload.
            string fileName = nombre_archivo;
            string campoactualizar = "";

            if (pos == "2") {
                campoactualizar = "posunodos";
            }
            else if (pos == "4")
            {
                campoactualizar = "postrescuatro";
            }
            else if (pos == "6")
            {
                campoactualizar = "poscincoseis";
            }
            else if (pos == "8")
            {
                campoactualizar = "possieteocho";
            }
            else if (pos == "10")
            {
                campoactualizar = "posnuevediez";
            }
            else if (pos == "12")
            {
                campoactualizar = "posoncedoce";
            }
            else if (pos == "14")
            {
                campoactualizar = "postrececatorce";
            }
            else if (pos == "16")
            {
                campoactualizar = "posquincedieciseis";
            }
            else if (pos == "18")
            {
                campoactualizar = "posdiecisietedieciocho";
            }
            else if (pos == "20")
            {
                campoactualizar = "posdiecinueveveinte";
            }
            else if (pos == "22")
            {
                campoactualizar = "posventiunoveintidos";
            }
            else if (pos == "24")
            {
                campoactualizar = "posveintitresveinticuatro";
            }
            else if (pos == "26")
            {
                campoactualizar = "posveinticincoveintiseis";
            }
            else if (pos == "28")
            {
                campoactualizar = "posveintisieteveintiocho";
            }
            Conectar();
            CrearComando("UPDATE  tb_det_revision_trailer SET " + campoactualizar + " = '" + mes_anio + "/" + nombre_archivo + "' WHERE fecha = '" + fechatrailer + "' AND conse = '"+conse+"'");
            EjecutarConsultatraeregistro();
            Desconectar();

            try
            {
                MemoryStream ms = new MemoryStream(foto);
                Bitmap bmp = new Bitmap(ms);
                bmp.Save(savePath + mes_anio + "/" + Path.GetFileName(nombre_archivo), bmp.RawFormat);
            }
            catch (Exception e)
            {
                return e.ToString();
            
            }
            return "1";
        }

    }
}
