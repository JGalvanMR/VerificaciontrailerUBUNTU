using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ionic.Zip;
using Datos;

namespace Tickets2
{
    public partial class Zip : System.Web.UI.Page
    {
        private DataVerificacionDataContext Dataver;
        tb_cat_usuarios objAdmin = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            Dataver = new DataVerificacionDataContext();

            try
            {
                string foliox = Convert.ToString(Request.QueryString["folio"]);

                if (string.IsNullOrEmpty(foliox))
                {
                    lblMsg.InnerText = "No se recibió el parámetro folio.";
                    return;
                }

                string[] substrings = foliox.Split('_');

                if (substrings.Length < 2)
                {
                    lblMsg.InnerText = "Formato de folio incorrecto.";
                    return;
                }

                string fechaStr = substrings[0];

                fechaStr = fechaStr.Insert(2, "/");
                fechaStr = fechaStr.Insert(5, "/");

                DateTime fecha;
                int conse;

                try
                {
                    fecha = DateTime.ParseExact(
                        fechaStr,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture);

                    conse = Convert.ToInt32(substrings[1]);
                }
                catch
                {
                    lblMsg.InnerText = "Error al procesar fecha y folio.";
                    return;
                }

                string originPath = Server.MapPath("~/FotoRevisionTrailer/");

                using (var zip = new ZipFile())
                {
                    var consulta = from u in Dataver.tb_mstr_trailer
                                   join p in Dataver.tb_det_revision_trailer
                                   on new { u.conse, u.fecha } equals
                                   new { conse = p.conseini, fecha = p.fechaini } into sr
                                   from x in sr.DefaultIfEmpty()
                                   where u.fecha == fecha
                                        && u.conse == conse
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

                    if (consulta.Count() <= 0)
                    {
                        lblMsg.InnerText = "No se encontró información.";
                        return;
                    }

                    zip.AddEntry(
                        "Contenido.txt",
                        "Este archivo zip contiene las fotos y videos que se han subido al trailer seleccionado");

                    foreach (var i in consulta)
                    {
                        List<string> fotos = new List<string>()
                        {
                            i.fotoanden,

                            i.foto1,
                            i.foto2,
                            i.foto3,
                            i.foto4,
                            i.foto5,
                            i.foto6,
                            i.foto7,
                            i.foto8,
                            i.foto9,
                            i.foto10,
                            i.foto11,
                            i.foto12,
                            i.foto13,

                            i.fotoposicion2,
                            i.fotoposicion4,
                            i.fotoposicion6,
                            i.fotoposicion8,
                            i.fotoposicion10,
                            i.fotoposicion12,
                            i.fotoposicion14,
                            i.fotoposicion16,
                            i.fotoposicion18,
                            i.fotoposicion20,
                            i.fotoposicion22,
                            i.fotoposicion24,
                            i.fotoposicion26,
                            i.fotoposicion28
                        };

                        foreach (string foto in fotos)
                        {
                            AgregarArchivoZip(zip, originPath, foto, "Fotos Trailer");
                        }

                        if (i.foto14 != "no.png")
                        {
                            AgregarArchivoZip(zip, originPath, i.foto14, "Video Trailer");
                        }
                    }

                    string zipPath = Server.MapPath("~/FotoRevisionTrailer/Archivos Trailer.zip");

                    if (File.Exists(zipPath))
                    {
                        File.Delete(zipPath);
                    }

                    zip.Save(zipPath);
                }

                Download(conse.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.InnerText = ex.Message;
            }
        }

        private void AgregarArchivoZip(
            ZipFile zip,
            string originPath,
            string nombreArchivo,
            string carpetaZip)
        {
            try
            {
                if (nombreArchivo == "no.png")
                {
                    return;
                }

                string originFile = Path.Combine(originPath, nombreArchivo);

                if (File.Exists(originFile))
                {
                    zip.AddFile(originFile, carpetaZip);
                }
            }
            catch
            {

            }
        }

        public void Download(string folio)
        {
            string zipPath = Server.MapPath("~/FotoRevisionTrailer/Archivos Trailer.zip");

            if (!File.Exists(zipPath))
            {
                lblMsg.InnerText = "No se encontró el archivo ZIP.";
                return;
            }

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();

            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/zip";

            Response.AppendHeader(
                "Content-Disposition",
                "attachment; filename=Archivos Trailer " + folio + ".zip");

            Response.TransmitFile(zipPath);

            Response.Flush();
            Response.End();
        }
    }
}