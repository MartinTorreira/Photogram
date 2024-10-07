<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.MainPage" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <br />
    <br />
    <asp:Localize ID="lclContent" runat="server" meta:resourcekey="lclContent" />

    <div id="form">
        <form runat="server">
            <div class="button">
                <asp:Button ID="btnMyProfile" runat="server" OnClick="BtnMyProfile_Click" meta:resourcekey="btnMyProfile" />
            </div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Table id="Table1" 
                GridLines="Both" 
                HorizontalAlign="Center" 
                Font-Names="Verdana" 
                Font-Size="8pt" 
                CellPadding="15" 
                CellSpacing="0" 
                Runat="server"/>

            <div class="button">

                <asp:Button ID="btnNext" runat="server" meta:resourcekey="btnNext" OnClick="Next" />

            </div>
        </form>
    </div>
</asp:Content>

    