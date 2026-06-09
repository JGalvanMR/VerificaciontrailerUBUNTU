using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Datos;

namespace Tickets2
{
    public partial class VerFoto : System.Web.UI.Page
    {
        private DataVerificacionDataContext Dataver;
        tb_cat_usuarios objAdmin = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarSesion();
            MostrarMensajes();

            if (Session["objAdminMan"] != null)
            {
                objAdmin = (tb_cat_usuarios)Session["objAdminMan"];

                if (!IsPostBack)
                {
                    Dataver = new DataVerificacionDataContext();
                    CargarFotosTrailers();
                }
            }
            else
            {
                Response.Redirect("PaginaLogin.aspx");
            }
        }

        private void ValidarSesion()
        {
            if (Session["Iniciada"] == null || Session["Iniciada"].ToString() != "1")
            {
                Session["Iniciada"] = "0";
                Response.Redirect("PaginaLogin.aspx");
            }
        }

        private void MostrarMensajes()
        {
            if (Session["error"] != null)
            {
                string error = Convert.ToString(Session["error"]);

                if (!string.IsNullOrEmpty(error))
                {
                    MessageBoxError.Show(error);
                    Session["error"] = "";
                }
            }

            if (Session["exito"] != null)
            {
                string exito = Convert.ToString(Session["exito"]);

                if (!string.IsNullOrEmpty(exito))
                {
                    MessageBoxSuccess.Show(exito);
                    Session["exito"] = "";
                }
            }
        }

