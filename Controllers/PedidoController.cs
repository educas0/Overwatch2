using Overwatch2.Models;
using Overwatch2.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace Overwatch2.Controllers
{
    public class PedidoController : Controller
    {
        #region ...propiedades clase PedidoController ......
        
        private IDBAccess _acccesoBD;

        #endregion

        public PedidoController(IDBAccess servicioBDInyect)
        {
            this._acccesoBD = servicioBDInyect;
            
        }





        #region ...metodos clase PedidoController .....

        #region ...metodos privados ...


        private Cliente DevuelveClienteDeSesion() {
            try
            {
                Cliente _clienteLogin = JsonSerializer.Deserialize<Cliente>(
                                        HttpContext.Session.GetString("datoscliente")
                                        );
                return _clienteLogin;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        private void AlmacenaClienteEnSesion(Cliente cliente) {
            //actualizo variable de sesion cliente....
            HttpContext.Session.SetString("datoscliente", JsonSerializer.Serialize(cliente));

        }

        #endregion


        #region ..metodos publicos del controlador, devuelven vistas...

        public void SetSession(Object _objeto)
        {

            HttpContext.Session.SetString("carrito", JsonSerializer.Serialize(_objeto));
        }

        public string GetSession()
        {
            return HttpContext.Session.GetString("carrito");
        }

        [HttpPost]
        public string Agregar([FromBody] Pedido _pedido)
        {
            

            if(GetSession() == null) {
                List<Pedido> lista = new List<Pedido>();
                lista.Add(_pedido);
                HttpContext.Session.SetString("carrito", JsonSerializer.Serialize(lista));
            }
            else
            {
                List<Pedido> lista = JsonSerializer.Deserialize<List<Pedido>>(GetSession());    //sesión "carrito"
                lista.Add(_pedido);
                HttpContext.Session.SetString("carrito", JsonSerializer.Serialize(lista));
            }

            var value = GetSession();

            return value;
        }

        [HttpGet]
        public IActionResult AddHeroePedido(int id)
        {
            //antes  de añadir el Heroe al pedido, compruebo q el cliente esta loguead
            //¿como? si existe variable de sesion "datoscliente" q se creo en el LOGIN
            try
            {
                Cliente _clienteLogin = this.DevuelveClienteDeSesion();
                //recuperar el objeto Heroe con el isbn q me pasan en parametro "id"
                //y meterlo en objeto pedido:  PedidoActual 
                //del objeto cliente recuperado de la sesion: _clienteLogin
                Heroe _heroe = this._acccesoBD.RecuperarHeroe(id);
                //Heroe _HeroeAdd = this._acccesoBD.RecuperarListaHeroes("NicknameHeroe", id).Single<Heroe>();
                if (_heroe != null)
                {

                    //compruebo si el Heroe existe ya en el pedido, para incrementar la
                    //cantidad...busco el ID del heroe a añadir en la coleccion de items
                    //del pedido, si es -1 no existe, se añade de nuevo
                    int _posLibroEnPedido = _clienteLogin
                                                    .PedidoActual
                                                    .ElementosPedido
                                                    .FindIndex((ItemPedido item) => item.HeroePedido.IdHeroe == id);

                    if (_posLibroEnPedido >= 0)
                    {
                        //ya existe...modifico su cantidad
                        _clienteLogin.PedidoActual.ElementosPedido[_posLibroEnPedido].Cantidad += 1;
                    }
                    else
                    {   //no existe...creo un itempedido nuevo
                        _clienteLogin
                            .PedidoActual
                            .ElementosPedido
                            .Add(
                                    new ItemPedido
                                    {
                                        HeroePedido = _heroe,
                                        Cantidad = 1
                                    }
                                  );

                    }

                    //actualizo variable de sesion cliente....
                    this.AlmacenaClienteEnSesion(_clienteLogin);

                    //redireccionar a vista Mostrar Pedido
                    return RedirectToAction("MostrarPedido");

                }
                else
                {
                    return RedirectToAction("Libros", "Tienda");
                }



            }
            catch (Exception ex)
            {

                return RedirectToAction("Login", "Cliente");
            }



        }


        [HttpGet]
        public IActionResult MostrarPedido()
        {

            try
            {
                TempData["ErrorServer"] = "";
                Cliente _clienteLogin = this.DevuelveClienteDeSesion();
                return View(_clienteLogin.PedidoActual);

            }
            catch (Exception ex)
            {

                return RedirectToAction("Login", "Cliente");
            }
        }

        [HttpGet]
        public IActionResult EliminarLibroPedido(int id)
        {
            try
            {
                Cliente _clienteLogin = this.DevuelveClienteDeSesion();
                ItemPedido _itemABorrar = _clienteLogin
                                               .PedidoActual
                                               .ElementosPedido
                                               .Find(
                                                    (ItemPedido item) => item.HeroePedido.IdHeroe == id
                                                );

                _clienteLogin.PedidoActual.ElementosPedido.Remove(_itemABorrar);

                //actualizo variable de sesion cliente....
                this.AlmacenaClienteEnSesion(_clienteLogin);

                if (_clienteLogin.PedidoActual.ElementosPedido.Count > 0)
                {
                    return RedirectToAction("MostrarPedido");

                }
                else
                {
                    return RedirectToAction("Libros", "Tienda");
                }
            }
            catch (Exception ex)
            {

                return RedirectToAction("Login", "Cliente");
            }
        }

        [HttpGet]
        public IActionResult SumarCantidad(int id) {
            try
            {
                Cliente _clienteLogin = this.DevuelveClienteDeSesion();
                _clienteLogin
                    .PedidoActual
                    .ElementosPedido
                    .Find((ItemPedido item) => item.HeroePedido.IdHeroe == id)
                    .Cantidad += 1;

                //actualizo variable sesion...
                this.AlmacenaClienteEnSesion(_clienteLogin);

                return RedirectToAction("MostrarPedido");
            }
            catch (Exception ex)
            {

                return RedirectToAction("Login","Cliente");
            }
        }

        [HttpGet]
        public IActionResult RestarCantidad(int id) {
            try
            {
                Cliente _clienteLogin = this.DevuelveClienteDeSesion();
                ItemPedido _itemLibro=_clienteLogin
                                        .PedidoActual
                                        .ElementosPedido
                                        .Find((ItemPedido item) => item.HeroePedido.IdHeroe == id);

                if (_itemLibro.Cantidad != 1) {

                    _itemLibro.Cantidad -= 1;

                    //actualizo variable sesion...
                    this.AlmacenaClienteEnSesion(_clienteLogin);

                } else
                {
                    //cliente cabron ha habilitado el boton - desde javascript....
                    //como queda solo uno, lo elimino....
                    return RedirectToAction("EliminarLibroPedido", new { id = id  }); 
                }

                return RedirectToAction("MostrarPedido");
            }
            catch (Exception ex)
            {

                return RedirectToAction("Login", "Cliente");
            }
        }
/*
        [HttpGet]
        public IActionResult FinalizarPedido() {

            Cliente _clienteLogueado = this.DevuelveClienteDeSesion();

            _clienteLogueado.PedidoActual.NickClientePedido = _clienteLogueado.Nickname;
            _clienteLogueado.PedidoActual.FechaPedido = DateTime.Now;
            
             * quitar

                        //1º almacenar en la bd el pedido actual
                        if (this._acccesoBD.GuardarPedido(_clienteLogueado.PedidoActual)) {
                            //2º mandar un email al cliente con el pedido detallado (factura)
                            //---generamos pdf con la factura a adjuntar en el email---
                            ChromePdfRenderer _renderPDF = new ChromePdfRenderer();

                            String _itemspedidoHTML = "<tr>";
                            _clienteLogueado.PedidoActual.ElementosPedido.ForEach(
                                   (ItemPedido item)=> {
                                       decimal _subtotal = item.LibroPedido.Precio * item.Cantidad;
                                       _itemspedidoHTML += $@"
                                                    <td>{item.LibroPedido.Titulo}</td>
                                                    <td>{item.LibroPedido.Precio} €</td>
                                                    <td>{item.Cantidad.ToString()}</td>
                                                    <td>{_subtotal} €</td>
                                                   </tr>
                                                   <tr> 
                                                ";
                                   }
                                );

                            _itemspedidoHTML += "</tr>";

                            String _facturaHTML = $@"
                                    <div>
                                        <h3><strong>RESUMEN DE TU PEDIDO con ID: {_clienteLogueado.PedidoActual.IdPedido}</strong></h3>
                                        <hr/>
                                    </div>
                                    <div>
                                        <p>Gracias por comprar en nuestra tienda. A continuacion le pasamos un desglose de su pedido: </p>
                                        <table>
                                            <tr>
                                                <td>Titulo Libro</td>
                                                <td>Precio Libro</td>
                                                <td>Cantidad Libro</td>
                                                <td>Subtotal Libro</td>
                                            </tr>
                                            {_itemspedidoHTML}
                                        </table>
                                        <hr/>
                                    </div>
                                    <div>
                                        <p><strong>SubTotal Pedido: {_clienteLogueado.PedidoActual.SubTotalPedido} €</strong></p>
                                        <p> Gastos de Envio: { _clienteLogueado.PedidoActual.GastosEnvio} € </p>
                                        <p><h3><strong>Total Pedido: {_clienteLogueado.PedidoActual.TotalPedido} €</strong></h3></p>
                                    </div>

                             ";
                            _renderPDF
                                    .RenderHtmlAsPdf(_facturaHTML)
                                    .SaveAs("factura-" + _clienteLogueado.PedidoActual.IdPedido + ".pdf");

                            //...lo mandamos por correo al email del cliente...
                            this._servicioEnvioEmail.EnviarEmail(_clienteLogueado.CredencialesCliente.Email,
                                "Pedido realizado correctamente en Agapea.com",
                                _facturaHTML,
                                "factura-" + _clienteLogueado.PedidoActual.IdPedido + ".pdf");


                            //3º redirigir al cliente al inicio con un pedido nuevo vacio
                            return View();
                        } else
                        {
                            TempData["ErrorServer"] = "Error interno del server al procesar pedido, intentelo mas tarde";
                            return RedirectToAction("MostrarPedido");
                        }
            

            
        }
*/

            
        #endregion


        #endregion
    }
}
