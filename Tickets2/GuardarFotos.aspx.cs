using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace Tickets2
{

    public partial class GuardarFotos : System.Web.UI.Page
    {
        string videoOriginalPath = "~/FotoRevisionTrailer/";
        string videoConvertedPath = "~/FotoRevisionTrailer/Converted/";

        //Variables utilizadas para manejar los nombres.
        string videoTmpName = string.Empty;
        string flv = ".mp4";
        string videoConvertedName = string.Empty;

        string trailerid = "Nuevo";

        public static string cadenaConexion = "Persist Security Info=False;user id=sa; password=Gabira2026$;Initial Catalog =GAB_Irapuato; server=tcp:189.206.160.206,2352; MultipleActiveResultSets=true; Connect Timeout = 130";

        SqlConnection thisConnection;

        private DataVerificacionDataContext dcDatos;
        tb_mstr_trailer trailer = null;
        tb_det_revision_trailer revision = null;
        int porcentaje = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            dcDatos = new DataVerificacionDataContext();
            CargarInfotrailer();
            CargarFotosTrailer();

            thisConnection = new SqlConnection(cadenaConexion);

        }

        private void CargarInfotrailer2()
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

            var fechaactual = Convert.ToDateTime(Session["fechareg"]).ToString("dd/MM/yyyy");
            var consulta = from u in dcDatos.tb_mstr_trailer
                           where u.conse == Convert.ToDecimal(Session["conse"])
                                && u.fecha == Convert.ToDateTime(fechaactual)
                           select u;

            if (consulta != null)
            {
                if (consulta.Count() > 0)
                {
                    trailer = consulta.First();

                    horaentrada.Text = trailer.HoraEnt.ToString();
                    placas.Text = trailer.no_trailer.ToString();
                    destino.Text = trailer.destino.ToString();
                    chofer.Text = trailer.chofer.ToString();
                    transporte.Text = trailer.transporte.ToString();
                    if (trailer.anden > 0)
                    {
                        Anden.SelectedValue = trailer.anden.ToString().Trim();
                        Anden.Enabled = false;
                        LblAnden.Text = "*La foto ya fue cargada*";
                        FotoAnden.Visible = false;
                    }

                    //Bloquear datos
                    horaentrada.Enabled = false;
                    placas.Enabled = false;
                    destino.Enabled = false;
                    chofer.Enabled = false;
                    transporte.Enabled = false;

                }
                else
                {
                    MessageBoxError.Show("El trailer no existe.");
                }
            }
        }

        private void CargarInfotrailer()
        {
            // Verificar si la sesión está iniciada
            if (Session["Iniciada"]?.ToString() != "1")
            {
                Session["Iniciada"] = "0";
                Response.Redirect("PaginaLogin.aspx");
                return;
            }

            // Validar existencia de Session["fechareg"]
            if (Session["fechareg"] == null || string.IsNullOrEmpty(Session["fechareg"].ToString()))
            {
                MessageBoxError.Show("Fecha de registro no encontrada en la sesión.");
                return;
            }

            DateTime fechaactual;
            try
            {
                fechaactual = DateTime.ParseExact(Session["fechareg"].ToString().Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                MessageBoxError.Show("Formato de fecha no válido en la sesión (fechareg).");
                return;
            }

            // Validar existencia de conse
            if (Session["conse"] == null || !decimal.TryParse(Session["conse"].ToString(), out decimal conse))
            {
                MessageBoxError.Show("Número de trailer (conse) no válido.");
                return;
            }

            // Consulta al trailer
            var trailer = (from u in dcDatos.tb_mstr_trailer
                           where u.conse == conse && u.fecha == fechaactual
                           select u).FirstOrDefault();

            if (trailer == null)
            {
                MessageBoxError.Show("El trailer no existe.");
                return;
            }

            // Rellenar campos
            horaentrada.Text = trailer.HoraEnt ?? "";
            placas.Text = trailer.no_trailer ?? "";
            destino.Text = trailer.destino ?? "";
            chofer.Text = trailer.chofer ?? "";
            transporte.Text = trailer.transporte ?? "";

            if (trailer.anden > 0)
            {
                Anden.SelectedValue = trailer.anden.ToString().Trim();
                Anden.Enabled = false;
                LblAnden.Text = "*La foto ya fue cargada*";
                FotoAnden.Visible = false;
            }

            // Bloquear campos de edición
            horaentrada.Enabled = false;
            placas.Enabled = false;
            destino.Enabled = false;
            chofer.Enabled = false;
            transporte.Enabled = false;
        }


        private void CargarFotosTrailer2()
        {
            Session["actualizar"] = 0;
            var fechaactual = Convert.ToDateTime(Session["fechareg"]).ToString("dd/MM/yyyy");
            var consulta = from u in dcDatos.tb_det_revision_trailer
                           where u.conse == Convert.ToDecimal(Session["conse"])
                                && u.fecha == Convert.ToDateTime(fechaactual)
                           select u;

            if (consulta != null)
            {
                if (consulta.Count() > 0)
                {
                    Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;
                    revision = consulta.First();
                    if (revision.setpointini != null && revision.setpointini != "")
                    {
                        FotoSetPointIn.Visible = false;
                        lblsetpointini.Text = "*La foto ya fue cargada*";
                        Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;
                        porcentaje = porcentaje + 1;
                    }
                    if (revision.setpointfin != null && revision.setpointfin != "")
                    {
                        FotoSetPointFin.Visible = false;
                        lblsetpointfin.Text = "*La foto ya fue cargada*";
                        Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;
                        porcentaje = porcentaje + 1;
                    }
                    if (revision.difusor != null && revision.difusor != "")
                    {
                        FotoDifusor.Visible = false;
                        lbldifusor.Text = "*La foto ya fue cargada*";
                        Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;
                        porcentaje = porcentaje + 1;
                    }
                    if (revision.numcaja != null && revision.numcaja != "")
                    {
                        Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;
                        lblnocaja.Text = "*La foto ya fue cargada*";
                        FotoNoCaja.Visible = false;
                        porcentaje = porcentaje + 1;
                    }
                    if (revision.cajacompleta != null && revision.cajacompleta != "")
                    {
                        FotoCajaCompleta.Visible = false;
                        lblcajacompleta.Text = "*La foto ya fue cargada*";
                        Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;
                        porcentaje = porcentaje + 1;
                    }
                    if (revision.piso != null && revision.piso != "")
                    {
                        FotoPiso.Visible = false;
                        lblfotopiso.Text = "*La foto ya fue cargada*";
                        Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;
                        porcentaje = porcentaje + 1;
                    }
                    if (revision.temprod1 != null && revision.temprod1 != "")
                    {
                        FotoTemPro1.Visible = false;
                        lbltemprod1.Text = "*La foto ya fue cargada*";
                        Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;
                        porcentaje = porcentaje + 1;
                    }
                    if (revision.temprod2 != null && revision.temprod2 != "")
                    {
                        FotoTemPro2.Visible = false;
                        lbltemprod2.Text = "*La foto ya fue cargada*";
                        Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;
                        porcentaje = porcentaje + 1;
                    }
                    if (revision.temprod3 != null && revision.temprod3 != "")
                    {
                        FotoTemPro3.Visible = false;
                        lbltemprod3.Text = "*La foto ya fue cargada*";
                        Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;
                        porcentaje = porcentaje + 1;
                    }
                    if (revision.temprod4 != null && revision.temprod4 != "")
                    {
                        FotoTemPro4.Visible = false;
                        lbltemprod4.Text = "*La foto ya fue cargada*";
                        Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;

                    }
                    if (revision.temprod5 != null && revision.temprod5 != "")
                    {
                        FotoTemPro5.Visible = false;
                        lbltemprod5.Text = "*La foto ya fue cargada*";
                        Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;

                    }
                    if (revision.temprod6 != null && revision.temprod6 != "")
                    {
                        FotoTemPro6.Visible = false;
                        lbltemprod6.Text = "*La foto ya fue cargada*";
                        Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;

                    }
                    if (revision.termino_carga != null && revision.termino_carga != "")
                    {
                        FotoTerminoCarga.Visible = false;
                        lblterminocarga.Text = "*La foto ya fue cargada*";
                        Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;
                        porcentaje = porcentaje + 1;
                    }
                    if (revision.vidrayan != null && revision.vidrayan != "")
                    {
                        Videoryan.Visible = false;
                        lblvideoryan.Text = "*El video ya fue cargado*";
                        Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;

                    }
                    if (revision.fotoryan != null && revision.fotoryan != "")
                    {
                        FotoRyan.Visible = false;
                        lblvideoryan.Text = "*La foto ya fue cargado*";
                        Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;
                        porcentaje = porcentaje + 1;
                    }
                }
            }
            //Session["porcentaje"] = porcentaje;
        }

        private void CargarFotosTrailer()
        {
            // Validar sesión iniciada
            if (Session["Iniciada"]?.ToString() != "1")
            {
                Session["Iniciada"] = "0";
                Response.Redirect("PaginaLogin.aspx");
                return;
            }

            Session["actualizar"] = 0;
            int porcentaje = 0;

            // Validar fecha
            if (Session["fechareg"] == null || string.IsNullOrEmpty(Session["fechareg"].ToString()))
            {
                MessageBoxError.Show("Fecha de registro no encontrada en la sesión.");
                return;
            }

            DateTime fechaactual;
            try
            {
                fechaactual = DateTime.ParseExact(Session["fechareg"].ToString().Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                MessageBoxError.Show("Formato de fecha no válido en la sesión (fechareg).");
                return;
            }

            // Validar conse
            if (Session["conse"] == null || !decimal.TryParse(Session["conse"].ToString(), out decimal conse))
            {
                MessageBoxError.Show("Número de trailer (conse) no válido.");
                return;
            }

            // Buscar revisión
            var revision = dcDatos.tb_det_revision_trailer
                .FirstOrDefault(u => u.conse == conse && u.fecha == fechaactual);

            if (revision == null)
            {
                // No hay revisión encontrada
                return;
            }

            Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;

            // Validar y mostrar cada campo si tiene valor
            void ValidarFoto(string valor, Label lbl, WebControl control, string mensaje, bool sumaPorcentaje = true)
            {
                if (!string.IsNullOrEmpty(valor))
                {
                    lbl.Text = mensaje;
                    control.Visible = false;
                    Session["actualizar"] = Convert.ToInt32(Session["actualizar"]) + 1;
                    if (sumaPorcentaje)
                        porcentaje++;
                }
            }

            ValidarFoto(revision.setpointini, lblsetpointini, FotoSetPointIn, "*La foto ya fue cargada*");
            ValidarFoto(revision.setpointfin, lblsetpointfin, FotoSetPointFin, "*La foto ya fue cargada*");
            ValidarFoto(revision.difusor, lbldifusor, FotoDifusor, "*La foto ya fue cargada*");
            ValidarFoto(revision.numcaja, lblnocaja, FotoNoCaja, "*La foto ya fue cargada*");
            ValidarFoto(revision.cajacompleta, lblcajacompleta, FotoCajaCompleta, "*La foto ya fue cargada*");
            ValidarFoto(revision.piso, lblfotopiso, FotoPiso, "*La foto ya fue cargada*");
            ValidarFoto(revision.temprod1, lbltemprod1, FotoTemPro1, "*La foto ya fue cargada*");
            ValidarFoto(revision.temprod2, lbltemprod2, FotoTemPro2, "*La foto ya fue cargada*");
            ValidarFoto(revision.temprod3, lbltemprod3, FotoTemPro3, "*La foto ya fue cargada*");
            ValidarFoto(revision.temprod4, lbltemprod4, FotoTemPro4, "*La foto ya fue cargada*", false);
            ValidarFoto(revision.temprod5, lbltemprod5, FotoTemPro5, "*La foto ya fue cargada*", false);
            ValidarFoto(revision.temprod6, lbltemprod6, FotoTemPro6, "*La foto ya fue cargada*", false);
            ValidarFoto(revision.termino_carga, lblterminocarga, FotoTerminoCarga, "*La foto ya fue cargada*");
            ValidarFoto(revision.vidrayan, lblvideoryan, Videoryan, "*El video ya fue cargado*", false);
            ValidarFoto(revision.fotoryan, lblvideoryan, FotoRyan, "*La foto ya fue cargada*");

            // Puedes guardar el porcentaje si lo usas en otra parte
            // Session["porcentaje"] = porcentaje;
        }


        private bool IsImage(HttpPostedFile file)
        {
            //Checks for image type... you could also do filename extension checks and other things
            return ((file != null) && System.Text.RegularExpressions.Regex.IsMatch(file.ContentType, "image/\\S+") && (file.ContentLength > 0));
        }

        protected void btnGuardarOG_Click(object sender, EventArgs e)
        {
            // ============================================================
            // 1. Validación inicial del andén (sin cambios)
            // ============================================================
            if (Anden.Enabled == true && FotoAnden.FileName == "")
            {
                MessageBoxError.Show("Debe Seleccionar un Anden para la primera Carga de fotos");
                return;
            }
            else if (Anden.Enabled == true && FotoAnden.FileName != "")
            {
                var consulta = from u in dcDatos.tb_mstr_trailer
                               where u.anden == Convert.ToDecimal(Anden.SelectedValue.ToString())
                               && u.horafin == "--:--" && u.Guardar == 'N'
                               && u.responsable != "J CONCEPCION RAZO PIZANO"
                               select u;

                if (consulta != null && consulta.Count() > 0)
                {
                    trailer = consulta.First();
                    MessageBox.Show("El Anden Seleccionado esta ocupado por el trailer " + trailer.no_trailer.ToString() + " Del dia: " + trailer.hora_trailer);
                    return;
                }
                else
                {
                    // Se usa la fecha convertida más adelante, pero aquí aún no la tenemos.
                    // La fecha la obtendremos de sesión de forma segura. 
                    // De momento se deja la lógica original, pero luego se modificará para usar la variable correcta.
                }
            }

            // ============================================================
            // 2. Conversión ÚNICA y segura de la fecha desde Session
            // ============================================================
            if (Session["fechareg"] == null || string.IsNullOrEmpty(Session["fechareg"].ToString()))
            {
                MessageBoxError.Show("Fecha de registro no encontrada en la sesión.");
                return;
            }

            DateTime fechaBase;
            if (!DateTime.TryParseExact(Session["fechareg"].ToString().Trim(), "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out fechaBase))
            {
                MessageBoxError.Show("Formato de fecha no válido en la sesión (se espera dd/MM/yyyy).");
                return;
            }

            string fechaactual = fechaBase.ToString("dd/MM/yyyy");   // para SQL y visualización
            string fechaarchivo = fechaBase.ToString("ddMMyyyy");    // para nombres de archivo
            string mes_anio = fechaBase.ToString("MMyyyy");          // para carpetas

            // Validar conse
            if (Session["conse"] == null || !decimal.TryParse(Session["conse"].ToString(), out decimal conse))
            {
                MessageBoxError.Show("Número de trailer (conse) no válido.");
                return;
            }

            // Obtener usuario de sesión
            Tb_Autoriza_OdeP objUser = (Tb_Autoriza_OdeP)Session["objAdmin"];
            if (objUser == null)
            {
                MessageBoxError.Show("Sesión de usuario no válida.");
                return;
            }

            // ============================================================
            // 3. Actualización del Anden en tb_mstr_trailer (usando la fecha convertida)
            // ============================================================
            if (Anden.Enabled == true && FotoAnden.FileName != "")
            {
                // Reutilizamos la consulta anterior para evitar repetir código
                var consulta = from u in dcDatos.tb_mstr_trailer
                               where u.anden == Convert.ToDecimal(Anden.SelectedValue.ToString())
                               && u.horafin == "--:--" && u.Guardar == 'N'
                               && u.responsable != "J CONCEPCION RAZO PIZANO"
                               select u;

                if (consulta == null || consulta.Count() == 0)
                {
                    try
                    {
                        thisConnection.Open();
                        string cadena = "UPDATE tb_mstr_trailer SET Anden = '" + Anden.SelectedValue.ToString().Trim() +
                                        "' WHERE hora_trailer = '" + fechaactual + "' AND conse = '" + conse + "'";
                        SqlCommand cmd = new SqlCommand(cadena, thisConnection);
                        cmd.ExecuteNonQuery();
                        thisConnection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBoxError.Show("Error al actualizar el andén: " + ex.Message);
                        return;
                    }
                }
            }

            // ============================================================
            // 4. Procesamiento de fotos y videos (INSERCIÓN o ACTUALIZACIÓN)
            // ============================================================
            int porcentajeLocal = 0; // variable local para el porcentaje

            try
            {
                if (Convert.ToInt32(Session["actualizar"]) == 0)  // INSERCIÓN
                {
                    tb_det_revision_trailer insert = new tb_det_revision_trailer();
                    insert.fecha = fechaBase;
                    insert.conse = conse;
                    insert.responsable_captu = objUser.usuario;
                    insert.fechaini = fechaBase;
                    insert.conseini = conse;

                    HttpFileCollection hfc = Request.Files;
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        string campo = hfc.AllKeys[i];
                        HttpPostedFile hpf = hfc[i];
                        if (hpf.ContentLength > 0)
                        {
                            if (campo != "Videoryan")
                            {
                                if (!IsImage(hpf))
                                {
                                    MessageBoxError.Show("El archivo no es una imagen: " + campo);
                                    continue; // no retorna, solo omite este archivo
                                }

                                string nombre_archivo = fechaarchivo + "_" + conse + "_" + campo + ".jpg";
                                string savePath = Server.MapPath("~/FotoRevisionTrailer/");
                                string carpetaDestino = Path.Combine(savePath, mes_anio);
                                if (!Directory.Exists(carpetaDestino))
                                    Directory.CreateDirectory(carpetaDestino);

                                try
                                {
                                    using (Bitmap bmpPostedImage = new Bitmap(hpf.InputStream))
                                    using (System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 405))
                                    {
                                        objImage.Save(Path.Combine(carpetaDestino, nombre_archivo), ImageFormat.Jpeg);
                                    }

                                    // Asignar según el campo
                                    if (campo == "FotoSetPointIn")
                                    {
                                        insert.setpointini = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoNoCaja")
                                    {
                                        insert.numcaja = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoDifusor")
                                    {
                                        insert.difusor = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoPiso")
                                    {
                                        insert.piso = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoCajaCompleta")
                                    {
                                        insert.cajacompleta = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoTemPro1")
                                    {
                                        insert.temprod1 = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoTemPro2")
                                    {
                                        insert.temprod2 = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoTemPro3")
                                    {
                                        insert.temprod3 = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoTemPro4")
                                    {
                                        insert.temprod4 = mes_anio + "/" + nombre_archivo;
                                        // No incrementa porcentaje (como en el original)
                                    }
                                    else if (campo == "FotoTemPro5")
                                    {
                                        insert.temprod5 = mes_anio + "/" + nombre_archivo;
                                    }
                                    else if (campo == "FotoTemPro6")
                                    {
                                        insert.temprod6 = mes_anio + "/" + nombre_archivo; // CORREGIDO
                                    }
                                    else if (campo == "FotoSetPointFin")
                                    {
                                        insert.setpointfin = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoTerminoCarga")
                                    {
                                        insert.termino_carga = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoAnden")
                                    {
                                        insert.anden = mes_anio + "/" + nombre_archivo;
                                    }
                                    else if (campo == "FotoRyan")
                                    {
                                        insert.fotoryan = nombre_archivo; // sin carpeta (como en original)
                                        porcentajeLocal++;
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    EnviarErrorPorCorreo(Ex, nombre_archivo, conse);
                                    MessageBox.Show("Error en foto " + campo + ": " + Ex.Message);
                                }
                            }
                            else // Campo "Videoryan"
                            {
                                if (!ValidateVideoExtension(hpf.FileName))
                                {
                                    MessageBoxError.Show("La extensión del video no es permitida.");
                                    return;
                                }
                                if (hpf.InputStream.Length > 52428800)
                                {
                                    MessageBoxError.Show("El video no puede exceder los 50MB");
                                    return;
                                }

                                string nombre_archivo = "Embarque_" + fechaarchivo + "_" + conse + "_" + campo;
                                string saveAs = Server.MapPath(videoOriginalPath);
                                videoTmpName = nombre_archivo;
                                string originalVideo = Path.Combine(saveAs, videoTmpName + Path.GetExtension(hpf.FileName));
                                hpf.SaveAs(originalVideo);

                                if (EncodingVideo(originalVideo))
                                {
                                    string fileName = videoConvertedName;
                                    string sourcePath = Server.MapPath(videoConvertedPath);
                                    string originPath = Server.MapPath(videoOriginalPath);
                                    string targetPath = @"\\192.168.123.4\FotosRevisionTrailer\";

                                    string originFile = Path.Combine(originPath, fileName);
                                    string sourceFile = Path.Combine(sourcePath, fileName);
                                    string destFile = Path.Combine(targetPath, fileName);

                                    if (File.Exists(originalVideo))
                                    {
                                        try
                                        {
                                            File.Delete(originalVideo);
                                            insert.vidrayan = nombre_archivo + ".mp4";
                                        }
                                        catch (IOException ed)
                                        {
                                            Console.WriteLine(ed.Message);
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    Session["error"] = "Error convirtiendo el video, intente nuevamente";
                                }
                            }
                        }
                    }

                    decimal porce = Convert.ToDecimal((porcentajeLocal * 100m) / 10m);
                    insert.porcentaje = porce;
                    dcDatos.tb_det_revision_trailer.InsertOnSubmit(insert);
                    dcDatos.SubmitChanges();

                    // Registro de movimiento
                    tb_registro_vertrai insertmov = new tb_registro_vertrai();
                    insertmov.fecha = DateTime.Now;
                    insertmov.nom_compu = "WebEmbarques";
                    insertmov.nom_usu = objUser.usuario;
                    insertmov.tipo_mov = "A";
                    insertmov.op_clave = "7.9";
                    insertmov.folio = conse.ToString();
                    insertmov.detalle = "Alta fotos al " + porce + " porciento";
                    insertmov.sistema = "EMBWEB";
                    insertmov.mov_folio = conse.ToString();
                    dcDatos.tb_registro_vertrai.InsertOnSubmit(insertmov);
                    dcDatos.SubmitChanges();

                    Session["exito"] = "Las fotos del trailer se han guardado correctamente";
                }
                else  // ACTUALIZACIÓN (ya existe registro)
                {
                    tb_det_revision_trailer actualizar = dcDatos.tb_det_revision_trailer
                        .FirstOrDefault(p => p.fecha == fechaBase && p.conse == conse);

                    if (actualizar == null)
                    {
                        MessageBoxError.Show("No se encontró el registro de revisión para actualizar.");
                        return;
                    }

                    HttpFileCollection hfc = Request.Files;
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        string campo = hfc.AllKeys[i];
                        HttpPostedFile hpf = hfc[i];
                        if (hpf.ContentLength > 0)
                        {
                            if (campo != "Videoryan")
                            {
                                if (!IsImage(hpf))
                                {
                                    MessageBoxError.Show("El archivo no es una imagen: " + campo);
                                    continue;
                                }

                                string nombre_archivo = "EM_" + fechaarchivo + "_" + conse + "_" + campo + ".jpg";
                                string savePath = Server.MapPath("~/FotoRevisionTrailer/");
                                string carpetaDestino = Path.Combine(savePath, mes_anio);
                                if (!Directory.Exists(carpetaDestino))
                                    Directory.CreateDirectory(carpetaDestino);

                                try
                                {
                                    using (Bitmap bmpPostedImage = new Bitmap(hpf.InputStream))
                                    using (System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 405))
                                    {
                                        objImage.Save(Path.Combine(carpetaDestino, nombre_archivo), ImageFormat.Jpeg);
                                    }

                                    // Asignar según el campo
                                    if (campo == "FotoSetPointIn")
                                    {
                                        actualizar.setpointini = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoNoCaja")
                                    {
                                        actualizar.numcaja = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoDifusor")
                                    {
                                        actualizar.difusor = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoPiso")
                                    {
                                        actualizar.piso = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoCajaCompleta")
                                    {
                                        actualizar.cajacompleta = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoTemPro1")
                                    {
                                        actualizar.temprod1 = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoTemPro2")
                                    {
                                        actualizar.temprod2 = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoTemPro3")
                                    {
                                        actualizar.temprod3 = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoTemPro4")
                                    {
                                        actualizar.temprod4 = mes_anio + "/" + nombre_archivo;
                                    }
                                    else if (campo == "FotoTemPro5")
                                    {
                                        actualizar.temprod5 = mes_anio + "/" + nombre_archivo;
                                    }
                                    else if (campo == "FotoTemPro6")
                                    {
                                        actualizar.temprod6 = mes_anio + "/" + nombre_archivo; // CORREGIDO
                                    }
                                    else if (campo == "FotoSetPointFin")
                                    {
                                        actualizar.setpointfin = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoTerminoCarga")
                                    {
                                        actualizar.termino_carga = mes_anio + "/" + nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                    else if (campo == "FotoAnden")
                                    {
                                        actualizar.anden = mes_anio + "/" + nombre_archivo;
                                    }
                                    else if (campo == "FotoRyan")   // <- AGREGADO (faltaba)
                                    {
                                        actualizar.fotoryan = nombre_archivo;
                                        porcentajeLocal++;
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    EnviarErrorPorCorreo(Ex, nombre_archivo, conse);
                                    MessageBox.Show("Error en foto " + campo + ": " + Ex.Message);
                                }
                            }
                            else // Video
                            {
                                if (!ValidateVideoExtension(hpf.FileName))
                                {
                                    MessageBoxError.Show("La extensión del video no es permitida.");
                                    return;
                                }
                                if (hpf.InputStream.Length > 52428800)
                                {
                                    MessageBoxError.Show("El video no puede exceder los 50MB");
                                    return;
                                }

                                string nombre_archivo = "Embarque_" + fechaarchivo + "_" + conse + "_" + campo;
                                string saveAs = Server.MapPath(videoOriginalPath);
                                videoTmpName = nombre_archivo;
                                string originalVideo = Path.Combine(saveAs, videoTmpName + Path.GetExtension(hpf.FileName));
                                hpf.SaveAs(originalVideo);

                                if (EncodingVideo(originalVideo))
                                {
                                    string fileName = videoConvertedName;
                                    string sourcePath = Server.MapPath(videoConvertedPath);
                                    string originPath = Server.MapPath(videoOriginalPath);
                                    string targetPath = @"\\192.168.123.4\FotosRevisionTrailer\";

                                    if (File.Exists(originalVideo))
                                    {
                                        try
                                        {
                                            // No se elimina el original (como en el código original)
                                            actualizar.vidrayan = nombre_archivo + ".mp4";
                                        }
                                        catch (IOException ed)
                                        {
                                            Console.WriteLine(ed.Message);
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBoxError.Show("Error convirtiendo el video, intente nuevamente");
                                }
                            }
                        }
                    }

                    decimal porce = Convert.ToDecimal((porcentajeLocal * 100m) / 10m);
                    actualizar.porcentaje = porce;
                    dcDatos.SubmitChanges();

                    // Registro de modificación
                    tb_registro_vertrai insertmov = new tb_registro_vertrai();
                    insertmov.fecha = DateTime.Now;
                    insertmov.nom_compu = "WebEmbarques";
                    insertmov.nom_usu = objUser.usuario;
                    insertmov.tipo_mov = "M";
                    insertmov.op_clave = "7.9";
                    insertmov.folio = conse.ToString();
                    insertmov.detalle = "Modificacion fotos al " + porce + " porciento";
                    insertmov.sistema = "EMBWEB";
                    insertmov.mov_folio = conse.ToString();
                    dcDatos.tb_registro_vertrai.InsertOnSubmit(insertmov);
                    dcDatos.SubmitChanges();

                    Session["exito"] = "Las fotos del trailer se han actualizado correctamente";
                }
            }
            catch (Exception ex)
            {
                Session["error"] = "Error de SQL: " + ex.Message + "\nConsulte con el administrador del sistema.";
            }

            Response.Redirect("SeleccionarTrailer.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // ============================================================
            // 1. Validación inicial del andén (sin cambios funcionales)
            // ============================================================
            if (Anden.Enabled == true && FotoAnden.FileName == "")
            {
                MessageBoxError.Show("Debe Seleccionar un Anden para la primera Carga de fotos");
                return;
            }

            // ============================================================
            // 2. Conversión ÚNICA y segura de la fecha desde Session
            // ============================================================
            if (Session["fechareg"] == null || string.IsNullOrEmpty(Session["fechareg"].ToString()))
            {
                MessageBoxError.Show("Fecha de registro no encontrada en la sesión.");
                return;
            }

            DateTime fechaBase;
            if (!DateTime.TryParseExact(Session["fechareg"].ToString().Trim(), "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out fechaBase))
            {
                MessageBoxError.Show("Formato de fecha no válido en la sesión (se espera dd/MM/yyyy).");
                return;
            }

            string fechaactual = fechaBase.ToString("dd/MM/yyyy");   // para SQL y visualización
            string fechaarchivo = fechaBase.ToString("ddMMyyyy");    // para nombres de archivo
            string mes_anio = fechaBase.ToString("MMyyyy");          // para carpetas

            // Validar conse
            if (Session["conse"] == null || !decimal.TryParse(Session["conse"].ToString(), out decimal conse))
            {
                MessageBoxError.Show("Número de trailer (conse) no válido.");
                return;
            }

            // Obtener usuario de sesión
            Tb_Autoriza_OdeP objUser = (Tb_Autoriza_OdeP)Session["objAdmin"];
            if (objUser == null)
            {
                MessageBoxError.Show("Sesión de usuario no válida.");
                return;
            }

            // ============================================================
            // 3. Actualización del Anden en tb_mstr_trailer
            // ============================================================
            if (Anden.Enabled == true && FotoAnden.FileName != "")
            {
                // Verificar si el andén está ocupado
                var consulta = from u in dcDatos.tb_mstr_trailer
                               where u.anden == Convert.ToDecimal(Anden.SelectedValue.ToString())
                               && u.horafin == "--:--" && u.Guardar == 'N'
                               && u.responsable != "J CONCEPCION RAZO PIZANO"
                               select u;

                if (consulta != null && consulta.Count() > 0)
                {
                    trailer = consulta.First();
                    MessageBox.Show("El Anden Seleccionado esta ocupado por el trailer " + trailer.no_trailer.ToString() + " Del dia: " + trailer.hora_trailer);
                    return;
                }
                else
                {
                    try
                    {
                        thisConnection.Open();
                        string cadena = "UPDATE tb_mstr_trailer SET Anden = '" + Anden.SelectedValue.ToString().Trim() +
                                        "' WHERE hora_trailer = '" + fechaactual + "' AND conse = '" + conse + "'";
                        SqlCommand cmd = new SqlCommand(cadena, thisConnection);
                        cmd.ExecuteNonQuery();
                        thisConnection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBoxError.Show("Error al actualizar el andén: " + ex.Message);
                        return;
                    }
                }
            }

            // ============================================================
            // 4. Procesamiento de fotos y videos (INSERCIÓN o ACTUALIZACIÓN)
            // ============================================================
            try
            {
                if (Convert.ToInt32(Session["actualizar"]) == 0)  // INSERCIÓN
                {
                    tb_det_revision_trailer insert = new tb_det_revision_trailer();
                    insert.fecha = fechaBase;
                    insert.conse = conse;
                    insert.responsable_captu = objUser.usuario;
                    insert.fechaini = fechaBase;
                    insert.conseini = conse;

                    HttpFileCollection hfc = Request.Files;
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        string campo = hfc.AllKeys[i];
                        HttpPostedFile hpf = hfc[i];
                        if (hpf.ContentLength > 0)
                        {
                            if (campo != "Videoryan")
                            {
                                // Validar imagen (incluye HEIC si se desea)
                                if (!IsImage(hpf) && !IsHeicImage(hpf))
                                {
                                    MessageBoxError.Show("El archivo no es una imagen válida: " + campo);
                                    continue;
                                }

                                string nombre_archivo = fechaarchivo + "_" + conse + "_" + campo + ".jpg";
                                string savePath = Server.MapPath("~/FotoRevisionTrailer/");
                                string carpetaDestino = Path.Combine(savePath, mes_anio);
                                if (!Directory.Exists(carpetaDestino))
                                    Directory.CreateDirectory(carpetaDestino);

                                try
                                {
                                    byte[] imageBytes;
                                    if (IsHeicImage(hpf))
                                    {
                                        // Convertir HEIC a JPG usando Magick.NET (requiere instalar paquete)
                                        //imageBytes = ConvertHeicToJpg(hpf.InputStream);
                                        using (Bitmap bmpPostedImage = new Bitmap(hpf.InputStream))
                                        using (System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 405))
                                        {
                                            using (MemoryStream ms = new MemoryStream())
                                            {
                                                objImage.Save(ms, ImageFormat.Jpeg);
                                                imageBytes = ms.ToArray();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        using (Bitmap bmpPostedImage = new Bitmap(hpf.InputStream))
                                        using (System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 405))
                                        {
                                            using (MemoryStream ms = new MemoryStream())
                                            {
                                                objImage.Save(ms, ImageFormat.Jpeg);
                                                imageBytes = ms.ToArray();
                                            }
                                        }
                                    }
                                    File.WriteAllBytes(Path.Combine(carpetaDestino, nombre_archivo), imageBytes);

                                    // Asignar según el campo
                                    if (campo == "FotoSetPointIn") insert.setpointini = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoNoCaja") insert.numcaja = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoDifusor") insert.difusor = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoPiso") insert.piso = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoCajaCompleta") insert.cajacompleta = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoTemPro1") insert.temprod1 = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoTemPro2") insert.temprod2 = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoTemPro3") insert.temprod3 = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoTemPro4") insert.temprod4 = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoTemPro5") insert.temprod5 = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoTemPro6") insert.temprod6 = mes_anio + "/" + nombre_archivo; // corregido
                                    else if (campo == "FotoSetPointFin") insert.setpointfin = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoTerminoCarga") insert.termino_carga = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoAnden") insert.anden = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoRyan") insert.fotoryan = nombre_archivo; // sin carpeta
                                }
                                catch (Exception Ex)
                                {
                                    EnviarErrorPorCorreo(Ex, nombre_archivo, conse);
                                    MessageBox.Show("Error en foto " + campo + ": " + Ex.Message);
                                }
                            }
                            else // Video
                            {
                                if (!ValidateVideoExtension(hpf.FileName))
                                {
                                    MessageBoxError.Show("La extensión del video no es permitida.");
                                    return;
                                }
                                if (hpf.InputStream.Length > 104857600) // 100MB para iPhone
                                {
                                    MessageBoxError.Show("El video no puede exceder los 100MB");
                                    return;
                                }

                                string nombre_archivo = "Embarque_" + fechaarchivo + "_" + conse + "_" + campo;
                                string saveAs = Server.MapPath(videoOriginalPath);
                                videoTmpName = nombre_archivo;
                                string originalVideo = Path.Combine(saveAs, videoTmpName + Path.GetExtension(hpf.FileName));
                                hpf.SaveAs(originalVideo);

                                if (EncodingVideo(originalVideo))
                                {
                                    if (File.Exists(originalVideo))
                                    {
                                        try { File.Delete(originalVideo); } catch { }
                                        insert.vidrayan = nombre_archivo + ".mp4";
                                    }
                                }
                                else
                                {
                                    Session["error"] = "Error convirtiendo el video, intente nuevamente";
                                }
                            }
                        }
                    }

                    dcDatos.tb_det_revision_trailer.InsertOnSubmit(insert);
                    dcDatos.SubmitChanges();

                    // Recalcular porcentaje mediante SQL (eficiente)
                    RecalcularPorcentajeSQL(conse, fechaBase);

                    // Registro de movimiento
                    tb_registro_vertrai insertmov = new tb_registro_vertrai();
                    insertmov.fecha = DateTime.Now;
                    insertmov.nom_compu = "WebEmbarques";
                    insertmov.nom_usu = objUser.usuario;
                    insertmov.tipo_mov = "A";
                    insertmov.op_clave = "7.9";
                    insertmov.folio = conse.ToString();
                    insertmov.detalle = "Alta fotos";
                    insertmov.sistema = "EMBWEB";
                    insertmov.mov_folio = conse.ToString();
                    dcDatos.tb_registro_vertrai.InsertOnSubmit(insertmov);
                    dcDatos.SubmitChanges();

                    Session["exito"] = "Las fotos del trailer se han guardado correctamente";
                }
                else  // ACTUALIZACIÓN
                {
                    tb_det_revision_trailer actualizar = dcDatos.tb_det_revision_trailer
                        .FirstOrDefault(p => p.fecha == fechaBase && p.conse == conse);

                    if (actualizar == null)
                    {
                        MessageBoxError.Show("No se encontró el registro de revisión para actualizar.");
                        return;
                    }

                    HttpFileCollection hfc = Request.Files;
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        string campo = hfc.AllKeys[i];
                        HttpPostedFile hpf = hfc[i];
                        if (hpf.ContentLength > 0)
                        {
                            if (campo != "Videoryan")
                            {
                                if (!IsImage(hpf) && !IsHeicImage(hpf))
                                {
                                    MessageBoxError.Show("El archivo no es una imagen válida: " + campo);
                                    continue;
                                }

                                string nombre_archivo = "EM_" + fechaarchivo + "_" + conse + "_" + campo + ".jpg";
                                string savePath = Server.MapPath("~/FotoRevisionTrailer/");
                                string carpetaDestino = Path.Combine(savePath, mes_anio);
                                if (!Directory.Exists(carpetaDestino))
                                    Directory.CreateDirectory(carpetaDestino);

                                try
                                {
                                    byte[] imageBytes;
                                    if (IsHeicImage(hpf))
                                    {
                                        //imageBytes = ConvertHeicToJpg(hpf.InputStream);
                                        using (Bitmap bmpPostedImage = new Bitmap(hpf.InputStream))
                                        using (System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 405))
                                        {
                                            using (MemoryStream ms = new MemoryStream())
                                            {
                                                objImage.Save(ms, ImageFormat.Jpeg);
                                                imageBytes = ms.ToArray();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        using (Bitmap bmpPostedImage = new Bitmap(hpf.InputStream))
                                        using (System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 405))
                                        {
                                            using (MemoryStream ms = new MemoryStream())
                                            {
                                                objImage.Save(ms, ImageFormat.Jpeg);
                                                imageBytes = ms.ToArray();
                                            }
                                        }
                                    }
                                    File.WriteAllBytes(Path.Combine(carpetaDestino, nombre_archivo), imageBytes);

                                    // Asignar según el campo
                                    if (campo == "FotoSetPointIn") actualizar.setpointini = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoNoCaja") actualizar.numcaja = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoDifusor") actualizar.difusor = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoPiso") actualizar.piso = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoCajaCompleta") actualizar.cajacompleta = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoTemPro1") actualizar.temprod1 = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoTemPro2") actualizar.temprod2 = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoTemPro3") actualizar.temprod3 = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoTemPro4") actualizar.temprod4 = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoTemPro5") actualizar.temprod5 = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoTemPro6") actualizar.temprod6 = mes_anio + "/" + nombre_archivo; // corregido
                                    else if (campo == "FotoSetPointFin") actualizar.setpointfin = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoTerminoCarga") actualizar.termino_carga = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoAnden") actualizar.anden = mes_anio + "/" + nombre_archivo;
                                    else if (campo == "FotoRyan") actualizar.fotoryan = nombre_archivo; // ¡agregado!
                                }
                                catch (Exception Ex)
                                {
                                    EnviarErrorPorCorreo(Ex, nombre_archivo, conse);
                                    MessageBox.Show("Error en foto " + campo + ": " + Ex.Message);
                                }
                            }
                            else // Video
                            {
                                if (!ValidateVideoExtension(hpf.FileName))
                                {
                                    MessageBoxError.Show("La extensión del video no es permitida.");
                                    return;
                                }
                                if (hpf.InputStream.Length > 104857600) // 100MB
                                {
                                    MessageBoxError.Show("El video no puede exceder los 100MB");
                                    return;
                                }

                                string nombre_archivo = "Embarque_" + fechaarchivo + "_" + conse + "_" + campo;
                                string saveAs = Server.MapPath(videoOriginalPath);
                                videoTmpName = nombre_archivo;
                                string originalVideo = Path.Combine(saveAs, videoTmpName + Path.GetExtension(hpf.FileName));
                                hpf.SaveAs(originalVideo);

                                if (EncodingVideo(originalVideo))
                                {
                                    if (File.Exists(originalVideo))
                                    {
                                        try { File.Delete(originalVideo); } catch { }
                                        actualizar.vidrayan = nombre_archivo + ".mp4";
                                    }
                                }
                                else
                                {
                                    MessageBoxError.Show("Error convirtiendo el video, intente nuevamente");
                                }
                            }
                        }
                    }

                    dcDatos.SubmitChanges();

                    // Recalcular porcentaje mediante SQL
                    RecalcularPorcentajeSQL(conse, fechaBase);

                    // Registro de modificación
                    tb_registro_vertrai insertmov = new tb_registro_vertrai();
                    insertmov.fecha = DateTime.Now;
                    insertmov.nom_compu = "WebEmbarques";
                    insertmov.nom_usu = objUser.usuario;
                    insertmov.tipo_mov = "M";
                    insertmov.op_clave = "7.9";
                    insertmov.folio = conse.ToString();
                    insertmov.detalle = "Modificacion fotos";
                    insertmov.sistema = "EMBWEB";
                    insertmov.mov_folio = conse.ToString();
                    dcDatos.tb_registro_vertrai.InsertOnSubmit(insertmov);
                    dcDatos.SubmitChanges();

                    Session["exito"] = "Las fotos del trailer se han actualizado correctamente";
                }
            }
            catch (Exception ex)
            {
                Session["error"] = "Error de SQL: " + ex.Message + "\nConsulte con el administrador del sistema.";
            }

            Response.Redirect("SeleccionarTrailer.aspx");
        }

        // Recalcular porcentaje mediante SQL (eficiente y preciso)
        private void RecalcularPorcentajeSQL(decimal conse, DateTime fecha)
        {
            string sql = @"
    UPDATE tb_det_revision_trailer
    SET porcentaje = ROUND(
    (
        (
            CASE WHEN ISNULL(CAST(setpointini AS VARCHAR(MAX)), '') <> '' THEN 1 ELSE 0 END +
            CASE WHEN ISNULL(CAST(numcaja AS VARCHAR(MAX)), '') <> '' THEN 1 ELSE 0 END +
            CASE WHEN ISNULL(CAST(difusor AS VARCHAR(MAX)), '') <> '' THEN 1 ELSE 0 END +
            CASE WHEN ISNULL(CAST(piso AS VARCHAR(MAX)), '') <> '' THEN 1 ELSE 0 END +
            CASE WHEN ISNULL(CAST(cajacompleta AS VARCHAR(MAX)), '') <> '' THEN 1 ELSE 0 END +
            CASE WHEN ISNULL(CAST(temprod1 AS VARCHAR(MAX)), '') <> '' THEN 1 ELSE 0 END +
            CASE WHEN ISNULL(CAST(temprod2 AS VARCHAR(MAX)), '') <> '' THEN 1 ELSE 0 END +
            CASE WHEN ISNULL(CAST(temprod3 AS VARCHAR(MAX)), '') <> '' THEN 1 ELSE 0 END +
            CASE WHEN ISNULL(CAST(setpointfin AS VARCHAR(MAX)), '') <> '' THEN 1 ELSE 0 END +
            CASE WHEN ISNULL(CAST(termino_carga AS VARCHAR(MAX)), '') <> '' THEN 1 ELSE 0 END +
            CASE WHEN ISNULL(CAST(fotoryan AS VARCHAR(MAX)), '') <> '' THEN 1 ELSE 0 END
        ) * 100.0
    ) / 11
    ,2)
    WHERE conse = @conse
    AND fecha = @fecha";

            using (SqlCommand cmd = new SqlCommand(sql, thisConnection))
            {
                cmd.Parameters.AddWithValue("@conse", conse);
                cmd.Parameters.AddWithValue("@fecha", fecha);

                thisConnection.Open();
                cmd.ExecuteNonQuery();
                thisConnection.Close();
            }
        }

        // Detectar imagen HEIC
        private bool IsHeicImage(HttpPostedFile file)
        {
            if (file == null) return false;
            string contentType = file.ContentType.ToLower();
            string extension = Path.GetExtension(file.FileName).ToLower();
            return contentType == "image/heic" || extension == ".heic";
        }

        // Convertir HEIC a JPG usando Magick.NET (requiere instalar NuGet: Magick.NET-Q16-AnyCPU)
        //private byte[] ConvertHeicToJpg(Stream heicStream)
        //{
        //    using (var image = new MagickImage(heicStream))
        //    {
        //        image.Format = MagickFormat.Jpeg;
        //        return image.ToByteArray();
        //    }
        //}

        private bool ValidateVideoExtension(string filename)
        {
            FileInfo info = new FileInfo(filename);
            switch (info.Extension.ToLower())
            {
                case ".mpg":
                case ".wmv":
                case ".avi":
                case ".mp4":
                case ".mov":   // Añadido para iPhone
                    return true;
                default:
                    return false;
            }
        }


        private void EnviarErrorPorCorreo(Exception ex, string nombre_archivo, decimal conse)
        {
            try
            {
                MailMessage mnsg = new MailMessage();
                mnsg.To.Add("jgalvan@mrlucky.com.mx");
                mnsg.Subject = "Embarques error en foto del " + conse;
                mnsg.SubjectEncoding = System.Text.Encoding.UTF8;
                mnsg.Body = "Error al subir una foto " + ex.ToString() + " Nombre del archivo " + nombre_archivo + " Del consecutivo " + conse;
                mnsg.BodyEncoding = System.Text.Encoding.UTF8;
                mnsg.IsBodyHtml = true;
                //mnsg.From = new MailAddress("aescamilla@mrlucky.com.mx");
                mnsg.From = new MailAddress("sistemas@mrlucky.com.mx");

                SmtpClient cliente = new SmtpClient();
                //cliente.Credentials = new NetworkCredential("aescamilla", "atrejo");
                cliente.Credentials = new NetworkCredential("sistemas", "sisgab");
                cliente.Port = 587;
                cliente.EnableSsl = true;
                cliente.Host = "mail1.mrlucky.com.mx";
                cliente.Send(mnsg);
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Queja", "alert('No fue enviado el correo electrónico');", true);
            }
        }

        protected void btnGuardarLEGACY_Click(object sender, EventArgs e)
        {
            if (Anden.Enabled == true && FotoAnden.FileName == "")
            {
                MessageBoxError.Show("Debe Seleccionar un Anden para la primera Carga de fotos");
                return;
            }
            else if (Anden.Enabled == true && FotoAnden.FileName != "")
            {
                var consulta = from u in dcDatos.tb_mstr_trailer
                               where u.anden == Convert.ToDecimal(Anden.SelectedValue.ToString())
                               && u.horafin == "--:--" && u.Guardar == 'N'
                               && u.responsable != "J CONCEPCION RAZO PIZANO"
                               select u;

                if (consulta != null)
                {
                    if (consulta.Count() > 0)
                    {
                        trailer = consulta.First();
                        MessageBox.Show("El Anden Seleccionado esta ocupado por el trailer " + trailer.no_trailer.ToString() + " Del dia: " + trailer.hora_trailer);
                        return;
                    }
                    else
                    {
                        var fechaactualtrailer = Convert.ToDateTime(Session["fechareg"]).ToString("dd/MM/yyyy");
                        var consetrailer = Convert.ToDecimal(Session["conse"]);

                        thisConnection.Open();
                        string cadena = "UPDATE tb_mstr_trailer SET Anden = '" + Anden.SelectedValue.ToString().Trim() + "' Where hora_trailer = '" + fechaactualtrailer + "' AND conse = '" + consetrailer + "'";
                        SqlCommand cmd = new SqlCommand(cadena, thisConnection);
                        cmd.ExecuteNonQuery();
                        thisConnection.Close();
                    }
                }
            }
            else
            {

            }


            var fechaactual2 = Convert.ToDateTime(Session["fechareg"]).ToString("ddMMyyyy");

            var mes_anio = fechaactual2.Remove(0, 2);

            Tb_Autoriza_OdeP objUser = (Tb_Autoriza_OdeP)Session["objAdmin"];
            try
            {
                var fechaactual = Convert.ToDateTime(Session["fechareg"]).ToString("dd/MM/yyyy");
                var fechaarchivo = Convert.ToDateTime(Session["fechareg"]).ToString("ddMMyyyy");
                var conse = Convert.ToDecimal(Session["conse"]);

                if (Convert.ToInt32(Session["actualizar"]) == 0)
                {
                    tb_det_revision_trailer insert = new tb_det_revision_trailer();
                    insert.fecha = Convert.ToDateTime(fechaactual);
                    insert.conse = conse;
                    insert.responsable_captu = objUser.usuario;
                    insert.fechaini = Convert.ToDateTime(fechaactual);
                    insert.conseini = conse;

                    //Recorrido de los campos de fSoto
                    HttpFileCollection hfc = Request.Files;
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        string campo = hfc.AllKeys[i];
                        HttpPostedFile hpf = hfc[i];
                        if (hpf.ContentLength > 0)
                        {
                            if (campo != "Videoryan")
                            {
                                if (IsImage(hpf) == false)
                                {
                                    MessageBoxError.Show("El archivo no es una imagen");
                                }
                                else
                                {



                                    string nombre_archivo = fechaarchivo + "_" + conse + "_" + campo + ".jpg";


                                    //Guardar Archivo en BD ***********************************************************

                                    HttpPostedFile file = hpf;


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

                                    HttpPostedFile userPostedFile = file;
                                    try
                                    {
                                        if (userPostedFile.ContentLength > 0)
                                        {

                                            //userPostedFile.SaveAs(savePath + Path.GetFileName(fileName) + (i + 1) + ".jpg");
                                            System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(userPostedFile.InputStream);
                                            System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 405);

                                            bmpPostedImage.Dispose();

                                            objImage.Save(savePath + mes_anio + "/" + Path.GetFileName(fileName), ImageFormat.Jpeg);

                                            objImage.Dispose();

                                            if (campo == "FotoSetPointIn")
                                            {
                                                insert.setpointini = mes_anio + "/" + nombre_archivo;
                                                porcentaje = porcentaje + 1;
                                            }
                                            else if (campo == "FotoNoCaja")
                                            {
                                                insert.numcaja = mes_anio + "/" + nombre_archivo;
                                                porcentaje = porcentaje + 1;
                                            }
                                            else if (campo == "FotoDifusor")
                                            {
                                                insert.difusor = mes_anio + "/" + nombre_archivo;
                                                porcentaje = porcentaje + 1;
                                            }
                                            else if (campo == "FotoPiso")
                                            {
                                                insert.piso = mes_anio + "/" + nombre_archivo;
                                                porcentaje = porcentaje + 1;
                                            }
                                            else if (campo == "FotoCajaCompleta")
                                            {
                                                insert.cajacompleta = mes_anio + "/" + nombre_archivo;
                                                porcentaje = porcentaje + 1;
                                            }
                                            else if (campo == "FotoTemPro1")
                                            {
                                                insert.temprod1 = mes_anio + "/" + nombre_archivo;
                                                porcentaje = porcentaje + 1;
                                            }
                                            else if (campo == "FotoTemPro2")
                                            {
                                                insert.temprod2 = mes_anio + "/" + nombre_archivo;
                                                porcentaje = porcentaje + 1;
                                            }
                                            else if (campo == "FotoTemPro3")
                                            {
                                                insert.temprod3 = mes_anio + "/" + nombre_archivo;
                                                porcentaje = porcentaje + 1;
                                            }
                                            else if (campo == "FotoTemPro4")
                                            {
                                                insert.temprod4 = mes_anio + "/" + nombre_archivo;
                                            }
                                            else if (campo == "FotoTemPro5")
                                            {
                                                insert.temprod5 = mes_anio + "/" + nombre_archivo;
                                            }
                                            else if (campo == "FotoTemPro6")
                                            {
                                                insert.temprod5 = mes_anio + "/" + nombre_archivo;
                                            }
                                            else if (campo == "FotoSetPointFin")
                                            {
                                                insert.setpointfin = mes_anio + "/" + nombre_archivo;
                                                porcentaje = porcentaje + 1;
                                            }
                                            else if (campo == "Videoryan")
                                            {
                                                insert.vidrayan = nombre_archivo;
                                            }
                                            else if (campo == "FotoTerminoCarga")
                                            {
                                                insert.termino_carga = mes_anio + "/" + nombre_archivo;
                                                porcentaje = porcentaje + 1;
                                            }
                                            else if (campo == "FotoAnden")
                                            {
                                                insert.anden = mes_anio + "/" + nombre_archivo;
                                            }
                                            else if (campo == "FotoRyan")
                                            {
                                                insert.fotoryan = nombre_archivo;
                                                porcentaje = porcentaje + 1;
                                            }
                                        }
                                    }
                                    catch (Exception Ex)
                                    {
                                        string host = Dns.GetHostName();
                                        IPHostEntry ipEntry = Dns.GetHostEntry(host);

                                        System.Net.Mail.MailMessage mnsg = new System.Net.Mail.MailMessage();
                                        mnsg.To.Add("dmunoz@mrlucky.com.mx");//msamano@mrlucky.com.mx
                                        mnsg.Subject = "Embarques error en foto del " + Session["conse"];
                                        mnsg.SubjectEncoding = System.Text.Encoding.UTF8;
                                        mnsg.Body = "Error al subir una foto " + Ex + " Nombre del archivo " + nombre_archivo + " Del consecutivo" + Session["conse"];
                                        mnsg.BodyEncoding = System.Text.Encoding.UTF8;
                                        mnsg.IsBodyHtml = true;
                                        mnsg.From = new MailAddress("aescamilla@mrlucky.com.mx");

                                        SmtpClient cliente = new SmtpClient();
                                        cliente.Credentials = new System.Net.NetworkCredential("aescamilla", "atrejo");
                                        cliente.Port = 587;
                                        cliente.EnableSsl = true;
                                        cliente.Host = "mail1.mrlucky.com.mx";

                                        try
                                        {
                                            cliente.Send(mnsg);
                                            //Response.Write("<script>alert('Correo enviado correctamente');</script>");
                                        }
                                        catch (Exception)
                                        {
                                            //Response.Write("<script>alert('No fue enviado el correo electronico');</script>");
                                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Queja", "alert('No fue enviado el correo electrónico');", true);
                                        }

                                        MessageBox.Show("Error " + Ex.Message);
                                    }//*** Termina Guardar Archivo

                                }
                            }
                            else
                            {
                                HttpPostedFile file = hpf;
                                if (!ValidateVideoExtension(file.FileName))
                                {
                                    MessageBoxError.Show("La extension del archivo no es permitido.");
                                    return;
                                }
                                if (file.InputStream.Length > 52428800)
                                {
                                    MessageBoxError.Show("El video no puede exceder los 50MB");
                                    return;
                                }

                                string nombre_archivo = "Embarque_" + fechaarchivo + "_" + conse + "_" + campo;
                                string saveAs = Server.MapPath(videoOriginalPath);

                                videoTmpName = nombre_archivo;

                                string originalVideo = saveAs + videoTmpName + new FileInfo(file.FileName).Extension;

                                file.SaveAs(originalVideo);

                                if (EncodingVideo(originalVideo))
                                {
                                    string fileName = videoConvertedName;
                                    string sourcePath = "~/FotoRevisionTrailer/Converted/";
                                    string originPath = "~/FotoRevisionTrailer/";
                                    string targetPath = @"\\192.168.123.4\FotosRevisionTrailer\";

                                    sourcePath = Server.MapPath(videoConvertedPath);
                                    originPath = Server.MapPath(videoOriginalPath);


                                    // Use Path class to manipulate file and directory paths.
                                    string originFile = System.IO.Path.Combine(originPath, fileName);
                                    string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                                    string destFile = System.IO.Path.Combine(targetPath, fileName);

                                    if (System.IO.File.Exists(originalVideo))
                                    {
                                        // Use a try block to catch IOExceptions, to
                                        // handle the case of the file already being
                                        // opened by another process.
                                        try
                                        {
                                            System.IO.File.Delete(originalVideo);
                                            insert.vidrayan = nombre_archivo + ".mp4";

                                        }
                                        catch (System.IO.IOException ed)
                                        {
                                            Console.WriteLine(ed.Message);
                                            return;
                                        }
                                    }
                                }

                                else
                                {
                                    Session["error"] = "Error convirtiendo el video, intente nuevamente";
                                }
                            }


                        }
                    }
                    decimal porce = Convert.ToDecimal((porcentaje * Convert.ToDecimal(100)) / Convert.ToDecimal(10));
                    insert.porcentaje = porce;
                    dcDatos.tb_det_revision_trailer.InsertOnSubmit(insert);
                    dcDatos.SubmitChanges();

                    //Alta trailer, insercion a base de datos - tabla registro de movimientos
                    DateTime fechahoramov = DateTime.Now;
                    tb_registro_vertrai insertmov = new tb_registro_vertrai();
                    insertmov.fecha = fechahoramov;
                    insertmov.nom_compu = "WebEmbarques";
                    insertmov.nom_usu = objUser.usuario;
                    insertmov.tipo_mov = "A";
                    insertmov.op_clave = "7.9";
                    insertmov.folio = conse.ToString();
                    insertmov.detalle = "Alta fotos al " + porce + " porciento";
                    insertmov.sistema = "EMBWEB";
                    insertmov.mov_folio = conse.ToString();
                    dcDatos.tb_registro_vertrai.InsertOnSubmit(insertmov);
                    dcDatos.SubmitChanges();

                    Session["exito"] = "Las fotos del trailer se han guardado correctamente";
                }
                else
                {
                    tb_det_revision_trailer actualizar = (from p in dcDatos.tb_det_revision_trailer
                                                          where p.fecha == Convert.ToDateTime(fechaactual)
                                                              && p.conse == conse
                                                          select p).SingleOrDefault();

                    //Recorrido de los campos de foto
                    HttpFileCollection hfc = Request.Files;

                    for (int i = 0; i < hfc.Count; i++)
                    {
                        string campo = hfc.AllKeys[i];
                        HttpPostedFile hpf = hfc[i];
                        if (hpf.ContentLength > 0)
                        {
                            if (campo != "Videoryan")
                            {
                                if (IsImage(hpf) == false)
                                {
                                    MessageBoxError.Show("El archivo no es una imagen");
                                }
                                else
                                {
                                    string nombre_archivo = "EM_" + fechaarchivo + "_" + conse + "_" + campo + ".jpg";


                                    //Guardar Archivo en BD ***********************************************************

                                    HttpPostedFile file = hpf;


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

                                    HttpPostedFile userPostedFile = file;
                                    try
                                    {
                                        if (userPostedFile.ContentLength > 0)
                                        {

                                            //userPostedFile.SaveAs(savePath + Path.GetFileName(fileName) + (i + 1) + ".jpg");
                                            System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(userPostedFile.InputStream);
                                            System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 405);

                                            bmpPostedImage.Dispose();

                                            objImage.Save(savePath + mes_anio + "/" + Path.GetFileName(fileName), ImageFormat.Jpeg);


                                            objImage.Dispose();

                                            if (campo == "FotoSetPointIn")
                                            {
                                                if (actualizar != default(tb_det_revision_trailer))
                                                {
                                                    actualizar.setpointini = mes_anio + "/" + nombre_archivo;
                                                    porcentaje = porcentaje + 1;
                                                }
                                            }
                                            else if (campo == "FotoNoCaja")
                                            {
                                                if (actualizar != default(tb_det_revision_trailer))
                                                {
                                                    actualizar.numcaja = mes_anio + "/" + nombre_archivo;
                                                    porcentaje = porcentaje + 1;
                                                }
                                            }
                                            else if (campo == "FotoDifusor")
                                            {
                                                if (actualizar != default(tb_det_revision_trailer))
                                                {
                                                    actualizar.difusor = mes_anio + "/" + nombre_archivo;
                                                    porcentaje = porcentaje + 1;
                                                }
                                            }
                                            else if (campo == "FotoPiso")
                                            {
                                                if (actualizar != default(tb_det_revision_trailer))
                                                {
                                                    actualizar.piso = mes_anio + "/" + nombre_archivo;
                                                    porcentaje = porcentaje + 1;
                                                }
                                            }
                                            else if (campo == "FotoCajaCompleta")
                                            {
                                                if (actualizar != default(tb_det_revision_trailer))
                                                {
                                                    actualizar.cajacompleta = mes_anio + "/" + nombre_archivo;
                                                    porcentaje = porcentaje + 1;
                                                }
                                            }
                                            else if (campo == "FotoTemPro1")
                                            {
                                                if (actualizar != default(tb_det_revision_trailer))
                                                {
                                                    actualizar.temprod1 = mes_anio + "/" + nombre_archivo;
                                                    porcentaje = porcentaje + 1;
                                                }
                                            }
                                            else if (campo == "FotoTemPro2")
                                            {
                                                if (actualizar != default(tb_det_revision_trailer))
                                                {
                                                    actualizar.temprod2 = mes_anio + "/" + nombre_archivo;
                                                    porcentaje = porcentaje + 1;
                                                }
                                            }
                                            else if (campo == "FotoTemPro3")
                                            {
                                                if (actualizar != default(tb_det_revision_trailer))
                                                {
                                                    actualizar.temprod3 = mes_anio + "/" + nombre_archivo;
                                                    porcentaje = porcentaje + 1;
                                                }
                                            }
                                            else if (campo == "FotoTemPro4")
                                            {
                                                if (actualizar != default(tb_det_revision_trailer))
                                                {
                                                    actualizar.temprod4 = mes_anio + "/" + nombre_archivo;
                                                }
                                            }
                                            else if (campo == "FotoTemPro5")
                                            {
                                                if (actualizar != default(tb_det_revision_trailer))
                                                {
                                                    actualizar.temprod5 = mes_anio + "/" + nombre_archivo;
                                                }
                                            }
                                            else if (campo == "FotoTemPro6")
                                            {
                                                if (actualizar != default(tb_det_revision_trailer))
                                                {
                                                    actualizar.temprod6 = mes_anio + "/" + nombre_archivo;
                                                }
                                            }
                                            else if (campo == "FotoSetPointFin")
                                            {
                                                if (actualizar != default(tb_det_revision_trailer))
                                                {
                                                    actualizar.setpointfin = mes_anio + "/" + nombre_archivo;
                                                    porcentaje = porcentaje + 1;
                                                }
                                            }
                                            else if (campo == "Videoryan")
                                            {
                                                if (actualizar != default(tb_det_revision_trailer))
                                                {
                                                    actualizar.vidrayan = nombre_archivo;
                                                }
                                            }
                                            else if (campo == "FotoTerminoCarga")
                                            {
                                                if (actualizar != default(tb_det_revision_trailer))
                                                {
                                                    actualizar.termino_carga = mes_anio + "/" + nombre_archivo;
                                                    porcentaje = porcentaje + 1;
                                                }
                                            }
                                            else if (campo == "FotoAnden")
                                            {
                                                if (actualizar != default(tb_det_revision_trailer))
                                                {
                                                    actualizar.anden = mes_anio + "/" + nombre_archivo;
                                                }
                                            }
                                            else if (campo == "FotoRyan")
                                            {
                                                if (actualizar != default(tb_det_revision_trailer))
                                                {
                                                    actualizar.fotoryan = nombre_archivo;
                                                    porcentaje = porcentaje + 1;
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception Ex)
                                    {
                                        string host = Dns.GetHostName();
                                        IPHostEntry ipEntry = Dns.GetHostEntry(host);

                                        System.Net.Mail.MailMessage mnsg = new System.Net.Mail.MailMessage();
                                        mnsg.To.Add("dmunoz@mrlucky.com.mx");//msamano@mrlucky.com.mx
                                        mnsg.Subject = "Embarques error en foto del " + Session["conse"];
                                        mnsg.SubjectEncoding = System.Text.Encoding.UTF8;
                                        mnsg.Body = "Error al subir una foto " + Ex + " Nombre del archivo " + nombre_archivo + " Del consecutivo" + Session["conse"];
                                        mnsg.BodyEncoding = System.Text.Encoding.UTF8;
                                        mnsg.IsBodyHtml = true;
                                        mnsg.From = new MailAddress("aescamilla@mrlucky.com.mx");

                                        SmtpClient cliente = new SmtpClient();
                                        cliente.Credentials = new System.Net.NetworkCredential("aescamilla", "atrejo");
                                        cliente.Port = 587;
                                        cliente.EnableSsl = true;
                                        cliente.Host = "mail1.mrlucky.com.mx";

                                        try
                                        {
                                            cliente.Send(mnsg);
                                            //Response.Write("<script>alert('Correo enviado correctamente');</script>");
                                        }
                                        catch (Exception)
                                        {
                                            //Response.Write("<script>alert('No fue enviado el correo electronico');</script>");
                                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Queja", "alert('No fue enviado el correo electrónico');", true);
                                        }
                                    }//*** Termina Guardar Archivo
                                }

                            }
                            else
                            {
                                HttpPostedFile file = hpf;
                                if (!ValidateVideoExtension(file.FileName))
                                {
                                    MessageBoxError.Show("La extension del archivo no es permitido.");
                                    return;
                                }
                                if (file.InputStream.Length > 52428800)
                                {
                                    MessageBoxError.Show("El video no puede exceder los 50MB");
                                    return;
                                }

                                string nombre_archivo = "Embarque_" + fechaarchivo + "_" + conse + "_" + campo;
                                string saveAs = Server.MapPath(videoOriginalPath);

                                videoTmpName = nombre_archivo;

                                string originalVideo = saveAs + videoTmpName + new FileInfo(file.FileName).Extension;

                                file.SaveAs(originalVideo);

                                if (EncodingVideo(originalVideo))
                                {
                                    string fileName = videoConvertedName;
                                    string sourcePath = "~/FotoRevisionTrailer/Converted/";
                                    string originPath = "~/FotoRevisionTrailer/";
                                    string targetPath = @"\\192.168.123.4\FotosRevisionTrailer\";

                                    sourcePath = Server.MapPath(videoConvertedPath);
                                    originPath = Server.MapPath(videoOriginalPath);


                                    // Use Path class to manipulate file and directory paths.
                                    string originFile = System.IO.Path.Combine(originPath, fileName);
                                    string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                                    string destFile = System.IO.Path.Combine(targetPath, fileName);


                                    if (System.IO.File.Exists(originalVideo))
                                    {
                                        try
                                        {
                                            //System.IO.File.Delete(originalVideo);
                                            if (actualizar != default(tb_det_revision_trailer))
                                            {
                                                actualizar.vidrayan = nombre_archivo + ".mp4";
                                            }
                                        }
                                        catch (System.IO.IOException ed)
                                        {
                                            Console.WriteLine(ed.Message);
                                            return;
                                        }
                                    }

                                }

                                else
                                {
                                    MessageBoxError.Show("Error convirtiendo el video, intente nuevamente");
                                }


                            }
                        }
                    }
                    decimal porce = Convert.ToDecimal((porcentaje * Convert.ToDecimal(100)) / Convert.ToDecimal(10));
                    actualizar.porcentaje = porce;
                    dcDatos.SubmitChanges();

                    //Modificacion trailer, insercion a base de datos - tabla registro de movimientos
                    DateTime fechahoramov = DateTime.Now;
                    tb_registro_vertrai insertmov = new tb_registro_vertrai();
                    insertmov.fecha = fechahoramov;
                    insertmov.nom_compu = "WebEmbarques";
                    insertmov.nom_usu = objUser.usuario;
                    insertmov.tipo_mov = "M";
                    insertmov.op_clave = "7.9";
                    insertmov.folio = conse.ToString();
                    insertmov.detalle = "Modificacion fotos al " + porce + " porciento";
                    insertmov.sistema = "EMBWEB";
                    insertmov.mov_folio = conse.ToString();
                    dcDatos.tb_registro_vertrai.InsertOnSubmit(insertmov);
                    dcDatos.SubmitChanges();
                    Session["exito"] = "Las fotos del trailer se han actualizado correctamente";
                }

                // Get the HttpFileCollection

            }
            catch (Exception ex)
            {
                Session["error"] = "Error de SQL: " + ex.Message
                                     + "\n" + "Consulte con el administrador del sistema.";
            }

            Response.Redirect("SeleccionarTrailer.aspx");
        }

        void SaveFile(HttpPostedFile file, string nombre_archivo)
        {


        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("PaginaLogin.aspx");
        }

        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
        {
            var ratio = (double)maxHeight / image.Height;
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }



        private bool EncodingVideo(string originalVideo)
        {
            bool value = false;
            string saveAs = Server.MapPath(videoConvertedPath);
            videoConvertedName = videoTmpName + flv;
            //Parametros que se le pasaran al ejecutable para fines de encoding.
            //string args = @" -i " + originalVideo + " -b 200 -r 24 -s 1052x864 -deinterlace -ab 64k " + saveAs + videoConvertedName;
            string args = @" -i " + originalVideo + "  -f mp4 -vcodec libx264 -preset fast -profile:v main -acodec aac " + saveAs + videoConvertedName;
            //Hacemos uso de la clase proxy la cual nos provee acceso directo al ejecutable.
            using (Process enconding = new Process())
            {
                enconding.StartInfo.WorkingDirectory = Server.MapPath("~/");
                enconding.StartInfo.FileName = Server.MapPath("js/ffmpeg.exe");
                enconding.StartInfo.Arguments = args;
                enconding.StartInfo.UseShellExecute = false;
                enconding.StartInfo.CreateNoWindow = false;
                enconding.StartInfo.RedirectStandardOutput = false;
                enconding.Start();
                //Como es un proceso en linea debemos esperar a que termine para mostrar el video sino el player dará un error de FileNotFound.
                enconding.WaitForExit();
                value = true;
            }
            return value;
        }

    }
}