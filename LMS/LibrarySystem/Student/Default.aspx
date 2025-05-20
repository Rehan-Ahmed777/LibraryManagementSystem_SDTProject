<%@ Page Title="" Language="C#" MasterPageFile="~/Student/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Student_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%">
        <tr>
            <td class="tblhead" bgcolor="White">
                WELCOME TO LIBRARY SYSTEM</td>
        </tr>
        <tr>
            <td bgcolor="White">
                &nbsp;</td>
        </tr>
        <tr>
            <td bgcolor="White" style="text-align: center">
            <video width="640" height="360" autoplay muted loop playsinline style="pointer-events: none;">
                <source src='<%= ResolveUrl("~/video/book.mp4") %>' type="video/mp4" />
                Your browser does not support the video tag.
            </video>
            </td>
        </tr>
    </table>
</asp:Content>

