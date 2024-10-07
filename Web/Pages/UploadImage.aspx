<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.UploadImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form">
        <form id="UploadImageForm" method="post" runat="server">
            <div class="field">
                <asp:FileUpload ID="FileUpload" runat="server"  Height="29px" Width="215px" />
                <asp:RequiredFieldValidator ID="FileValidator" runat="server" ControlToValidate="FileUpload"
                            Display="Dynamic" Text="<%$ Resources: Common, mandatoryField %>" ForeColor="Red">
                        </asp:RequiredFieldValidator>
            </div >

            <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclTitle" runat="server" meta:resourcekey="lclTitle" /></span><span
                            class="entry">
                            <asp:TextBox ID="txtTitle" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtTitleResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                                Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                                meta:resourcekey="rfvTitleResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclDescription" runat="server" meta:resourcekey="lclDescription" /></span><span
                            class="entry">
                            <asp:TextBox ID="txtDescription" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription"
                                Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                                meta:resourcekey="rfvDescriptionResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclExposureTime" runat="server" meta:resourcekey="lclExposureTime" /></span><span
                            class="entry">
                            <asp:TextBox ID="txtExposureTime" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtExposureTimeResource1" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvExposureTime" runat="server" ControlToValidate="txtExposureTime"
                                Display="Dynamic" ValidationExpression="^\d+(\.\d{1,2})?$" Text="<%$ Resources: Common, mandatoryField %>"
                                meta:resourcekey="rfvExposureTimeResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclApertureSize" runat="server" meta:resourcekey="lclApertureSize" /></span><span
                            class="entry">
                            <asp:TextBox ID="txtApertureSize" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtApertureSizeResource1" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvApertureSize" runat="server" ControlToValidate="txtApertureSize"
                                Display="Dynamic" ValidationExpression="^\d+(\.\d{1,2})?$" Text="<%$ Resources: Common, mandatoryField %>"
                                meta:resourcekey="rfvApertureSizeResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclWhite" runat="server" meta:resourcekey="lclWhite" /></span><span
                            class="entry">
                            <asp:TextBox ID="txtWhite" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtWhiteResource1" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvWhite" runat="server" ControlToValidate="txtWhite"
                                Display="Dynamic" ValidationExpression="^\d+(\.\d{1,2})?$" Text="<%$ Resources: Common, mandatoryField %>"
                                meta:resourcekey="rfvWhiteResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclTags" runat="server" meta:resourcekey="lclTags" /></span><span
                            class="entry">
                            <asp:TextBox ID="txtTags" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtTagsResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTags" runat="server" ControlToValidate="txtTags"
                                Display="Dynamic" Text="<%$ Resources: Common, mandatoryField %>"
                                meta:resourcekey="rfvTagsResource1"></asp:RequiredFieldValidator></span>
            </div>


            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCategory" runat="server" meta:resourcekey="lclCategory" /></span><span
                        class="entry">
                        <asp:DropDownList ID="DropDownListCategories" runat="server" Height="44px" 
                            OnSelectedIndexChanged="DropDownListCategories_SelectedIndexChanged" AutoPostBack ="true">
                        </asp:DropDownList></span>
            </div>


            <div class="button">

                <asp:Button ID="btnUploadImage" runat="server" meta:resourcekey="btnUploadImage" OnClick="UploadPhoto" />

            </div>
        </form>
    </div>
</asp:Content>
