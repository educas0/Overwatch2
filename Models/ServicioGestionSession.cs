using Overwatch2.Models.Interfaces;

using Microsoft.AspNetCore.Http;
using System.Text.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overwatch2.Models
{
    //necesito tener acceso al HttpContext <--- para tener acceso a .Session
    //q es donde esta definido el estado de sesion para el usuario

    public class ServicioGestionSession : IControlSession
    {
        private IHttpContextAccessor _httpContextService;

        public ServicioGestionSession(IHttpContextAccessor servicioHttpContextIny)
        {
            this._httpContextService = servicioHttpContextIny;
        }


        #region ....... metodos de clase .........
        public void AddItemSession<T>(string clave, T valor)
        {
            this._httpContextService
                .HttpContext
                .Session
                .SetString(clave, JsonSerializer.Serialize<T>(valor));
        }

        public T RecuperaItemSession<T>(string clave)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(
                            this._httpContextService
                                .HttpContext
                                .Session
                                .GetString(clave)
                                );
            }
            catch (Exception ex)
            {
                return default(T); //default hace q se devuelva el valor por defecto para el tipo T 
            }
        }

        #endregion
    }
}
