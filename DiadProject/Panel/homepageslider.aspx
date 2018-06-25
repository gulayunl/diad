<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/Admin.Master" AutoEventWireup="true" CodeBehind="homepageslider.aspx.cs" Inherits="DiadProject.Panel.homepageslider" %>
<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <h4>Anasayfa Slider Yönetimi</h4>
    <ol class="breadcrumb no-bg mb-1">
        <li class="breadcrumb-item"><a href="default.aspx">Anasayfa</a></li>
        <li class="breadcrumb-item active">Anasayfa Slider Yönetimi</li>
    </ol>
    <div class="box box-block bg-white">
        <div class="table-responsive">
            <a href="addhpslide.aspx" style="margin-bottom:15px;" class="btn btn-primary w-min-sm mb-1-00 waves-effect waves-light">Yeni Slide Ekle</a>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>Slide Resmi</th>
                        <th>Ana  Slogan</th>
                        <th>Alt Slogan</th>
                         <th>İşlem Tarihi</th>
                        <th>İşlem Yapan Admin</th>
                        <th>İşlemler</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptmembers" OnItemCommand="rptmembers_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><img src='<%# "/" + Eval("HS_ImageUrl") %>' width="150" /></td>
                                <td><%#Eval("HS_Text1") %></td>
                                <td><%#Eval("HS_Text2") %></td>
                                <td><%#Convert.ToDateTime(Eval("HS_AddedDateTime")).ToString("dd MMMM yyyy HH:mm", System.Globalization.CultureInfo.CreateSpecificCulture("tr-TR"))%></td>
                                <td><%#Eval("usr_name") + " " + Eval("usr_surname") %></td>
                                <td>
                                    <asp:Button runat="server" ID="btnedit" CommandName="edit" CommandArgument='<%#Eval("HS_Id") %>' Text="Düzenle" CssClass="btn btn-success btn-sm w-min-sm mb-0-25 waves-effect waves-light" />
                                    <asp:Button runat="server" ID="Button1" CommandName="delete" OnClientClick="return confirm('Bu slide resmini silmek istediğinize emin misiniz?')" CommandArgument='<%#Eval("HS_Id") %>' Text="Sil" CssClass="btn btn-danger btn-sm w-min-sm mb-0-25 waves-effect waves-light" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div class="pagination">
            <cc1:collectionpager id="paginator" runat="server" backnextlocation="None" backtext="Önceki Sayfa" hideonsinglepage="True" labeltext="" nexttext="Sonraki Sayfa" querystringkey="page" resultslocation="Bottom" pagenumbersseparator="&amp;nbsp;" pagesize="10" showlabel="False" maxpages="100000" pagingmode="QueryString" resultsformat="Toplam {2} kayıt içerisinden {0}-{1} arasındakiler gösteriliyor." resultsstyle="float:left;" backnextlinkseparator="" backnextstyle="padding: 8px 12px;text-decoration: none;background-color: #fff;border: 1px solid #ddd;color: #999;" controlcssclass="pager" ignorequerystring="False" pagenumbersdisplay="Numbers" sectionpadding="0"></cc1:collectionpager>
        </div>
    </div>


</asp:Content>
