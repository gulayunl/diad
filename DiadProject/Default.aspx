<%@ Page Title="" Language="C#" MasterPageFile="~/Diad.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DiadProject.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


   

    <div id="content">
        <div class="container-fluid bg-dark-grey">
            <div class="container">
                <div class="home-sec-1">
                    <div class="row">
                        <div class="col-md-8">
                            <div class="tabs-container">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="active"><a href="#haberler" data-toggle="tab">Haberler</a></li>
                                    <li><a href="#haberler" data-toggle="tab">Online Sergi</a></li>
                                    <li><a href="#haberler" data-toggle="tab">Anlaşmalar</a> </li>
                                    <li><a href="#haberler" data-toggle="tab">Araştırma Konuları</a> </li>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="haberler">
                                        <div id="haber-slide" class="carousel slide" data-ride="carousel">

                                            <!-- Wrapper for slides -->
                                            <div class="carousel-inner" role="listbox">
                                                <div class="item active">
                                                    <img src="assets/images/banner.jpg" alt="...">
                                                    <div class="banner-caption">
                                                        Dışişleri Bakanı Mevlüt Çavuşoğlu'nun Kanada'yı Ziyareti
                                                    </div>
                                                </div>
                                                <div class="item">
                                                    <img src="assets/images/baskomutan.jpg" alt="...">
                                                    <div class="banner-caption">
                                                        Dışişleri Bakanı Mevlüt Çavuşoğlu'nun Kanada'yı Ziyareti
                                                    </div>
                                                </div>
                                            </div>

                                            <!-- Controls -->
                                            <a class="left carousel-control" href="#haber-slide" role="button" data-slide="prev">
                                                <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                                                <span class="sr-only">Geri</span>
                                            </a>
                                            <a class="right carousel-control" href="#haber-slide" role="button" data-slide="next">
                                                <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                                                <span class="sr-only">İleri</span>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div><!-- .tab-content -->
                        </div><!-- .tabs-container -->
                        <div class="col-md-4">
                            <div class="row">
                                <div class="col-sm-6 col-md-12">
                                    <div class="bkk-block dark-block">
                                        <img src="assets/images/baskomutan.jpg" class="img-responsive">
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-12">
                                    <div class="bkk-block mid-dark-block">
                                        <div class="media baskan">
                                            <div class="media-left">
                                                <img src="assets/images/baskan.png" class="media-object">
                                            </div>
                                            <div class="media-body">
                                                <span>T.C. DIŞİŞLERİ BAKANI</span>
                                                <p><strong>MEVLÜT ÇAVUŞOĞLU</strong></p>
                                                <span><i>Sayın Bakanımızdan</i></span>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div class="col-sm-6 col-md-12">
                                    <div class="bkk-block light-block basvuru">
                                        <p>Arsiv Arastirma Basvurusu</p>
                                        <i class="fa fa-5x fa-clock-o"></i>
                                    </div>
                                </div>
                            </div>

                        </div><!-- .col -->
                    </div><!-- .row -->
                </div><!-- .home-sec-1 -->
            </div>

        </div>


        <div class="home-sec-2">
            <div class="container">
                <div class="row">
                    <div class="col-sm-4">
                        <div class="media">
                            <div class="media-left">
                                <i class="fa fa-film"></i>
                            </div>
                            <div class="media-body">
                                <h4 class="media-heading">TDA Tanıtım Filmi</h4>
                                <p>Türk Diplomatik Arşivi tanıtım filmini izleyebilirsiniz.</p>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="media">
                            <div class="media-left">
                                <i class="fa fa-external-link-square"></i>
                            </div>
                            <div class="media-body">
                                <h4 class="media-heading">Faydalı Linkler</h4>
                                <p>Arşivlerle ilgili diğer web sitelerine
                                    buradan ulaşabilirsiniz. </p>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="media">
                            <div class="media-left">
                                <i class="fa fa-book"></i>
                            </div>
                            <div class="media-body">
                                <h4 class="media-heading">Okuma Malzemeleri</h4>
                                <p>Diplomatlarımızın yazdığı ve Bakanlığımıza ait okuma malzemelerine ulaşabilirsiniz.  </p>
                            </div>
                        </div>
                    </div>
                </div><!-- .row -->
            </div><!-- .container -->
        </div><!-- .home-sec-2 -->
    </div>

    

 
</asp:Content>
