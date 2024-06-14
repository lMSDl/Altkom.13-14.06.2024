using AutoFixture;
using Moq;
using Shop;

namespace ShopTest
{
    public class OrderProcessorTests
    {
        [Fact]
        public void ProcessOrder_ShouldReturnFalse_WhenStockIsInsufficient()
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockInventoryService = new Mock<IInventoryService>();
            var mockPaymentService = new Mock<IPaymentService>();
            var orderProcessor = new OrderProcessor(mockOrderRepository.Object, mockInventoryService.Object, mockPaymentService.Object);

            var fixture = new Fixture();

            var order = fixture.Create<Order>();
            var cardNumber = fixture.Create<string>();

            /*var order = new Order
            {
                Items = new List<OrderItem>
            {
                new OrderItem { ProductId = 1, Quantity = 10, UnitPrice = 100 }
            }
            };*/

            mockInventoryService.Setup(s => s.CheckStock(It.IsAny<int>(), It.IsAny<int>())).Returns(false);

            // Act
            //var result = orderProcessor.ProcessOrder(order, "1234-5678-9101-1121");
            var result = orderProcessor.ProcessOrder(order, cardNumber);

            // Assert
            Assert.False(result);
            //mockPaymentService.Verify(s => s.ProcessPayment(It.IsAny<string>(), It.IsAny<decimal>()), Times.Never);
            //mockOrderRepository.Verify(r => r.SaveOrder(It.IsAny<Order>()), Times.Never);
        }

        [Fact]
        public void ProcessOrder_ShouldReturnFalse_WhenPaymentFails()
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockInventoryService = new Mock<IInventoryService>();
            var mockPaymentService = new Mock<IPaymentService>();
            var orderProcessor = new OrderProcessor(mockOrderRepository.Object, mockInventoryService.Object, mockPaymentService.Object);

            var order = new Order
            {
                Items = new List<OrderItem>
            {
                new OrderItem { ProductId = 1, Quantity = 10, UnitPrice = 100 }
            }
            };

            mockInventoryService.Setup(s => s.CheckStock(1, 10)).Returns(true);
            mockPaymentService.Setup(s => s.ProcessPayment("1234-5678-9101-1121", 1000)).Returns(false);

            // Act
            var result = orderProcessor.ProcessOrder(order, "1234-5678-9101-1121");

            // Assert
            Assert.False(result);
            mockOrderRepository.Verify(r => r.SaveOrder(It.IsAny<Order>()), Times.Never);
            mockInventoryService.Verify(s => s.ReserveStock(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ProcessOrder_ShouldReturnTrue_WhenAllConditionsAreMet()
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockInventoryService = new Mock<IInventoryService>();
            var mockPaymentService = new Mock<IPaymentService>();
            var orderProcessor = new OrderProcessor(mockOrderRepository.Object, mockInventoryService.Object, mockPaymentService.Object);

            var order = new Order
            {
                Items = new List<OrderItem>
            {
                new OrderItem { ProductId = 1, Quantity = 10, UnitPrice = 100 }
            }
            };

            mockInventoryService.Setup(s => s.CheckStock(1, 10)).Returns(true);
            mockPaymentService.Setup(s => s.ProcessPayment("1234-5678-9101-1121", 1000)).Returns(true);

            // Act
            var result = orderProcessor.ProcessOrder(order, "1234-5678-9101-1121");

            // Assert
            Assert.True(result);
            mockOrderRepository.Verify(r => r.SaveOrder(order), Times.Once);
            mockInventoryService.Verify(s => s.ReserveStock(1, 10), Times.Exactly(order.Items.Count));
        }
    }
}