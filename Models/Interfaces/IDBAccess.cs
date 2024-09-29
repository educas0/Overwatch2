using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Overwatch2.Models.Interfaces
{
    public interface IDBAccess
    {

        #region CLIENTE
        public Cliente ComprobarLogin(String Nickname, String Password);

        public bool InsertarCliente(Cliente _logged);

        public List<Cliente> DevolverListaClientes();

        public Cliente DevuelveCliente_porNickname(string Nickname);

        public bool ActualizarCliente(Cliente _cliente);

        public bool BorrarCliente(string Nickname);


        public Cliente RecuperarCliente(String Nickname);
        #endregion

        //------------------------------------------------------------------------//

        #region HÉROE

        public List<Heroe> RecuperarListaHeroes();

        public Heroe RecuperarHeroe(int IdHeroe);


        public bool BorrarHeroe(int idHeroe);

        public bool CrearHeroe(Heroe newHero);
        public bool ActualizarHeroe(Heroe Heroe);


        #endregion




    }
}
