<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerFoto.aspx.cs" Inherits="Tickets2.VerFoto" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />

    <title>Galería - Revisión de Trailer</title>

    <meta name="description" content="Galería de fotos de revisión de trailers" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.1.0/css/all.css" crossorigin="anonymous" />

    <link rel="stylesheet" href="assets/css/fonts.googleapis.com.css" />
    <link rel="stylesheet" href="assets/css/ace.min.css" class="ace-main-stylesheet" />
    <link rel="stylesheet" href="assets/css/ace-skins.min.css" />
    <link rel="stylesheet" href="assets/css/ace-rtl.min.css" />

    <link rel="stylesheet" href="css/lightgallery.css" />

    <script src="assets/js/ace-extra.min.js"></script>

    <style type="text/css">
        body {
            background-color: #152836;
        }

        .demo-gallery {
            padding-top: 10px;
        }

            .demo-gallery > ul {
                margin-bottom: 0;
            }

                .demo-gallery > ul > li {
                    margin-bottom: 20px;
                }

                    .demo-gallery > ul > li a {
                        border: 3px solid #ED174F;
                        border-radius: 5px;
                        display: block;
                        overflow: hidden;
                        position: relative;
                        background-color: #000;
                    }

                        .demo-gallery > ul > li a > img {
                            width: 100%;
                            height: 220px;
                            object-fit: cover;
                            transition: transform 0.3s ease;
                        }

                        .demo-gallery > ul > li a:hover > img {
                            transform: scale(1.05);
                        }

        .demo-gallery-poster {
            background-color: rgba(0,0,0,0.4);
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            transition: background-color 0.3s ease;
        }

            .demo-gallery-poster img {
                position: absolute;
                top: 50%;
                left: 50%;
                width: 48px;
                height: 48px;
                margin-left: -24px;
                margin-top: -24px;
                opacity: 0.9;
            }

        .demo-gallery > ul > li a:hover .demo-gallery-poster {
            background-color: rgba(0,0,0,0.6);
        }

        .titulo-galeria {
            color: white;
            text-align: center;
            margin-top: 10px;
            font-size: 14px;
            font-weight: bold;
        }

        @media(max-width:768px) {
            .demo-gallery > ul > li a > img {
                height: 180px;
            }
        }
    </style>
</head>

<body class="no-skin home">

    <form id="form1" runat="server">

        <div id="navbar" class="navbar navbar-default ace-save-state">
            <div class="navbar-container ace-save-state" id="navbar-container">

                <div class="navbar-header pull-left">
                    <a href="#" class="navbar-brand">
                        <small>
                            <img src="assets/images/avatars/logos.png" alt="Logo" />
                        </small>
                    </a>
                </div>

                <div class="navbar-buttons navbar-header pull-right" role="navigation">
                    <ul class="nav ace-nav">

                        <img src="assets/images/avatars/logo_trailer_small.png"
                            alt="Trailer"
                            title="Trailer"
                            class="icon-animated-vertical" />

                        <asp:LinkButton ID="btnSalir"
                            runat="server"
                            CssClass="btn btn-danger btn-sm"
                            Text="<i class='ace-icon fas fa-power-off icon-only'></i>"
                            OnClick="btnSalir_Click" />

                    </ul>
                </div>
            </div>
        </div>

        <div class="main-container ace-save-state" id="main-container">

            <div class="main-content">

                <div class="main-content-inner">

                    <div class="page-content">

                        <div class="page-header">
                            <h1>Galería de Fotos
                                <small>
                                    <i class="ace-icon fa fa-angle-double-right"></i>
                                    Fotos realizadas a los trailers
                                </small>
                            </h1>
                        </div>

                        <div class="row">

                            <div class="col-xs-12">

                                <div class="demo-gallery">

                                    <ul id="lightgallery" class="list-unstyled row">
                                        <asp:Literal ID="LiteralFoto" runat="server"></asp:Literal>
                                    </ul>

                                </div>

                                <asp:Literal ID="Literalvideo" runat="server"></asp:Literal>

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

                    </div>
                </div>
            </div>

            <a href="#" id="btn-scroll-up" class="btn-scroll-up btn btn-sm btn-inverse">
                <i class="ace-icon fa fa-angle-double-up icon-only bigger-110"></i>
            </a>

        </div>

        <script src="assets/js/jquery-2.1.4.min.js"></script>

        <script type="text/javascript">
            if ('ontouchstart' in document.documentElement) {
                document.write("<script src='assets/js/jquery.mobile.custom.min.js'><\/script>");
            }
        </script>

        <script src="assets/js/bootstrap.min.js"></script>

        <script src="assets/js/ace-elements.min.js"></script>
        <script src="assets/js/ace.min.js"></script>

        <script src="js/lightgallery-all.js"></script>
        <script src="js/jquery.mousewheel.min.js"></script>

        <script type="text/javascript">

            $(document).ready(function () {

                $('#lightgallery').lightGallery({
                    thumbnail: true,
                    selector: 'li'
                });

            });

        </script>

    </form>

</body>
</html>
