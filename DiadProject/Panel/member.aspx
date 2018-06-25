<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/Admin.Master" AutoEventWireup="true" CodeBehind="member.aspx.cs" Inherits="DiadProject.Panel.member" %>
<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>Üye Yönetimi</h4>
    <ol class="breadcrumb no-bg mb-1">
        <li class="breadcrumb-item"><a href="default.aspx">Anasayfa</a></li>
        <li class="breadcrumb-item active">Üye Yönetimi</li>
    </ol>
    <div class="box box-block bg-white">
        <div class="table-responsive">
            <a href="addmember.aspx" style="margin-bottom:15px;" class="btn btn-primary w-min-sm mb-1-00 waves-effect waves-light">Yeni Üye Ekle</a>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>Ad Soyad</th>
                        <th>Email</th>
                        <th>Telefon</th>
                        <th>İşlemler</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptmembers" OnItemCommand="rptmembers_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("usr_name") + " " + Eval("usr_surname") %></td>
                                <td><%#Eval("usr_email_address") %></td>
                                <td><%#Eval("usr_phone") %></td>
                                <td>
                                    <asp:Button runat="server" ID="btnedit" CommandName="edit" CommandArgument='<%#Eval("usr_user_id") %>' Text="Düzenle" CssClass="btn btn-success btn-sm w-min-sm mb-0-25 waves-effect waves-light" />
                                    <asp:Button runat="server" ID="Button1" CommandName="delete" OnClientClick="return confirm('Bu üyeyi silmek istediğinize emin misiniz?')" CommandArgument='<%#Eval("usr_user_id") %>' Text="Sil" CssClass="btn btn-danger btn-sm w-min-sm mb-0-25 waves-effect waves-light" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div class="pagination">
            <cc1:CollectionPager ID="paginator" runat="server" BackNextLocation="None" BackText="Önceki Sayfa" HideOnSinglePage="True" LabelText="" NextText="Sonraki Sayfa" QueryStringKey="page" ResultsLocation="Bottom" PageNumbersSeparator="&amp;nbsp;" PageSize="20" ShowLabel="False" MaxPages="100000" PagingMode="QueryString" ResultsFormat="Toplam {2} kayıt içerisinden {0}-{1} arasındakiler gösteriliyor." ResultsStyle="float:left;" BackNextLinkSeparator="" BackNextStyle="padding: 8px 12px;text-decoration: none;background-color: #fff;border: 1px solid #ddd;color: #999;" ControlCssClass="pager" IgnoreQueryString="False" PageNumbersDisplay="Numbers" SectionPadding="0"></cc1:CollectionPager>
        </div>
    </div>
</asp:Content>
