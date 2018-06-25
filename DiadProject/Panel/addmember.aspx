<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/Admin.Master" AutoEventWireup="true" CodeBehind="addmember.aspx.cs" Inherits="DiadProject.Panel.addmember" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>Üye Ekle</h4>
    <ol class="breadcrumb no-bg mb-1">
        <li class="breadcrumb-item"><a href="default.aspx">Anasayfa</a></li>
        <li class="breadcrumb-item"><a href="members.aspx">Üyeler</a></li>
        <li class="breadcrumb-item active">Üye Ekle</li>
    </ol>
    <div class="box box-block bg-white">
        <h5>Üye Ekle</h5>
		<p class="font-90 text-muted mb-1">Yönetim paneline erişebilecek yeni bir kullanıcı oluşturun.</p>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
					<label>Ad</label>
                    <asp:TextBox runat="server" ID="ad" CssClass="form-control"></asp:TextBox>
				</div>
                <div class="form-group">
					<label>Soyad</label>
                    <asp:TextBox runat="server" ID="soyad" CssClass="form-control"></asp:TextBox>
				</div>
                <div class="form-group">
					<label>Email</label>
                    <asp:TextBox runat="server" ID="email" CssClass="form-control"></asp:TextBox>
				</div>
                <div class="form-group">
					<label>Telefon</label>
                    <asp:TextBox runat="server" ID="telefon" MaxLength="11" CssClass="form-control"></asp:TextBox>
				</div>
                <div class="form-group">
					<label>Admin Rolü</label>
                    <asp:DropDownList runat="server" ID="rol" CssClass="form-control">
                        <asp:ListItem Value="Admin" Text="IT"></asp:ListItem>
                        <asp:ListItem Value="Marketing" Text="Marketing"></asp:ListItem>
                    </asp:DropDownList>
				</div>
                <div class="form-group">
                <asp:Literal runat="server" ID="islemsonucu"></asp:Literal>
                    </div>
                <div class="form-group">
                    <asp:Button runat="server" ID="kaydet" Text="Kaydet" CssClass="btn btn-primary w-min-sm mb-1-00 waves-effect waves-light" OnClick="kaydet_Click" />
				</div>
            </div>
        </div>
    </div>
</asp:Content>
