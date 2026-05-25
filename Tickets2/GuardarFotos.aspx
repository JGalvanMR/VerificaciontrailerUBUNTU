<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuardarFotos.aspx.cs" Inherits="Tickets2.GuardarFotos" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title>Guardar Fotos - Revisión de Trailer</title>

    <meta name="description" content="Common form elements and layouts" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />


    <script src="js/jquery-2.1.3.min.js"></script>
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.1.0/css/all.css" integrity="sha384-lKuwvrZot6UHsBSfcMvOkWwlCMgc0TaWr+30HWe3a4ltaBwTZhyTEggF5tJv8tbt" crossorigin="anonymous">

    <link rel="stylesheet" href="assets/css/jquery-ui.custom.min.css" />
    <link rel="stylesheet" href="assets/css/chosen.min.css" />
    <link rel="stylesheet" href="assets/css/bootstrap-datepicker3.min.css" />
    <link rel="stylesheet" href="assets/css/bootstrap-timepicker.min.css" />
    <link rel="stylesheet" href="assets/css/daterangepicker.min.css" />
    <link rel="stylesheet" href="assets/css/bootstrap-datetimepicker.min.css" />
    <link rel="stylesheet" href="assets/css/bootstrap-colorpicker.min.css" />
    <link rel="stylesheet" href="assets/css/jquery.gritter.min.css" />

    <link rel="stylesheet" href="assets/css/fonts.googleapis.com.css" />


    <link rel="stylesheet" href="assets/css/ace.min.css" class="ace-main-stylesheet" id="main-ace-style" />


    <link rel="stylesheet" href="assets/css/ace-skins.min.css" />
    <link rel="stylesheet" href="assets/css/ace-rtl.min.css" />


    <script src="assets/js/ace-extra.min.js"></script>


</head>

