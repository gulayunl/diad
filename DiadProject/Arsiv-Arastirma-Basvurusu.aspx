<%@ Page Title="" Language="C#" MasterPageFile="~/Diad.Master" AutoEventWireup="true" CodeBehind="Arsiv-Arastirma-Basvurusu.aspx.cs" Inherits="DiadProject.Arsiv_Arastirma_Basvurusu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    

    <div class="container-fluid">
        <div class="row belt">
            <div class="col-xs-12">
                <p class="text-center uppercase font-md no_b_margin">
                    Arsiv Arastirma Basvurusu
                </p>
            </div>
        </div>
    </div>
    <div class="container hidden-sm hidden-xs">
        <div class="row">
            <div class="col-xs-12">
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i></a></li>

                    <li class="active">Arsiv Arastirma Basvurusu</li>
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
                    <form>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Tarih Seciniz</label>
                                    <div class="icon-addon addon-md">
                                        <input type="text" placeholder="GG/AA/YYYY" class="form-control date" id="date">
                                        <label for="date" class="fa fa-calendar" rel="tooltip" title="date"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Saat Araligi Seciniz</label>
                                <div class="icon-addon addon-md">
                                    <input type="text" placeholder="14:00 - 15:00" class="form-control time" id="time">
                                    <label for="time" class="fa fa-clock-o" rel="tooltip" title="time"></label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Adiniz</label>
                                    <input type="text" class="form-control" name="name">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Soyadiniz</label>
                                    <input type="text" class="form-control" name="surname">
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Telefon Numaraniz</label>
                                    <input type="text" class="form-control" name="phone">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>E-posta Adresiniz</label>
                                    <input type="email" class="form-control" name="email">
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Uyrugu</label>
                                    <select name="nationality" class="form-control">
                                        <option hidden selected disabled>Seciniz</option>
                                        <option value="TR">Turkiye Cumhuriyeti</option>
                                        <option value="XO">Other</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Adres</label>
                                    <textarea name="address" class="form-control"></textarea>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Adiniz</label>
                                    <input type="text" class="form-control" name="name">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Soyadiniz</label>
                                    <input type="text" class="form-control" name="surname">
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Telefon Numaraniz</label>
                                    <input type="text" class="form-control" name="phone">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>E-posta Adresiniz</label>
                                    <input type="email" class="form-control" name="email">
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="form-group">
                                    <button class="btn btn-lg btn-primary btn-block">Kaydet ve Gonder</button>
                                </div>
                            </div>
                        </div>

                    </form>

                </div>
            </div>
        </div>

    </div>
 



</asp:Content>
