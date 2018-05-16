using lab2.Models.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Helpers
{
    public static class NavigationHelper
    {
        public static HtmlString CreateNavigation(this IHtmlHelper html, string Route, PageViewModel pageViewModel)
        {
            string result = "";
            if (pageViewModel.HasPreviousPage == true)
            {
                result += "<a href =" + Route + "?page=" + (pageViewModel.PageNumber - 1) +
                          " class=\"btn btn-default btn\"><i class=\"glyphicon glyphicon-chevron-left\"></i>Назад</a>";
            }
            if (pageViewModel.HasNextPage == true)
            {
                result += "<a href =" + Route + "?page=" + (pageViewModel.PageNumber + 1) +
                          " class=\"btn btn-default btn\">Вперед</a><i class=\"glyphicon glyphicon-chevron-right\"></i>";
            }
            return new HtmlString(result);
        }
    }
}
