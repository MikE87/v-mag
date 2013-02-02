<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WirtualnyMagazyn.Models.ProductModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Szczegóły produktu
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Szczegóły produktu</h2>
    <div id="productDetailsView">
        <fieldset>
            <legend>Dane produktu</legend>
            
            <div class="inline-display-field">
                <div class="display-label">Nazwa:</div>
                <div class="display-field"><%: Model.Name %></div>
        
                <div class="display-label">Opis:</div>
                <div class="display-field"><%: Model.Description %></div>
        
                <div class="display-label">Dostępność:</div>
                <div class="display-field"><%: Model.Count %></div>
        
                <div class="display-label">Cena:</div>
                <div class="display-field"><%: String.Format("{0:F}", Model.Price) %></div>
            </div>
            <div class="inline-display-field">
                <img id="photo" src="<%:Url.Action("GetImage", "File", new { id=Model.ID }) + "?" + DateTime.Now.Millisecond %>" 
                        alt="<%: Model.PhotoName %>" />
            </div>
        </fieldset>
        <%: Html.Hidden("modelID", Model.ID, new{id="modelID"}) %>
        <p>
            <%: Html.ActionLink("Edytuj", "Edit", new { id = Model.ID }, new { id="editProductButton", @class="button" })%> |
            <%: Html.ActionLink("Usuń", "Delete", new { id = Model.ID, category=Model.Category }, new { id = "deleteProductButton", @class = "button" })%> |
            <% if (Model.hasImage)
               {%>
                <%: Html.ActionLink("Zmień zdjęcie", "SetImage", "File", new { id = Model.ID }, new { id = "addImageButton", @class = "button" })%> |
                <%: Html.ActionLink("Usuń zdjęcie", "DeleteImage", "File", new { id = Model.ID }, new { id = "deleteImageButton", @class = "button" })%>
            <%}
               else
               { %>
                <%: Html.ActionLink("Dodaj zdjęcie", "SetImage", "File", new { id = Model.ID }, new { id = "addImageButton", @class = "button" })%>
                <%} %>
        </p>
    </div>

    <%: Html.ActionLink("Wróć", "Index", "Category", new { id=Model.Category }, null)%>

</asp:Content>

