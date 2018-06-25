<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/Admin.Master" AutoEventWireup="true" CodeBehind="userlogs.aspx.cs" Inherits="DiadProject.Panel.userlogs" %>
<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>Yönetim Paneli Logları</h4>
    <ol class="breadcrumb no-bg mb-1">
        <li class="breadcrumb-item"><a href="default.aspx">Anasayfa</a></li>
        <li class="breadcrumb-item active">Yönetim Paneli Logları</li>
    </ol>
    <div class="box box-block bg-white">
        <div class="table-responsive">
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>İşlem Tipi</th>
                        <th>İşlem</th>
                        <th>İşlemi Yapan Kullanıcı</th>
                        <th>İşlem Tarihi</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptmembers">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("usa_action_type") %></td>
                                <td><%#Eval("usa_page_type") %></td>
                                <td><%#Eval("usr_name") + " " + Eval("usr_surname") + " / " + Eval("usr_email_address") %></td>
                                <td><%#Convert.ToDateTime(Eval("usa_operation_date")).ToString("dd MMMM yyyy HH:mm", System.Globalization.CultureInfo.CreateSpecificCulture("tr-TR")) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div class="pagination">
            <cc1:CollectionPager ID="paginator" runat="server" BackNextLocation="None" BackText="Önceki Sayfa" HideOnSinglePage="True" LabelText="" NextText="Sonraki Sayfa" QueryStringKey="page" ResultsLocation="Bottom" PageNumbersSeparator="&amp;nbsp;" PageSize="50" ShowLabel="False" MaxPages="100000" PagingMode="QueryString" ResultsFormat="Toplam {2} kayıt içerisinden {0}-{1} arasındakiler gösteriliyor." ResultsStyle="float:left;" BackNextLinkSeparator="" BackNextStyle="padding: 8px 12px;text-decoration: none;background-color: #fff;border: 1px solid #ddd;color: #999;" ControlCssClass="pager" IgnoreQueryString="False" PageNumbersDisplay="Numbers" SectionPadding="0"></cc1:CollectionPager>
        </div>
    </div>
</asp:Content>

