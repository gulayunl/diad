<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/Admin.Master" AutoEventWireup="true" CodeBehind="addhpslide.aspx.cs" Inherits="DiadProject.Panel.addhpslide" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="css/croppic.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <h4>Anasayfa Slide Ekle</h4>
    <ol class="breadcrumb no-bg mb-1">
        <li class="breadcrumb-item"><a href="default.aspx">Anasayfa</a></li>
        <li class="breadcrumb-item"><a href="homepageslider.aspx">Anasayfa Slider Yönetimi</a></li>
        <li class="breadcrumb-item active">Anasayfa Slide Ekle</li>
    </ol>
    <div class="box box-block bg-white">
        <h5>Anasayfa Slide Ekle</h5>
        <p class="font-90 text-muted mb-1">Anasayfadaki slider için yeni bir slide ekle.</p>
        <div class="row">
            <div class="col-md-6">
                <div class="cropHeaderWrapper" style="position: relative;">
                    <div id="croppicmid"></div>
                    <span class="btn" id="cropContainerHeaderButtonmid">* Slide Resmini Seçiniz 1000x370px</span>
                </div>
                <div class="form-group">
                    <label>Ana Slogan</label>
                    <asp:TextBox runat="server" ID="anaslogan" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Alt Slogan</label>
                    <asp:TextBox runat="server" ID="altslogan" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Buton Faiz Yazısı</label>
                    <asp:TextBox runat="server" ID="btnfaiz" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Buton Slogan</label>
                    <asp:TextBox runat="server" ID="btnslogan" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Yazı Rengi</label>
                    <asp:DropDownList runat="server" ID="yazirengi" CssClass="form-control">
                        <asp:ListItem>Siyah</asp:ListItem>
                        <asp:ListItem>Beyaz</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Yönlendirme Linki</label>
                    <asp:TextBox runat="server" ID="link" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Yönlendirme Türü</label>
                    <asp:DropDownList runat="server" ID="yonlendirmeturu" CssClass="form-control">
                        <asp:ListItem Value="_self">Aynı sayfada aç</asp:ListItem>
                        <asp:ListItem Value="_blank">Yeni sayfada aç</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group" id="islemsonucu">
                </div>
                <div class="form-group">
                    <a class="btn btn-primary w-min-sm mb-1-00 waves-effect waves-light" style="color: #fff;" onclick="addhpslide();">Kaydet</a>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="../vendor/jquery/jquery-1.12.3.min.js"></script>
    <script src="js/jquery.mousewheel.min.js"></script>
    <script src="js/croppic.min.js"></script>
    <script src="js/main.js"></script>
    <script>
        var croppicHeaderOptions = {
           
            cropUrl: 'proccess.aspx?type=hpslideimage',
            customUploadButtonId: 'cropContainerHeaderButtonmid',
            modal: true,
            rotateControls: false,
            processInline: true,
            loaderHtml: '<div class="loader bubblingG"><span id="bubblingG_1"></span><span id="bubblingG_2"></span><span id="bubblingG_3"></span></div> '
        }
        var croppic = new Croppic('croppicmid', croppicHeaderOptions);

        function addhpslide() {
            if ($('#croppicmid:has(img)').length == 0) {
                $("#islemsonucu").html("");
                $("#islemsonucu").html("<div class='alert alert-danger-fill alert-dismissible fade in mb-0' role='alert'>" +
										"<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
										"<span aria-hidden='true'>×</span>" +
										"</button>" +
										"Lütfen slide resmini seçiniz." +
									"</div>");
                return;
            }
            var formData = new FormData();
            formData.append("anaslogan", $("#ContentPlaceHolder1_anaslogan")[0].value);
            formData.append("altslogan", $("#ContentPlaceHolder1_altslogan")[0].value);
            formData.append("butonfaiz", $("#ContentPlaceHolder1_btnfaiz")[0].value);
            formData.append("butonslogan", $("#ContentPlaceHolder1_btnslogan")[0].value);
            formData.append("slideresmi", $("#croppicmid .croppedImg")[0].src);
            formData.append("yazirengi", $("#ContentPlaceHolder1_yazirengi")[0].value);
            formData.append("link", $("#ContentPlaceHolder1_link")[0].value);
            formData.append("yonlendirmeturu", $("#ContentPlaceHolder1_yonlendirmeturu")[0].value);
            var request = new XMLHttpRequest();
            request.open("POST", "proccess.aspx?type=addhpslide");
            request.onload = function () {
                if (request.readyState === request.DONE) {
                    if (request.status === 200) {
                        if (request.responseText == "true") {
                            $("#islemsonucu").html("");
                            $("#islemsonucu").html("<div class='alert alert-success-fill alert-dismissible fade in' role='alert'>" +
										"<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
										"<span aria-hidden='true'>×</span>" +
										"</button>" +
										"Slide resmi başarıyla eklenmiştir." +
									"</div>");

                            $("#ContentPlaceHolder1_anaslogan")[0].value = '';
                            $("#ContentPlaceHolder1_altslogan")[0].value = '';
                            $("#ContentPlaceHolder1_btnfaiz")[0].value = '';
                            $("#ContentPlaceHolder1_btnslogan")[0].value = '';
                            $("#ContentPlaceHolder1_link")[0].value = '';
                        }
                        else {
                            $("#islemsonucu").html("");
                            $("#islemsonucu").html("<div class='alert alert-danger-fill alert-dismissible fade in mb-0' role='alert'>" +
										"<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
										"<span aria-hidden='true'>×</span>" +
										"</button>" +
										"Slide resmi eklenirken hata oluştu. Lüften daha sonra tekrar deneyiniz." +
									"</div>");
                        }
                    }
                }
            };

            request.send(formData);
        };
    </script>

</asp:Content>
