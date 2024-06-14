using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public interface IInventoryService
    {
        bool CheckStock(int procutId, int quantity);
        void ReserveStock(int productId, int quantity);
    }
}
