using CodingMilitia.PlayBall.GroupManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using CodingMilitia.PlayBall.GroupManagement.Business.Services;
using CodingMilitia.PlayBall.GroupManagement.Web.Mappings;
using Microsoft.Extensions.Options;
using CodingMilitia.PlayBall.GroupManagement.Web.Demo;

namespace CodingMilitia.PlayBall.GroupManagement.Web.Controllers
{
	// 终结点http://localhost:{PORT}/groups
	//       协议    网络位置（端口） 目标URI
	public class GroupsController : Controller
	{
		private readonly IGroupsService _groupsService;
		private readonly SomeRootConfiguration _config;

		public GroupsController(IGroupsService groupsService, IOptions<SomeRootConfiguration> config)
		{
			_groupsService = groupsService;
			// _config不仅能获取到 appsettings.json 中 SomeRoot 节点中的2个值，还能获取到运行时的参数携带的值 CmdLineKey	456789
			_config = config.Value;
		}

		// GET: /groups/index
		public IActionResult Index()
		{
			//return Content("Hello world!");
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
