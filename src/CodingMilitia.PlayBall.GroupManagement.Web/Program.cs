using Autofac;
using Autofac.Extensions.DependencyInjection;
using CodingMilitia.PlayBall.GroupManagement.Web.Demo;
using CodingMilitia.PlayBall.GroupManagement.Web.IoC;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
// 在容器中进行添加各种服务、DI等；
builder.Services.AddControllersWithViews();
//builder.Services.AddSingleton<IGroupsService, InMemoryGroupsSerivce>();
// 封装AddSingleton实例化服务容器
// 如需使用默认DI，放开下面的注释
//builder.Services.AddBusiness();
// 从应用运行时的参数获取值
builder.Services.Configure<SomeRootConfiguration>(builder.Configuration.GetSection("SomeRoot")); // 这种写法NO GOOD ，建议POCO
// builder.Configuration.GetSection("SomeSecrets").Get<SomeSecretsConfiguration>();
builder.Services.Configure<SomeSecretsConfiguration>(builder.Configuration.GetSection("SomeSecrets"));
// Use the three-party package implement DI -- AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(b =>
{
	b.RegisterModule<AutofacModule>();
});


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
