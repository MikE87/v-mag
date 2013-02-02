<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<WirtualnyMagazyn.Models.ProductModel>>" %>

        <table>
            <tr>
                <th>
                    Nazwa
                </th>
                <th>
                    Opis
                </th>
                <th>
                    Dostępne
                </th>
                <th>
                    Cena
                </th>
            </tr>

        <% foreach (var item in Model) { %>
    
            <tr>
                <td class="tdName">
                    <%: Html.ActionLink(item.Name, "Details", new { id = item.ID }, new { @class="productDetailsLink" })%>
                </td>
                <td class="tdDesc">
                    <%: item.Description %>
                </td>
                <td class="tdNum">
                    <%: item.Count %>
                </td>
                <td class="tdNum">
                    <%: String.Format("{0:F}", item.Price) %>
                </td>
            </tr>
    
        <% } %>

        </table>

        <p>
            <%: Html.ActionLink("Dodaj nowy produkt", "Create", new { id = ViewData["category"] }, new { @class = "button", id="addProductButton"})%>
        </p>