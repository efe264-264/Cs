using System;
using System.Collections.Generic;
using System.Linq;

namespace assignment6_old
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }

        public decimal TotalAmount => OrderDetails.Sum(od => od.Amount);

        public Order(int orderId, string customerName)
        {
            OrderId = orderId;
            CustomerName = customerName;
            OrderDetails = new List<OrderDetails>();
        }

        public override bool Equals(object obj)
        {
            return obj is Order order && OrderId == order.OrderId;
        }

        public override int GetHashCode()
        {
            unchecked // 允许整数溢出
            {
                int hash = 17;
                hash = hash * 23 + OrderId.GetHashCode();
                hash = hash * 23 + (CustomerName?.GetHashCode() ?? 0);
                return hash;
            }
        }

        public override string ToString()
        {
            return $"订单号: {OrderId}, 客户: {CustomerName}, 总金额: {TotalAmount:C}";
        }
    }

    public class OrderDetails
    {
        public string ProductName { get; set; }
        public decimal Amount { get; set; }

        public OrderDetails(string productName, decimal amount)
        {
            ProductName = productName;
            Amount = amount;
        }

        public override bool Equals(object obj)
        {
            return obj is OrderDetails details &&
                   ProductName == details.ProductName &&
                   Amount == details.Amount;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (ProductName?.GetHashCode() ?? 0);
                hash = hash * 23 + Amount.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{ProductName} - {Amount:C}";
        }
    }

    public class OrderService
    {
        private List<Order> _orders;

        public OrderService()
        {
            _orders = new List<Order>();
        }

        public void AddOrder(Order order)
        {
            if (_orders.Any(o => o.OrderId == order.OrderId))
            {
                throw new Exception("订单已存在");
            }
            _orders.Add(order);
        }

        public void RemoveOrder(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new Exception("订单不存在");
            }
            _orders.Remove(order);
        }

        public void UpdateOrder(int orderId, string customerName)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new Exception("订单不存在");
            }
            order.CustomerName = customerName;
        }

        public List<Order> QueryOrders(Func<Order, bool> predicate)
        {
            return _orders.Where(predicate).OrderBy(o => o.TotalAmount).ToList();
        }

        public void SortOrders(Func<Order, object> orderBy = null)
        {
            _orders = (orderBy == null)
                ? _orders.OrderBy(o => o.OrderId).ToList()
                : _orders.OrderBy(orderBy).ToList();
        }

        public List<Order> GetAllOrders()
        {
            return _orders;
        }
    }

    class Program
    {
        static void Main()
        {
            var orderService = new OrderService();

            try
            {
                // 使用显式类型初始化
                var order1 = new Order(1, "Elon Musk");
                order1.OrderDetails.Add(new OrderDetails("SpaceX", 25000.00m));
                order1.OrderDetails.Add(new OrderDetails("Cybertruck", 69900.00m));

                var order2 = new Order(2, "Marie Curie");
                order2.OrderDetails.Add(new OrderDetails("移液枪", 999.00m));
                order2.OrderDetails.Add(new OrderDetails("防护服", 45.00m));

                orderService.AddOrder(order1);
                orderService.AddOrder(order2);

                Console.WriteLine("按客户查询订单：");
                var orders = orderService.QueryOrders(o => o.CustomerName == "Elon Musk");
                foreach (var o in orders) Console.WriteLine(o);

                orderService.UpdateOrder(1, "Nikola Tesla"); 
                Console.WriteLine("修改后的订单：");
                Console.WriteLine(orderService.GetAllOrders()[0]);

                orderService.RemoveOrder(1);
                Console.WriteLine("删除后的订单列表：");
                foreach (var o in orderService.GetAllOrders()) Console.WriteLine(o);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"错误: {ex.Message}");
            }
        }
    }
}