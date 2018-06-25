<%@ Page Title="" Language="C#" MasterPageFile="~/Diad.Master" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="DiadProject.error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="/assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/assets/css/animate.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="/assets/css/mbfh.css?v3.2" rel="stylesheet" />
    <link href="/assets/css/mbfh-mobile.css?v3.0" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <br /><br />
    <div class="container-fluid">
        <div class="container white" style="min-height:500px;">
            <div class="col-md-12">
               <div style="width:100%;padding:50px 0 10px 0;text-align:center;font-size:20px;font-weight:bold;font-family: 'CorporateAConOM', sans-serif;"><asp:Literal runat="server" ID="code"></asp:Literal></div>
               <div style="width: 100%;padding: 10px 0;text-align: center;font-size: 14px;font-family: Arial, helvetica, sans-serif;">Aramış olduğunuz sayfaya şu anda ulaşılamamaktadır.</div>
               <div style="width: 100%;padding: 10px 0;text-align: center;font-size: 14px;font-family: Arial, helvetica, sans-serif;">Lütfen daha sonra tekrar deneyiniz.</div>
               <div style="width: 100%;padding: 10px 0;text-align: center;font-size: 14px;font-family: Arial, helvetica, sans-serif;"><a href="/">Anasayfa</a></div>
            </div>
        </div>
    </div>
    <!--scripts-->
    <script type="text/javascript" src="/assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="/assets/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="/assets/js/jquery.bxslider.min.js"></script>
    <script type="text/javascript" src="/assets/js/mbfh.js"></script>
  

</asp:Content>
