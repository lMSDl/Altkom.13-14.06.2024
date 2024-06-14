using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public interface IOrderRepository
    {
        void SaveOrder(Order order);
    }
}
