using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Overwatch2.Models;
using Overwatch2.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Overwatch2.Controllers
{
    public class HeroeController : Controller
    {
        #region "Config y Constructor"

        private IDBAccess __servicioSQLServer;

        public HeroeController(IDBAccess servicioInyectado)
        {
            this.__servicioSQLServer = servicioInyectado;
        }

        #endregion


        #region ...Index...


        [HttpGet]
        public IActionResult Index()
        {
            List<Heroe> _heroes = __servicioSQLServer.RecuperarListaHeroes();
            ViewData["listaHeroes"] = _heroes;

            return View("../Heroe/Index");
        }



        #endregion
        

        #region "Metodos registro"

        [HttpGet]
        [Route("Heroe/Registro")]
        public IActionResult CrearHeroe()
        {
            return View("Registro");
        }

        [HttpPost]
        [Route("Heroe/Registro")]
        public IActionResult CrearHeroe(Heroe NuevoHeroe)
        {
            //se inserta el heroe
            bool Resultado = __servicioSQLServer.CrearHeroe(NuevoHeroe);

            if (Resultado)
            {
                return View("RegistroOK");
            }
            else
            {
                ErrorViewModel _ErrorModel = new ErrorViewModel();
                return View("Error", _ErrorModel);
            }
        }

        #endregion

        

        #region "Metodos Borrado"

        [HttpGet]
        [Route("Heroe/Borrar")]
        public IActionResult BorrarHeroe()
        {
            List<Heroe> _Heroes = this.__servicioSQLServer.RecuperarListaHeroes();
            ViewData["ListaHeroes"] = _Heroes;

            return View("Borrado");
           // return View( _Heroes);
        }

        [HttpPost]
        [Route("Heroe/Borrar")]
        public IActionResult BorrarHeroe(Heroe HeroeBorrar)
        {
            //se borra el heroe
            bool Resultado = __servicioSQLServer.BorrarHeroe(HeroeBorrar.IdHeroe);

            if (Resultado)
            {
                return View("BorradoOk");
            }
            else
            {
                ErrorViewModel _ErrorModel = new ErrorViewModel();
                return View("Error", _ErrorModel);
            }
        }

        #endregion


        

        #region "Metodos Actualizado"
        //seleccionarHeroe get post, actualizarHeroe get post, actualizacionOK get

        /// <summary>
        /// Get pantalla de seleccion heroe a editar
        /// </summary>
        /// <returns>Vista con modelo de lista de Heroe</returns>
        [HttpGet]               
        [Route("Heroe/SeleccionarHeroe")]
        public IActionResult SeleccionarHeroe()  // Accedo por 1ª vez a ActualizarHeroe
        {
            List<Heroe> _Heroes = this.__servicioSQLServer.RecuperarListaHeroes();
            ViewData["ListaHeroes"] = _Heroes;

            return View("UpdateList");
            // return View( _Heroes);
        }

        /// <summary>
        /// Post pantalla de selecion de heroe a editar
        /// </summary>
        /// <param name="Heroe"></param>
        /// <returns>Redireccion a get de edicion de heroe</returns>
        [HttpPost]
        [Route("Heroe/SeleccionarHeroe")]
        public IActionResult SeleccionarHeroe(int IdHeroe)
        {
            //se rcogen los datos del heroe a editar
            Heroe _Heroe = this.__servicioSQLServer.RecuperarHeroe(IdHeroe);

            return View("UpdateForm", _Heroe);
        }

        /// <summary>
        /// Post pantalla edicion con heroe editado
        /// </summary>
        /// <param name="HeroeEditado"></param>
        /// <returns>pantalla de ok o pantalla de error</returns>
        [HttpPost]
        [Route("Heroe/EdicionHeroe")]
        public IActionResult EdicionHeroe(Heroe HeroeEditado)             ////esto sólo debe redireccionar entregando el id 
        {
            //se actualizan los datos del heroe
            bool _resAct = this.__servicioSQLServer.ActualizarHeroe(HeroeEditado);

            if (_resAct)
                return View("UpdateOK");
            else
                return View("Error");
        }

        //[HttpGet]
        //[Route("Heroe/Editar")]
        //public IActionResult actualizacionOK(Heroe HeroeActualizar)
        //{
        //    //se borra el heroe
        //    bool Resultado = __servicioSQLServer.ActualizarHeroe(HeroeActualizar.IdHeroe);

        //    if (Resultado)
        //    {
        //        return View("UpdateOK");
        //    }
        //    else
        //    {
        //        ErrorViewModel _ErrorModel = new ErrorViewModel();
        //        return View("Error", _ErrorModel);
        //    }
        //}




        /// <summary>
        /// 
        /// </summary>
        /// <param name="NuevoHeroe"></param>
        /// <returns></returns>
        /// 

        #endregion

        #region "Métodos Visionado"
        [HttpGet]
        [Route("Heroe/Ver")]
        public IActionResult DevolverHeroes()
        {
            List<Heroe> _Heroes = this.__servicioSQLServer.RecuperarListaHeroes();
            ViewData["ListaHeroes"] = _Heroes;

            return View("Ver");
            // return View( _Heroes);
        }
        #endregion


        #region "Métodos Carrito"

        [HttpPost]
        public IActionResult GuardarHeroeCarro(int IdHeroe)
        {
            try
            {
                //se recogen los datos existentes en el carrito
                List<HeroeCarrito> _HeroesCarrito = JsonSerializer.Deserialize<List<HeroeCarrito>>(HttpContext.Session.GetString("carrito"));

                //se mira si el heroe elegido no esta ya en la lista
                bool ExisteCarro = false;
                foreach (HeroeCarrito heroe in _HeroesCarrito)
                {
                    if (heroe.IdHeroe == IdHeroe)
                    {
                        //si esta se le añade 1+ y sale marcando true que existe
                        heroe.Cantidad++;
                        ExisteCarro = true;
                        break;
                    }
                }

                //si no existe en carro se recupera un modelo del heroe elegido y se añade a la lista del carrito
                if (ExisteCarro == false)
                {
                    HeroeCarrito _Heroe = JsonSerializer.Deserialize<HeroeCarrito>(
                                            JsonSerializer.Serialize(
                                                this.__servicioSQLServer.RecuperarHeroe(IdHeroe)
                                                )
                                            );
                    _Heroe.Cantidad = 1;
                    _HeroesCarrito.Add(_Heroe);
                }

                //se guarda el carrito
                HttpContext.Session.SetString("carrito", JsonSerializer.Serialize(_HeroesCarrito));

                //se vuelve a la lista de heroes
                return RedirectToAction("index");
            }
            catch (ArgumentNullException Ex)
            {
                //Si la lista esta vacia entrara aqui por excepcion, se genera lista nueva y se añade al heroe
                List<HeroeCarrito> _HeroesCarrito = new List<HeroeCarrito>();

                //Recupera Heroe
                Heroe _HeroeBD = this.__servicioSQLServer.RecuperarHeroe(IdHeroe);

                // Serializa el heroe como json
                string _HeroeJson = JsonSerializer.Serialize(_HeroeBD);

                // Deserializa el json de heroe mapeado a heroeCarrito
                HeroeCarrito _Heroe = JsonSerializer.Deserialize<HeroeCarrito>(_HeroeJson);

                //esto casca por que si aparentemente
                //HeroeCarrito _Heroe = (HeroeCarrito)this.__servicioSQLServer.RecuperarHeroe(IdHeroe);

                _Heroe.Cantidad = 1;
                _HeroesCarrito.Add(_Heroe);

                //se genera la sesion de carrito y se recarga la pagina
                HttpContext.Session.SetString("carrito", JsonSerializer.Serialize(_HeroesCarrito));
                return RedirectToAction("index");

            }
        }
        #endregion

    }



}
