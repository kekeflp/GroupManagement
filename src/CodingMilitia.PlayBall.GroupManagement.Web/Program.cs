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

// ��web������
app.Run();
