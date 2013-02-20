using System;
using System.Text;
using System.Web.Mvc;
using DemoStore.WebUI.Models;

namespace DemoStore.WebUI.HtmlHelpers
{
    /// <summary>
    /// This class contains the helper function to display the paging functionality.
    /// </summary>
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            var builder = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tagBuilder = new TagBuilder("a");
                tagBuilder.MergeAttribute("href", pageUrl(i));
                tagBuilder.InnerHtml = i.ToString();

                if (i == pagingInfo.CurrentPage)
                {
                    tagBuilder.AddCssClass("selected");
                }

                builder.Append(tagBuilder.ToString());
            }

            return MvcHtmlString.Create(builder.ToString());
        }
    }
}