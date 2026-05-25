using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using System.IO;
using System.Transactions;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;

namespace Tickets2
{
    public partial class VerFoto : System.Web.UI.Page
    {
        private DataVerificacionDataContext Dataver;
        tb_cat_usuarios objAdmin = null;
        string verFotosLocal = "http://192.168.123.4:81/verificaciontrailer/FotoRevisionTrailer/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Iniciada"] != null)
            {
                if (Session["Iniciada"].ToString() != "1")
                {
                    Session["Iniciada"] = "0";
                    Response.Redirect("PaginaLogin.aspx");
                }

            }
            else
            {
                Session["Iniciada"] = "0";
                Response.Redirect("PaginaLogin.aspx");
            }


            if (Session["error"] != null)
            {
                if (Session["error"].ToString() != "")
                {
                    MessageBoxError.Show(Session["error"].ToString());
                    Session["error"] = "";
                }

            }


            if (Session["exito"] != null)
            {
                if (Session["exito"].ToString() != "")
                {
                    MessageBoxSuccess.Show(Session["exito"].ToString());
                    Session["exito"] = "";
                }

            }


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

            string fechaStr = substrings[0];        // Ej: "22052026"
            string folioStr = substrings[1];

            // Reconstruir fecha correctamente
            if (fechaStr.Length == 8)
            {
                fechaStr = fechaStr.Insert(2, "/").Insert(5, "/");   // "22/05/2026"
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
                fecha = DateTime.ParseExact(fechaStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                conse = Convert.ToInt32(folioStr);
            }
            catch
            {
                Session["error"] = "Error al procesar la fecha o folio";
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
            if (consulta.Count() > 0)
            {
                foreach (var i in consulta)
                {
                    if (i.fotoanden != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.fotoanden + "' data-src='FotoRevisionTrailer/" + i.fotoanden + "' data-sub-html='<h4>Anden De Carga</h4>'>";
                        html += "<a href = ''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.fotoanden + "'/>";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.foto1 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.foto1 + "' data-src='FotoRevisionTrailer/" + i.foto1 + "' data-sub-html='<h4>Set point Inicial</h4>'>";
                        html += "<a href = ''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.foto1 + "'/>";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.foto2 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.foto2 + "' 375, 'FotoRevisionTrailer/" + i.foto2 + "' 480, 'FotoRevisionTrailer/" + i.foto2 + "' 800' data-src='FotoRevisionTrailer/" + i.foto2 + "' data-sub-html='<h4>Numero de Caja</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.foto2 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.foto3 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.foto3 + "' 375, 'FotoRevisionTrailer/" + i.foto3 + "' 480, 'FotoRevisionTrailer/" + i.foto3 + "' 800' data-src='FotoRevisionTrailer/" + i.foto3 + "' data-sub-html='<h4>Difusor</h4>'>";
                        html += "<a href>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.foto3 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.foto4 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.foto4 + "' 375, 'FotoRevisionTrailer/" + i.foto4 + "' 480, 'FotoRevisionTrailer/" + i.foto4 + "' 800' data-src='FotoRevisionTrailer/" + i.foto4 + "' data-sub-html='<h4>Piso</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.foto4 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.foto5 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.foto5 + "' 375, 'FotoRevisionTrailer/" + i.foto5 + "' 480, 'FotoRevisionTrailer/" + i.foto5 + "' 800' data-src='FotoRevisionTrailer/" + i.foto5 + "' data-sub-html='<h4>Caja Completa</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.foto5 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.foto6 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.foto6 + "' 375, 'FotoRevisionTrailer/" + i.foto6 + "' 480, 'FotoRevisionTrailer/" + i.foto6 + "' 800' data-src='FotoRevisionTrailer/" + i.foto6 + "' data-sub-html='<h4>Temperatura del Producto 1</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.foto6 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.foto7 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.foto7 + "' 375, 'FotoRevisionTrailer/" + i.foto7 + "' 480, 'FotoRevisionTrailer/" + i.foto7 + "' 800' data-src='FotoRevisionTrailer/" + i.foto7 + "' data-sub-html='<h4>Temperatura del Producto 2</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.foto7 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.foto8 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.foto8 + "' 375, 'FotoRevisionTrailer/" + i.foto8 + "' 480, 'FotoRevisionTrailer/" + i.foto8 + "' 800' data-src='FotoRevisionTrailer/" + i.foto8 + "' data-sub-html='<h4>Temperatura del Producto 3</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.foto8 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.foto9 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.foto9 + "' 375, 'FotoRevisionTrailer/" + i.foto9 + "' 480, 'FotoRevisionTrailer/" + i.foto9 + "' 800' data-src='FotoRevisionTrailer/" + i.foto9 + "' data-sub-html='<h4>Temperatura del Producto 4</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.foto9 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.foto10 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.foto10 + "' 375, 'FotoRevisionTrailer/" + i.foto10 + "' 480, 'FotoRevisionTrailer/" + i.foto10 + "' 800' data-src='FotoRevisionTrailer/" + i.foto10 + "' data-sub-html='<h4>Temperatura del Producto 5</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.foto10 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.foto11 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.foto11 + "' 375, 'FotoRevisionTrailer/" + i.foto11 + "' 480, 'FotoRevisionTrailer/" + i.foto11 + "' 800' data-src='FotoRevisionTrailer/" + i.foto11 + "' data-sub-html='<h4>Temperatura del Producto 6</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.foto11 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.foto12 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.foto12 + "' 375, 'FotoRevisionTrailer/" + i.foto12 + "' 480, 'FotoRevisionTrailer/" + i.foto12 + "' 800' data-src='FotoRevisionTrailer/" + i.foto12 + "' data-sub-html='<h4>Set Point Final</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.foto12 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.foto13 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.foto13 + "' 375, 'FotoRevisionTrailer/" + i.foto13 + "' 480, 'FotoRevisionTrailer/" + i.foto13 + "' 800' data-src='FotoRevisionTrailer/" + i.foto13 + "' data-sub-html='<h4>Termino de Carga</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.foto13 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.foto14 != "no.png")
                    {
                        html += "<li class='col-xs-6 col-sm-4 col-md-3 video' data-poster = 'img/Ryanportada.jpg' data-sub-html = 'Video Encendido RYAN' data-html = '#video1' >";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='img/Ryanportada.jpg' />";
                        html += "<div class='demo-gallery-poster'>";
                        html += "<img src = 'img/play-button.png'/>";
                        html += "</div>";
                        html += "</a>";
                        html += "</li>";
                        html2 += "<div style = 'display:none;' id = 'video1' > ";
                        html2 += "<video class = 'lg-video-object lg-html5' controls muted loop preload = 'none' >";
                        html2 += "<source src = 'FotoRevisionTrailer/Converted/" + i.foto14 + "' type = 'video/mp4' > ";
                        html2 += "Su navegador no es compatible con video HTML5.";
                        html2 += "</ video>";
                        html2 += "</ div>";
                        Literalvideo.Text = html2;
                    }
                    if (i.fotoposicion2 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.fotoposicion2 + "' 375, 'FotoRevisionTrailer/" + i.fotoposicion2 + "' 480, 'FotoRevisionTrailer/" + i.fotoposicion2 + "' 800' data-src='FotoRevisionTrailer/" + i.fotoposicion2 + "' data-sub-html='<h4>POSICION 1 Y 2</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.fotoposicion2 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.fotoposicion4 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.fotoposicion4 + "' 375, 'FotoRevisionTrailer/" + i.fotoposicion4 + "' 480, 'FotoRevisionTrailer/" + i.fotoposicion4 + "' 800' data-src='FotoRevisionTrailer/" + i.fotoposicion4 + "' data-sub-html='<h4>POSICION 3 Y 4</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.fotoposicion4 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.fotoposicion6 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.fotoposicion6 + "' 375, 'FotoRevisionTrailer/" + i.fotoposicion6 + "' 480, 'FotoRevisionTrailer/" + i.fotoposicion6 + "' 800' data-src='FotoRevisionTrailer/" + i.fotoposicion6 + "' data-sub-html='<h4>POSICION 5 Y 6</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.fotoposicion6 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.fotoposicion8 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.fotoposicion8 + "' 375, 'FotoRevisionTrailer/" + i.fotoposicion8 + "' 480, 'FotoRevisionTrailer/" + i.fotoposicion8 + "' 800' data-src='FotoRevisionTrailer/" + i.fotoposicion8 + "' data-sub-html='<h4>POSICION 7 Y 8</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.fotoposicion8 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.fotoposicion10 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.fotoposicion10 + "' 375, 'FotoRevisionTrailer/" + i.fotoposicion10 + "' 480, 'FotoRevisionTrailer/" + i.fotoposicion10 + "' 800' data-src='FotoRevisionTrailer/" + i.fotoposicion10 + "' data-sub-html='<h4>POSICION 9 Y 10</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.fotoposicion10 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.fotoposicion12 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.fotoposicion12 + "' 375, 'FotoRevisionTrailer/" + i.fotoposicion12 + "' 480, 'FotoRevisionTrailer/" + i.fotoposicion12 + "' 800' data-src='FotoRevisionTrailer/" + i.fotoposicion12 + "' data-sub-html='<h4>POSICION 11 Y 12</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.fotoposicion12 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.fotoposicion14 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.fotoposicion14 + "' 375, 'FotoRevisionTrailer/" + i.fotoposicion14 + "' 480, 'FotoRevisionTrailer/" + i.fotoposicion14 + "' 800' data-src='FotoRevisionTrailer/" + i.fotoposicion14 + "' data-sub-html='<h4>POSICION 13 Y 14</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.fotoposicion14 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.fotoposicion16 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.fotoposicion16 + "' 375, 'FotoRevisionTrailer/" + i.fotoposicion16 + "' 480, 'FotoRevisionTrailer/" + i.fotoposicion16 + "' 800' data-src='FotoRevisionTrailer/" + i.fotoposicion16 + "' data-sub-html='<h4>POSICION 15 Y 16</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.fotoposicion16 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.fotoposicion18 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.fotoposicion18 + "' 375, 'FotoRevisionTrailer/" + i.fotoposicion18 + "' 480, 'FotoRevisionTrailer/" + i.fotoposicion18 + "' 800' data-src='FotoRevisionTrailer/" + i.fotoposicion18 + "' data-sub-html='<h4>POSICION 17 Y 18</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.fotoposicion18 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.fotoposicion20 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.fotoposicion20 + "' 375, 'FotoRevisionTrailer/" + i.fotoposicion20 + "' 480, 'FotoRevisionTrailer/" + i.fotoposicion20 + "' 800' data-src='FotoRevisionTrailer/" + i.fotoposicion20 + "' data-sub-html='<h4>POSICION 19 Y 20</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.fotoposicion20 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.fotoposicion22 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.fotoposicion22 + "' 375, 'FotoRevisionTrailer/" + i.fotoposicion22 + "' 480, 'FotoRevisionTrailer/" + i.fotoposicion22 + "' 800' data-src='FotoRevisionTrailer/" + i.fotoposicion22 + "' data-sub-html='<h4>POSICION 21 Y 22</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.fotoposicion22 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.fotoposicion24 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.fotoposicion24 + "' 375, 'FotoRevisionTrailer/" + i.fotoposicion24 + "' 480, 'FotoRevisionTrailer/" + i.fotoposicion24 + "' 800' data-src='FotoRevisionTrailer/" + i.fotoposicion24 + "' data-sub-html='<h4>POSICION 23 Y 24</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.fotoposicion24 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.fotoposicion26 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.fotoposicion26 + "' 375, 'FotoRevisionTrailer/" + i.fotoposicion26 + "' 480, 'FotoRevisionTrailer/" + i.fotoposicion26 + "' 800' data-src='FotoRevisionTrailer/" + i.fotoposicion26 + "' data-sub-html='<h4>POSICION 25 Y 26</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.fotoposicion26 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                    if (i.fotoposicion28 != "no.png")
                    {
                        html += " <li class='col-xs-6 col-sm-4 col-md-3' data-responsive='FotoRevisionTrailer/" + i.fotoposicion28 + "' 375, 'FotoRevisionTrailer/" + i.fotoposicion28 + "' 480, 'FotoRevisionTrailer/" + i.fotoposicion28 + "' 800' data-src='FotoRevisionTrailer/" + i.fotoposicion28 + "' data-sub-html='<h4>POSICION 27 Y 28</h4>'>";
                        html += "<a href=''>";
                        html += "<img class='img-responsive' src='FotoRevisionTrailer/" + i.fotoposicion28 + "' />";
                        html += "</a>";
                        html += "</li>";
                    }
                }
            }
            else
            {
                Session["error"] = "El trailer seleccionado aun no tiene fotos a mostrar";
                Response.Redirect("AdminManto.aspx");
            }
            LiteralFoto.Text = html;
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("PaginaLogin.aspx");
        }


    }
}