using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Ionic.Zip;
using System.Net;
using Datos;
using System.Transactions;
using System.Drawing;
using System.Drawing.Imaging;

namespace Tickets2
{
    public partial class Zip : System.Web.UI.Page
    {
        private DataVerificacionDataContext Dataver;
        tb_cat_usuarios objAdmin = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            Dataver = new DataVerificacionDataContext();
            using (var zip = new ZipFile())
            {
                var originPath = Server.MapPath("~/FotoRevisionTrailer/");
                string foliox = Convert.ToString(Request.QueryString["folio"]);
                string[] substrings = foliox.Split('_');
                string fecha = substrings[0].Insert(2, "/");
                fecha = fecha.Insert(5, "/");
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
                    zip.AddEntry("Contenido.txt", "Este archivo zip contiene las fotos y videos que se han subido al trailer seleccionado");

                    foreach (var i in consulta)
                    {
                        if (i.fotoanden != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.fotoanden);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }

                        }
                        if (i.foto1 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.foto1);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }

                        }
                        if (i.foto2 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.foto2);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.foto3 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.foto3);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.foto4 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.foto4);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.foto5 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.foto5);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.foto6 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.foto6);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.foto7 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.foto7);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.foto8 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.foto8);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.foto9 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.foto9);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.foto10 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.foto10);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.foto11 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.foto11);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.foto12 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.foto12);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.foto13 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.foto13);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.foto14 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.foto14);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Video Trailer");
                            }
                        }
                        if (i.fotoposicion2 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.fotoposicion2);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.fotoposicion4 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.fotoposicion4);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.fotoposicion6 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.fotoposicion6);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.fotoposicion8 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.fotoposicion8);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.fotoposicion10 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.fotoposicion10);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.fotoposicion12 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.fotoposicion12);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.fotoposicion14 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.fotoposicion14);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.fotoposicion16 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.fotoposicion16);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.fotoposicion18 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.fotoposicion18);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.fotoposicion20 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.fotoposicion20);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.fotoposicion22 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.fotoposicion22);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.fotoposicion24 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.fotoposicion24);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.fotoposicion26 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.fotoposicion26);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }
                        if (i.fotoposicion28 != "no.png")
                        {
                            string originFile = System.IO.Path.Combine(originPath, i.fotoposicion28);
                            if (System.IO.File.Exists(originFile))
                            {
                                zip.AddFile(originFile, "Fotos Trailer");
                            }
                        }

                    }
                }
         

                // ZIP THE FOLDER WITH THE FILES IN IT.
                
                

                if (System.IO.File.Exists(Server.MapPath("~/FotoRevisionTrailer/Archivos Trailer.zip")))
                {
                    System.IO.File.Delete(Server.MapPath("~/FotoRevisionTrailer/Archivos Trailer.zip"));
                }

                zip.Save(Server.MapPath("~/FotoRevisionTrailer/Archivos Trailer.zip"));  // SAVE THE ZIP FILE.

                Download(folio);
                
                //Response.Redirect(Server.MapPath("~/FotoRevisionTrailer/Archivos Trailer " + folio + ".zip"));
            };

        }

        public void Download(string folio)
        {
            Response.Clear();
            Response.Charset = "";
            Response.ContentType = "application/zip";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Archivos Trailer " + folio + ".zip");
            Response.TransmitFile(Server.MapPath("~/FotoRevisionTrailer/Archivos Trailer.zip"));
            Response.Flush();
            Response.End();

        }

    }
}