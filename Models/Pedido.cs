using Overwatch2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overwatch2.Models
{
    public class Pedido
    {
        #region ....propiedades clase modelo Pedido....
        public String IdPedido { get; set; }
        public String NickClientePedido { get; set; }
        public DateTime FechaPedido { get; set; }

        public Decimal TotalPedido { get; set; }
        public List<ItemPedido> ElementosPedido { get; set; }

        #endregion


        #region ....metodos clase modelo Pedido...

        public Pedido()
        {
            this.IdPedido = System.Guid.NewGuid().ToString();
            this.ElementosPedido = new List<ItemPedido>();
        }   


        private Decimal CalculoSubTotalPedido() {
            //metodo para calcular el subtotal del pedido...
            //tendria q recorrer la lista de itemsPedido cogiendo el precio de cada libro
            //multiplicarlo por la cantidad de libros y irlo acumulando
            
            Decimal __subtotal = 0;
            foreach (ItemPedido item in this.ElementosPedido)
            {
                //__subtotal += item.Cantidad * item.HeorePedido.Precio;
            }

            return __subtotal;

        }


        #endregion
    }
}