        private void CargarFotosTrailers()
        {
            string foliox = Convert.ToString(Request.QueryString["folio"]);

            if (string.IsNullOrEmpty(foliox))
            {
                Session["error"] = "Parámetros inválidos";
                Response.Redirect("AdminManto.aspx");
                return;
            }

            string[] substrings = foliox.Split('_');

            if (substrings.Length < 2)
            {
                Session["error"] = "Formato de folio incorrecto";
                Response.Redirect("AdminManto.aspx");
                return;
            }

            string fechaStr = substrings[0];
            string folioStr = substrings[1];

            if (fechaStr.Length == 8)
            {
                fechaStr = fechaStr.Insert(2, "/").Insert(5, "/");
            }
            else
            {
                Session["error"] = "Formato de fecha inválido";
                Response.Redirect("AdminManto.aspx");
                return;
            }

            DateTime fecha;
            int conse;

            try
            {
                fecha = DateTime.ParseExact(
                    fechaStr,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);

                conse = Convert.ToInt32(folioStr);
            }
            catch
            {
                Session["error"] = "Error al procesar fecha y folio";
                Response.Redirect("AdminManto.aspx");
                return;
            }
            var folio = substrings[1].ToString();
            var consulta = from u in Dataver.tb_mstr_trailer
                           join p in Dataver.tb_det_revision_trailer
                           on new { u.conse, u.fecha } equals
                           new { conse = p.conseini, fecha = p.fechaini } into sr
                           from x in sr.DefaultIfEmpty()
                           where u.fecha == Convert.ToDateTime(fecha)
                                && u.conse == Convert.ToInt32(folio)
                           orderby u.conse ascending
                           select new
                           {
                               conse = u.conse,
                               HoraEnt = u.HoraEnt,
                               chofer = u.chofer,
                               no_trailer = u.no_trailer,
                               destino = u.destino,
                               turno = u.turno,
                               responsable = u.responsable,
                               anden = u.anden,
                               pdn_folio = u.pdn_folio,
                               porcentaje = x.porcentaje == null ? "0" : x.porcentaje.ToString(),
                               foto1 = x.setpointini == null ? "no.png" : x.setpointini.ToString(),
                               foto2 = x.numcaja == null ? "no.png" : x.numcaja.ToString(),
                               foto3 = x.difusor == null ? "no.png" : x.difusor.ToString(),
                               foto4 = x.piso == null ? "no.png" : x.piso.ToString(),
                               foto5 = x.cajacompleta == null ? "no.png" : x.cajacompleta.ToString(),
                               foto6 = x.temprod1 == null ? "no.png" : x.temprod1.ToString(),
                               foto7 = x.temprod2 == null ? "no.png" : x.temprod2.ToString(),
                               foto8 = x.temprod3 == null ? "no.png" : x.temprod3.ToString(),
                               foto9 = x.temprod4 == null ? "no.png" : x.temprod4.ToString(),
                               foto10 = x.temprod5 == null ? "no.png" : x.temprod5.ToString(),
                               foto11 = x.temprod6 == null ? "no.png" : x.temprod6.ToString(),
                               foto12 = x.setpointfin == null ? "no.png" : x.setpointfin.ToString(),
                               foto13 = x.termino_carga == null ? "no.png" : x.termino_carga.ToString(),
                               foto14 = x.vidrayan == null ? "no.png" : x.vidrayan.ToString(),
                               fotoanden = x.anden == null ? "no.png" : x.anden.ToString(),
                               fotoposicion2 = x.posunodos == null ? "no.png" : x.posunodos.ToString(),
                               fotoposicion4 = x.postrescuatro == null ? "no.png" : x.postrescuatro.ToString(),
                               fotoposicion6 = x.poscincoseis == null ? "no.png" : x.poscincoseis.ToString(),
                               fotoposicion8 = x.possieteocho == null ? "no.png" : x.possieteocho.ToString(),
                               fotoposicion10 = x.posnuevediez == null ? "no.png" : x.posnuevediez.ToString(),
                               fotoposicion12 = x.posoncedoce == null ? "no.png" : x.posoncedoce.ToString(),
                               fotoposicion14 = x.postrececatorce == null ? "no.png" : x.postrececatorce.ToString(),
                               fotoposicion16 = x.posquincedieciseis == null ? "no.png" : x.posquincedieciseis.ToString(),
                               fotoposicion18 = x.posdiecisietedieciocho == null ? "no.png" : x.posdiecisietedieciocho.ToString(),
                               fotoposicion20 = x.posdiecinueveveinte == null ? "no.png" : x.posdiecinueveveinte.ToString(),
                               fotoposicion22 = x.posventiunoveintidos == null ? "no.png" : x.posventiunoveintidos.ToString(),
                               fotoposicion24 = x.posveintitresveinticuatro == null ? "no.png" : x.posveintitresveinticuatro.ToString(),
                               fotoposicion26 = x.posveinticincoveintiseis == null ? "no.png" : x.posveinticincoveintiseis.ToString(),
                               fotoposicion28 = x.posveintisieteveintiocho == null ? "no.png" : x.posveintisieteveintiocho.ToString(),
                               fecha = u.fecha

                           };

            string html = "";
            string html2 = "";

            foreach (var i in consulta)
            {
                html += CrearImagen(i.fotoanden, "Anden de carga");

                html += CrearImagen(i.foto1, "Set point inicial");
                html += CrearImagen(i.foto2, "Número de caja");
                html += CrearImagen(i.foto3, "Difusor");
                html += CrearImagen(i.foto4, "Piso");
                html += CrearImagen(i.foto5, "Caja completa");

                html += CrearImagen(i.foto6, "Temperatura producto 1");
                html += CrearImagen(i.foto7, "Temperatura producto 2");
                html += CrearImagen(i.foto8, "Temperatura producto 3");
                html += CrearImagen(i.foto9, "Temperatura producto 4");
                html += CrearImagen(i.foto10, "Temperatura producto 5");
                html += CrearImagen(i.foto11, "Temperatura producto 6");

                html += CrearImagen(i.foto12, "Set point final");
                html += CrearImagen(i.foto13, "Término de carga");

                html += CrearImagen(i.fotoposicion2, "Posición 1 y 2");
                html += CrearImagen(i.fotoposicion4, "Posición 3 y 4");
                html += CrearImagen(i.fotoposicion6, "Posición 5 y 6");
                html += CrearImagen(i.fotoposicion8, "Posición 7 y 8");
                html += CrearImagen(i.fotoposicion10, "Posición 9 y 10");
                html += CrearImagen(i.fotoposicion12, "Posición 11 y 12");
                html += CrearImagen(i.fotoposicion14, "Posición 13 y 14");
                html += CrearImagen(i.fotoposicion16, "Posición 15 y 16");
                html += CrearImagen(i.fotoposicion18, "Posición 17 y 18");
                html += CrearImagen(i.fotoposicion20, "Posición 19 y 20");
                html += CrearImagen(i.fotoposicion22, "Posición 21 y 22");
                html += CrearImagen(i.fotoposicion24, "Posición 23 y 24");
                html += CrearImagen(i.fotoposicion26, "Posición 25 y 26");
                html += CrearImagen(i.fotoposicion28, "Posición 27 y 28");

                if (!string.IsNullOrEmpty(i.foto14))
                {
                    html += "<li class='col-xs-6 col-sm-4 col-md-3 video' " +
                            "data-poster='img/Ryanportada.jpg' " +
                            "data-sub-html='Video Encendido RYAN' " +
                            "data-html='#video1'>";

                    html += "<a href=''>";
                    html += "<img class='img-responsive' src='img/Ryanportada.jpg'/>";
                    html += "<div class='demo-gallery-poster'>";
                    html += "<img src='img/play-button.png'/>";
                    html += "</div>";
                    html += "</a>";
                    html += "</li>";

                    html2 += "<div style='display:none;' id='video1'>";
                    html2 += "<video class='lg-video-object lg-html5' controls muted loop preload='none'>";
                    html2 += "<source src='FotoRevisionTrailer/Converted/" + i.foto14 + "' type='video/mp4'>";
                    html2 += "Su navegador no soporta video HTML5.";
                    html2 += "</video>";
                    html2 += "</div>";
                }
            }

            LiteralFoto.Text = html;
            Literalvideo.Text = html2;
        }

        private string CrearImagen(string ruta, string titulo)
        {
            if (string.IsNullOrEmpty(ruta) || ruta == "no.png")
                return "";

            string html = "";

            html += "<li class='col-xs-6 col-sm-4 col-md-3' ";
            html += "data-src='FotoRevisionTrailer/" + ruta + "' ";
            html += "data-sub-html='<h4>" + titulo + "</h4>'>";

            html += "<a href=''>";
            html += "<img class='img-responsive' ";
            html += "src='FotoRevisionTrailer/" + ruta + "' />";
            html += "</a>";

            html += "</li>";

            return html;
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("PaginaLogin.aspx");
        }
    }
}