using Overwatch2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overwatch2.Models.Interfaces
{
    public interface IStorageService
    {
        public event EventHandler<Cliente> ClienteRecupIndedexDBEvent;
        public event EventHandler<List<Tuple<Cliente, int>>> ItemsRecupIndexedDBEvent;

        Task DeleteDB();
        Task<bool> IsClienteLogged();
        Task DevuelveClienteFromStorage();
        Task DevuelveItemsPedidoFromStorage();
        Task InsertaClienteJWTInStorage(Cliente cliente, String jwt);
        Task InsertaItemtsPedidoStorage(List<Tuple<Cliente, int>> listaItems);

    }
}
