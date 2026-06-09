using Datos;
using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;

namespace Tickets2
{
    public partial class AdminManto : System.Web.UI.Page
    {
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

        private void CargarGridTrailers(string inicio, string final)
        {
            if (string.IsNullOrEmpty(inicio) || inicio.Trim() == "" ||
                string.IsNullOrEmpty(final) || final.Trim() == "")
            {
                MessageBoxError.Show("Las fechas inicial y final son obligatorias.");
                return;
            }

            DateTime fechaInicio;
            DateTime fechaFinal;

            if (!DateTime.TryParseExact(
                inicio,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out fechaInicio))
            {
                MessageBoxError.Show("Formato inválido en fecha inicial.");
                return;
            }

            if (!DateTime.TryParseExact(
                final,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out fechaFinal))
            {
                MessageBoxError.Show("Formato inválido en fecha final.");
                return;
            }

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

                           where u.fecha >= fechaInicio
                                 && u.fecha <= fechaFinal
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

                               porcentaje = x.porcentaje == null
                                    ? "0"
                                    : x.porcentaje.ToString(),

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
                consulta = consulta.Where(x => x.destino == "CANCUN");
            }
            else if (objAdmin.usu_departamento.ToString().Trim() == "TRANSPORTES GAB")
            {
                consulta = consulta.Where(x => x.no_trailer != null);
            }

            Session["fotos"] = consulta;

            string html = "";

            int cien = 0;
            int total = consulta.Count();

            if (total > 0)
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
                    decimal porcentaje = 0;

                    decimal.TryParse(i.porcentaje, out porcentaje);

                    porcentaje = Math.Round(porcentaje, 0);

                    DateTime fechaTemp = Convert.ToDateTime(i.fecha);

                    string fechaFormateada = fechaTemp.ToString("dd/MM/yyyy");
                    string fechaId = fechaTemp.ToString("ddMMyyyy");

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

                    html += "<td>";
                    html += "<div class='pull-left easy-pie-chart percentage' " +
                            "data-size='30' " +
                            "data-color='#ED174F' " +
                            "data-percent='" + porcentaje + "'>";

                    html += "<span class='percent'><font size='2'>" +
                            porcentaje +
                            "</font></span>%";

                    html += "</div>";
                    html += "</td>";

                    if (porcentaje == 100)
                    {
                        cien++;
                    }

                    if (porcentaje != 0)
                    {
                        html += "<td><div class='action-buttons'>";

                        html += "<a id='" + fechaId + "_" + i.conse + "_lnkView' " +
                                "class='btn btn-success btn-xs'>";

                        html += "<i id='" + fechaId + "_" + i.conse + "_lnkView' " +
                                "class='ace-icon fas fa-images bigger-110 icon-only'></i>";

                        html += "</a>";

                        html += "<a id='" + fechaId + "_" + i.conse + "_download' " +
                                "class='btn btn-danger btn-xs'>";

                        html += "<i id='" + fechaId + "_" + i.conse + "_download' " +
                                "class='ace-icon fas fa-download bigger-110 icon-only'></i>";

                        html += "</a>";

                        html += "</div></td>";
                    }
                    else
                    {
                        html += "<td><div class='action-buttons'>";

                        html += "<a class='btn disabled btn-success btn-xs'>";
                        html += "<i class='ace-icon fas fa-images bigger-110 icon-only'></i>";
                        html += "</a>";

                        html += "<a class='btn disabled btn-danger btn-xs'>";
                        html += "<i class='ace-icon fas fa-download bigger-110 icon-only'></i>";
                        html += "</a>";

                        html += "</div></td>";
                    }

                    html += "</tr>";
                }

                html += "</tbody>";
                html += "</table>";
            }
            else
            {
                html += "<table>";
                html += "<tr><td class='FieldCaption' colspan='3'>Sin registros encontrados</td></tr>";
                html += "</table>";
            }

            Literal1.Text = html;

            if (cien == 0)
            {
                Literal2.Text = cien + " de " + total + " - 0%";
            }
            else
            {
                Literal2.Text = cien + " de " + total + " - " + ((cien * 100) / total) + "%";
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("PaginaLogin.aspx");
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string inicio = fechainicial.Text.Trim();
            string final = fechafinal.Text.Trim();

            if (string.IsNullOrEmpty(inicio) || inicio.Trim() == "" ||
                string.IsNullOrEmpty(final) || final.Trim() == "")
            {
                MessageBoxError.Show("Las fechas inicial y final son obligatorias.");
                return;
            }

            DateTime fechaInicio;
            DateTime fechaFinal;

            if (!DateTime.TryParseExact(inicio,
                                        "dd/MM/yyyy",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None,
                                        out fechaInicio))
            {
                MessageBoxError.Show("La fecha inicial no tiene un formato válido.");
                return;
            }

            if (!DateTime.TryParseExact(final,
                                        "dd/MM/yyyy",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None,
                                        out fechaFinal))
            {
                MessageBoxError.Show("La fecha final no tiene un formato válido.");
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