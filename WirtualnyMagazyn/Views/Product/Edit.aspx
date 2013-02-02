<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WirtualnyMagazyn.Models.ProductModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edycja produktu
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edycja produktu</h2>
    
    <% Html.EnableClientValidation(); %>
        <% using (Html.BeginForm("Edit", "Product", new { id=Model.ID }, FormMethod.Post, new { id = "editProductForm" }))
           {%>
            <%: Html.ValidationSummary(true, "Wprowadź prawidłowe dane.")%>

            <fieldset>
            
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
                    <%: Html.TextBoxFor(model => model.Price, String.Format("{0:F}", Model.Price))%>
                    <%: Html.ValidationMessageFor(model => model.Price)%>
                </div>
            
                <p>
                    <input class="button" type="submit" value="Zapisz" />
                    <%: Html.ActionLink("Anuluj", "Details", "Product", new { id=Model.ID }, new { id = "cancelForm", @class = "button" })%>
                </p>
            </fieldset>

        <% } %>

</asp:Content>

