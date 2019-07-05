using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using crudweb_mvc.Models;
using System.Net;
using crudweb_mvc.ViewModels;
using System.Data.Entity;
using PagedList;
using System.Web.UI;

namespace crudweb_mvc.Controllers
{
    public class PersonPhysicalPersonController : Controller
    {
        dbRegistrationContext context = new dbRegistrationContext();
        private static List<PersonPhysicalPersonViewModel> list;

        [Authorize]
        public ActionResult Index(int? p_numPage)
        {
            try
            {
                int sizePage = 10;
                int numPage = p_numPage ?? 1;

                var query = from p in context.PERSON
                            join pp in context.PHYSICALPERSON
                            on p.ID equals pp.PERSON_ID
                            select p;

                list = new List<PersonPhysicalPersonViewModel>();

                foreach (var item_p in query)
                {
                    PersonPhysicalPersonViewModel ppr = new PersonPhysicalPersonViewModel();

                    ppr.Id = item_p.ID;
                    ppr.Name = item_p.NAME;
                    ppr.Email = item_p.EMAIL;

                    foreach (var item_pp in item_p.PHYSICALPERSON)
                    {
                        ppr.Salary = item_pp.SALARY;
                        ppr.DateBirth = DateTime.Parse(item_pp.DATEBIRTH.ToString().Replace("00:00:00", ""));
                        ppr.Genre = item_pp.GENRE;
                    }

                    list.Add(ppr);
                }

                List<SelectListItem> orders = new List<SelectListItem>();
                orders.Add(new SelectListItem() { Text = "Name", Value = "name", Selected = true });
                orders.Add(new SelectListItem() { Text = "Date Of Birth", Value = "date" });
                ViewData["orders"] = orders;

                return View(list.OrderBy(p => p.Name).ToPagedList(numPage, sizePage));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(int? p_numPage, string searchName, string opc)
        {
            try
            {
                List<SelectListItem> orders = new List<SelectListItem>();
                orders.Add(new SelectListItem() { Text = "Name", Value = "name", Selected = true });
                orders.Add(new SelectListItem() { Text = "Date Of Birth", Value = "date" });
                ViewData["orders"] = orders;

                int sizePage = 10;
                int numPage = p_numPage ?? 1;

                if (!String.IsNullOrEmpty(searchName))
                {
                    if ((opc == null) || (opc == "name") || (opc == String.Empty))
                    {
                        return View(list.Where(p => p.Name.StartsWith(searchName)).OrderBy(p => p.Name).ToPagedList(numPage, sizePage));
                    }
                    else
                    {
                        return View(list.Where(p => p.Name.StartsWith(searchName)).OrderBy(p => p.DateBirth).ToPagedList(numPage, sizePage));
                    }
                }
                else
                {
                    if ((opc == null) || (opc == "name") || (opc == String.Empty))
                    {
                        return View(list.OrderBy(p => p.Name).ToPagedList(numPage, sizePage));
                    }
                    else
                    {
                        return View(list.OrderBy(p => p.DateBirth).ToPagedList(numPage, sizePage));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private PersonPhysicalPersonViewModel Get_Person_PhysicalPersonForID(int? id)
        {
            try
            {
                var query = from p in context.PERSON
                            join pp in context.PHYSICALPERSON
                            on p.ID equals pp.PERSON_ID
                            where p.ID == id
                            select p;

                PersonPhysicalPersonViewModel ppr = new PersonPhysicalPersonViewModel();

                foreach (var item_p in query)
                {
                    ppr.Id = item_p.ID;
                    ppr.Name = item_p.NAME;
                    ppr.Email = item_p.EMAIL;

                    foreach (var item_pp in item_p.PHYSICALPERSON)
                    {
                        ppr.Salary = item_pp.SALARY;
                        ppr.DateBirth = DateTime.Parse(item_pp.DATEBIRTH.ToString().Replace("00:00:00", ""));
                        ppr.Genre = item_pp.GENRE;
                    }
                }

                return ppr;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewData["date18"] = DateTime.Now.AddYears(-18);
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Salary,DateBirth,Genre")] PersonPhysicalPersonViewModel person)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = this.GenerateId();

                    context.PERSON.Add(new PERSON()
                    {
                        ID = id,
                        NAME = person.Name,
                        EMAIL = person.Email
                    });

                    context.PHYSICALPERSON.Add(new PHYSICALPERSON()
                    {
                        PERSON_ID = id,
                        ID = id,
                        SALARY = person.Salary,
                        DATEBIRTH = person.DateBirth,
                        GENRE = person.Genre
                    });

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(person);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int GenerateId()
        {
            int id = 0;

            try
            {
                id = context.PERSON.Max(p => p.ID + 1);
            }
            catch (Exception)
            {
                id = 1;
            }

            return id;
        }

        [Authorize]
        public ActionResult Edit(int? id, int? opc)
        {
            if (opc == 1)
            {
                try
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    List<SelectListItem> genres = new List<SelectListItem>();
                    genres.Add(new SelectListItem() { Value = "M", Text = "Male", Selected = true });
                    genres.Add(new SelectListItem() { Value = "F", Text = "Female" });
                    ViewData["Genres"] = genres;

                    PersonPhysicalPersonViewModel person = this.Get_Person_PhysicalPersonForID(id);
                    if (person == null)
                    {
                        return HttpNotFound();
                    }

                    return View(person);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                try
                {
                    PHYSICALPERSON pp = context.PHYSICALPERSON.Find(id);
                    PERSON pes = context.PERSON.Find(id);

                    context.PHYSICALPERSON.Remove(pp);
                    context.PERSON.Remove(pes);

                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Salary,DateBirth,Genre")] PersonPhysicalPersonViewModel person)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PERSON pes = new PERSON()
                    {
                        ID = person.Id,
                        NAME = person.Name,
                        EMAIL = person.Email
                    };

                    PHYSICALPERSON pp = new PHYSICALPERSON()
                    {
                        ID = person.Id,
                        PERSON_ID = person.Id,
                        SALARY = person.Salary,
                        DATEBIRTH = person.DateBirth,
                        GENRE = person.Genre
                    };

                    context.PERSON.Add(pes);
                    context.Entry(pes).State = EntityState.Modified;

                    context.PHYSICALPERSON.Add(pp);
                    context.Entry(pp).State = EntityState.Modified;

                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(person);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    context.Dispose();
                }
                base.Dispose(disposing);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
