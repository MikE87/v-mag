<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WirtualnyMagazyn.Models.CategoryModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <div id="dialog">
        </div>

        <% using (Html.BeginForm("Index", "Category", FormMethod.Post, new { id="categoryIndexForm" }))
           {%>

        <fieldset>
            <div>
                Wybierz kategorię:<br /> 
                <%: Html.DropDownListFor(m => m.selectedCategory, Model.CategoryList)%>
                <% if (Model.CategoryList.Count() > 0)
                   { %>  
                    <input class="button" id="showCategoryButton" type="submit" name="submitButton" value="Pokaż" />
                 <%} %>
            </div>
            <p>
                <% if (Model.CategoryList.Count() > 0)
                   { %>
                    <input class="button" id="editCategoryButton" type="submit" name="submitButton" value="Edytuj" />
                    <input class="button" id="deleteCategoryButton" type="submit" name="submitButton" value="Usuń" />
                 <%} %>
            </p>
        </fieldset>

        <div>
            Opis kategorii:
            <div id="categoryDesc">
                <% Html.RenderAction("GetCategoryDescription", "Category", new { id = Model.ID }); %>
            </div>
        </div>

        <% } %>
        
        <div id="categoryContent">
            <% if (Model.ID > 0)
               {
                   Html.RenderAction("Index", "Product", new { id = Model.ID });
               }%>
        </div>

</asp:Content>
