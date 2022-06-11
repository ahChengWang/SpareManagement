#pragma checksum "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "94ca9af3a0a68bd589b0d0fa9bd760c8e5043040"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Expendables__Partial), @"mvc.1.0.view", @"/Views/Expendables/_Partial.cshtml")]
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
#line 1 "D:\Repository\SpareManagement\Views\_ViewImports.cshtml"
using SpareManagement;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Repository\SpareManagement\Views\_ViewImports.cshtml"
using SpareManagement.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"94ca9af3a0a68bd589b0d0fa9bd760c8e5043040", @"/Views/Expendables/_Partial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef1a0e23f2e4e61bf8d0ae60da25b416b1af2d9a", @"/Views/_ViewImports.cshtml")]
    public class Views_Expendables__Partial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ExpendablesListViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<table class=\"table\">\r\n    <thead style=\"background-color: slategrey; color: white;\">\r\n        <tr>\r\n            <th scope=\"col\">");
#nullable restore
#line 7 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                       Write(Html.DisplayNameFor(model => model.Details.First().PartNo));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th scope=\"col\">");
#nullable restore
#line 8 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                       Write(Html.DisplayNameFor(model => model.Details.First().Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th scope=\"col\">");
#nullable restore
#line 9 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                       Write(Html.DisplayNameFor(model => model.Details.First().PurchaseId));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th scope=\"col\">");
#nullable restore
#line 10 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                       Write(Html.DisplayNameFor(model => model.Details.First().Spec));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th scope=\"col\">");
#nullable restore
#line 11 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                       Write(Html.DisplayNameFor(model => model.Details.First().IsKeySpare));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th scope=\"col\">");
#nullable restore
#line 12 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                       Write(Html.DisplayNameFor(model => model.Details.First().IsCommercial));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th scope=\"col\">");
#nullable restore
#line 13 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                       Write(Html.DisplayNameFor(model => model.Details.First().Placement));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th scope=\"col\">");
#nullable restore
#line 14 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                       Write(Html.DisplayNameFor(model => model.Details.First().Inventory));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th scope=\"col\">");
#nullable restore
#line 15 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                       Write(Html.DisplayNameFor(model => model.Details.First().SafetyCount));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n");
            WriteLiteral("            <th scope=\"col\"></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n\r\n");
#nullable restore
#line 22 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
         foreach (var item in Model.Details)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td class=\"row-data\">");
#nullable restore
#line 25 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                                Write(item.PartNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"row-data\">");
#nullable restore
#line 26 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                                Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"row-data\">");
#nullable restore
#line 27 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                                Write(item.PurchaseId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"row-data\">");
#nullable restore
#line 28 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                                Write(item.Spec);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"row-data\">");
#nullable restore
#line 29 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                                Write(item.IsKeySpare);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"row-data\">");
#nullable restore
#line 30 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                                Write(item.IsCommercial);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"row-data\">");
#nullable restore
#line 31 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                                Write(item.Placement);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"row-data\">");
#nullable restore
#line 32 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                                Write(item.Inventory);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"row-data\">");
#nullable restore
#line 33 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
                                Write(item.SafetyCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
            WriteLiteral("                <td>\r\n                    <input");
            BeginWriteAttribute("id", " id=\"", 1886, "\"", 1903, 1);
#nullable restore
#line 36 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
WriteAttributeValue("", 1891, item.PartNo, 1891, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" type=\"button\" onclick=\"onHistoryClick(this)\" style=\"text-decoration:none;\" class=\"btn btn-outline-danger btn-sm\" value=\"History\" />\r\n\r\n");
            WriteLiteral("                </td>\r\n            </tr>\r\n");
#nullable restore
#line 43 "D:\Repository\SpareManagement\Views\Expendables\_Partial.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ExpendablesListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591