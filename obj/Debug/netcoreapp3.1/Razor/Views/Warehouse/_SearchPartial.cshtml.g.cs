#pragma checksum "D:\Repository\SpareManagement\Views\Warehouse\_SearchPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "795b4e7939ddfb6dd9b65fa6b8f5ba9aab2add37"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Warehouse__SearchPartial), @"mvc.1.0.view", @"/Views/Warehouse/_SearchPartial.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"795b4e7939ddfb6dd9b65fa6b8f5ba9aab2add37", @"/Views/Warehouse/_SearchPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef1a0e23f2e4e61bf8d0ae60da25b416b1af2d9a", @"/Views/_ViewImports.cshtml")]
    public class Views_Warehouse__SearchPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WarehouseViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<h3>This is partial</h3>

<table class=""table"">
    <thead>
        <tr>
            <th scope=""col"">編號</th>
            <th scope=""col"">名稱</th>
            <th scope=""col"">項目</th>
            <th scope=""col"">流水號</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 15 "D:\Repository\SpareManagement\Views\Warehouse\_SearchPartial.cshtml"
         foreach (var item in Model.SpareList)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 18 "D:\Repository\SpareManagement\Views\Warehouse\_SearchPartial.cshtml"
               Write(item.No);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 19 "D:\Repository\SpareManagement\Views\Warehouse\_SearchPartial.cshtml"
               Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 20 "D:\Repository\SpareManagement\Views\Warehouse\_SearchPartial.cshtml"
               Write(item.Manager);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 21 "D:\Repository\SpareManagement\Views\Warehouse\_SearchPartial.cshtml"
               Write(item.PurchaseId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 23 "D:\Repository\SpareManagement\Views\Warehouse\_SearchPartial.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WarehouseViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
