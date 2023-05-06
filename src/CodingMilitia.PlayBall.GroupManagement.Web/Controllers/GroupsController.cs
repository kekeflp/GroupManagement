using CodingMilitia.PlayBall.GroupManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingMilitia.PlayBall.GroupManagement.Web.Controllers
{
	// 终结点http://localhost:{PORT}/groups
	//       协议    网络位置（端口） 目标URI
	public class GroupsController : Controller
	{
		private static long _currentGroupId = 1;
		private static List<GroupViewModel> _groups = new List<GroupViewModel> {
			new GroupViewModel { Id=1,Name="AAA 1" }
		};

		// GET: /groups/index
		public IActionResult Index()
		{
			//return Content("Hello world!");
			return View(_groups);
		}

		// GET: /groups/Details/5
		public IActionResult Details(long? id)
		{
			var group = _groups.SingleOrDefault(x => x.Id == id);
			if (group == null)
			{
				return NotFound();
			}
			return View(group);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(long? id, GroupViewModel model)
		{
			var group = _groups.SingleOrDefault(x => x.Id == id);
			if (group == null)
			{
				return NotFound();
			}
			group.Name = model.Name;

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
			model.Id = ++_currentGroupId;
			_groups.Add(model);
			return RedirectToAction("Index");
		}
	}
}
