<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WirtualnyMagazyn.Models.CategoryModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Dodaj nową kategorię
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Dodaj nową kategorię</h2>

    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm("Create", "Category", FormMethod.Post, new { id="createCategoryForm" }))
       {%>
        <%: Html.ValidationSummary(true, "Wprowadź prawidłowe dane.")%>

        <fieldset>
            <legend id="createCategoryLe">Dane kategorii</legend>

            <div class="editor-label">
                <%: Html.LabelFor(model => model.Name)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Name, new { minlength="3", maxlength="25" })%>
                <%: Html.ValidationMessageFor(model => model.Name)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Description)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Description, new { maxlength="100" })%>
                <%: Html.ValidationMessageFor(model => model.Description)%>
            </div>
            
            <p>
                <input class="button" type="submit" value="Dodaj" />
                <%: Html.ActionLink("Anuluj", "Index", null, new { id="cancelForm", @class="button" })%>
            </p>
        </fieldset>

    <% } %>

</asp:Content>
