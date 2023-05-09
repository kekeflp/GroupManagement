using CodingMilitia.PlayBall.GroupManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using CodingMilitia.PlayBall.GroupManagement.Business.Services;
using CodingMilitia.PlayBall.GroupManagement.Web.Mappings;

namespace CodingMilitia.PlayBall.GroupManagement.Web.Controllers
{
    // 终结点http://localhost:{PORT}/groups
    //       协议    网络位置（端口） 目标URI
    public class GroupsController : Controller
    {
        private readonly IGroupsService _groupsService;

        public GroupsController(IGroupsService groupsService)
        {
            _groupsService = groupsService;
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
