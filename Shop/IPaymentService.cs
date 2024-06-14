using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public interface IPaymentService
    {
        bool ProcessPayment(string cardNumber, decimal amount);
    }
}
