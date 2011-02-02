using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Lekkachara.Models;
using Service.DataAccess;
using Service.Domain;
using System.Linq;

namespace Lekkachara.Controllers
{
    public class TransactionController : Controller
    {
        private readonly Repository repository;

        public TransactionController()
        {
            repository = new Repository();
        }

        public ActionResult Index(string month,string year,int pageIndex = 1)
        {
            var transactions = repository.List<Transaction>().OrderByDescending(t => t.Id);
            int passedMonth = string.IsNullOrWhiteSpace(month) ? DateTime.Now.Month : int.Parse(month);
            int passedYear = string.IsNullOrWhiteSpace(year) ? DateTime.Now.Year: int.Parse(year);
            var currentTransactions = transactions.Where(transaction => transaction.When.Month == passedMonth && transaction.When.Year == passedYear);
            ViewData["pageIndex"] = pageIndex;
            var count = currentTransactions.Count();
            ViewData["pageCount"] = (count/Constants.PAGE_SIZE) + (count%Constants.PAGE_SIZE == 0 ? 0 : 1);
            var users = repository.List<User>().Where(user => !user.Inactive).ToList();
            var transactionModel = new TransactionModel(currentTransactions.ToList(), users,pageIndex-1);
            return View(transactionModel);
        }


        public ActionResult Details(int id)
        {
            return View(repository.Get<Transaction>(id));
        }

        public ActionResult Create()
        {
            var users = repository.List<User>().Where(user => !user.Inactive);
            ViewData["users"] = new SelectList(users, "Id", "Name");
            return View(new Transaction {When = DateTime.Now});
        }


        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            try
            {
                var transaction = new Transaction();
                UpdateModel(transaction, formCollection);
                transaction.Who = new User {Id = Convert.ToInt32(formCollection["temp"])};
                repository.Save(transaction);
                return RedirectToAction("Index");
            }
            catch
            {
                return Create();
            }
        }

        public ActionResult Edit(int id)
        {
            var transaction = repository.Get<Transaction>(id);
            var users = repository.List<User>();
            ViewData["users"] = new SelectList(users, "Id", "Name", transaction.Who.Id);
            return View(transaction);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection formCollection)
        {
            try
            {
                var transaction = new Transaction();
                UpdateModel(transaction, formCollection);
                transaction.Who = new User {Id = Convert.ToInt32(formCollection["temp"])};
                repository.Update(transaction);
                return RedirectToAction("Index");
            }
            catch
            {
                return Edit(id);
            }
        }


        public ActionResult Delete(int id)
        {
            return View(repository.Get<Transaction>(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                repository.Delete<Transaction>(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}