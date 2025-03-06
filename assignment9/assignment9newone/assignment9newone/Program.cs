
using assignment9newone.Data;
using assignment9newone.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// 添加mysql上下文配置
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("OrderDb"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("OrderDb"))
    )
);


// 注册订单服务
builder.Services.AddScoped<OrderService>();

// 添加控制器和Swagger文档
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// 添加Swagger配置
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "订单管理系统 API",
        Version = "v1"
    });

    c.EnableAnnotations(); // 新增这行启用注解
    c.CustomOperationIds(apiDesc =>
        apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)
            ? methodInfo.Name
            : null); // 强制使用OperationId
});
// 添加在builder.Build()之前
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();

// 让数据库迁移
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    dbContext.Database.Migrate();
}

// 配置swagger，可以操作接口
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "订单系统 v1");
        c.RoutePrefix = "swagger";
    });
}


app.UseHttpsRedirection();
app.UseAuthorization();

// 路由配置
app.MapControllers();

app.Run();