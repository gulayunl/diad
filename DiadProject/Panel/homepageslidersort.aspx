<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/Admin.Master" AutoEventWireup="true" CodeBehind="homepageslidersort.aspx.cs" Inherits="DiadProject.Panel.homepageslidersort" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #sortable {
            list-style-type: none;
            margin: 10px 0;
            padding: 0;
            width: 100%;
        }

            #sortable li {
                margin: 2px 0;
                padding: 5px;
                font-size: 13px;
                font-family: Arial, Helvetica, sans-serif;
                width: 100%;
                background: #e5e5e5;
                border-bottom: #ddd;
                padding: 10px 15px;
                cursor: pointer;
            }

                #sortable li img {
                    margin-right: 20px;
                }

        .ui-state-highlight {
            background: #ffd800 !important;
            width: 100%;
            display: block;
            z-index: 999;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>Anasayfa Slider Sıralama</h4>
    <ol class="breadcrumb no-bg mb-1">
        <li class="breadcrumb-item"><a href="default.aspx">Anasayfa</a></li>
        <li class="breadcrumb-item active">Anasayfa Slider Sıralama</li>
    </ol>
    <div class="box box-block bg-white">
        <h5>Anasayfa Slider Sıralama</h5>
        <p class="font-90 text-muted mb-1">Anasayfadaki slider görsellerinin sıralamasını belirleyin.</p>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">Mouse ile ilgili slide resmini aşağı/yukarı taşıyabilirsiniz.</div>
                <ul id="sortable">
                    <asp:Literal runat="server" ID="siralama"></asp:Literal>
                </ul>
                <div class="form-group" id="islemsonucu"></div>
                <div class="form-group">
                    <a id="editbtn" onclick="sirala();" style="cursor: pointer; color: #fff;" class="btn btn-primary w-min-sm mb-1-00 waves-effect waves-light">Sıralamayı Kaydet</a>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function sirala() {
            $("#editbtn").addClass("preloaderactive");
            var count = $("#sortable li").length;
            var idler = "";
            for (var i = 0; i < count; i++) {
                idler += $("#sortable li:nth-child(" + (i + 1) + "n)").data('id') + ",";
            }
            $.ajax({
                type: "POST",
                url: "process.aspx",
                data: "type=hpsort&siraliidler=" + idler,
                success: function (resultData) {
                    if (resultData == "true") {
                        $("#islemsonucu").html("");
                        $("#islemsonucu").html("<div class='alert alert-success-fill alert-dismissible fade in' role='alert'>" +
                                    "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                                    "<span aria-hidden='true'>×</span>" +
                                    "</button>" +
                                    "Sıralama başarıyla düzenlenmiştir." +
                                "</div>");
                        $("#editbtn").removeClass("preloaderactive");
                    }
                    else {
                        $("#islemsonucu").html("");
                        $("#islemsonucu").html("<div class='alert alert-danger-fill alert-dismissible fade in mb-0' role='alert'>" +
                                    "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                                    "<span aria-hidden='true'>×</span>" +
                                    "</button>" +
                                    "Hata oluştu. Lüften daha sonra tekrar deneyiniz." +
                                "</div>");
                    }
                }
            });
        }
    </script>
</asp:Content>
