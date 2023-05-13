using CodingMilitia.PlayBall.GroupManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using CodingMilitia.PlayBall.GroupManagement.Business.Services;
using CodingMilitia.PlayBall.GroupManagement.Web.Mappings;
using Microsoft.Extensions.Options;
using CodingMilitia.PlayBall.GroupManagement.Web.Demo;
using CodingMilitia.PlayBall.GroupManagement.Web.LoggingTool;

namespace CodingMilitia.PlayBall.GroupManagement.Web.Controllers
{
	// 终结点http://localhost:{PORT}/groups
	//       协议    网络位置（端口） 目标URI
	public class GroupsController : Controller
	{
		private readonly IGroupsService _groupsService;
		private readonly SomeRootConfiguration _config;
		private readonly SomeSecretsConfiguration _scecret;
		private readonly ILogger<GroupsController> _logger;

		public GroupsController(IGroupsService groupsService, IOptions<SomeRootConfiguration> config, IOptions<SomeSecretsConfiguration> scecret, ILogger<GroupsController> logger)
		{
			_groupsService = groupsService;
			// _config不仅能获取到 appsettings.json 中 SomeRoot 节点中的2个值，还能获取到运行时的参数携带的值 CmdLineKey	456789
			_config = config.Value;
			_scecret = scecret.Value;
			_logger = logger;
		}

		// GET: /groups/index
		public IActionResult Index()
		{
			//return Content("Hello world!");
			_logger.LogCritical("***************************Home Page Critical 严重级别***************************");
			_logger.LogWarning("***************************Home Page Warning 警告级别***************************");
			_logger.LogDebug("***************************Home Page Debug 调试级别***************************");
			_logger.LogError("***************************Home Page Error 错误级别***************************");
			_logger.LogTrace("***************************Home Page Trace 追踪级别***************************");
			// 注意：{}中的实际显示的值由后面参数位置决定，而不是{}中的名称（意味着可以任意取名）；与插值表达式有点不同；只与位置有关与名称无关
			_logger.LogInformation(MyLogEvents.GetItem,
						  " [{time}] [{id}]-{hhhh} ****Home Page Information 信息级别**********",
						  DateTime.Now.ToString("O"),
						  MyLogEvents.GetItem,
						  nameof(Index));
			return View(_groupsService.GetAll().ToViewModel());
		}

		// GET: /groups/Details/5
		public IActionResult Details(long id)
		{
			var group = _groupsService.GetById(id);
			if (group == null)
			{
				return NotFound();
			}
			return View(group.ToViewModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(long id, GroupViewModel model)
		{
			var group = _groupsService.Update(model.ToServiceModel());
			if (group == null)
			{
				return NotFound();
			}
			return RedirectToAction("Index");
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(GroupViewModel model)
		{
			_groupsService.Add(model.ToServiceModel());
			return RedirectToAction("Index");
		}
	}
}
