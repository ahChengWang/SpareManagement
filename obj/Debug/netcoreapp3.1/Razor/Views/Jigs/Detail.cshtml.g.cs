#pragma checksum "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b3cd7a8685f5812eda08a789849d9b3a762dbe20"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Jigs_Detail), @"mvc.1.0.view", @"/Views/Jigs/Detail.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b3cd7a8685f5812eda08a789849d9b3a762dbe20", @"/Views/Jigs/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef1a0e23f2e4e61bf8d0ae60da25b416b1af2d9a", @"/Views/_ViewImports.cshtml")]
    public class Views_Jigs_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<JigsListViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<h3>治具明細　(");
#nullable restore
#line 6 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
     Write(Html.DisplayNameFor(model => model.Details.First().PartNo));

#line default
#line hidden
#nullable disable
            WriteLiteral(": ");
#nullable restore
#line 6 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                                                                  Write(Model.Details.First().PartNo);

#line default
#line hidden
#nullable disable
            WriteLiteral(")</h3>\r\n<div id=\"mydiv\">\r\n    <table class=\"table table-bordered\">\r\n        <thead class=\"thead-dark\">\r\n            <tr>\r\n                <th scope=\"col\">");
#nullable restore
#line 11 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                           Write(Html.DisplayNameFor(model => model.Details.First().PartNo));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th scope=\"col\">");
#nullable restore
#line 12 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                           Write(Html.DisplayNameFor(model => model.Details.First().Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th scope=\"col\">");
#nullable restore
#line 13 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                           Write(Html.DisplayNameFor(model => model.Details.First().SerialNo));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th scope=\"col\">");
#nullable restore
#line 14 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                           Write(Html.DisplayNameFor(model => model.Details.First().Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th scope=\"col\">");
#nullable restore
#line 15 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                           Write(Html.DisplayNameFor(model => model.Details.First().PurchaseId));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th scope=\"col\">");
#nullable restore
#line 16 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                           Write(Html.DisplayNameFor(model => model.Details.First().Spec));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th scope=\"col\">");
#nullable restore
#line 17 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                           Write(Html.DisplayNameFor(model => model.Details.First().Placement));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th scope=\"col\">");
#nullable restore
#line 18 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                           Write(Html.DisplayNameFor(model => model.Details.First().IsNeedInspect));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th scope=\"col\">");
#nullable restore
#line 19 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                           Write(Html.DisplayNameFor(model => model.Details.First().InspectDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th scope=\"col\">");
#nullable restore
#line 20 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                           Write(Html.DisplayNameFor(model => model.Details.First().NextInspectDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th scope=\"col\">");
#nullable restore
#line 21 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                           Write(Html.DisplayNameFor(model => model.Details.First().IsOverdueInspect));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th scope=\"col\">");
#nullable restore
#line 22 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                           Write(Html.DisplayNameFor(model => model.Details.First().IsTemporary));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <th scope=\"col\"></th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n\r\n");
#nullable restore
#line 28 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
             foreach (var item in Model.Details)
            {
                var trid = @item.SerialNo + "tr";
                var overdueStyle = "";

                if (@item.IsOverdueInspect == "是")
                {
                    overdueStyle = "background-color:#fcdcdc";
                }


#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr");
            BeginWriteAttribute("id", " id=\"", 1904, "\"", 1914, 1);
#nullable restore
#line 38 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
WriteAttributeValue("", 1909, trid, 1909, 5, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("style", " style=\"", 1915, "\"", 1936, 1);
#nullable restore
#line 38 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
WriteAttributeValue("", 1923, overdueStyle, 1923, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                    <td id=\"partNo\" class=\"row-data\">");
#nullable restore
#line 39 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                                                Write(item.PartNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td class=\"row-data\">");
#nullable restore
#line 40 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                                    Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td id=\"serialNo\" class=\"row-data\">");
#nullable restore
#line 41 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                                                  Write(item.SerialNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td class=\"row-data\">");
#nullable restore
#line 42 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                                    Write(item.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td class=\"row-data\">");
#nullable restore
#line 43 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                                    Write(item.PurchaseId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td class=\"row-data\">");
#nullable restore
#line 44 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                                    Write(item.Spec);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td class=\"row-data\">");
#nullable restore
#line 45 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                                    Write(item.Placement);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td class=\"row-data\">");
#nullable restore
#line 46 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                                    Write(item.IsNeedInspect);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td class=\"row-data\">");
#nullable restore
#line 47 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                                    Write(item.InspectDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td class=\"row-data\">");
#nullable restore
#line 48 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                                    Write(item.NextInspectDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td class=\"row-data\">");
#nullable restore
#line 49 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                                    Write(item.IsOverdueInspect);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td class=\"row-data\">");
#nullable restore
#line 50 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                                    Write(item.IsTemporary);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>\r\n");
#nullable restore
#line 52 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                          
                            if (item.StatusId == 1)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <input");
            BeginWriteAttribute("id", " id=\"", 2903, "\"", 2922, 1);
#nullable restore
#line 55 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
WriteAttributeValue("", 2908, item.SerialNo, 2908, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" type=\"button\" onclick=\"onEditClick(this)\" style=\"text-decoration:none;margin:5px;\" class=\"btn btn-primary btn-sm\" value=\"Edit\" />\r\n");
#nullable restore
#line 56 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <input");
            BeginWriteAttribute("id", " id=\"", 3189, "\"", 3208, 1);
#nullable restore
#line 59 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
WriteAttributeValue("", 3194, item.SerialNo, 3194, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" type=\"button\" disabled style=\"text-decoration:none;margin:5px;\" class=\"btn btn-primary btn-sm\" value=\"Edit\" />\r\n");
#nullable restore
#line 60 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
                            }
                        

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <input");
            BeginWriteAttribute("id", " id=\"", 3410, "\"", 3429, 1);
#nullable restore
#line 62 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
WriteAttributeValue("", 3415, item.SerialNo, 3415, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" type=\"button\" onclick=\"onHistoryClick(this)\" style=\"text-decoration:none;margin:5px;\" class=\"btn btn-danger btn-sm\" value=\"History\" />\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 65 "D:\Repository\SpareManagement\Views\Jigs\Detail.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        </tbody>
    </table>
</div>


<script type=""text/javascript"">
    function onEditClick(e) {

        var _trid = ""#"" + e.id + ""tr"";

        var _partNo = $(_trid).find('#partNo').html();
        var _serialNo = $(_trid).find('#serialNo').html();

        var _url = ""/Jigs/Edit?partNo="" + _partNo + ""&serialNo="" + _serialNo;

        location.href = _url;
    }

    function onHistoryClick(e) {

        var _url = ""/History/History?categoryId=3&partNo="" + e.id;

        location.href = _url;

        //window.open(_url, '_blank');
    }

</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<JigsListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
