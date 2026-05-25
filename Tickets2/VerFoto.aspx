<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerFoto.aspx.cs" Inherits="Tickets2.VerFoto" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title>Galeria - Revisión de Trailer</title>

    <meta name="description" content="responsive photo gallery using colorbox" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />


    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.1.0/css/all.css" integrity="sha384-lKuwvrZot6UHsBSfcMvOkWwlCMgc0TaWr+30HWe3a4ltaBwTZhyTEggF5tJv8tbt" crossorigin="anonymous">


    <link rel="stylesheet" href="assets/css/colorbox.min.css" />

    <link rel="stylesheet" href="assets/css/fonts.googleapis.com.css" />

    <link rel="stylesheet" href="assets/css/ace.min.css" class="ace-main-stylesheet" id="main-ace-style" />


    <link rel="stylesheet" href="assets/css/ace-skins.min.css" />
    <link rel="stylesheet" href="assets/css/ace-rtl.min.css" />
    <link rel="stylesheet" href="css/lightgallery.css" />


    <script src="assets/js/ace-extra.min.js"></script>
    <style type="text/css">
        body {
            background-color: #152836
        }

        .demo-gallery > ul {
            margin-bottom: 0;
        }

            .demo-gallery > ul > li {
                float: left;
                margin-bottom: 15px;
                margin-right: 20px;
                width: 200px;
            }

                .demo-gallery > ul > li a {
                    border: 3px solid #ED174F;
                    border-radius: 3px;
                    display: block;
                    overflow: hidden;
                    position: relative;
                    float: left;
                }

                    .demo-gallery > ul > li a > img {
                        -webkit-transition: -webkit-transform 0.15s ease 0s;
                        -moz-transition: -moz-transform 0.15s ease 0s;
                        -o-transition: -o-transform 0.15s ease 0s;
                        transition: transform 0.15s ease 0s;
                        -webkit-transform: scale3d(1, 1, 1);
                        transform: scale3d(1, 1, 1);
                        height: 100%;
                        width: 100%;
                    }

                    .demo-gallery > ul > li a:hover > img {
                        -webkit-transform: scale3d(1.1, 1.1, 1.1);
                        transform: scale3d(1.1, 1.1, 1.1);
                    }

                    .demo-gallery > ul > li a:hover .demo-gallery-poster > img {
                        opacity: 1;
                    }

                    .demo-gallery > ul > li a .demo-gallery-poster {
                        background-color: rgba(0, 0, 0, 0.1);
                        bottom: 0;
                        left: 0;
                        position: absolute;
                        right: 0;
                        top: 0;
                        -webkit-transition: background-color 0.15s ease 0s;
                        -o-transition: background-color 0.15s ease 0s;
                        transition: background-color 0.15s ease 0s;
                    }

                        .demo-gallery > ul > li a .demo-gallery-poster > img {
                            left: 50%;
                            margin-left: -10px;
                            margin-top: -10px;
                            opacity: 0;
                            position: absolute;
                            top: 50%;
                            -webkit-transition: opacity 0.3s ease 0s;
                            -o-transition: opacity 0.3s ease 0s;
                            transition: opacity 0.3s ease 0s;
                        }

                    .demo-gallery > ul > li a:hover .demo-gallery-poster {
                        background-color: rgba(0, 0, 0, 0.5);
                    }

        .demo-gallery .justified-gallery > a > img {
            -webkit-transition: -webkit-transform 0.15s ease 0s;
            -moz-transition: -moz-transform 0.15s ease 0s;
            -o-transition: -o-transform 0.15s ease 0s;
            transition: transform 0.15s ease 0s;
            -webkit-transform: scale3d(1, 1, 1);
            transform: scale3d(1, 1, 1);
            height: 100%;
            width: 100%;
        }

        .demo-gallery .justified-gallery > a:hover > img {
            -webkit-transform: scale3d(1.1, 1.1, 1.1);
            transform: scale3d(1.1, 1.1, 1.1);
        }

        .demo-gallery .justified-gallery > a:hover .demo-gallery-poster > img {
            opacity: 1;
        }

        .demo-gallery .justified-gallery > a .demo-gallery-poster {
            background-color: rgba(0, 0, 0, 0.1);
            bottom: 0;
            left: 0;
            position: absolute;
            right: 0;
            top: 0;
            -webkit-transition: background-color 0.15s ease 0s;
            -o-transition: background-color 0.15s ease 0s;
            transition: background-color 0.15s ease 0s;
        }

            .demo-gallery .justified-gallery > a .demo-gallery-poster > img {
                left: 50%;
                margin-left: -10px;
                margin-top: -10px;
                opacity: 0;
                position: absolute;
                top: 50%;
                -webkit-transition: opacity 0.3s ease 0s;
                -o-transition: opacity 0.3s ease 0s;
                transition: opacity 0.3s ease 0s;
            }

        .demo-gallery .justified-gallery > a:hover .demo-gallery-poster {
            background-color: rgba(0, 0, 0, 0.5);
        }

        .demo-gallery .video .demo-gallery-poster img {
            height: 48px;
            margin-left: -24px;
            margin-top: -24px;
            opacity: 0.8;
            width: 48px;
        }

        .demo-gallery.dark > ul > li a {
            border: 3px solid #04070a;
        }

        .home .demo-gallery {
            padding-bottom: 80px;
        }
    </style>


</head>

<body class="no-skin home">
    <form runat="server">
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
            <div class="main-content">
                <div class="main-content-inner">
                    <div class="page-content">
                        <div class="page-header">
                            <h1>Galeria de Fotos
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


        <script src="assets/js/jquery.colorbox.min.js"></script>


        <script src="assets/js/ace-elements.min.js"></script>
        <script src="assets/js/ace.min.js"></script>
        <script src="https://cdn.jsdelivr.net/picturefill/2.3.1/picturefill.min.js"></script>
        <script src="js/lightgallery-all.js"></script>
        <script src="js/jquery.mousewheel.min.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $('#lightgallery').lightGallery();
            });
        </script>

    </form>
</body>
</html>
