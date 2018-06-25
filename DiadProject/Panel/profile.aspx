<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/Admin.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="DiadProject.Panel.profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <h4>Profil</h4>
    <ol class="breadcrumb no-bg mb-1">
        <li class="breadcrumb-item"><a href="default.aspx">Anasayfa</a></li>
        <li class="breadcrumb-item active">Profil</li>
    </ol>
    <div class="box box-block bg-white">
        <h5>Üye Ekle</h5>
		<p class="font-90 text-muted mb-1">Yönetim paneline giriş şifrenizi değiştirin.</p>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
					<label>Mevcut Şifre</label>
                  
                    <asp:TextBox ID="currentPassword" runat="server" CssClass="form-control"></asp:TextBox>

				</div>
                <div class="form-group">
					<label>Yeni Şifre</label>
                    <asp:TextBox runat="server" ID="newPassword" CssClass="form-control"></asp:TextBox>
				</div>
                <div class="form-group">
					<label>Yeni Şifre Tekrar</label>

                    <asp:TextBox ID="newPasswordAgain" runat="server" CssClass="form-control"></asp:TextBox>
            
				</div>                
                <div class="form-group">
                <asp:Literal runat="server" ID="islemsonucu"></asp:Literal>
                    </div>
                <div class="form-group">
                    <asp:Button ID="kaydet" runat="server" Text="Kaydet" CssClass="btn btn-primary w-min-sm mb-1-00 waves-effect waves-light" OnClick="kaydet_Click"/>
				</div>
            </div>
        </div>
    </div>

</asp:Content>
