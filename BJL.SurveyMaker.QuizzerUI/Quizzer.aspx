<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Quizzer.aspx.cs" Inherits="BJL.SurveyMaker.QuizzerUI.Quizzer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <div class="control-label col-md-6">
            <asp:Label ID="lblErrorMsg" runat="server" Text=""></asp:Label>
        </div>


    <div class="form-row ml-2 mt-3">

        <div class="control-label col-md-2">
            <asp:Label ID="lblActivationCode" runat="server" Text="Activation Code: "></asp:Label>
        </div>

        <div class="control-label col-md-2">
            <asp:TextBox ID="txtCode" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <asp:Button ID="btnSubmitCode" runat="server" Text="Submit Code" CssClass="btn btn-outline-primary ml-3" OnClick="btnSubmitCode_Click" />
    </div>

    <br />

    <div class="form-row ml-2 mt-2">
        <div class="control-label col-md-8">
            <asp:Label ID="lblQuestion" runat="server" 
                Text="" 
                Visible="false" 
                Font-Size="Large">
            </asp:Label>
        </div>
    </div>

    <div class="form-row ml-2 mt-2">
        <div class="control-label col-md-2">
            <asp:Label ID="lblAnswer" runat="server" Text="Select an answer: " Visible="false"></asp:Label>
        </div>

        <div class="control-label col-md-3">
            <asp:DropDownList
                ID="ddlAnswers"
                runat="server"
                CssClass="form-control" AutoPostBack="true"
                Visible="false">
            </asp:DropDownList>
        </div>

        <asp:Button ID="btnSubmitAnswer" runat="server" Text="Submit Answer" CssClass="btn btn-outline-primary ml-3" Visible="false" OnClick="btnSubmitAnswer_Click"/>
    </div>

</asp:Content>
