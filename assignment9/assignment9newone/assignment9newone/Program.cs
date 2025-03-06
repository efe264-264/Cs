
using assignment9newone.Data;
using assignment9newone.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// ���mysql����������
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("OrderDb"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("OrderDb"))
    )
);


// ע�ᶩ������
builder.Services.AddScoped<OrderService>();

// ��ӿ�������Swagger�ĵ�
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// ���Swagger����
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "��������ϵͳ API",
        Version = "v1"
    });

    c.EnableAnnotations(); // ������������ע��
    c.CustomOperationIds(apiDesc =>
        apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)
            ? methodInfo.Name
            : null); // ǿ��ʹ��OperationId
});
// �����builder.Build()֮ǰ
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();

// �����ݿ�Ǩ��
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    dbContext.Database.Migrate();
}

// ����swagger�����Բ����ӿ�
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "����ϵͳ v1");
        c.RoutePrefix = "swagger";
    });
}


app.UseHttpsRedirection();
app.UseAuthorization();

// ·������
app.MapControllers();

app.Run();