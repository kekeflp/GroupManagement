namespace CodingMilitia.PlayBall.GroupManagement.Web.Demo
{
	public class SomeSubRootConfiguration
	{
		// appsettings.json
		// "SomeKey": 123456,
		// "AnotherKey": "QWERTY"
		public int SomeKey { get; set; }
        public string AnotherKey { get; set; }

		// 通过应用启动时,作为参数传入 --CmdLineKey 456789
		public int CmdLineKey { get; set; }

	}
}
