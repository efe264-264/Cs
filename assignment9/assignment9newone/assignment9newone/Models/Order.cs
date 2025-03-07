using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace assignment9newone.Models
{
  
    public class Order
    {

        [Key]
        public int OrderId { get; set; }

        // 客户姓名
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        // 下单时间，记录到具体时分秒
        public DateTime OrderDate { get; set; }

    
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        
        public List<OrderDetail> OrderDetails { get; set; } = new();
    }

   
    public class OrderDetail // 订单明细具体商品信息
    {
       
        [Key]
        public int DetailId { get; set; }

   
        public int OrderId { get; set; }     // Order表的OrderId


        [Required]      // 商品名称（必填项，最多100个字）
        [StringLength(100)]
        public string ProductName { get; set; } = string.Empty;

        // 数量
        public int Quantity { get; set; }

        // 单价
        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }

        
        [ForeignKey("OrderId")] // 关联的订单对象（注！！EF Core用这个属性建立表关联）

        [JsonIgnore]// 加了JsonIgnore因为debug时返回数据时出现循环引用问题
        public Order? Order { get; set; }
    }
}
