using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Mvc.Core.Editors;

public static class HtmlInputFor {

    public static IHtmlContent InputFor<TModel, TResult>(
        this IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e) {
            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var label = h.LabelFor(e, new { @class = "control-label" });
            var input = h.EditorFor(e, new { htmlAttributes = new { @class = "form-control" } });
            var validation = h.ValidationMessageFor(e, null, new { @class = "text-danger" });
            div.InnerHtml.AppendHtml(label);
            div.InnerHtml.AppendHtml(input);
            div.InnerHtml.AppendHtml(validation);
            return div;
        } 
}