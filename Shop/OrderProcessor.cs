using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public class OrderProcessor
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IInventoryService _inventoryService;
        private readonly IPaymentService _paymentService;

        public OrderProcessor(IOrderRepository orderRepository, IInventoryService inventoryService, IPaymentService paymentService)
        {
            _orderRepository = orderRepository;
            _inventoryService = inventoryService;
            _paymentService = paymentService;
        }

        public bool ProcessOrder(Order order, string cardNumber)
        {
            foreach (var item in order.Items)
            {
                if(!_inventoryService.CheckStock(item.ProductId, item.Quantity))
                {
                    return false;
                }
            }

            decimal total = order.Items.Sum(x => x.Quantity * x.UnitPrice);

            if(!_paymentService.ProcessPayment(cardNumber, total))
            {
                return false;
            }

            foreach (var item in order.Items)
            {
                _inventoryService.ReserveStock(item.ProductId, item.Quantity);
            }

            _orderRepository.SaveOrder(order);
            return true;
        }
    }
}
