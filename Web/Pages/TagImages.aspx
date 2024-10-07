<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TagImages.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.TagImages" MasterPageFile="~/PracticaMaD.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <br />
    <br />
    <asp:Localize ID="lclContent" runat="server" meta:resourcekey="lclContent" />

    <div id="form">
        <form runat="server">
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
