<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminManto.aspx.cs" Inherits="Tickets2.AdminManto" %>

<!DOCTYPE html>
<html lang="es">
	<head>
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
		<meta charset="utf-8" />
		<title>Registro de trailers - Revisión de Trailer</title>

		<meta name="description" content="Static &amp; Dynamic Tables" />
		<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

		<!-- bootstrap & fontawesome -->
        <link rel="stylesheet" href="assets/css/bootstrap-datepicker3.min.css" />
		<link rel="stylesheet" href="assets/css/bootstrap-timepicker.min.css" />
        <link rel="stylesheet" href="assets/css/bootstrap-datetimepicker.min.css" />
		<link rel="stylesheet" href="assets/css/bootstrap.min.css" />
        <link rel="stylesheet" href="assets/font-awesome/4.5.0/css/font-awesome.min.css" />
		 <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.1.0/css/all.css" integrity="sha384-lKuwvrZot6UHsBSfcMvOkWwlCMgc0TaWr+30HWe3a4ltaBwTZhyTEggF5tJv8tbt" crossorigin="anonymous">

		<!-- page specific plugin styles -->

		<!-- text fonts -->
		<link rel="stylesheet" href="assets/css/fonts.googleapis.com.css" />

		<!-- ace styles -->
		<link rel="stylesheet" href="assets/css/ace.min.css" class="ace-main-stylesheet" id="main-ace-style" />

		<!--[if lte IE 9]>
			<link rel="stylesheet" href="assets/css/ace-part2.min.css" class="ace-main-stylesheet" />
		<![endif]-->
		<link rel="stylesheet" href="assets/css/ace-skins.min.css" />
		<link rel="stylesheet" href="assets/css/ace-rtl.min.css" />

		<!--[if lte IE 9]>
		  <link rel="stylesheet" href="assets/css/ace-ie.min.css" />
		<![endif]-->

		<!-- inline styles related to this page -->

		<!-- ace settings handler -->
		<script src="assets/js/ace-extra.min.js"></script>


        

		<!-- HTML5shiv and Respond.js for IE8 to support HTML5 elements and media queries -->

		<!--[if lte IE 8]>
		<script src="assets/js/html5shiv.min.js"></script>
		<script src="assets/js/respond.min.js"></script>
		<![endif]-->
	</head>

	<body class="no-skin">
        <form runat ="server">
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
                            OnClick="btnSalir_Click"/> 
                       
					</ul>
				</div>
                
			</div><!-- /.navbar-container -->
		</div>

		<div class="main-container ace-save-state" id="main-container">
			

			
            
                <div class="main-content">
				<div class="main-content-inner">
					<div class="page-content">
                        <div id="container"></div>

								<div class="row">
									<div class="col-xs-12">
										<div class="clearfix">
                                            <div class="pull-left">
                                                <div class="row">
                                                    <div class="col-sm-5">
												        <label for="form-field-8">Fecha Inicial</label>
												        <asp:TextBox runat="server" ID="fechainicial"  CssClass="form-control date-picker" data-date-format="dd/mm/yyyy" placeholder="Id del Servicio"></asp:TextBox> 
											       </div>
                                                    <div class="col-sm-5"> 
												        <label for="form-field-9">Fecha Final</label>
												        <asp:TextBox runat="server" ID="fechafinal" CssClass="form-control date-picker" data-date-format="dd/mm/yyyy" placeholder="Placas"></asp:TextBox> 
											        </div>
                                                     <div class="col-sm-2"> 
                                                         <label for="form-field-9"></label>
												         <asp:LinkButton ID="btnFiltrar" runat="server" CssClass="btn btn-info btn-block" Text="<i class='fab fa-searchengin'></i>"
                                                 OnClick="btnFiltrar_Click"/> 
											        </div>
                                                </div>
                                            </div>
											<div class="pull-right tableTools-container"></div>
										</div>
										<div class="table-header">
											Verificación de Trailer - <asp:Literal ID="Literal2" runat="server"></asp:Literal>
										</div>

										<!-- div.table-responsive -->

										<!-- div.dataTables_borderWrap -->
                                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
									</div>
								</div>
								<!-- PAGE CONTENT ENDS -->
							</div><!-- /.col -->
						</div><!-- /.row -->
					</div><!-- /.page-content -->
				</div>
			</div><!-- /.main-content -->

            <!--Modal-->
                        <div class="modal fade bs-example-modal-lg" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                            <div class="modal-dialog modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="myModalLabel">Evidencia</h4>
                                </div>
                                <div class="modal-body">

                                    <div id="carousel-example-generic" class="carousel slide" data-ride="carousel" data-interval="false">

                                        <!-- Indicators -->
                                        <ol class="carousel-indicators">
                                            <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                                            <li data-target="#carousel-example-generic" data-slide-to="1"></li>
                                            <li data-target="#carousel-example-generic" data-slide-to="2"></li>
                                            <li data-target="#carousel-example-generic" data-slide-to="3"></li>
                                            <li data-target="#carousel-example-generic" data-slide-to="4"></li>
                                            <li data-target="#carousel-example-generic" data-slide-to="5"></li>
                                            <li data-target="#carousel-example-generic" data-slide-to="6"></li>
                                            <li data-target="#carousel-example-generic" data-slide-to="7"></li>
                                            <li data-target="#carousel-example-generic" data-slide-to="8"></li>
                                            <li data-target="#carousel-example-generic" data-slide-to="9"></li>
                                            <li data-target="#carousel-example-generic" data-slide-to="10"></li>
                                            <li data-target="#carousel-example-generic" data-slide-to="11"></li>
                                            <li data-target="#carousel-example-generic" data-slide-to="12"></li>
                                            <li data-target="#carousel-example-generic" data-slide-to="13"></li>

                                        </ol>

                                      <!-- Wrapper for slides -->
                                      <div class="carousel-inner">
                                          <%
                                          for (int i = 0; i < 14; i++)
                                          {
                                              if (i == 0)
                                              {
                                                  Response.Write("<div class='item active'><center><img class='img-responsive' id='foto" + (i).ToString() + "' src=''><div id='Titulo" + (i).ToString() + "'></div></center><div class='carousel-caption'></div></div>");

                                              }else if (i == 13 && i > 0)
                                              {
                                                  Response.Write("<div class='item'><center><video width='600' height='400' id='foto"+(i).ToString()+"' type='video/mp4' controls muted loop><source src='' type='video/mp4'></video><div id='Titulo" + (i).ToString() + "'></div></center><div class='carousel-caption'></div></div>");

                                              }
                                              else {
                                                  Response.Write("<div class='item'><center><img class='img-responsive' id='foto" + (i).ToString() + "' src=''><div id='Titulo" + (i).ToString() + "'></div></center><div class='carousel-caption'></div></div>");
                                              
                                              }
                                              
                                          }
                                          %>
                                      </div>

                                      <!-- Controls -->
                                      <a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
                                        <span class="glyphicon glyphicon-chevron-left"></span>
                                      </a>
                                      <a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
                                        <span class="glyphicon glyphicon-chevron-right"></span>
                                      </a>
                                    </div>
                             
                                </div>
                                <div class="modal-footer">
                                <button type="button" class="btn btn-primary" data-dismiss="modal">Cerrar</button>
                                </div>
                            </div>
                            </div>
                        </div>
                  <!---->


            

			
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
		</div><!-- /.main-container -->

		<!-- basic scripts -->

		<!--[if !IE]> -->
		<script src="assets/js/jquery-2.1.4.min.js"></script>

		<!-- <![endif]-->

		<!--[if IE]>
