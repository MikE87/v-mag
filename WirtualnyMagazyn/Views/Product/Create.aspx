<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WirtualnyMagazyn.Models.ProductModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Dodaj nowy produkt
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Dodaj nowy produkt</h2>
    
    <% Html.EnableClientValidation(); %>
        <% using (Html.BeginForm("Create", "Product", new { id=Model.Category }, FormMethod.Post, new { id = "createProductForm" }))
           {%>
            <%: Html.ValidationSummary(true, "Wprowadź prawidłowe dane.")%>

            <fieldset>
                <legend>Dane produktu</legend>
            
                <div class="editor-label">
                    <%: Html.LabelFor(model => model.Name)%>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.Name, new { minlength = "3", maxlength = "25" })%>
                    <%: Html.ValidationMessageFor(model => model.Name)%>
                </div>
            
                <div class="editor-label">
                    <%: Html.LabelFor(model => model.Description)%>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.Description, new { maxlength = "100" })%>
                    <%: Html.ValidationMessageFor(model => model.Description)%>
                </div>
            
                <div class="editor-label">
                    <%: Html.LabelFor(model => model.Count)%>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.Count, new { maxlength = "4" })%>
                    <%: Html.ValidationMessageFor(model => model.Count)%>
                </div>
            
                <div class="editor-label">
                    <%: Html.LabelFor(model => model.Price)%>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.Price, new { maxlength = "12" })%>
                    <%: Html.ValidationMessageFor(model => model.Price)%>
                </div>
            
                <p>
                    <input class="button" type="submit" value="Dodaj" />
                    <%: Html.ActionLink("Anuluj", "Index", "Category", new { id=Model.Category }, new { id = "cancelForm", @class = "button" })%>
                </p>
            </fieldset>

        <% } %>

</asp:Content>

