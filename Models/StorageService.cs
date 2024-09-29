using Overwatch2.Models;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace Overwatch2.Models
{
    public class StorageService //: IStorageService
    {

        public DotNetObjectReference<StorageService> StorageSrvReference; //variable para ejecutar desde codigo js metodos de este servicio...
        public event EventHandler<Cliente> ClienteRecupIndedexDBEvent;
        public event EventHandler<List<Tuple<Cliente, int>>> ItemsRecupIndexedDBEvent;
        private IJSRuntime _js;
        

        public StorageService(IJSRuntime javascriptDI)
        {
            this.StorageSrvReference = DotNetObjectReference.Create(this);
            this._js = javascriptDI;
        }

        //-------------- esto es para ejecutar desde js un metodo del servicio cuando devuelve algo de forma asincrona----
        // necesito 2 variables: DoNetObjectReferecnce para q el fichero js sepa q metodo de vuelta ejecutar y un evento
        //q disparo desde aqui cuando recibo el valor para q lo intercepte el componente...
        public  async Task DevuelveClienteFromStorage()
        {
             await this._js.InvokeAsync<Cliente>("manageIndexedDB.devuelveCliente",this.StorageSrvReference);           
        }

        [JSInvokable("BlazorDBCallback")]
        public void CalledFromJS(Cliente cliente) {
            this.ClienteRecupIndedexDBEvent.Invoke(this,cliente);
        }

        //...para items pedido igual....
        public async Task DevuelveItemsPedidoFromStorage() {
            await this._js.InvokeAsync<Cliente>("manageIndexedDB.devuelveItemsPedido", this.StorageSrvReference);
        }

        [JSInvokable("BlazorDBCallbackItems")]
        public void CalledFromJSItems(List<Tuple<Cliente, int>> lista) {
            this.ItemsRecupIndexedDBEvent.Invoke(this, lista);
        }
        //---------------------------------------------------------------------------------

        public async Task DeleteDB()
        {
            await this._js.InvokeVoidAsync("manageIndexedDB.borrarDB");
        }

        public async Task InsertaClienteJWTInStorage(Cliente cliente, string jwt)
        {
             await this._js.InvokeVoidAsync("manageIndexedDB.almacenaClienteJWT",cliente, jwt );
        }

        public async Task<bool> IsClienteLogged()
        {
           return await this._js.InvokeAsync<bool>("manageIndexedDB.checkIsLogged");
        }

        public async Task InsertaItemtsPedidoStorage(List<Tuple<Cliente, int>> listaItems)
        {
            await this._js.InvokeVoidAsync("manageIndexedDB.almacenaItemsPedido",listaItems);
        }
    }
}
