using Autofac;
using CodingMilitia.PlayBall.GroupManagement.Business.Services;
using CodingMilitia.PlayBall.GroupManagement.Impl.Services;

namespace CodingMilitia.PlayBall.GroupManagement.Web.IoC
{
	public class AutofacModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<InMemoryGroupsSerivce>().As<IGroupsService>().SingleInstance();

			// more business service

		}
	}
}
