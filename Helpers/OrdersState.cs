using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using StackExchange.Redis;
using Stateless;

namespace Ecommerce.Helpers
{
    public class OrdersState
    {
        private StateMachine<OrderStatus, string> machine;

        public OrdersState(Orders order)
        {
            machine = new StateMachine<OrderStatus, string>(order.Order_Status);

            machine.Configure(OrderStatus.Pending)
               .Permit("Confirmed", OrderStatus.Confirmed)
               .Permit("Cancelled", OrderStatus.Cancelled)
               .Permit("Failed", OrderStatus.Failed);
            machine.Configure(OrderStatus.Confirmed)
               .Permit("Proccessing", OrderStatus.Processing);
            machine.Configure(OrderStatus.Processing)
               .Permit("Shipped", OrderStatus.Shipped);
            machine.Configure(OrderStatus.Shipped)
               .Permit("Delivered", OrderStatus.Delivered);
            machine.Configure(OrderStatus.Delivered)
               .Permit("Returned", OrderStatus.Returned);

        }
        public void TriggerEvent(string trigger)
        {
            machine.Fire(trigger);
        }

    }
}