<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WirtualnyMagazyn.Models.FileModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Dodaj zdjęcie produktu
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm("SetImage", "File", new { id=Model.ProductID }, FormMethod.Post, new { enctype = "multipart/form-data", id = "addImageForm" }))
       {%>
        <%: Html.ValidationSummary(true, "Nie można dodać tego pliku !")%>
            <%: Html.Hidden("modelID", Model.ProductID, new { id = "modelID" })%>

        <fieldset>
            <legend>Dodaj zdjęcie</legend>

            <div class="editor-field">
            <%: Html.ValidationMessageFor(model => model.file) %> <br />
                <input type="file" id="file" name="file" />
            </div>
            
            <p>
                <input class="button" type="submit" value="Dodaj" onclick="upload()" />
                <%: Html.ActionLink("Anuluj", "Details", "Product", new { id = Model.ProductID }, new { id="cancelForm", @class="button" })%>
            </p>
        </fieldset>

    <% } %>

</asp:Content>
