<%@ Page Title="" Language="C#" MasterPageFile="~/Diad.Master" AutoEventWireup="true" CodeBehind="Haberler.aspx.cs" Inherits="DiadProject.Haberler" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <div class="container-fluid">
        <div class="row belt">
            <div class="col-xs-12">
                <p class="text-center uppercase font-md no_b_margin">
                    Haberler
                </p>
            </div>
        </div>
    </div>
    <div class="container hidden-sm hidden-xs">
        <div class="row">
            <div class="col-xs-12">
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i></a></li>

                    <li class="active">Haberler</li>
                </ol>
            </div>
        </div>
    </div>
    <div id="content">
	
        <div class="container">
            <div class="row">
                <div class="col-sm-4 margin-v-40 margin-v-20-on-mobile">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default collapsable-menu">
                                <div class="panel-heading toggle">
                                    <div class="row">
                                        <div class="col-xs-10">
                                            <h3 class="panel-title">
                                                Menüler
                                            </h3>
                                        </div>
                                        <div class="col-xs-2 text-right " data-toggle="collapse" data-target="#list-collapse">
                                            <i class="fa fa-chevron-down indicator"></i>
                                        </div>
                                    </div>

                                </div>
                                <ul class="list-group collapse in" id="list-collapse">
                                    <li class="list-group-item no-b-margin collapsable-menu">
                                        <div class="row toggle" id="dropdown-detail-calisma-gruplari" data-toggle="detail-calisma-gruplari">
                                            <div class="col-xs-10">
                                                <a href="#">ACILIR MENU</a>
                                            </div>
                                            <div class="col-xs-2"><i class="fa pull-right fa-chevron-right"></i></div>
                                        </div>
                                        <div id="detail-calisma-gruplari" style="display: none;">
                                            <hr class="no-b-margin">
                                            <ul class="list-group no-b-margin">

                                                <li class="list-group-item">
                                                    <a href="#">
                                                        MENU 1
                                                    </a>
                                                </li>
                                                <li class="list-group-item">
                                                    <a href="#">
                                                        MENU2
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>


                                    </li>
                                    <li class="list-group-item no-b-margin collapsable-menu">
                                        <div class="row toggle" id="dropdown-detail-iletisim" data-toggle="detail-iletisim">
                                            <div class="col-xs-10">
                                                <a href="#">İletişim</a>
                                            </div>
                                        </div>
                                        <div id="detail-iletisim" style="display: none;">
                                            <hr class="no-b-margin">
                                            <ul class="list-group no-b-margin">
                                            </ul>
                                        </div>


                                    </li>
                                   

                                </ul>
                            </div>
                        </div>

                    </div>


                </div>

                <div class="col-sm-8 col-sm-offset bg-white margin-v-40 padding-h-20 padding-v-20 margin-v-20-on-mobile">

                    <div class="row">
                        <div class="col-sm-6">
                            <h3 class="font-lg no-t-margin"> Haberler </h3>
                        </div>
                        <div class="col-sm-6">
                            <form class="form-horizontal" method="get">
                                <div class="form-group">
                                    <div class="input-group">
                                        <input type="text" class="form-control" name="q" value="">
                                        <div class="input-group-btn">
                                            <button type="submit" class="btn btn-primary">Ara</button>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>

                    </div>

                    <hr>
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="contact-info">
                                <ul>
                                    <li class="new">
                                        <a href="haber-detay.html">
                                            <strong>1)</strong>
                                            <strong>Dışişleri Bakanı Mevlüt Çavuşoğlu'nun Kanada'yı Ziyareti</strong>
                                            <span class="text-muted">(13 Kasım 2017)</span>
                                        </a>
                                    </li>
                                    <li class="new">
                                        <a href="haber-detay.html">
                                            <strong>1)</strong>
                                            <strong>Dışişleri Bakanı Mevlüt Çavuşoğlu'nun Kanada'yı Ziyareti</strong>
                                            <span class="text-muted">(13 Kasım 2017)</span>
                                        </a>
                                    </li>

                                    <li class="new">
                                        <a href="haber-detay.html">
                                            <strong>1)</strong>
                                            <strong>Dışişleri Bakanı Mevlüt Çavuşoğlu'nun Kanada'yı Ziyareti</strong>
                                            <span class="text-muted">(13 Kasım 2017)</span>
                                        </a>
                                    </li>
                                    <li class="new">
                                        <a href="haber-detay.html">
                                            <strong>1)</strong>
                                            <strong>Dışişleri Bakanı Mevlüt Çavuşoğlu'nun Kanada'yı Ziyareti</strong>
                                            <span class="text-muted">(13 Kasım 2017)</span>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                            <ul class="pagination">

                                <li class="disabled"><span>«</span></li>





                                <li class="active"><span>1</span></li>
                                <li><a href="#">2</a></li>
                                <li><a href="#">3</a></li>
                                <li><a href="#">4</a></li>


                                <li><a href="#" rel="next">»</a></li>
                            </ul>

                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>
 



</asp:Content>