<script src="assets/js/jquery-1.11.3.min.js"></script>
<![endif]-->
		<script type="text/javascript">
		    if ('ontouchstart' in document.documentElement) document.write("<script src='assets/js/jquery.mobile.custom.min.js'>" + "<" + "/script>");
		</script>
		<script src="assets/js/bootstrap.min.js"></script>

		<!-- page specific plugin scripts -->
		<script src="assets/js/jquery.dataTables.min.js"></script>
		<script src="assets/js/jquery.dataTables.bootstrap.min.js"></script>
		<script src="assets/js/dataTables.buttons.min.js"></script>
		<script src="assets/js/buttons.flash.min.js"></script>
		<script src="assets/js/buttons.html5.min.js"></script>
		<script src="assets/js/buttons.print.min.js"></script>
		<script src="assets/js/buttons.colVis.min.js"></script>
		<script src="assets/js/dataTables.select.min.js"></script>
        <script src="assets/js/jquery.easypiechart.min.js"></script>
        <link rel="stylesheet" href="assets/css/jquery.gritter.min.css" />
        <script src="assets/js/jquery.gritter.min.js"></script>
        <script src="assets/js/bootstrap-datepicker.min.js"></script>
		<script src="assets/js/bootstrap-timepicker.min.js"></script>


		<!-- ace scripts -->
		<script src="assets/js/ace-elements.min.js"></script>
		<script src="assets/js/ace.min.js"></script>

		<!-- inline scripts related to this page -->
		<script type="text/javascript">
		    $(document).on("click", "[id*=download]", function (x) {
		        var folio = x.target.id;
		        folio = folio.replace("_download", "");

		        var url = 'zip.aspx?folio=valor';
		        url = url.replace("valor", folio);
		        window.open(url, "blank");

		    });


		        $(document).on("click", "[id*=lnkView]", function (e) {
		            var folio = e.target.id;
		            folio = folio.replace("_lnkView", "");
		            var url = 'VerFoto.aspx?folio=valor';
		            url = url.replace("valor", folio);
		            location.href = url;
                    /*
		            $("#foto0").attr('src', 'FotoRevisionTrailer/Embarque_' + folio + '_FotoSetPointIn.jpg');
		            $('#Titulo0').html('<h3>Foto SetPoint Inicial</h3>');
		            $("#foto1").attr('src', 'FotoRevisionTrailer/Embarque_' + folio + '_FotoNoCaja.jpg');
		            $('#Titulo1').html('<h3>Foto Numero de Caja</h3>');
		            $("#foto2").attr('src', 'FotoRevisionTrailer/Embarque_' + folio + '_FotoDifusor.jpg');
		            $('#Titulo2').html('<h3>Foto Difusor</h3>');
		            $("#foto3").attr('src', 'FotoRevisionTrailer/Embarque_' + folio + '_FotoPiso.jpg');
		            $('#Titulo3').html('<h3>Foto Piso</h3>');
		            $("#foto4").attr('src', 'FotoRevisionTrailer/Embarque_' + folio + '_FotoCajaCompleta.jpg');
		            $('#Titulo4').html('<h3>Foto Caja Completa</h3>');
		            $("#foto5").attr('src', 'FotoRevisionTrailer/Embarque_' + folio + '_FotoTemPro1.jpg');
		            $('#Titulo5').html('<h3>Foto Temperatura del Producto (1)*</h3>');
		            $("#foto6").attr('src', 'FotoRevisionTrailer/Embarque_' + folio + '_FotoTemPro2.jpg');
		            $('#Titulo6').html('<h3>Foto Temperatura del Producto (2)*</h3>');
		            $("#foto7").attr('src', 'FotoRevisionTrailer/Embarque_' + folio + '_FotoTemPro3.jpg');
		            $('#Titulo7').html('<h3>Foto Temperatura del Producto (3)*</h3>');
		            $("#foto8").attr('src', 'FotoRevisionTrailer/Embarque_' + folio + '_FotoTemPro4.jpg');
		            $('#Titulo8').html('<h3>Foto Temperatura del Producto (4)</h3>');
		            $("#foto9").attr('src', 'FotoRevisionTrailer/Embarque_' + folio + '_FotoTemPro5.jpg');
		            $('#Titulo9').html('<h3>Foto Temperatura del Producto (5)</h3>');
		            $("#foto10").attr('src', 'FotoRevisionTrailer/Embarque_' + folio + '_FotoTemPro6.jpg');
		            $('#Titulo10').html('<h3>Foto Temperatura del Producto (6)</h3>');
		            $("#foto11").attr('src', 'FotoRevisionTrailer/Embarque_' + folio + '_FotoSetPointFin.jpg');
		            $('#Titulo11').html('<h3>Foto SetPoint Final</h3>');
		            $("#foto12").attr('src', 'FotoRevisionTrailer/Embarque_' + folio + '_FotoTerminoCarga.jpg');
		            $('#Titulo12').html('<h3>Foto Termino de Carga</h3>');
		            $("#foto13").attr('src', 'FotoRevisionTrailer/Converted/Embarque_' + folio + '_Videoryan.mp4');
		            $('#Titulo13').html('<h3>Video Encendido RYAN</h3>');

		            var img0 = document.getElementById('foto0');
		            img0.onerror = cargarImagenPorDefecto;
		            var img1 = document.getElementById('foto1');
		            img1.onerror = cargarImagenPorDefecto;
		            var img2 = document.getElementById('foto2');
		            img2.onerror = cargarImagenPorDefecto;
		            var img3 = document.getElementById('foto3');
		            img3.onerror = cargarImagenPorDefecto;
		            var img4 = document.getElementById('foto4');
		            img4.onerror = cargarImagenPorDefecto;
		            var img5 = document.getElementById('foto5');
		            img5.onerror = cargarImagenPorDefecto;
		            var img6 = document.getElementById('foto6');
		            img6.onerror = cargarImagenPorDefecto;
		            var img7 = document.getElementById('foto7');
		            img7.onerror = cargarImagenPorDefecto;
		            var img8 = document.getElementById('foto8');
		            img8.onerror = cargarImagenPorDefecto;
		            var img9 = document.getElementById('foto9');
		            img9.onerror = cargarImagenPorDefecto;
		            var img10 = document.getElementById('foto10');
		            img10.onerror = cargarImagenPorDefecto;
		            var img11 = document.getElementById('foto11');
		            img11.onerror = cargarImagenPorDefecto;
		            var img12 = document.getElementById('foto12');
		            img12.onerror = cargarImagenPorDefecto;*/


		        });

		        function cargarImagenPorDefecto(e) {
		            e.target.src = 'FotoRevisionTrailer/nodisponible.png';
		        }

		        //initiate dataTables plugin



		        $('.easy-pie-chart.percentage').each(function () {
		            var $box = $(this).closest('.infobox');
		            var barColor = $(this).data('color') || (!$box.hasClass('infobox-dark') ? $box.css('color') : 'rgba(255,255,255,0.95)');
		            var trackColor = barColor == 'rgba(255,255,255,0.95)' ? 'rgba(255,255,255,0.25)' : '#E2E2E2';
		            var size = parseInt($(this).data('size')) || 30;
		            $(this).easyPieChart({
		                barColor: barColor,
		                trackColor: trackColor,
		                scaleColor: false,
		                lineCap: 'butt',
		                lineWidth: parseInt(size / 10),
		                animate: ace.vars['old_ie'] ? false : 1000,
		                size: size
		            });
		        })

		        $('.sparkline').each(function () {
		            var $box = $(this).closest('.infobox');
		            var barColor = !$box.hasClass('infobox-dark') ? $box.css('color') : '#FFF';
		            $(this).sparkline('html',
									 {
									     tagValuesAttribute: 'data-values',
									     type: 'bar',
									     barColor: barColor,
									     chartRangeMin: $(this).data('min') || 0
									 });
		        });


		        saludo();
                

		        function saludo() {
		            //initiate dataTables plugin

		            var myTable = $('#dynamic-table')
                    .wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
                    .DataTable({
                        bAutoWidth: false,
                        "scrollY": "500px",
                        "scrollCollapse": true,
                        "scrollX": true,
                        language: {
                            "decimal": "",
                            "emptyTable": "No hay información",
                            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
                            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
                            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
                            "infoPostFix": "",
                            "thousands": ",",
                            "lengthMenu": "Mostrar _MENU_ Entradas",
                            "loadingRecords": "Cargando...",
                            "processing": "Procesando...",
                            "search": "Buscar:",
                            "zeroRecords": "Sin resultados encontrados",
                            "paginate": {
                                "first": "Primero",
                                "last": "Ultimo",
                                "next": "Siguiente",
                                "previous": "Anterior"
                            }
                        }
                    });

		            $('.date-picker').datepicker({
		                autoclose: true,
		                todayHighlight: true
		            })
				    //show datepicker when clicking on the icon
				    .next().on(ace.click_event, function () {
				        $(this).prev().focus();
				    });



		            $.fn.dataTable.Buttons.defaults.dom.container.className = 'dt-buttons btn-overlap btn-group btn-overlap';

		            new $.fn.dataTable.Buttons(myTable, {
		                buttons: [
                          {
                              "extend": "colvis",
                              "text": "<i class='fas fa-columns bigger-110 blue'></i> <span class='hidden'>Mostrar Ocultar Columnas</span>",
                              "className": "btn btn-white btn-primary btn-bold",
                              columns: ':not(:first):not(:last)'
                          },
                          {
                              "extend": "copy",
                              "text": "<i class='fa fa-copy bigger-110 pink'></i> <span class='hidden'>Copiar</span>",
                              "className": "btn btn-white btn-primary btn-bold"
                          },
                          {
                              "extend": "csv",
                              "text": "<i class='fas fa-file-export bigger-110 green'></i> <span class='hidden'>Exportar a CSV</span>",
                              "className": "btn btn-white btn-primary btn-bold"
                          },
                          {
                              "extend": "excel",
                              "text": "<i class='fa fa-file-excel-o bigger-110 green'></i> <span class='hidden'>Exportar a Excel</span>",
                              "className": "btn btn-white btn-primary btn-bold"
                          },
                          {
                              "extend": "pdf",
                              "text": "<i class='fa fa-file-pdf-o bigger-110 red'></i> <span class='hidden'>Exportar a PDF</span>",
                              "className": "btn btn-white btn-primary btn-bold"
                          },
                          {
                              "extend": "print",
                              "text": "<i class='fa fa-print bigger-110 grey'></i> <span class='hidden'>Imprimir</span>",
                              "className": "btn btn-white btn-primary btn-bold",
                              autoPrint: false,
                              message: 'This print was produced using the Print button for DataTables'
                          }
		                ]
		            });
		            myTable.buttons().container().appendTo($('.tableTools-container'));

		            //style the message box
		            var defaultCopyAction = myTable.button(1).action();
		            myTable.button(1).action(function (e, dt, button, config) {
		                defaultCopyAction(e, dt, button, config);
		                $('.dt-button-info').addClass('gritter-item-wrapper gritter-info gritter-center white');
		            });


		            var defaultColvisAction = myTable.button(0).action();
		            myTable.button(0).action(function (e, dt, button, config) {

		                defaultColvisAction(e, dt, button, config);


		                if ($('.dt-button-collection > .dropdown-menu').length == 0) {
		                    $('.dt-button-collection')
                            .wrapInner('<ul class="dropdown-menu dropdown-light dropdown-caret dropdown-caret" />')
                            .find('a').attr('href', '#').wrap("<li />")
		                }
		                $('.dt-button-collection').appendTo('.tableTools-container .dt-buttons')
		            });

		            ////

		            setTimeout(function () {
		                $($('.tableTools-container')).find('a.dt-button').each(function () {
		                    var div = $(this).find(' > div').first();
		                    if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
		                    else $(this).tooltip({ container: 'body', title: $(this).text() });
		                });
		            }, 500);





		            myTable.on('select', function (e, dt, type, index) {
		                if (type === 'row') {
		                    $(myTable.row(index).node()).find('input:checkbox').prop('checked', true);
		                }
		            });
		            myTable.on('deselect', function (e, dt, type, index) {
		                if (type === 'row') {
		                    $(myTable.row(index).node()).find('input:checkbox').prop('checked', false);
		                }
		            });




		            /////////////////////////////////
		            //table checkboxes
		            $('th input[type=checkbox], td input[type=checkbox]').prop('checked', false);

		            //select/deselect all rows according to table header checkbox
		            $('#dynamic-table > thead > tr > th input[type=checkbox], #dynamic-table_wrapper input[type=checkbox]').eq(0).on('click', function () {
		                var th_checked = this.checked;//checkbox inside "TH" table header

		                $('#dynamic-table').find('tbody > tr').each(function () {
		                    var row = this;
		                    if (th_checked) myTable.row(row).select();
		                    else myTable.row(row).deselect();
		                });
		            });

		            //select/deselect a row when the checkbox is checked/unchecked
		            $('#dynamic-table').on('click', 'td input[type=checkbox]', function () {
		                var row = $(this).closest('tr').get(0);
		                if (this.checked) myTable.row(row).deselect();
		                else myTable.row(row).select();
		            });



		            $(document).on('click', '#dynamic-table .dropdown-toggle', function (e) {
		                e.stopImmediatePropagation();
		                e.stopPropagation();
		                e.preventDefault();
		            });



		            //And for the first simple table, which doesn't have TableTools or dataTables
		            //select/deselect all rows according to table header checkbox
		            var active_class = 'active';
		            $('#simple-table > thead > tr > th input[type=checkbox]').eq(0).on('click', function () {
		                var th_checked = this.checked;//checkbox inside "TH" table header

		                $(this).closest('table').find('tbody > tr').each(function () {
		                    var row = this;
		                    if (th_checked) $(row).addClass(active_class).find('input[type=checkbox]').eq(0).prop('checked', true);
		                    else $(row).removeClass(active_class).find('input[type=checkbox]').eq(0).prop('checked', false);
		                });
		            });

		            //select/deselect a row when the checkbox is checked/unchecked
		            $('#simple-table').on('click', 'td input[type=checkbox]', function () {
		                var $row = $(this).closest('tr');
		                if ($row.is('.detail-row ')) return;
		                if (this.checked) $row.addClass(active_class);
		                else $row.removeClass(active_class);
		            });



		            /********************************/
		            //add tooltip for small view action buttons in dropdown menu
		            $('[data-rel="tooltip"]').tooltip({ placement: tooltip_placement });

		            //tooltip placement on right or left
		            function tooltip_placement(context, source) {
		                var $source = $(source);
		                var $parent = $source.closest('table')
		                var off1 = $parent.offset();
		                var w1 = $parent.width();

		                var off2 = $source.offset();
		                //var w2 = $source.width();

		                if (parseInt(off2.left) < parseInt(off1.left) + parseInt(w1 / 2)) return 'right';
		                return 'left';
		            }




		            /***************/
		            $('.show-details-btn').on('click', function (e) {
		                e.preventDefault();
		                $(this).closest('tr').next().toggleClass('open');
		                $(this).find(ace.vars['.icon']).toggleClass('fa-angle-double-down').toggleClass('fa-angle-double-up');
		            });
		            /***************/





		            /**
                    //add horizontal scrollbars to a simple table
                    $('#simple-table').css({'width':'2000px', 'max-width': 'none'}).wrap('<div style="width: 1000px;" />').parent().ace_scroll(
                      {
                        horizontal: true,
                        styleClass: 'scroll-top scroll-dark scroll-visible',//show the scrollbars on top(default is bottom)
                        size: 2000,
                        mouseWheelLock: true
                      }
                    ).css('padding-top', '12px');
                    */


		        }
		</script>
      </form>
	</body>
</html>

