using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.Mvc.Html;

namespace Lekkachara.Helpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString PagingList(this HtmlHelper helper, string controller,string action, int currentPageIndex, int pageCount)
        {
            var builder = new StringBuilder();
            for (int i = 1; i <= pageCount; i++)
            {
                var link = helper.ActionLink(" " + i + " |", action,controller, new {pageIndex = i},null);
                if (i != currentPageIndex)
                {
                    builder.Append(link);
                }
                else
                {
                    builder.Append("<b>" + i + "</b>"+" |");
                }
            }
            return new MvcHtmlString(builder.ToString());
        }

        public static string DisplayMoney(this HtmlHelper helper,int money,string alternate="-")
        {
            return money == 0 ? alternate : money.ToString();
        }

        public static MvcHtmlString MonthYearTextBox(this HtmlHelper helper,string name)
        {
            HttpRequestBase request = helper.ViewContext.HttpContext.Request;
            var queryParams = HttpUtility.ParseQueryString(request.Url.Query);
            int passedMonth = string.IsNullOrWhiteSpace(queryParams["year"]) ? DateTime.Now.Year : int.Parse(queryParams["year"]);
            int passedYear = string.IsNullOrWhiteSpace(queryParams["month"]) ? DateTime.Now.Month : int.Parse(queryParams["month"]);
            return helper.TextBox(name, String.Format("{0:MMMM-yyyy}", new DateTime(passedMonth, passedYear, 1)));
        }
    }
}