#pragma checksum "D:\Repository\SpareManagement\Views\WirePanel\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f660f2b6bb66a487496373a5a80bb44826e24f98"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_WirePanel_Index), @"mvc.1.0.view", @"/Views/WirePanel/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f660f2b6bb66a487496373a5a80bb44826e24f98", @"/Views/WirePanel/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef1a0e23f2e4e61bf8d0ae60da25b416b1af2d9a", @"/Views/_ViewImports.cshtml")]
    public class Views_WirePanel_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""container"">
    <h2>線板材管理</h2>
    <div class=""card"">
        <div class=""card-header"" style=""background-color: slategrey; color: white; height: 40px; text-align:left;"">
            查詢
        </div>
        <div class=""card-body"">
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f660f2b6bb66a487496373a5a80bb44826e24f983787", async() => {
                WriteLiteral(@"
                <div class=""row"">
                    <div class=""form-group form-row"">
                        <label class=""col-form-label"">物料編號: </label>
                        <div>
                            <input id=""partNo"" class=""form-control col-sm-10"" type=""text""");
                BeginWriteAttribute("value", " value=\"", 563, "\"", 571, 0);
                EndWriteAttribute();
                WriteLiteral(@" />
                        </div>
                    </div>
                    <div class=""form-group form-row"">
                        <label class=""col-form-label"">名稱：</label>
                        <div>
                            <input id=""name"" class=""form-control col-sm-10"" type=""text""");
                BeginWriteAttribute("value", " value=\"", 877, "\"", 885, 0);
                EndWriteAttribute();
                WriteLiteral(@" />
                        </div>
                    </div>
                    <div class=""form-group form-row"">
                        <label class=""col-form-label"">採購料號：</label>
                        <div>
                            <input id=""purchaseId"" class=""form-control col-sm-10"" type=""text""");
                BeginWriteAttribute("value", " value=\"", 1199, "\"", 1207, 0);
                EndWriteAttribute();
                WriteLiteral(@" />
                        </div>
                    </div>
                    <div class=""form-group form-row"">
                        <input id=""btnSearch"" type=""submit"" class=""btn btn-info btn-sm"" value=""查詢"">
                    </div>
                </div>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
        </div>
    </div>
    <p></p>
    <div id=""outTable"" class=""card-body"">
    </div>
</div>

<script type=""text/javascript"">
    $(document).ready(function () {
        $('#form').submit(function (e) {
            e.preventDefault();
            $.ajax({
                type: ""GET"",
                url: ""/WirePanel/WirePanel?partNo="" + $('#partNo').val() + ""&name="" + $('#name').val() + ""&purchaseId="" + $('#purchaseId').val(),
                dataType: 'html',
                success: function (result) {
                    if (isJsonString(result)) {
                        var _res = JSON.parse(result);
                        if (result == '""""') {
                            alert('查無資料');
                            $('#outTable').empty();
                            $('#outTable').css('height', '');
                            $('#outTable').css('overflow', '');
                        }
                        if ('isException' in _res) {
                            alert");
            WriteLiteral(@"(_res.msg);
                            $('#outTable').empty();
                            $('#outTable').css('height', '');
                            $('#outTable').css('overflow', '');
                            return false;
                        }
                    }
                    else {
                        alert('查詢成功');
                        $('#outTable').html(result);
                        var height = $(window).height();
                        $('#outTable').css('height', height / 2);
                        $('#outTable').css('overflow', 'scroll');
                    }
                }
            });
        });
    });

    function onDetailClick(e) {

        var _url = ""/WirePanel/Detail?partNo="" + e.id;

        location.href = _url;

        //window.open(_url, '_blank');

        //$.ajax({
        //    type: ""GET"",
        //    url: ""/Expendables/History?categoryId=1&partNo="" + e.id,
        //    dataType: 'html',
        //    succe");
            WriteLiteral(@"ss: function (result) {
        //        if (result == """") {
        //            alert('查詢錯誤');
        //        }
        //        else {
        //        }
        //    },
        //});
    }

    function onHistoryClick(e) {

        var _url = ""/Jigs/History?categoryId=1&partNo="" + e.id;

        location.href = _url;

        //window.open(_url, '_blank');

        //$.ajax({
        //    type: ""GET"",
        //    url: ""/Expendables/History?categoryId=1&partNo="" + e.id,
        //    dataType: 'html',
        //    success: function (result) {
        //        if (result == """") {
        //            alert('查詢錯誤');
        //        }
        //        else {
        //        }
        //    },
        //});
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
