#pragma checksum "D:\4x\Repository\SpareManagement\Views\Components\History.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0ebce194b8f8b6ccb511858cce21fef93e491298"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Components_History), @"mvc.1.0.view", @"/Views/Components/History.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\4x\Repository\SpareManagement\Views\_ViewImports.cshtml"
using SpareManagement;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\4x\Repository\SpareManagement\Views\_ViewImports.cshtml"
using SpareManagement.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0ebce194b8f8b6ccb511858cce21fef93e491298", @"/Views/Components/History.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef1a0e23f2e4e61bf8d0ae60da25b416b1af2d9a", @"/Views/_ViewImports.cshtml")]
    public class Views_Components_History : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<HistoryViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<h3>");
#nullable restore
#line 6 "D:\4x\Repository\SpareManagement\Views\Components\History.cshtml"
Write(Html.DisplayNameFor(model => model.PartNo));

#line default
#line hidden
#nullable disable
            WriteLiteral(": ");
#nullable restore
#line 6 "D:\4x\Repository\SpareManagement\Views\Components\History.cshtml"
                                            Write(Model.PartNo);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h3>\r\n<div>\r\n    <table class=\"table\">\r\n        <thead class=\"thead-dark\">\r\n            <tr>\r\n                <th scope=\"col\">");
#nullable restore
#line 11 "D:\4x\Repository\SpareManagement\Views\Components\History.cshtml"
                           Write(Html.DisplayNameFor(model => model.details.First().Date));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th scope=\"col\">");
#nullable restore
#line 12 "D:\4x\Repository\SpareManagement\Views\Components\History.cshtml"
                           Write(Html.DisplayNameFor(model => model.details.First().Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th scope=\"col\">");
#nullable restore
#line 13 "D:\4x\Repository\SpareManagement\Views\Components\History.cshtml"
                           Write(Html.DisplayNameFor(model => model.details.First().Quantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th scope=\"col\">");
#nullable restore
#line 14 "D:\4x\Repository\SpareManagement\Views\Components\History.cshtml"
                           Write(Html.DisplayNameFor(model => model.details.First().Employee));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th scope=\"col\">");
#nullable restore
#line 15 "D:\4x\Repository\SpareManagement\Views\Components\History.cshtml"
                           Write(Html.DisplayNameFor(model => model.details.First().Memo));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n\r\n");
#nullable restore
#line 20 "D:\4x\Repository\SpareManagement\Views\Components\History.cshtml"
             foreach (var item in Model.details)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 23 "D:\4x\Repository\SpareManagement\Views\Components\History.cshtml"
                   Write(item.Date);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 24 "D:\4x\Repository\SpareManagement\Views\Components\History.cshtml"
                   Write(item.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 25 "D:\4x\Repository\SpareManagement\Views\Components\History.cshtml"
                   Write(item.Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 26 "D:\4x\Repository\SpareManagement\Views\Components\History.cshtml"
                   Write(item.Employee);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 27 "D:\4x\Repository\SpareManagement\Views\Components\History.cshtml"
                   Write(item.Memo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n");
#nullable restore
#line 29 "D:\4x\Repository\SpareManagement\Views\Components\History.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<HistoryViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
