
using System;
using System.Collections.Generic;
using System.Linq;

namespace assignment5
{
   
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }

    public class Order : IEntity<int>
    {
        public int Id { get; }
        public string Customer { get; set; }
        public List<OrderItem> Items { get; }

        public decimal Total => Items.Sum(i => i.Price * i.Quantity);

        public Order(int id, string customer)
        {
            if (id <= 0) throw new ArgumentException("订单ID必须大于0");
            Id = id;
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Items = new List<OrderItem>();
        }

        public override bool Equals(object obj) => obj is Order order && Id == order.Id;
        public override int GetHashCode() => HashCode.Combine(Id, Customer);
    }

    public class OrderItem : IEntity<string>
    {
        public string Id => $"{ProductName}_{UnitPrice:0.##}";
        public string ProductName { get; }
        public decimal UnitPrice { get; }
        public int Quantity { get; }

        public decimal Price => UnitPrice * Quantity;

        public OrderItem(string name, decimal price, int quantity)
        {
            ProductName = name ?? throw new ArgumentNullException(nameof(name));
            UnitPrice = price > 0 ? price : throw new ArgumentException("价格必须大于0");
            Quantity = quantity > 0 ? quantity : throw new ArgumentException("数量必须大于0");
        }
    }



    public interface IRepository<T, TKey> where T : IEntity<TKey>
    {
        void Create(T entity);
        T Read(TKey id);
        void Update(T entity);
        void Delete(TKey id);
        IEnumerable<T> Find(Func<T, bool> predicate);
    }
  
    public class OrderRepository : IRepository<Order, int>
    {
        private readonly List<Order> _orders = new List<Order>();

        public void Create(Order order)
        {
            if (_orders.Any(o => o.Id == order.Id))
                throw new InvalidOperationException($"订单{order.Id}已存在");
            _orders.Add(order);
        }

        public Order Read(int id) =>
            _orders.FirstOrDefault(o => o.Id == id) ??
            throw new KeyNotFoundException($"找不到订单{id}");

        public void Update(Order updatedOrder)
        {
            var existing = Read(updatedOrder.Id);
            existing.Customer = updatedOrder.Customer;
            existing.Items.Clear();
            existing.Items.AddRange(updatedOrder.Items);
        }

        public void Delete(int id)
        {
            var order = Read(id);
            _orders.Remove(order);
        }

        public IEnumerable<Order> Find(Func<Order, bool> predicate) =>
            _orders.Where(predicate).OrderBy(o => o.Id);

        // 扩展方法：按金额范围查询
        public IEnumerable<Order> FindByAmount(decimal min, decimal max) =>
            _orders.Where(o => o.Total >= min && o.Total <= max);
    }


    class Program
    {
        static void Main(string[] args)
        {
            var repository = new OrderRepository();

            // 创建订单
            var order1 = new Order(1, "张三");
            order1.Items.Add(new OrderItem("笔记本电脑", 5999m, 1));
            repository.Create(order1);

            // 更新订单
            var updatedOrder = new Order(1, "张三");
            updatedOrder.Items.Add(new OrderItem("笔记本电脑+鼠标套餐", 6199m, 1));
            repository.Update(updatedOrder);

            // 查询订单
            var found = repository.Find(o => o.Customer.Contains("张")).ToList();
            Console.WriteLine($"找到 {found.Count} 条订单");

            // 删除订单
            repository.Delete(1);
        }
    }
}