var builder = WebApplication.CreateBuilder(args);
// 在容器中进行添加各种服务、DI等；
builder.Services.AddControllersWithViews();
// 构建容器得到web应用
var app = builder.Build();

// 以下是添加各种请求管道、中间件等；
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 把web跑起来
app.Run();
