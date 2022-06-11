#pragma checksum "D:\Repository\SpareManagement\Views\Inspect\_Partial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e536edebbd52acbcffb9cdf4fdfa71be9016e53a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Inspect__Partial), @"mvc.1.0.view", @"/Views/Inspect/_Partial.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e536edebbd52acbcffb9cdf4fdfa71be9016e53a", @"/Views/Inspect/_Partial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef1a0e23f2e4e61bf8d0ae60da25b416b1af2d9a", @"/Views/_ViewImports.cshtml")]
    public class Views_Inspect__Partial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<InspectSpareViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"

<div class=""row col-12"">
    <div class=""form-group form-row col-sm-4"">
        <label>操作人員：</label>
        <input id=""inspectUser"" type=""text"" class=""custom-control"" />
    </div>
    <div class=""form-group form-row col-sm-4"">
        <label>操作時間：</label>
        <input id=""inspectDate"" type=""date"" class=""custom-control"" />
    </div>
</div>
<table class=""table"">
    <thead style=""background-color: slategrey; color: white; "">
        <tr>
            <th scope=""col"">");
#nullable restore
#line 17 "D:\Repository\SpareManagement\Views\Inspect\_Partial.cshtml"
                       Write(Html.DisplayNameFor(model => model.Details.First().PartNo));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th scope=\"col\">");
#nullable restore
#line 18 "D:\Repository\SpareManagement\Views\Inspect\_Partial.cshtml"
                       Write(Html.DisplayNameFor(model => model.Details.First().Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th scope=\"col\">");
#nullable restore
#line 19 "D:\Repository\SpareManagement\Views\Inspect\_Partial.cshtml"
                       Write(Html.DisplayNameFor(model => model.Details.First().SerialNo));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th scope=\"col\">");
#nullable restore
#line 20 "D:\Repository\SpareManagement\Views\Inspect\_Partial.cshtml"
                       Write(Html.DisplayNameFor(model => model.Details.First().Spec));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th scope=\"col\">");
#nullable restore
#line 21 "D:\Repository\SpareManagement\Views\Inspect\_Partial.cshtml"
                       Write(Html.DisplayNameFor(model => model.Details.First().NextInspectDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <th scope=\"col\"></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 26 "D:\Repository\SpareManagement\Views\Inspect\_Partial.cshtml"
         foreach (var item in Model.Details)
        {
            var trid = @item.SerialNo + "tr";
            

#line default
#line hidden
#nullable disable
            WriteLiteral("<tr");
            BeginWriteAttribute("id", " id=\"", 1165, "\"", 1175, 1);
#nullable restore
#line 29 "D:\Repository\SpareManagement\Views\Inspect\_Partial.cshtml"
WriteAttributeValue("", 1170, trid, 1170, 5, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                <td id=\"partNo\" class=\"row-data\">");
#nullable restore
#line 30 "D:\Repository\SpareManagement\Views\Inspect\_Partial.cshtml"
                                            Write(item.PartNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"row-data\">");
#nullable restore
#line 31 "D:\Repository\SpareManagement\Views\Inspect\_Partial.cshtml"
                                Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"row-data\">");
#nullable restore
#line 32 "D:\Repository\SpareManagement\Views\Inspect\_Partial.cshtml"
                                Write(item.SerialNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"row-data\">");
#nullable restore
#line 33 "D:\Repository\SpareManagement\Views\Inspect\_Partial.cshtml"
                                Write(item.Spec);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"row-data\">");
#nullable restore
#line 34 "D:\Repository\SpareManagement\Views\Inspect\_Partial.cshtml"
                                Write(item.NextInspectDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>\r\n                    <input");
            BeginWriteAttribute("id", " id=\"", 1526, "\"", 1545, 1);
#nullable restore
#line 36 "D:\Repository\SpareManagement\Views\Inspect\_Partial.cshtml"
WriteAttributeValue("", 1531, item.SerialNo, 1531, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" type=\"button\" onclick=\"onInspectClick(this)\" style=\"text-decoration:none;\" class=\"btn btn-danger btn-sm\" value=\"檢驗\" />\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 39 "D:\Repository\SpareManagement\Views\Inspect\_Partial.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<InspectSpareViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591