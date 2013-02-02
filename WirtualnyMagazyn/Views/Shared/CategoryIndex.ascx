<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<WirtualnyMagazyn.Models.CategoryModel>>" %>

<h2>Wybierz kategorię</h2>

        <%  
            SelectList sl = new SelectList(Model, "ID", "Name"); %>

        <%: Html.DropDownList("Kategorie", sl) %>

         <br />
         <%: Html.ActionLink("Nowa kategoria", "Create", "Category") %>
         <br />
         <%: Html.ActionLink("Edytuj kategorię", "Edit", "Category", sl.SelectedValue) %>