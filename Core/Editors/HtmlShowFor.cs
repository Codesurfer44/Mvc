using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mvc.Core.Editors;


public static class HtmlShowFor {

    public static IHtmlContent ShowFor<TModel, TResult>(
        this IHtmlHelper <TModel> h, Expression<Func<TModel, TResult>> e) {

            var container = new TagBuilder("dl");
            container.AddCssClass("d-flex align-items-start mb-2"); 

            var dt = new TagBuilder("dt");
            dt.AddCssClass("col-sm-2");           
            dt.InnerHtml.AppendHtml(h.DisplayNameFor(e));

            var dd = new TagBuilder("dd");
            dd.AddCssClass("col-sm-10");           
            dd.InnerHtml.AppendHtml(h.DisplayFor(e));

            container.InnerHtml.AppendHtml(dt);
            container.InnerHtml.AppendHtml(dd);

            return container;
        }
}