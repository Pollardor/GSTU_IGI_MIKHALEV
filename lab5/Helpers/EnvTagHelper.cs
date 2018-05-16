using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace lab2.Helpers
{
    public class EnvTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "environment";
            output.Attributes.SetAttribute("exclude", "Development");
            HtmlString html = new HtmlString(" <link rel=\"stylesheet\" href=\"https://ajax.aspnetcdn.com/ajax/bootstrap/4.0.0/css/bootstrap.min.css \"" +
                                             "asp-fallback-href = \"/lib/bootstrap/dist/css/bootstrap.min.css\"" +
                                             "asp-fallback-test-class=\"sr-only\" asp-fallback-test-property=\"position\" asp-fallback-test-value=\"absolute\" />" +
                                             "<script src=\"/lib/jquery/dist/jquery.js\"></script>" +
                                             "<script src = \"/lib/bootstrap/dist/js/bootstrap.js\" ></script>" +
                                             "<link rel = \"stylesheet\" href = \"/css/site.min.css\" asp-append-version=\"true\" />" +
                                             "<link rel = \"stylesheet\" href =\"/css/site.css\" />" +
                                             "<script src = \"/lib/bootstrap/dist/js/npm.js\" ></script >" +
                                             "<script src=\"/lib/bootstrap/dist/js/bootstrap.js\" ></script>" +
                                             "<link href = \"/lib/bootstrap/dist/css/bootstrap-theme.css\" rel=\"stylesheet\" />" +
                                             "<link href = \"/lib/bootstrap/dist/css/bootstrap.css\" rel =\"stylesheet\" />");
            output.Content.SetHtmlContent(html);
        }
    }
}
