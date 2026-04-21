<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeleccionarTrailer.aspx.cs" Inherits="Tickets2.SeleccionarTrailer" %>

<!DOCTYPE html>
<html lang="es">
	<head>
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
		<meta charset="utf-8" />
		<title>Registro de trailers - Revisión de Trailer</title>

		<meta name="description" content="Static &amp; Dynamic Tables" />
		<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

		<!-- bootstrap & fontawesome -->
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
        <form id="datos" runat = "server">
		<div id="navbar" class="navbar navbar-default          ace-save-state">
			<div class="navbar-container ace-save-state" id="navbar-container">
				<div class="navbar-header pull-left">
					<a href="" class="navbar-brand">
						<small>
							<img src="assets/images/avatars/logos.png"/>
						</small>
					</a>
				</div>
        	</div><!-- /.navbar-container -->
		</div>
                
				<div class="navbar-buttons navbar-header pull-right" role="navigation">
                    <img src="assets/images/avatars/logo_trailer_small.png" title="logotrailer" class="icon-animated-vertical" />
					<ul class="nav ace-nav">
                        <asp:LinkButton ID="btnSalir" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btnSalir_Click" Text="<i class='ace-icon fas fa-power-off'></i>"/>
					</ul>
				</div>
                


		<div class="main-container ace-save-state" id="main-container">
			<script type="text/javascript">
			    try { ace.settings.loadState('main-container') } catch (e) { }
			</script>

			
            
                <div class="main-content">
				<div class="main-content-inner">
					<div class="page-content">
                        <div id="container"></div>

								<div class="row">
									<div class="col-xs-12">
										<h3 class="header smaller lighter blue">Registro de Entradas de Trailer</h3>

										<div class="clearfix">
											<div class="pull-right tableTools-container"></div>
										</div>
										<div class="table-header">
											Resultados para "Todos los Trailers Entrantes"
										</div>

										<!-- div.table-responsive -->

										<!-- div.dataTables_borderWrap -->
										<div>
                                            <asp:GridView runat ="server" ID="dgtraiilers" OnRowDataBound="dgtraiilers_RowDataBound" CssClass="table table-striped table-bordered table-hover"
                                                AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowCommand ="dgtraiilers_RowCommand" >                    
                                                <Columns>
                                                    <asp:BoundField HeaderText="#" DataField="conse" ItemStyle-CssClass="ID" >
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Hora Entrada" DataField="HoraEnt" ItemStyle-CssClass="ID" >
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Placa" DataField="no_trailer" >
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderStyle-CssClass="hidden-480" ItemStyle-CssClass="hidden-480" HeaderText="Chofer" DataField="chofer" >
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderStyle-CssClass="hidden-480" ItemStyle-CssClass="hidden-480" HeaderText="Destino" DataField="destino" >
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderStyle-CssClass="hidden-480" ItemStyle-CssClass="hidden-480" HeaderText="Turno" DataField="turno" >
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderStyle-CssClass="hidden-480" ItemStyle-CssClass="hidden-480" HeaderText="Responsable" DataField="responsable" >
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Anden" DataField="anden" >
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderStyle-CssClass="hidden-480" ItemStyle-CssClass="hidden-480" HeaderText="Pedido" DataField="pdn_folio" >
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Avance" DataField="porcentaje" >
                                                    </asp:BoundField>
                                                    <asp:ButtonField HeaderText="" Text ="<i class='ace-icon fas fa-camera icon-only'></i>" ItemStyle-CssClass="btn btn-minier btn-info"  CommandName="redirect"/>
                                                    </Columns>
                                                <footerstyle BackColor="#5D7B9D" Font-Bold="True" 

                                                ForeColor="White"></footerstyle>
                                                

                                                <pagerstyle BackColor="#284775" ForeColor="White" 

                                                HorizontalAlign="Center"></pagerstyle>
                                                <rowstyle BackColor="#F7F6F3" ForeColor="#333333"></rowstyle>
                                                <selectedrowstyle BackColor="#E2DED6" Font-Bold="True" 

                                                ForeColor="#333333"></selectedrowstyle>
                                                <sortedascendingcellstyle BackColor="#E9E7E2"></sortedascendingcellstyle>
                                                <sortedascendingheaderstyle BackColor="#506C8C"></sortedascendingheaderstyle>
                                                <sorteddescendingcellstyle BackColor="#FFFDF8"></sorteddescendingcellstyle>
                                                <sorteddescendingheaderstyle BackColor="#6F8DAE"></sorteddescendingheaderstyle>
                                                
                                            </asp:GridView>
										</div>
									</div>
								</div>
								<!-- PAGE CONTENT ENDS -->
							</div><!-- /.col -->
						</div><!-- /.row -->
					</div><!-- /.page-content -->
				</div>
			</div><!-- /.main-content -->


            

			
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


		<!-- ace scripts -->
		<script src="assets/js/ace-elements.min.js"></script>
		<script src="assets/js/ace.min.js"></script>

		<!-- inline scripts related to this page -->
		<script type="text/javascript">
		    


		    jQuery(function ($) {



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

		    })

		    saludo();


		    function saludo() {
		        //initiate dataTables plugin

		        var myTable = $('#dgtraiilers')
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

	



		        $.fn.dataTable.Buttons.defaults.dom.container.className = 'dt-buttons btn-overlap btn-group btn-overlap';

		        new $.fn.dataTable.Buttons(myTable, {
		            buttons: [
                      {
                          "extend": "colvis",
                          "text": "<i class='fa fa-search bigger-110 blue'></i> <span class='hidden'>Mostrar Ocultar Columnas</span>",
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
                          "text": "<i class='fa fa-database bigger-110 orange'></i> <span class='hidden'>Exportar a CSV</span>",
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

