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
	}
}
