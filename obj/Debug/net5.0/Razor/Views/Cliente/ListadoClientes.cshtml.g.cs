#pragma checksum "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Cliente\ListadoClientes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "95cdd5da572100b0d2bc9d4ad1a792d400e39941"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cliente_ListadoClientes), @"mvc.1.0.view", @"/Views/Cliente/ListadoClientes.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"95cdd5da572100b0d2bc9d4ad1a792d400e39941", @"/Views/Cliente/ListadoClientes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"adb2d7e14c4a4a947023b10cfcace8d822eaec94", @"/Views/_ViewImports.cshtml")]
    public class Views_Cliente_ListadoClientes : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Cliente>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 6 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Cliente\ListadoClientes.cshtml"
      //List<Cliente> _listaClientes =  (List<Cliente>)ViewData["listaClientes"];


#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container"">
    <div class=""col"">
        <div class=""row"">
            <h1>Listado de Clientes!!!</h1>
        </div>
        <div class=""row"">

            <table class=""table table-striped  table-dark"">
                <tr>
                    <th>Nombre</th>
                    <th>Apellido</th>
                    <th>Nickname</th>
                </tr>



");
#nullable restore
#line 26 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Cliente\ListadoClientes.cshtml"
                 if (ViewData["listaClientes"] != null)
                {

                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Cliente\ListadoClientes.cshtml"
                     foreach (Cliente _cliente in (List<Cliente>)ViewData["listaClientes"])
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 32 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Cliente\ListadoClientes.cshtml"
                           Write(_cliente.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 33 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Cliente\ListadoClientes.cshtml"
                           Write(_cliente.Apellido);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 34 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Cliente\ListadoClientes.cshtml"
                           Write(_cliente.Nickname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        </tr>\r\n");
#nullable restore
#line 36 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Cliente\ListadoClientes.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <button class=\"btn-dark\" value=\"InDB\">Enviar a IndexedDB</button>\r\n");
#nullable restore
#line 38 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Cliente\ListadoClientes.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td><p class=\"text-danger\">Logueate para acceder...</p></td>\r\n                    </tr>\r\n");
#nullable restore
#line 44 "C:\Users\RogStrix_educas\source\repos\Overwatch2\Views\Cliente\ListadoClientes.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </table>\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n        </div>\r\n\r\n\r\n    </div>\r\n\r\n\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Cliente> Html { get; private set; }
    }
}
#pragma warning restore 1591
