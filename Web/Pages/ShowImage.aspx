<%@ Page Language="C#" EnableEventValidation="false"  AutoEventWireup="true" CodeBehind="ShowImage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.ShowImage"  MasterPageFile="~/PracticaMaD.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

        <form id="showImageForm" runat="server">
            <div style="margin-bottom: 10px; margin-top: 20px" align="left">
                <img ID="imagePath"  runat="server" src="aa" ReadOnly="true">
            </div>
            <div style="margin-bottom: 10px; margin-top: 20px" align="left">
                <asp:Label ID="lblImageId" runat="server" Width="100" Columns="16"  meta:resourcekey="lblImageId"></asp:Label>
                <asp:TextBox ID="txtImageId" runat="server" ReadOnly="true" BorderWidth="0px"></asp:TextBox>
            </div>
            <div style="margin-bottom: 10px; margin-top: 20px" align="left">
                <asp:Label ID="lblName" runat="server" Width="100" Columns="16"  meta:resourcekey="lblName"></asp:Label>
                <asp:TextBox ID="txtName" runat="server" ReadOnly="true" BorderWidth="0px"></asp:TextBox>
            </div>
            <div style="margin-bottom: 10px; margin-top: 20px" align="left">
                <asp:Label ID="lblDescription" runat="server" Width="100" Columns="16"  meta:resourcekey="lblDescription"></asp:Label>
                <asp:TextBox ID="txtDescription" runat="server" ReadOnly="true" BorderWidth="0px"></asp:TextBox>
            </div>
            <div style="margin-bottom: 10px; margin-top: 20px" align="left">
                <asp:Label ID="lblAuthor" runat="server" Width="100" Columns="16"  meta:resourcekey="lblAuthor"></asp:Label>
                <asp:TextBox ID="txtAuthor" runat="server" ReadOnly="true" BorderWidth="0px"></asp:TextBox>
            </div>
            <div style="margin-bottom: 10px; margin-top: 20px" align="left">
                <asp:Label ID="lblAperture" runat="server" Width="100" Columns="16"  meta:resourcekey="lblAperture"></asp:Label>
                <asp:TextBox ID="txtAperture" runat="server" ReadOnly="true" BorderWidth="0px"></asp:TextBox>
            </div>
            <div style="margin-bottom: 10px; margin-top: 20px" align="left">
                <asp:Label ID="lblExposure" runat="server" Width="100" Columns="16"  meta:resourcekey="lblExposure"></asp:Label>
                <asp:TextBox ID="txtExposure" runat="server" ReadOnly="true" BorderWidth="0px"></asp:TextBox>
            </div>
            <div style="margin-bottom: 10px; margin-top: 20px" align="left">
                <asp:Label ID="lblWhite" runat="server" Width="100" Columns="16"  meta:resourcekey="lblWhite"></asp:Label>
                <asp:TextBox ID="txtWhite" runat="server" ReadOnly="true" BorderWidth="0px"></asp:TextBox>
            </div>
            <div style="margin-bottom: 10px; margin-top: 20px" align="left">
                <asp:Label ID="lblLikes" runat="server" Width="100" Columns="16"  meta:resourcekey="lblLikes"></asp:Label>
                <asp:TextBox ID="txtLikes" runat="server" ReadOnly="true" BorderWidth="0px"></asp:TextBox>
            </div>
            <asp:Table id="Table1" 
                GridLines="Both" 
                HorizontalAlign="Center" 
                Font-Names="Verdana" 
                Font-Size="8pt" 
                CellPadding="15" 
                CellSpacing="0" 
                Runat="server"/>
             <div class="button">

                <asp:Button ID="btnLike" runat="server" meta:resourcekey="btnLike" OnClick="Like" />

            </div>
            <div class="button">

                <asp:Button ID="btnUnlike" runat="server" meta:resourcekey="btnUnlike" OnClick="Unlike" />

            </div>


            <div class="form-group col-md-6 col-sm-6"> 

                <asp:Label ID="lblCommet" runat="server"  meta:resourcekey="lblCommet"></asp:Label>
                <textarea class="form-control  input-sm" id="comment" runat="server"> </textarea> 
            </div> 


            <div class="form-group col-md-6 col-sm-6"> 
                <asp:Button ID="Button1" runat="server" meta:resourcekey="btnSubmit" OnClick="Comment_Submit" /> 
            </div> 



            <div  style="margin-bottom: 10px; margin-top: 20px" align="left">
                <div id="Comments">
                        <asp:Repeater ID="repeater" runat="server">
                            <ItemTemplate>
                                <div class="comment-container" style="margin-top:20px; margin-bottom:20px">
                                    <asp:TextBox ID="txtUserName" runat="server" ReadOnly="true" BorderWidth="0px" Text='<%# Eval("userId") %>'/>
                                    <br /> 
                                    <br />
                                    <asp:TextBox ID="txtCommentContent" runat="server" ReadOnly="true" BorderWidth="0px"  Text='<%# Eval("content") %>'/>
                                    <br /> 
                                    <br />
                                    <asp:Button ID="btnDelete" Visible='<%# ShowButton((long)Eval("userId")) %>' runat="server" CommandArgument='<%# Eval("commentId") %>' meta:resourcekey="btnDelete" OnClick="Comment_Delete" /> 
                                    <asp:Button ID="btnEdit" Visible='<%# ShowButton((long)Eval("userId")) %>' runat="server" CommandArgument='<%# Eval("commentId") %>' meta:resourcekey="btnEdit" OnClick="Comment_Edit" /> 

                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
             </div>


            <div class="button">

                <asp:Button ID="btnNext" runat="server" meta:resourcekey="btnNext" OnClick="Next" />

            </div>
            <div class="button">

                <asp:Button ID="btnPrevious" runat="server" meta:resourcekey="btnPrevious" OnClick="Previous" />

            </div>
            </form>


</asp:Content>