<body class="no-skin">
    <form class="form-horizontal" runat="server" role="form">
        <div id="navbar" class="navbar navbar-default          ace-save-state">
            <div class="navbar-container ace-save-state" id="navbar-container">
                <div class="navbar-header pull-left">
                    <a href="" class="navbar-brand">
                        <small>
                            <img src="assets/images/avatars/logos.png" title="logo" />
                        </small>
                    </a>
                </div>

                <div class="navbar-buttons navbar-header pull-right" role="navigation">
                    <ul class="nav ace-nav">
                        <img src="assets/images/avatars/logo_trailer_small.png" title="logo" class="icon-animated-vertical" />
                        <asp:LinkButton ID="btnSalir" runat="server" CssClass="btn btn-danger btn-sm" Text="<i class='ace-icon fas fa-power-off icon-only'></i>"
                            OnClick="btnSalir_Click" />
                    </ul>
                </div>
            </div>
        </div>

        <div class="main-container ace-save-state" id="main-container">
            <script type="text/javascript">
                try { ace.settings.loadState('main-container') } catch (e) { }
            </script>

            <div class="main-content">
                <div class="page-content">
                    <div class="page-header">
                        <h1>Asignar Fotografias por transporte
								<small>
                                    <i class="ace-icon fa fa-angle-double-right"></i>
                                    validar información y asignar fotografias
                                </small>
                        </h1>
                    </div>

                    <div class="row">
                        <div class="col-xs-12">

                            <div class="hr hr-24"></div>

                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="widget-box">
                                        <div class="widget-header">
                                            <h4 class="widget-title">Información del trailer</h4>

                                            <div class="widget-toolbar">
                                                <a href="#" data-action="collapse">
                                                    <i class="ace-icon fa fa-chevron-up"></i>
                                                </a>

                                                <a href="#" data-action="close">
                                                    <i class="ace-icon fa fa-times"></i>
                                                </a>
                                            </div>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <div>
                                                    <label for="form-field-8">Hora de entrada</label>
                                                    <asp:TextBox runat="server" ID="horaentrada" CssClass="form-control" placeholder="Id del Servicio"></asp:TextBox>
                                                </div>

                                                <div>
                                                    <label for="form-field-9">Placas</label>

                                                    <asp:TextBox runat="server" ID="placas" CssClass="form-control" placeholder="Placas"></asp:TextBox>
                                                </div>

                                                <div>
                                                    <label for="form-field-11">Destino</label>

                                                    <asp:TextBox runat="server" ID="destino" CssClass="form-control" placeholder="Destino"></asp:TextBox>
                                                </div>

                                                <div>
                                                    <label for="form-field-11">Nombre del Chofer</label>

                                                    <asp:TextBox runat="server" ID="chofer" CssClass="form-control" placeholder="Chofer"></asp:TextBox>
                                                </div>

                                                <div>
                                                    <label for="form-field-11">Transporte</label>
                                                    <asp:TextBox runat="server" ID="transporte" CssClass="form-control" placeholder="Transporte"></asp:TextBox>
                                                </div>
                                                <div>
                                                    <label for="form-field-11">Anden</label>

                                                    <asp:DropDownList ID="Anden" CssClass="form-control" runat="server">
                                                        <asp:ListItem Selected="True" Value="1"> 1 </asp:ListItem>
                                                        <asp:ListItem Value="2"> 2 </asp:ListItem>
                                                        <asp:ListItem Value="3"> 3 </asp:ListItem>
                                                        <asp:ListItem Value="4"> 4 </asp:ListItem>
                                                        <asp:ListItem Value="5"> 5 </asp:ListItem>
                                                        <asp:ListItem Value="6"> 6 </asp:ListItem>
                                                        <asp:ListItem Value="7"> 7 </asp:ListItem>
                                                        <asp:ListItem Value="8"> 8 </asp:ListItem>
                                                        <asp:ListItem Value="9"> 9 </asp:ListItem>
                                                        <asp:ListItem Value="10"> 10 </asp:ListItem>
                                                        <asp:ListItem Value="11"> 11 </asp:ListItem>
                                                        <asp:ListItem Value="12"> 12 </asp:ListItem>
                                                        <asp:ListItem Value="13"> 13 </asp:ListItem>
                                                        <asp:ListItem Value="14"> 14 </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div>
                                                    <label>Foto Anden:</label>
                                                    <asp:Label runat="server" ID="LblAnden"></asp:Label>
                                                    <asp:FileUpload ID="FotoAnden" runat="server" CssClass="file" data-show-upload="false" data-show-caption="true" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="widget-box col-sm-7">
                                    <div class="widget-header widget-header-small">
                                        <h4 class="widget-title">Fotos Por Trailer</h4>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div>
                                                        <label>Foto SetPoint Inicial:</label>
                                                        <asp:Label runat="server" ID="lblsetpointini"></asp:Label>
                                                        <asp:FileUpload ID="FotoSetPointIn" runat="server" CssClass="file" data-show-upload="false" data-show-caption="true" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div>
                                                        <label>Foto Numero de Caja:</label>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblnocaja"></asp:Label></td>
                                                        <asp:FileUpload ID="FotoNoCaja" runat="server" CssClass="file" data-show-upload="false" data-show-caption="true" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div>
                                                        <label>Foto Difusor:</label>
                                                        <asp:Label runat="server" ID="lbldifusor"></asp:Label>
                                                        <asp:FileUpload ID="FotoDifusor" runat="server" CssClass="file" data-show-upload="false" data-show-caption="true" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div>
                                                        <label>Foto Piso:</label>
                                                        <asp:Label runat="server" ID="lblfotopiso"></asp:Label>
                                                        <asp:FileUpload ID="FotoPiso" runat="server" CssClass="file" data-show-upload="false" data-show-caption="true" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div>
                                                        <label>Foto Caja Completa:</label>
                                                        <asp:Label runat="server" ID="lblcajacompleta"></asp:Label>
                                                        <asp:FileUpload ID="FotoCajaCompleta" runat="server" CssClass="file" data-show-upload="false" data-show-caption="true" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div>
                                                        <label>Foto Temperatura Producto (1) *Minimo 3:</label>
                                                        <asp:Label runat="server" ID="lbltemprod1"></asp:Label>
                                                        <asp:FileUpload ID="FotoTemPro1" runat="server" CssClass="file" data-show-upload="false" data-show-caption="true" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div>
                                                        <label>Foto Temperatura Producto (2) *Minimo 3:</label>
                                                        <asp:Label runat="server" ID="lbltemprod2"></asp:Label>
                                                        <asp:FileUpload ID="FotoTemPro2" runat="server" CssClass="file" data-show-upload="false" data-show-caption="true" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div>
                                                        <label>Foto Temperatura Producto (3) *Minimo 3:</label>
                                                        <asp:Label runat="server" ID="lbltemprod3"></asp:Label>
                                                        <asp:FileUpload ID="FotoTemPro3" runat="server" CssClass="file" data-show-upload="false" data-show-caption="true" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div>
                                                        <label>Foto Temperatura Producto (4):</label>
                                                        <asp:Label runat="server" ID="lbltemprod4"></asp:Label>
                                                        <asp:FileUpload ID="FotoTemPro4" runat="server" CssClass="file" data-show-upload="false" data-show-caption="true" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div>
                                                        <label>Temperatura Producto (5):</label>
                                                        <asp:Label runat="server" ID="lbltemprod5"></asp:Label>
                                                        <asp:FileUpload ID="FotoTemPro5" runat="server" CssClass="file" data-show-upload="false" data-show-caption="true" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div>
                                                        <label>Temperatura Producto (6):</label>
                                                        <asp:Label runat="server" ID="lbltemprod6"></asp:Label>
                                                        <asp:FileUpload ID="FotoTemPro6" runat="server" CssClass="file" data-show-upload="false" data-show-caption="true" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div>
                                                        <label>Foto SetPoint Final:</label>
                                                        <asp:Label runat="server" ID="lblsetpointfin"></asp:Label>
                                                        <asp:FileUpload ID="FotoSetPointFin" runat="server" CssClass="file" data-show-upload="false" data-show-caption="true" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div>
                                                        <label>Video Activación Ryan:</label>
                                                        <asp:Label runat="server" ID="lblvideoryan"></asp:Label>
                                                        <asp:FileUpload ID="Videoryan" runat="server" CssClass="file" data-show-upload="false" data-show-caption="true" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div>
                                                        <label>Foto Ryan (Temperatura Visible)</label>
                                                        <asp:Label runat="server" ID="lblFotoRyan"></asp:Label>
                                                        <asp:FileUpload ID="FotoRyan" runat="server" CssClass="file" data-show-upload="false" data-show-caption="true" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div>
                                                        <label>Foto Termino de Carga:</label>
                                                        <asp:Label runat="server" ID="lblterminocarga"></asp:Label>
                                                        <asp:FileUpload ID="FotoTerminoCarga" runat="server" CssClass="file" data-show-upload="false" data-show-caption="true" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <center>
                                <div class="col-xs-8 col-sm-4 col-md-2">
                                    <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary btn-block" Text="Guardar Fotos Trailer <span class='glyphicon glyphicon-floppy-disk'></span>"
                                        OnClick="btnGuardar_Click" />
                                </div>
                            </center>




                            <div class="hr hr-18 dotted hr-double"></div>



                            <div class="space-6"></div>


                        </div>
                    </div>

                </div>
            </div>
        </div>
        </div>
			</div>

			<div class="footer">
                <div class="footer-inner">
                    <div class="footer-content">
                        <span class="bigger-120">
                            <span class="blue bolder">COMERCIALIZADORA GAB</span>
                            CONDICIONES DE CARGA &copy; 2018
                        </span>

                        &nbsp; &nbsp;
                    </div>
                </div>
            </div>

        <a href="#" id="btn-scroll-up" class="btn-scroll-up btn btn-sm btn-inverse">
            <i class="ace-icon fa fa-angle-double-up icon-only bigger-110"></i>
        </a>
        </div>

		
		<script src="assets/js/jquery-2.1.4.min.js"></script>


        <script type="text/javascript">
            if ('ontouchstart' in document.documentElement) document.write("<script src='assets/js/jquery.mobile.custom.min.js'>" + "<" + "/script>");
        </script>
        <script src="assets/js/bootstrap.min.js"></script>


        <script src="assets/js/jquery-ui.custom.min.js"></script>
        <script src="assets/js/jquery.ui.touch-punch.min.js"></script>
        <script src="assets/js/chosen.jquery.min.js"></script>
        <script src="assets/js/spinbox.min.js"></script>
        <script src="assets/js/bootstrap-datepicker.min.js"></script>
        <script src="assets/js/bootstrap-timepicker.min.js"></script>
        <script src="assets/js/moment.min.js"></script>
        <script src="assets/js/daterangepicker.min.js"></script>
        <script src="assets/js/bootstrap-datetimepicker.min.js"></script>
        <script src="assets/js/bootstrap-colorpicker.min.js"></script>
        <script src="assets/js/jquery.knob.min.js"></script>
        <script src="assets/js/autosize.min.js"></script>
        <script src="assets/js/jquery.inputlimiter.min.js"></script>
        <script src="assets/js/jquery.maskedinput.min.js"></script>
        <script src="assets/js/bootstrap-tag.min.js"></script>
        <script src="assets/js/jquery.gritter.min.js"></script>


        <script src="assets/js/ace-elements.min.js"></script>
        <script src="assets/js/ace.min.js"></script>

        <script type="text/javascript">
            jQuery(function ($) {


                $('#FotoSetPointFin, #Videoryan, #FotoTemPro6, #FotoSetPointIn, #FotoNoCaja, #FotoDifusor, #FotoCajaCompleta, #FotoPiso, #FotoTemPro1, #FotoTemPro2, #FotoTemPro3, #FotoTemPro4, #FotoTemPro5, #FotoTerminoCarga, #FotoAnden, #FotoRyan').ace_file_input({
                    no_file: 'Sin Archivo Asignado',
                    btn_choose: 'Escoger',
                    btn_change: 'Cambiar',
                    droppable: false,
                    onchange: null,
                    thumbnail: false, //| true | large
                    whitelist: 'png|jpg|jpeg',
                    blacklist: 'exe|php|pdf|gif|doc'

                });


                $('#id-input-file-3').ace_file_input({
                    style: 'well',
                    btn_choose: 'Drop files here or click to choose',
                    btn_change: null,
                    no_icon: 'ace-icon fa fa-cloud-upload',
                    droppable: true,
                    thumbnail: 'small'
                    ,
                    preview_error: function (filename, error_code) {

                    }

                }).on('change', function () {

                });



                $('#id-file-format').removeAttr('checked').on('change', function () {
                    var whitelist_ext, whitelist_mime;
                    var btn_choose
                    var no_icon
                    if (this.checked) {
                        btn_choose = "Drop images here or click to choose";
                        no_icon = "ace-icon fa fa-picture-o";

                        whitelist_ext = ["jpeg", "jpg", "png", "gif", "bmp"];
                        whitelist_mime = ["image/jpg", "image/jpeg", "image/png", "image/gif", "image/bmp"];
                    }
                    else {
                        btn_choose = "Drop files here or click to choose";
                        no_icon = "ace-icon fa fa-cloud-upload";

                        whitelist_ext = null;
                        whitelist_mime = null;
                    }
                    var file_input = $('#id-input-file-3');
                    file_input
                        .ace_file_input('update_settings',
                            {
                                'btn_choose': btn_choose,
                                'no_icon': no_icon,
                                'allowExt': whitelist_ext,
                                'allowMime': whitelist_mime
                            })
                    file_input.ace_file_input('reset_input');

                    file_input
                        .off('file.error.ace')
                        .on('file.error.ace', function (e, info) {

                        });




                });

                $('#spinner1').ace_spinner({ value: 0, min: 0, max: 200, step: 10, btn_up_class: 'btn-info', btn_down_class: 'btn-info' })
                    .closest('.ace-spinner')
                    .on('changed.fu.spinbox', function () {

                    });
                $('#spinner2').ace_spinner({ value: 0, min: 0, max: 10000, step: 100, touch_spinner: true, icon_up: 'ace-icon fa fa-caret-up bigger-110', icon_down: 'ace-icon fa fa-caret-down bigger-110' });
                $('#spinner3').ace_spinner({ value: 0, min: -100, max: 100, step: 10, on_sides: true, icon_up: 'ace-icon fa fa-plus bigger-110', icon_down: 'ace-icon fa fa-minus bigger-110', btn_up_class: 'btn-success', btn_down_class: 'btn-danger' });
                $('#spinner4').ace_spinner({ value: 0, min: -100, max: 100, step: 10, on_sides: true, icon_up: 'ace-icon fa fa-plus', icon_down: 'ace-icon fa fa-minus', btn_up_class: 'btn-purple', btn_down_class: 'btn-purple' });


                $('.date-picker').datepicker({
                    autoclose: true,
                    todayHighlight: true
                })

                    .next().on(ace.click_event, function () {
                        $(this).prev().focus();
                    });


                $('.input-daterange').datepicker({ autoclose: true });



                $('input[name=date-range-picker]').daterangepicker({
                    'applyClass': 'btn-sm btn-success',
                    'cancelClass': 'btn-sm btn-default',
                    locale: {
                        applyLabel: 'Apply',
                        cancelLabel: 'Cancel',
                    }
                })
                    .prev().on(ace.click_event, function () {
                        $(this).next().focus();
                    });


                $('#timepicker1').timepicker({
                    minuteStep: 1,
                    showSeconds: true,
                    showMeridian: false,
                    disableFocus: true,
                    icons: {
                        up: 'fa fa-chevron-up',
                        down: 'fa fa-chevron-down'
                    }
                }).on('focus', function () {
                    $('#timepicker1').timepicker('showWidget');
                }).next().on(ace.click_event, function () {
                    $(this).prev().focus();
                });




                if (!ace.vars['old_ie']) $('#date-timepicker1').datetimepicker({
                    icons: {
                        time: 'fa fa-clock-o',
                        date: 'fa fa-calendar',
                        up: 'fa fa-chevron-up',
                        down: 'fa fa-chevron-down',
                        previous: 'fa fa-chevron-left',
                        next: 'fa fa-chevron-right',
                        today: 'fa fa-arrows ',
                        clear: 'fa fa-trash',
                        close: 'fa fa-times'
                    }
                }).next().on(ace.click_event, function () {
                    $(this).prev().focus();
                });


                $('#colorpicker1').colorpicker();


                $('#simple-colorpicker-1').ace_colorpicker();



                $(".knob").knob();


                var tag_input = $('#form-field-tags');
                try {
                    tag_input.tag(
                        {
                            placeholder: tag_input.attr('placeholder'),

                            source: ace.vars['US_STATES'],
                        }
                    )


                    var $tag_obj = $('#form-field-tags').data('tag');
                    $tag_obj.add('Programmatically Added');

                    var index = $tag_obj.inValues('some tag');
                    $tag_obj.remove(index);
                }
                catch (e) {

                    tag_input.after('<textarea id="' + tag_input.attr('id') + '" name="' + tag_input.attr('name') + '" rows="3">' + tag_input.val() + '</textarea>').remove();

                }



                $('#modal-form input[type=file]').ace_file_input({
                    style: 'well',
                    btn_choose: 'Drop files here or click to choose',
                    btn_change: null,
                    no_icon: 'ace-icon fa fa-cloud-upload',
                    droppable: true,
                    thumbnail: 'large'
                })


                $('#modal-form').on('shown.bs.modal', function () {
                    if (!ace.vars['touch']) {
                        $(this).find('.chosen-container').each(function () {
                            $(this).find('a:first-child').css('width', '210px');
                            $(this).find('.chosen-drop').css('width', '210px');
                            $(this).find('.chosen-search input').css('width', '200px');
                        });
                    }
                })


                $(document).one('ajaxloadstart.page', function (e) {
                    autosize.destroy('textarea[class*=autosize]')

                    $('.limiterBox,.autosizejs').remove();
                    $('.daterangepicker.dropdown-menu,.colorpicker.dropdown-menu,.bootstrap-datetimepicker-widget.dropdown-menu').remove();
                });

            });
        </script>
    </form>
</body>
</html>
