<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindImageResult.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.FindImageResult"
    MasterPageFile="~/PracticaMaD.Master" meta:resourcekey="Page" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server"  meta:resourcekey="lclMenuExplanation">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <h1><asp:Localize ID="lclHeader" runat="server" meta:resourcekey="lclHeader" /></h1>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lclImages" runat="server" Style="position: relative" meta:resourcekey="lclImages"></asp:Label>
             <div id="images">
                    <asp:Repeater ID="repeater" runat="server"> 
                        <ItemTemplate>
                         <div class="image-container" style="margin-top:20px; margin-bottom:20px">
                            <asp:Image ID="ImageLatest" Height="120" Width="124" runat="server" ImageUrl='<%# Eval("path") %>' />
                            <br /> 
                            <br />
                            <asp:HyperLink ID="lnkImage" runat="server" NavigateUrl='<%# "ShowImage.aspx?searchValue=" + Eval("imageId") %>' Text='<%# Eval("title") %>' Visible="true" />
                            <br /> 
                            <br />
                         </div>
                        </ItemTemplate>
                    </asp:Repeater>
              </div>
        </div>
    </form>
</asp:Content>
