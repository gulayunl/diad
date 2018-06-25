<%@ Page Title="" Language="C#" MasterPageFile="~/Diad.Master" AutoEventWireup="true" CodeBehind="HaberDetay.aspx.cs" Inherits="DiadProject.HaberDetay" %>
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
                    <li><a href="/haber-list.html">Haberler</a></li>
                    <li class="active">Kanada Ziyareti</li>
                </ol>
            </div>
        </div>
    </div>
 

        <div id="content">
        <div class="container">
            <div class="row ">
                <div class="col-xs-12 ">
                    <div class="col-sm-8 bg-white margin-v-40 padding-h-20 padding-v-40">
                        <div class="col-sm-12 ">
                            <h3 class="font-lg no-t-margin">Dışişleri Bakanı Mevlüt Çavuşoğlu'nun Kanada'yı Ziyareti</h3>
                            <p class="text-info text-sm">
                                Pazartesi 13 Kasım 2017
                            </p>
                            <hr>
                        </div>

                        <div class="col-sm-10 col-sm-offset-1">
                            <ul id="news-slider">
                                <li data-thumb="assets/images/haber1.jpg">
                                    <img src="assets/images/haber1.jpg" class="img-responsive">
                                </li>
                                <li data-thumb="assets/images/haber2.jpg">
                                    <img src="assets/images/haber2.jpg" class="img-responsive">
                                </li>
                            </ul>
                        </div>
                        <div class="clearfix"></div>
                        <br>
                        <div class="col-sm-12 page-description">
                            <p class="text-justify">
                                Bakan Çavuşoğlu, 15-16 Ocak 2018 tarihlerinde Kanada ve ABD Dışişleri Bakanlarının ortak evsahipliğinde düzenlenen “Kore Yarımadasında Güvenlik ve İstikrar Konulu Dışişleri Bakanları Toplantısı”na katılmak üzere Kanada’nın Vancouver kentini ziyaret etti.
                            </p>
                            <p class="text-justify">
                            Bakan Çavuşoğlu Vancouver ziyareti sırasında 15 Ocak Pazartesi günü Vancouver Başkonsolosluğumuzun resmi açılışını gerçekleştirdi ve vatandaşlarımızla bir araya geldi.
                            </p>

                            <p class="text-justify">16 Ocak Salı günü, Kanadalı akademisyen ve düşünce kuruluşlarının temsilcilerinin katılımıyla British Columbia Üniversitesi Peter Wall Enstitüsü’nde düzenlenen toplantıda, ülkemizin insani ve girişimci dış politikası ele alındı.</p>
                            <p class="text-justify">Bakan Çavuşoğlu, “Kore Yarımadasında Güvenlik ve İstikrar Konulu Dışişleri Bakanları Toplantısı”na katıldı.</p>
                            <p class="text-justify">Toplantı marjında, Bakan Çavuşoğlu ve ABD Dışişleri Bakanı Rex Tillerson ikili bir görüşme gerçekleştirdi.</p>
                        </div>

                    </div>
                    <hr class="visible-sm">
                    <div class="col-sm-4 right-side">
                        <div class="col-sm-12" id="news-holder">
                            <div class="panel panel-default no-b-margin">
                                <div class="panel-heading" ><a href="#list-collapse" data-toggle="collapse" data-target="#news-collapse">Son Haberler</a>  <a class="text-info panel-link" href="haber-list.html">Tümünü Gör</a></div>
                                <div class="panel-body collapse in" id="news-collapse">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <a href="/">
                                                <div style="background-image: url(assets/images/haber1.jpg);"
                                                     class="new-holder">

                                                </div>
                                                <span class="text-info">
                    23 Ekim 2017
                    </span>
                                                <p class="title font-md no-b-margin">
                                                    Dışişleri Bakanı Mevlüt Çavuşoğlu'nun Kanada'yı Ziyareti
                                                </p>
                                            </a>
                                        </div>

                                    </div>
                                    <hr>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <a href="/">
                                                <div style="background-image: url(assets/images/haber1.jpg);"
                                                     class="new-holder">

                                                </div>
                                                <span class="text-info">
                    23 Ekim 2017
                    </span>
                                                <p class="title font-md no-b-margin">
                                                    Dışişleri Bakanı Mevlüt Çavuşoğlu'nun Kanada'yı Ziyareti
                                                </p>
                                            </a>
                                        </div>

                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>
  
       <script src="assets/vendor/jquery/jquery-3.1.1.min.js"></script>
    <script src="assets/js/lightslider.min.js"></script>
        
 <script>
    (function($){
        $(document).ready(function(){
            $('#news-slider').lightSlider({
                gallery: true,
                item: 1,
                loop: true,
                slideMargin: 0,
                thumbItem: 9
            });
        })
    })(jQuery);
</script>

<script>
    $(document).ready(function(){
        var w = $(document).width();
        if(w<768){
            $('#news-collapse').removeClass('in');
        }
    })
</script>

</asp:Content>
