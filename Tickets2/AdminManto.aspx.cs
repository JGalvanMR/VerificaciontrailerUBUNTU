using Datos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tickets2
{
    public partial class AdminManto : System.Web.UI.Page
    {
        //private dcTicketsDataContext dcDatos;
        private DataVerificacionDataContext Dataver;
        tb_cat_usuarios objAdmin = null;



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

            Dataver = new DataVerificacionDataContext();

            if (Session["objAdminMan"] != null)
            {
                objAdmin = (tb_cat_usuarios)Session["objAdminMan"];

                if (!IsPostBack)
                {
                    string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                    fechafinal.Text = fecha;
                    fechainicial.Text = fecha;
                    CargarGridTrailers(fecha, fecha);
                }
            }
            else
            {
                Response.Redirect("PaginaLogin.aspx");
            }
        }

        private void CargarGridTrailers2(string inicio, string final)
        {

            var fechaactual = DateTime.Now.ToString("dd/MM/yyyy");
            var consulta = from u in Dataver.tb_mstr_trailer
                           join p in Dataver.tb_det_revision_trailer
                           on new { u.conse, u.fecha } equals
                           new { conse = p.conseini, fecha = p.fechaini } into sr
                           from x in sr.DefaultIfEmpty()
                           where (u.fecha >= Convert.ToDateTime(inicio) && u.fecha <= Convert.ToDateTime(final))
                                           && u.HoraEnt != ""
                                           && u.conse != 0
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
                               captura = x.responsable_captu,
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
                               fecha = u.fecha

                           };
            if (objAdmin.usu_departamento.ToString().Trim() == "CEDIS CANCUN")
            {
                consulta = from u in Dataver.tb_mstr_trailer
                           join p in Dataver.tb_det_revision_trailer
                           on new { u.conse, u.fecha } equals
                           new { conse = p.conseini, fecha = p.fechaini } into sr
                           from x in sr.DefaultIfEmpty()
                           where (u.fecha >= Convert.ToDateTime(inicio) && u.fecha <= Convert.ToDateTime(final))
                                && u.HoraEnt != ""
                                && u.conse != 0
                                && u.destino == "CANCUN"
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
                               captura = x.responsable_captu,
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
                               fecha = u.fecha

                           };


            }
            else if (objAdmin.usu_departamento.ToString().Trim() == "TRANSPORTES GAB")
            {
                consulta = from u in Dataver.tb_mstr_trailer
                           join p in Dataver.tb_det_revision_trailer
                           on new { u.conse, u.fecha } equals
                           new { conse = p.conseini, fecha = p.fechaini } into sr
                           from x in sr.DefaultIfEmpty()
                           where (u.fecha >= Convert.ToDateTime(inicio) && u.fecha <= Convert.ToDateTime(final))
                                && u.HoraEnt != ""
                                && u.conse != 0
                                && u.transporte == "TRANSPGAB"
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
                               captura = x.responsable_captu,
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
                               fecha = u.fecha

                           };


            }


            Session["fotos"] = consulta;
            string html = "";
            int cien = 0;
            int total = consulta.Count();


            if (consulta.Count() > 0)
            {
                html += "<tr><td>";
                html += "<table id='dynamic-table' class='table table-striped table-bordered table-hover'>";
                html += "<thead>";
                html += "<tr>";
                html += "<th>#</th>";
                html += "<th>Hora Entrada</th>";
                html += "<th>Fecha</th>";
                html += "<th>Placa</th>";
                html += "<th>Chofer</th>";
                html += "<th>Destino</th>";
                html += "<th>Turno</th>";
                html += "<th>Captura</th>";
                html += "<th>Responsable</th>";
                html += "<th>Anden</th>";
                html += "<th>Pedido</th>";
                html += "<th>Avance</th>";
                html += "<th></th>";
                html += "</tr>";
                html += "</thead>";
                html += "<tbody>";
                foreach (var i in consulta)
                {
                    html += "<tr>";
                    html += "<td>" + i.conse + "</td>";
                    html += "<td>" + i.HoraEnt + "</td>";
                    html += "<td>" + Convert.ToDateTime(i.fecha).ToString("dd/MM/yyyy") + "</td>";
                    html += "<td>" + i.no_trailer + "</td>";
                    html += "<td>" + i.chofer + "</td>";
                    html += "<td>" + i.destino + "</td>";
                    html += "<td>" + i.turno + "</td>";
                    html += "<td>" + i.captura + "</td>";
                    html += "<td>" + i.responsable + "</td>";
                    html += "<td>" + i.anden + "</td>";
                    html += "<td>" + i.pdn_folio + "</td>";
                    html += "<td> <div class='pull-left easy-pie-chart percentage' data-size='30' data-color='#ED174F' data-percent='" + Math.Round(Convert.ToDecimal(i.porcentaje), 0) + "'><span class='percent'><FONT SIZE=2>" + Math.Round(Convert.ToDecimal(i.porcentaje), 0) + "</font></span>%</div></td>";
                    int xs = Convert.ToInt16(Math.Round(Convert.ToDecimal(i.porcentaje), 0));
                    if (xs == 100)
                    {
                        cien++;
                    }
                    if (Math.Round(Convert.ToDecimal(i.porcentaje), 0) != 0)
                    {
                        html += "<td><div class='action-buttons'><a id='" + Convert.ToDateTime(i.fecha).ToString("ddMMyyyy") + "_" + i.conse + "_lnkView' runat='server' class='btn btn-success btn-xs' ><i id='" + Convert.ToDateTime(i.fecha).ToString("ddMMyyyy") + "_" + i.conse + "_lnkView' class='ace-icon fas fa-images  bigger-110 icon-only'></i></a><a id='" + Convert.ToDateTime(i.fecha).ToString("ddMMyyyy") + "_" + i.conse + "_download' runat='server' class='btn btn-danger btn-xs' ><i id='" + Convert.ToDateTime(i.fecha).ToString("ddMMyyyy") + "_" + i.conse + "_download' class='ace-icon fas fa-download  bigger-110 icon-only'></i></a></div>";
                        html += "</td>";
                    }
                    else
                    {
                        html += "<td><div class='action-buttons'><a id='" + Convert.ToDateTime(i.fecha).ToString("ddMMyyyy") + "_" + i.conse + "_lnkView' runat='server' class='btn disabled btn-success btn-xs' readonly ><i id='" + Convert.ToDateTime(i.fecha).ToString("ddMMyyyy") + "_" + i.conse + "_lnkView' class='ace-icon fas fa-images  bigger-110 icon-only'></i></a><a id='" + Convert.ToDateTime(i.fecha).ToString("ddMMyyyy") + "_" + i.conse + "_download' runat='server'  class='btn disabled btn-danger btn-xs' ><i id='" + Convert.ToDateTime(i.fecha).ToString("ddMMyyyy") + "_" + i.conse + "_download' class='ace-icon fas fa-download  bigger-110 icon-only'></i></a></div>";
                        html += "</td>";
                    }


                    html += "</tr>";
                }
                html += "</tbody>";
                html += "</table>";
            }
            else
            {
                html += "<table>";
                html += "<tr><td class='FieldCaption' colspan=3>Sin registros encontrados</td></tr>";
                html += "</table>";
            }

            Literal1.Text = html;
            if (cien == 0)
            {
                Literal2.Text = cien + " de " + total + " - 0%";
            }
            else
            {
                Literal2.Text = cien + " de " + total + " - " + (cien * 100) / total + "%";
            }


        }


        private void CargarGridTrailers(string inicio, string final)
        {
            // Validar que las cadenas no sean nulas, vacías o solo espacios en blanco
            if (string.IsNullOrEmpty(inicio) || inicio.Trim().Length == 0 ||
                string.IsNullOrEmpty(final) || final.Trim().Length == 0)
            {
                MessageBoxError.Show("Las fechas inicial y final no pueden estar vacías.");
                return;
            }

            // Intentar parsear las fechas con un formato específico
            DateTime fechaInicio, fechaFinal;
            try
            {
                fechaInicio = DateTime.ParseExact(inicio, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                fechaFinal = DateTime.ParseExact(final, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                MessageBoxError.Show("Formato de fecha no válido. Por favor, use el formato dd/MM/yyyy.");
                return;
            }

            // Verificar que la fecha inicial no sea mayor que la final
            if (fechaInicio > fechaFinal)
            {
                MessageBoxError.Show("La fecha inicial no puede ser mayor a la fecha final.");
                return;
            }

            var consulta = from u in Dataver.tb_mstr_trailer
                           join p in Dataver.tb_det_revision_trailer
                           on new { u.conse, u.fecha } equals
                           new { conse = p.conseini, fecha = p.fechaini } into sr
                           from x in sr.DefaultIfEmpty()
                           where (u.fecha >= fechaInicio && u.fecha <= fechaFinal)
                                   && u.HoraEnt != ""
                                   && u.conse != 0
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
                               captura = x.responsable_captu,
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
                               fecha = u.fecha // Mantener como está, pero manejar en el bucle
                           };

            // Resto del código para las condiciones de objAdmin.usu_departamento
            if (objAdmin.usu_departamento.ToString().Trim() == "CEDIS CANCUN")
            {
                consulta = from u in Dataver.tb_mstr_trailer
                           join p in Dataver.tb_det_revision_trailer
                           on new { u.conse, u.fecha } equals
                           new { conse = p.conseini, fecha = p.fechaini } into sr
                           from x in sr.DefaultIfEmpty()
                           where (u.fecha >= fechaInicio && u.fecha <= fechaFinal)
                                   && u.HoraEnt != ""
                                   && u.conse != 0
                                   && u.destino == "CANCUN"
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
                               captura = x.responsable_captu,
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
                               fecha = u.fecha
                           };
            }
            else if (objAdmin.usu_departamento.ToString().Trim() == "TRANSPORTES GAB")
            {
                consulta = from u in Dataver.tb_mstr_trailer
                           join p in Dataver.tb_det_revision_trailer
                           on new { u.conse, u.fecha } equals
                           new { conse = p.conseini, fecha = p.fechaini } into sr
                           from x in sr.DefaultIfEmpty()
                           where (u.fecha >= fechaInicio && u.fecha <= fechaFinal)
                                   && u.HoraEnt != ""
                                   && u.conse != 0
                                   && u.transporte == "TRANSPGAB"
                           orderby u.conse ascending
                           select new
                           {
                               conse = u.conse,
                               HoraEnt = u.HoraEnt,
                               chofer = u.chofer, // Corregido el error tipográfico
                               no_trailer = u.no_trailer,
                               destino = u.destino,
                               turno = u.turno,
                               responsable = u.responsable,
                               anden = u.anden,
                               pdn_folio = u.pdn_folio,
                               porcentaje = x.porcentaje == null ? "0" : x.porcentaje.ToString(),
                               captura = x.responsable_captu,
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
                               fecha = u.fecha
                           };
            }

            Session["fotos"] = consulta;
            string html = "";
            int cien = 0;
            int total = consulta.Count();

            if (consulta.Count() > 0)
            {
                html += "<tr><td>";
                html += "<table id='dynamic-table' class='table table-striped table-bordered table-hover'>";
                html += "<thead>";
                html += "<tr>";
                html += "<th>#</th>";
                html += "<th>Hora Entrada</th>";
                html += "<th>Fecha</th>";
                html += "<th>Placa</th>";
                html += "<th>Chofer</th>";
                html += "<th>Destino</th>";
                html += "<th>Turno</th>";
                html += "<th>Captura</th>";
                html += "<th>Responsable</th>";
                html += "<th>Anden</th>";
                html += "<th>Pedido</th>";
                html += "<th>Avance</th>";
                html += "<th></th>";
                html += "</tr>";
                html += "</thead>";
                html += "<tbody>";
                foreach (var i in consulta)
                {
                    // Manejar i.fecha como una cadena y convertirla a DateTime
                    string fechaFormateada;
                    try
                    {
                        // Opción 1: Formatear correctamente antes de parsear
                        string fechaStr = i.fecha?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime fecha = DateTime.ParseExact(fechaStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        //DateTime fecha = DateTime.ParseExact(i.fecha.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        fechaFormateada = fecha.ToString("dd/MM/yyyy");
                    }
                    catch (FormatException ex)
                    {
                        fechaFormateada = "Fecha no válida";
                    }

                    html += "<tr>";
                    html += "<td>" + i.conse + "</td>";
                    html += "<td>" + i.HoraEnt + "</td>";
                    html += "<td>" + fechaFormateada + "</td>";
                    html += "<td>" + i.no_trailer + "</td>";
                    html += "<td>" + i.chofer + "</td>";
                    html += "<td>" + i.destino + "</td>";
                    html += "<td>" + i.turno + "</td>";
                    html += "<td>" + i.captura + "</td>";
                    html += "<td>" + i.responsable + "</td>";
                    html += "<td>" + i.anden + "</td>";
                    html += "<td>" + i.pdn_folio + "</td>";
                    html += "<td> <div class='pull-left easy-pie-chart percentage' data-size='30' data-color='#ED174F' data-percent='" + Math.Round(Convert.ToDecimal(i.porcentaje), 0) + "'><span class='percent'><FONT SIZE=2>" + Math.Round(Convert.ToDecimal(i.porcentaje), 0) + "</font></span>%</div></td>";
                    int xs = Convert.ToInt16(Math.Round(Convert.ToDecimal(i.porcentaje), 0));
                    if (xs == 100)
                    {
                        cien++;
                    }
                    if (Math.Round(Convert.ToDecimal(i.porcentaje), 0) != 0)
                    {
                        // Usar la fecha formateada para los IDs de los botones
                        string fechaId;
                        try
                        {
                            string fechaStr = i.fecha?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            DateTime fecha = DateTime.ParseExact(fechaStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            
                            fechaId = fecha.ToString("ddMMyyyy");
                        }
                        catch (FormatException)
                        {
                            fechaId = "00000000"; // Valor predeterminado en caso de error
                        }

                        html += "<td><div class='action-buttons'><a id='" + fechaId + "_" + i.conse + "_lnkView' runat='server' class='btn btn-success btn-xs' ><i id='" + fechaId + "_" + i.conse + "_lnkView' class='ace-icon fas fa-images  bigger-110 icon-only'></i></a><a id='" + fechaId + "_" + i.conse + "_download' runat='server' class='btn btn-danger btn-xs' ><i id='" + fechaId + "_" + i.conse + "_download' class='ace-icon fas fa-download  bigger-110 icon-only'></i></a></div>";
                        html += "</td>";
                    }
                    else
                    {
                        string fechaId;
                        try
                        {
                            string fechaStr = i.fecha?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            DateTime fecha = DateTime.ParseExact(fechaStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                            fechaId = fecha.ToString("ddMMyyyy");
                        }
                        catch (FormatException)
                        {
                            fechaId = "00000000"; // Valor predeterminado en caso de error
                        }

                        html += "<td><div class='action-buttons'><a id='" + fechaId + "_" + i.conse + "_lnkView' runat='server' class='btn disabled btn-success btn-xs' readonly ><i id='" + fechaId + "_" + i.conse + "_lnkView' class='ace-icon fas fa-images  bigger-110 icon-only'></i></a><a id='" + fechaId + "_" + i.conse + "_download' runat='server' class='btn disabled btn-danger btn-xs' ><i id='" + fechaId + "_" + i.conse + "_download' class='ace-icon fas fa-download  bigger-110 icon-only'></i></a></div>";
                        html += "</td>";
                    }
                    html += "</tr>";
                }
                html += "</tbody>";
                html += "</table>";
            }
            else
            {
                html += "<table>";
                html += "<tr><td class='FieldCaption' colspan=3>Sin registros encontrados</td></tr>";
                html += "</table>";
            }

            Literal1.Text = html;
            if (cien == 0)
            {
                Literal2.Text = cien + " de " + total + " - 0%";
            }
            else
            {
                Literal2.Text = cien + " de " + total + " - " + (cien * 100) / total + "%";
            }
        }


        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("PaginaLogin.aspx");
        }


        protected void btnFiltrarLEGACY_Click(object sender, EventArgs e)
        {
            string inicio = fechainicial.Text;
            string final = fechafinal.Text;

            if (Convert.ToDateTime(inicio) <= Convert.ToDateTime(final))
            {
                CargarGridTrailers(inicio, final);
            }
            else
            {
                MessageBoxError.Show("La fecha inicial no puede ser mayor a la fecha final");
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string inicio = fechainicial.Text?.Trim();
            string final = fechafinal.Text?.Trim();

            // Validación de campos vacíos
            if (string.IsNullOrEmpty(inicio) || string.IsNullOrEmpty(final))
            {
                MessageBoxError.Show("Las fechas inicial y final son obligatorias.");
                return;
            }

            DateTime fechaInicio, fechaFinal;

            try
            {
                fechaInicio = DateTime.ParseExact(inicio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                fechaFinal = DateTime.ParseExact(final, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                MessageBoxError.Show("Formato de fecha incorrecto. Use el formato: dd/MM/yyyy (ej: 22/05/2026)");
                return;
            }

            if (fechaInicio > fechaFinal)
            {
                MessageBoxError.Show("La fecha inicial no puede ser mayor a la fecha final.");
                return;
            }

            CargarGridTrailers(inicio, final);
        }


    }
}