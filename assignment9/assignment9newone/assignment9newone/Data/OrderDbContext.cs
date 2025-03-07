using Microsoft.EntityFrameworkCore;
using assignment9newone.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace assignment9newone.Data
{
    // 数据库上下文类。和MySQL数据库
  
    public class OrderDbContext : DbContext
    {
    
       
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }


        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)  // 一个订单有很多明细；每个明细属于一个订单
                .WithOne(d => d.Order)          
                .HasForeignKey(d => d.OrderId) 
                .OnDelete(DeleteBehavior.Cascade); //删除订单时，自动删除关联的所有明细
                                                  
        }
    }
}
