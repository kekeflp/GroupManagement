var builder = WebApplication.CreateBuilder(args);
// �������н�����Ӹ��ַ���DI�ȣ�
builder.Services.AddControllersWithViews();
// ���������õ�webӦ��
var app = builder.Build();

// ��������Ӹ�������ܵ����м���ȣ�
app.UseRouting();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles();

// Episode 004 �Զ�������/��Ӧ����ͷ
app.Use(async (context, next) =>
{
	context.Response.OnStarting(() =>
	{
		context.Response.Headers.Add("X-Powered-By", "ASP.NET Core: From 0 to master");
		return Task.CompletedTask;
	});
	await next.Invoke();
});

// ��web������
app.Run();
