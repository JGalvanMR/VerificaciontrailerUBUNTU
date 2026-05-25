<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaginaLogin.aspx.cs" Inherits="Tickets2.PaginaLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Revisión de trailers - Login</title>
    <meta charset="UTF-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>


    <script src="js/jquery-2.1.3.min.js"></script>

    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <link rel="stylesheet" href="assets/css/jquery.gritter.min.css" />
    <link rel="stylesheet" href="assets/css/jquery-ui.custom.min.css" />
    <script src="js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="css/bootstrap.min.css"/>
    <link rel="stylesheet" href="css/formLogin.css" />
    <script src="assets/js/jquery.gritter.min.js"></script>
    <link rel="stylesheet" href="assets/css/ace.min.css" />
    <link rel="stylesheet" href="assets/css/ace-rtl.min.css" />
    <script src="assets/js/ace-elements.min.js"></script>
	<script src="assets/js/ace.min.js"></script>
    <link rel="stylesheet" href="assets/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.1.0/css/all.css" integrity="sha384-lKuwvrZot6UHsBSfcMvOkWwlCMgc0TaWr+30HWe3a4ltaBwTZhyTEggF5tJv8tbt" crossorigin="anonymous">
    <style>

        div[class="col-xs-12 col-sm-5 col-md-5 col-lg-5"] h1{
            margin-top: 180px;
            font-size: 60px;

            word-wrap: break-word;
            -webkit-hyphens: auto;
            -moz-hyphens: auto;
            -ms-hyphens: auto;
            -o-hyphens: auto;
            hyphens: auto;
        }
    </style>
</head>
<body class="login-layout blur-login">
        		<div id="navbar" class="navbar navbar-default          ace-save-state">
			<div class="navbar-container ace-save-state" id="navbar-container">
				<div class="navbar-header pull-left">
					<a href="index.html" class="navbar-brand">
						<small>
							<img src="assets/images/avatars/logos.png" title="logo" />
						</small>
					</a>
				</div>
			</div>
		</div>
    	<div class="main-container">
			<div class="main-content">
				<div class="row">
					<div class="col-sm-10 col-sm-offset-1">
						<div class="login-container">
							<div class="center">
								<h1>
									<i class="ace-icon fa fa-truck-moving green"></i>
									<span class="redlucky">Verificación de</span>
									<span class="BLUElucky" id="id-text2">Trailers</span>
								</h1>
								<h4 class="blue" id="id-company-text">Comercializadora GAB</h4>
							</div>

							<div class="space-6"></div>

							<div class="position-relative">
								<div id="login-box" class="login-box visible widget-box no-border">
									<div class="widget-body">
										<div class="widget-main">
                                            <center>
                                                <span class="profile-picture">
								                <img id="avatar" class="img-responsive" alt="Alex's Avatar" src="assets/images/avatars/logo_trailer.png">
							                </span>
                                            </center>
											<h4 class="header blue lighter bigger center">
												Ingreso Al Sistema
											</h4>

											<div class="space-6"></div>

											<form id="fLogin" runat="server">
												<fieldset>
													<label class="block clearfix">
														<span class="block input-icon input-icon-right">
															<asp:TextBox runat="server" ID="txtUsuario" CssClass="form-control" placeholder="Usuario"></asp:TextBox>
															<i class="ace-icon fa fa-user"></i>
														</span>
													</label>

													<label class="block clearfix">
														<span class="block input-icon input-icon-right">
															<asp:TextBox runat="server" ID="txtPass" TextMode="Password" CssClass="form-control" placeholder="Password"></asp:TextBox>
															<i class="ace-icon fa fa-lock"></i>
														</span>
													</label>

                                                    <asp:DropDownList ID="cmbRol" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="emb" Text="Embarques" Selected="True">Embarques</asp:ListItem>
                                                        <asp:ListItem Value="consulta" Text="Consulta"></asp:ListItem>
                                                    </asp:DropDownList>

													<div class="space"></div>

													<div class="clearfix">
														<asp:LinkButton runat="server" ID="btnLogIn" Text="Entrar <span class='glyphicon glyphicon-log-in'></span>" CssClass="btn btn-primary btn-lg btn-block" OnClick="btnLogIn_Click"/>
													</div>

													<div class="space-4"></div>
												</fieldset>
											</form>
										</div>
									</div>
								</div>

								
							</div>

						
						</div>
					</div>
				</div>
			</div>
		</div>
</body>
</html>
