<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/Admin.Master" AutoEventWireup="true" CodeBehind="aboutus.aspx.cs" Inherits="DiadProject.Panel.aboutus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="css/croppic.css" rel="stylesheet" />
    <script type="text/javascript" src="../vendor/jquery/jquery-1.12.3.min.js"></script>
    <script src="ckeditor/ckeditor.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div class="box box-block bg-white">
        <h5>Hakkımızda Düzenle</h5>
      
        <div class="row">
            <div class="col-md-12">
         
             
                <div class="form-group">
                    <label>* Sayfa İçeriği</label>
                    <asp:TextBox runat="server" ID="editor" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                    <script>
                        CKEDITOR.replace('ContentPlaceHolder1_editor');
                    </script>
                </div>
                <div class="form-group">
                    <asp:Button ID="Button1" runat="server" Text="Düzenle" CssClass="btn btn-primary w-min-sm mb-1-00 waves-effect waves-light" OnClick="Button1_Click"/>
                </div>
            </div>
              <div class="form-group">
                <asp:Literal runat="server" ID="Literal1"></asp:Literal>
             </div>
            






</asp:Content>
