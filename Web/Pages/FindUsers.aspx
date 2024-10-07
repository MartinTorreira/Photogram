<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindUsers.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.FindUsers"
    MasterPageFile="~/PracticaMaD.Master" meta:resourcekey="Page" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server"  meta:resourcekey="lclMenuExplanation">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="FindForm" runat="server">                                
            <div class="field">
                <span class="label">    
                    <asp:Localize ID="lclFindUser" runat="server" meta:resourcekey="lclFindUser" /></span>

                <span
                    class="entry">
                        <asp:TextBox ID="txtUser" runat="server" Width="100" meta:resourcekey="txtUser" TextMode="Number"></asp:TextBox>
                </span>
            </div>

            <div class="button">
                <asp:Button ID="btnFindUserClick" runat="server" OnClick="BtnFindUserClick" meta:resourcekey="btnFindUserClick" />
                <asp:Label ID="lblNotFound" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblNotFound"></asp:Label>
            </div>
        </form>
    </div>
    <br />
    <br />
</asp:Content>



