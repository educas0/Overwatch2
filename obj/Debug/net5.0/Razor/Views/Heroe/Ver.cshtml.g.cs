#pragma checksum "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Heroe\Ver.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a0f283aa6f56b633f10d2056f55bd22d6df76788"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Heroe_Ver), @"mvc.1.0.view", @"/Views/Heroe/Ver.cshtml")]
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
#line 1 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\_ViewImports.cshtml"
using Overwatch2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\_ViewImports.cshtml"
using Overwatch2.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a0f283aa6f56b633f10d2056f55bd22d6df76788", @"/Views/Heroe/Ver.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"adb2d7e14c4a4a947023b10cfcace8d822eaec94", @"/Views/_ViewImports.cshtml")]
    public class Views_Heroe_Ver : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div class=\"container\">\r\n\r\n    <table class=\"table table-dark\">\r\n        <tr>\r\n            <th>Nombre</th>\r\n            <th>Rol</th>\r\n            <th>Daño</th>\r\n            <th>Vida</th>\r\n            <th>Cura</th>\r\n        </tr>\r\n");
#nullable restore
#line 16 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Heroe\Ver.cshtml"
         foreach (Heroe _H in (List<Heroe>)ViewData["ListaHeroes"])
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 19 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Heroe\Ver.cshtml"
               Write(_H.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 20 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Heroe\Ver.cshtml"
               Write(_H.Rol);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 21 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Heroe\Ver.cshtml"
               Write(_H.Dano);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 22 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Heroe\Ver.cshtml"
               Write(_H.Vida);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 23 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Heroe\Ver.cshtml"
               Write(_H.Cura);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                \r\n\r\n            </tr>\r\n");
#nullable restore
#line 27 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Heroe\Ver.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </table>\r\n\r\n</div>");
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
