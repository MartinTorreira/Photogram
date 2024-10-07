<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindImage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.FindImage"
    MasterPageFile="~/PracticaMaD.Master" meta:resourcekey="Page" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server"  meta:resourcekey="lclMenuExplanation">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <h1><asp:Localize ID="lclTitle" runat="server" meta:resourcekey="lclTitle" /></h1>
    <div id="form">
        <form id="FindForm" runat="server">
            <div class="field">
                <span class="label">    
                    <asp:Localize ID="lclFindImage" runat="server" meta:resourcekey="lclFindImage" /></span>
                <span
                    class="entry">
                        <asp:TextBox ID="txtImage" runat="server" Width="100" meta:resourcekey="txtImage"></asp:TextBox>
                </span>
            </div>
            <br />
            <br />
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lblCategory" runat="server" meta:resourcekey="lblCategory" /></span><span
                        class="entry">
                        <asp:DropDownList ID="DropDownListCategories" runat="server" Height="25px" 
                            OnSelectedIndexChanged="DropDownListCategories_SelectedIndexChanged" AutoPostBack ="true">
                        </asp:DropDownList></span>
            </div>

            <div class="button">
                <asp:Button ID="btnFindImageClick" runat="server" OnClick="BtnFindImageClick" meta:resourcekey="btnFindImageClick"/>
                <asp:Label ID="lblNotFound" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblNotFound"></asp:Label>
            </div>

        </form>
    </div>
    <br />
    <br />
</asp:Content>



