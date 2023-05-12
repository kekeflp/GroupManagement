using CodingMilitia.PlayBall.GroupManagement.Business.Services;
using CodingMilitia.PlayBall.GroupManagement.Impl.Services;

namespace CodingMilitia.PlayBall.GroupManagement.Web.IoC
{
	public static class SerivceCollectionExtensions
	{
		public static IServiceCollection AddBusiness(this IServiceCollection services)
		{
			services.AddSingleton<IGroupsService, InMemoryGroupsSerivce>();

			// more business service

			return services;
		}

		// 具体待研究，是否使用dotnet6以后的版本
		// 除了应用 IOptions<T> .Value的方式对配置信息进行全局注册外可以应用的另一个微软给出的组件，需要依赖两个包
		// Microsoft.Extensions.Configuration.Binder
		// Microsoft.Extensions.Options.ConfigurationExtensions
		// 就不需要像 IOptions<T>应用之前还得注册services.AddOptions()了。（多配置行代码在团队中对不熟悉又喜欢偷懒的人来说是很烦恼的。。）
		// 而且程序在启动时将配置单例注入到内存要好过IOptions的“懒加载”模式，而且避免对象初始化错误造成的运行时错误。
		public static TConfig ConfigurePOCO<TConfig>(this IServiceCollection services, IConfiguration configuration, Func<TConfig> pocoProvider) where TConfig : class
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));
			if (pocoProvider == null) throw new ArgumentNullException(nameof(pocoProvider));

			var config = pocoProvider();
			configuration.Bind(config);
			services.AddSingleton(config);
			return config;
		}

		public static TConfig ConfigurePOCO<TConfig>(this IServiceCollection services, IConfiguration configuration, TConfig config) where TConfig : class
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));
			if (config == null) throw new ArgumentNullException(nameof(config));

			configuration.Bind(config);
			services.AddSingleton(config);
			return config;
		}
	}
}
