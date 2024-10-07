﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="Followers.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Followers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
        <asp:Localize ID="lclContent" runat="server" meta:resourcekey="lclContent" />

    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
     <form id="form1" runat="server">
        <h2><asp:Localize ID="Localize1" runat="server" meta:resourcekey="lclHeader" /></h2>
        <br />
        <br />
        <center>
            <div >
                <asp:GridView ID="GridFollowers" runat="server" DataKeyNames="loginName" AutoGenerateColumns="False" OnRowCommand="GridFollowers_RowCommand" Width="400px" BorderColor="White" BorderWidth="0px" CellSpacing="30" HorizontalAlign="Center">
                    <Columns>
                             <asp:HyperLinkField  DataTextField="loginName" HeaderText="<%$ Resources:Common, loginName %>" DataNavigateUrlFields="userId" DataNavigateUrlFormatString="~/Pages/ShowUserDetails.aspx?searchValue={0}" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="firstName" HeaderText="<%$ Resources:Common, firstName %>" ItemStyle-HorizontalAlign="Center"  />
                            <asp:BoundField DataField="lastName" HeaderText="<%$ Resources:Common, lastName %>" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                            <EditRowStyle BackColor="White" />
                </asp:GridView>
            </div>
            <br />
            <div>
                
            </div>
        </center>
    </form>


</asp:Content>