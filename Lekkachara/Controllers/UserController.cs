using System;
using System.Web.Mvc;
using Service.DataAccess;
using Service.Domain;
using System.Linq;

namespace Lekkachara.Controllers
{
    public class UserController : Controller
    {
        private readonly Repository repository;

        public UserController()
        {
            repository = new Repository();
        }

        public ActionResult Index()
        {
            return View(repository.List<User>().OrderBy(user => user.Name));
        }

        public ActionResult Details(int id)
        {
            return View(repository.Get<User>(id));
        }

        public ActionResult Create()
        {
            return View(new User{Name = "Enter new name"});
        }


        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            try
            {
                var user = new User();
                UpdateModel(user, formCollection);
                repository.Save(user);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View(repository.Get<User>(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection formCollection)
        {
            try
            {
                var user = new User();
                UpdateModel(user, formCollection);
                repository.Update(user);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            return View(repository.Get<User>(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                repository.Delete<User>(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}