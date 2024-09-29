using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Overwatch2.Models;
using Overwatch2.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace Overwatch2.Controllers
{
    public class ClienteController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private IHttpContextAccessor _contextoHttp;
        private IDBAccess _servicioSqlServer;
        private IControlSession _servicioSession;
        

        public ClienteController(ILogger<HomeController> logger,
                                Models.Interfaces.IDBAccess servicioInyectado,
                                IHttpContextAccessor contextoHttp,
                                IControlSession servicioSessionInyectado)
        {
            _logger = logger;
            _servicioSqlServer = servicioInyectado;
            _contextoHttp = contextoHttp;
            _servicioSession = servicioSessionInyectado;
            
        }


        #region ...LOGIN...
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Cliente _logged)
        {
            //Acceder a DB para comprobar que Nickname y Password son correctos

            Cliente _cliente = this._servicioSqlServer.ComprobarLogin(_logged.Nickname, _logged.Password);

            
            if (_cliente != null)
            {

                //se rcogen los datos del heroe con el que haremos la sesión
                _cliente = this._servicioSqlServer.DevuelveCliente_porNickname(_logged.Nickname);


                // almacenar datos en variable sesion....
                HttpContext.Session.SetString("datoscliente", JsonSerializer.Serialize(_cliente));//Ya hay sesión!

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["error"] = "Error surgió";
                return View("Login");
            }


        }
        #endregion


        #region ...Registro...
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

            [HttpPost]
        public IActionResult Registro(Cliente _logged)
        {
            //Acceder a DB para comprobar que Nickname y Password son correctos

            bool _resultado = this._servicioSqlServer.InsertarCliente(_logged);


            if (_resultado )
            {

                return RedirectToAction("RegistroOK", "Cliente");
            }
            else
            {
                ViewData["error"] = "Error surgió al intentar Registrar el usuario";
                return View("../Home/Index");
            }


        }

        [HttpGet]
        public IActionResult RegistroOK()
        {
            return View();
        }
        #endregion



        #region ...VISIONADO...
        [HttpGet]
        public IActionResult ListadoClientes()
        {
            //Vamos a pasarle una lista de todos los objetos cliente de la BD a la vista ListadoClientes a través de ViewData

            //Crear lista y darle valor con el resultado de devolverListaClientes()


            Cliente _clienteLogin = _servicioSession.RecuperaItemSession<Cliente>("datoscliente");

            if (_clienteLogin != null)
            {
                Console.WriteLine("pasa por ListadoClientes() -" + _contextoHttp.HttpContext + "-" );




            List<Cliente> _clientesDevueltos = this._servicioSqlServer.DevolverListaClientes();
            ViewData["listaClientes"] = _clientesDevueltos;

            }



            return View();
        }
        #endregion

        #region ...ACTUALIZACIÓN...
        // pinta el select de clientes
        [HttpGet]
        public IActionResult ActualizaCliente()
        {
            //acceder a la BD para coger todos los clientes
            List<Cliente> _clientesDevueltos = this._servicioSqlServer.DevolverListaClientes();
            ViewData["listaClientes"] = _clientesDevueltos;
            return View();
        }

        // pinta el formulario con los datos actuales del cliente
        [HttpPost]
        public IActionResult ActualizaCliente(string Nickname)
        {
            Cliente _clienteActualizar = this._servicioSqlServer.DevuelveCliente_porNickname(Nickname);
            ViewData["clienteSeleccionado"] = _clienteActualizar;

            return View("ActualizaClienteForm", _clienteActualizar);
        }

        //actualiza los datos de x cliente
        [HttpPost]
        public IActionResult ActualizaCliente2(Cliente _cliente)
        {

            bool actualizado = this._servicioSqlServer.ActualizarCliente(_cliente);

            if (actualizado)
            {
                return View("ActualizaClienteOK");
            }
            else
            {

                ViewData["error"] = "Error al actualizar";
                return View("../Home/Privacy");
            }

        }

        #endregion

        #region ...BORRADO...

        [HttpGet]
        public IActionResult BorradoCliente()
        {


            List<Cliente> _clientesDevueltos = this._servicioSqlServer.DevolverListaClientes();
            ViewData["listaClientes"] = _clientesDevueltos;

            return View();
        }


        [HttpPost]
        public IActionResult BorradoCliente(string Nickname)
        {
            bool borrado = this._servicioSqlServer.BorrarCliente(Nickname);

            if (borrado)
            {
                return View("BorradoClienteOK");
            }
            else
            {
                ViewData["error"] = "Algo falló al borrar...";
                return View("../Home/Privacy");
            }

            
        }
            #endregion
        }



}
