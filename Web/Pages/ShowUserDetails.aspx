<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowUserDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.ShowUserDetails" MasterPageFile="~/PracticaMaD.Master" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div style="margin-top:20px">
        <asp:Label ID="lblFormTitle" runat="server" Font-Size="30px" meta:resourcekey="lblFormTitle" Font-Bold="True" style="margin-top:30px"  />
    </div>
     <div class="container mx-auto">
        <div class="row">
            <div class="col-md-6 order-md-1" style="margin-bottom: 30px;" >
                <form id="form" runat="server">
                    <div style="margin-bottom: 10px;">

                        <!-- FOLLOW BUTTONS -->
                        <div style="margin-top: 20px; text-align: right;"  >
                            <asp:Button ID="btnFollow" runat="server"  meta:resourcekey="btnFollow" OnClick="BtnFollow_Click"  /> &nbsp;&nbsp;&nbsp;
                             <asp:Button ID="btnUnfollow" runat="server" meta:resourcekey="btnUnfollow" OnClick="BtnUnfollow_Click" />
                        </div>

                        <asp:Label ID="lblUserId" runat="server" Width="100" Columns="16" meta:resourcekey="lblUserId"></asp:Label>
                        <asp:TextBox ID="txtUserId" runat="server" ReadOnly="true" BorderWidth="0px"></asp:TextBox>
                        </div>
                        <div style="margin-bottom: 10px;">
                            <asp:Label ID="lblFirstname" runat="server" Width="100" Columns="16" meta:resourcekey="lblFirstname"></asp:Label>
                            <asp:TextBox ID="txtFirstname" runat="server" ReadOnly="true" BorderWidth="0px"></asp:TextBox>
                        </div>
                        <div style="margin-bottom: 10px;">
                            <asp:Label ID="lblLastname" runat="server" Width="100" Columns="16"  meta:resourcekey="lblLastname"></asp:Label>
                            <asp:TextBox ID="txtLastname" runat="server" ReadOnly="true" BorderWidth="0px"></asp:TextBox>
                        </div>
                        <div style="margin-bottom: 10px;">
                            <asp:Label ID="lblEmail" runat="server" Width="100" Columns="16" meta:resourcekey="lblEmail"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="true" BorderWidth="0px"></asp:TextBox>
                        </div>
                        <div style="margin-bottom: 10px;" >
                            <asp:Label ID="lblCountry" runat="server" Width="100" Columns="16" meta:resourcekey="lblCountry"></asp:Label>
                            <asp:TextBox ID="txtCountry" runat="server" ReadOnly="true" BorderWidth="0px"></asp:TextBox>
                        </div>

                    <!-- FOLLOW LINKS -->
                    <div style="margin-bottom: 10px; margin-top:20px" >
                        <asp:HyperLink ID="lnkFollowed" runat="server" meta:resourcekey="lnkFollowed"/>&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="lnkFollowers" runat="server" meta:resourcekey="lnkFollowers" />&nbsp;&nbsp;&nbsp;
                    </div>

                    
                </form>
                 <br />
                 <br />
                 <br />
             </div>
            <div class="col-md-6 order-md-2 ">
             <div style="margin-top:20px">
                <asp:Label ID="lblImageTitle" runat="server" Font-Size="30px" meta:resourcekey="lblImageTitle" Font-Bold="True" style="margin-bottom: 30px; margin-top:30px"  />
             </div>
                <br />
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
        </div>
    </div>
</asp:Content>