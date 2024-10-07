<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.EditComment" MasterPageFile="~/PracticaMaD.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

        <form id="showImageForm" runat="server">
            <div class="form-group col-md-6 col-sm-6"> 

                <asp:Label ID="lblCommet" runat="server"  meta:resourcekey="lblCommet"></asp:Label>
                <textarea class="form-control  input-sm" id="comment" runat="server"> </textarea> 
            </div> 


            <div class="form-group col-md-6 col-sm-6"> 
                <asp:Button ID="btnSubmit" runat="server" meta:resourcekey="btnSubmit" OnClick="Comment_Update" /> 
            </div> 
        
        </form>


</asp:Content>
