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

app.UseStaticFiles();

// Episode 004 自定义请求/响应报文头
app.Use(async (context, next) =>
{
	context.Response.OnStarting(() =>
	{
		context.Response.Headers.Add("X-Powered-By", "ASP.NET Core: From 0 to master");
		return Task.CompletedTask;
	});
	await next.Invoke();
});

// 把web跑起来
app.Run();
