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

namespace Tickets2
{
    public partial class SeleccionarTrailer : System.Web.UI.Page
    {
        //private dcTicketsDataContext dcDatos;
        private DataVerificacionDataContext Dataver;
        Tb_Autoriza_OdeP objAdmin = null;

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

            if (Session["objAdmin"] != null)
            {
                objAdmin = (Tb_Autoriza_OdeP)Session["objAdmin"];

                if (!IsPostBack)
                {
                    CargarGridTrailers();
                }
            }
            else
            {
                Response.Redirect("PaginaLogin.aspx");
            }
        }

        private void CargarGridTrailers2()
        {
            //var fechaactual = DateTime.Now.ToString("dd/MM/yyyy");
            var fechaactual = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            var fechamin = DateTime.Today.AddDays(-2).ToString("dd/MM/yyyy");

            var consulta = from u in Dataver.tb_mstr_trailer
                           join p in Dataver.tb_det_revision_trailer
                           on new { u.conse, u.fecha } equals
                           new { conse = p.conseini, fecha = p.fechaini } into sr
                           from x in sr.DefaultIfEmpty()
                           where u.fecha >= Convert.ToDateTime(fechamin)
                                && u.fecha <= Convert.ToDateTime(fechaactual)
                                && u.HoraEnt != ""
                                && u.conse != 0
                                && (x.porcentaje != Convert.ToDecimal(100) || x.porcentaje == null)
                           orderby u.fecha ascending
                           select new
                           {
                               fechareg = u.hora_trailer,
                               conse = u.conse,
                               HoraEnt = u.HoraEnt,
                               chofer = u.chofer,
                               no_trailer = u.no_trailer,
                               destino = u.destino,
                               turno = u.turno,
                               responsable = u.responsable,
                               anden = u.anden,
                               pdn_folio = u.pdn_folio,
                               porcentaje = x.porcentaje == null ? "0 %" : x.porcentaje.ToString() + " %",
                           };
            /*if (consulta.Count() == 0)
            {
                fechaactual = DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy"); ;
                consulta = from u in Dataver.tb_mstr_trailer
                               join p in Dataver.tb_det_revision_trailer
                               on new { u.conse, u.fecha } equals
                               new { conse = p.conseini, fecha = p.fechaini } into sr
                               from x in sr.DefaultIfEmpty()
                               where u.fecha == Convert.ToDateTime(fechaactual)
                                    && u.HoraEnt != ""
                                    && u.conse != 0
                                    && (x.porcentaje != Convert.ToDecimal(100) || x.porcentaje == null)
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
                                   porcentaje = x.porcentaje == null ? "0 %" : x.porcentaje.ToString() + " %",
                               };
            }*/
            Session["fechaconse"] = fechaactual;
            dgtraiilers.DataSource = consulta;
            dgtraiilers.DataBind();



        }

        private void CargarGridTrailers()
        {
            try
            {
                var fechaactual = DateTime.Now.AddDays(1);
                var fechamin = DateTime.Today.AddDays(-2);

                var consulta = from u in Dataver.tb_mstr_trailer
                               join p in Dataver.tb_det_revision_trailer
                               on new { u.conse, u.fecha } equals
                               new { conse = p.conseini, fecha = p.fechaini } into sr
                               from x in sr.DefaultIfEmpty()
                               where u.fecha >= fechamin
                                    && u.fecha <= fechaactual
                                    && u.HoraEnt != ""
                                    && u.conse != 0
                                    && (x.porcentaje != Convert.ToDecimal(100) || x.porcentaje == null)
                               orderby u.fecha ascending
                               select new
                               {
                                   fechareg = u.hora_trailer,
                                   conse = u.conse,
                                   HoraEnt = u.HoraEnt,
                                   chofer = u.chofer,
                                   no_trailer = u.no_trailer,
                                   destino = u.destino,
                                   turno = u.turno,
                                   responsable = u.responsable,
                                   anden = u.anden,
                                   pdn_folio = u.pdn_folio,
                                   porcentaje = x.porcentaje == null ? "0 %" : x.porcentaje.ToString() + " %",
                               };

                Session["fechaconse"] = fechaactual.ToString("dd/MM/yyyy");
                dgtraiilers.DataSource = consulta.ToList();
                dgtraiilers.DataBind();
            }
            catch (FormatException ex)
            {
                Session["error"] = $"Error de formato de fecha: {ex.Message}";
                Response.Redirect("PaginaLogin.aspx");
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("PaginaLogin.aspx");
        }

        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
        {
            /*   var ratio = (double)maxHeight / image.Height;
               var newWidth = (int)(image.Width * ratio);
               var newHeight = (int)(image.Height * ratio);
               var newImage = new Bitmap(newWidth, newHeight);
               using (var g = Graphics.FromImage(newImage))
               {
                   g.DrawImage(image, 0, 0, newWidth, newHeight);
               */
            return null;
        }

        protected void gdview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CargarGridTrailers();
            dgtraiilers.PageIndex = e.NewPageIndex;
            dgtraiilers.DataBind();
        }

        protected void dgtraiilers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "redirect")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow Row = dgtraiilers.Rows[index];
                if (Row.Cells[9].Text == "100.00 %")
                {
                    MessageBox.Show("Las fotos del trailer ya fueron cubiertas al 100 %");
                }
                else
                {
                    Session["placa"] = Server.HtmlDecode(Row.Cells[3].Text);
                    Session["conse"] = Server.HtmlDecode(Row.Cells[0].Text);
                    Session["fechareg"] = Server.HtmlDecode(Row.Cells[1].Text);

                    Session["pedido"] = Server.HtmlDecode(Row.Cells[9].Text);
                    Response.Redirect("GuardarFotos.aspx");
                }

            }
        }

        protected void dgtraiilers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //check if the row is the header row
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //add the thead and tbody section programatically
                e.Row.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}