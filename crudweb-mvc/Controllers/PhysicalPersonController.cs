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
using crudweb_mvc.Factories;

namespace crudweb_mvc.Controllers
{
    public class PhysicalPersonController : Controller
    {
        #region Attributes
        dbRegistrationContext context = new dbRegistrationContext();
        private List<PhysicalPersonViewModel> list;
        public static bool statusClearMessage = false;
        #endregion

        [Authorize]
        public ActionResult Index(int? p_numPage)
        {
            try
            {
                int sizePage = 10;
                int numPage = p_numPage ?? 1;
                list = this.GetAllPhysicalPerson();
                ViewData["orders"] = GetOrdersHelper();
                return View(list.OrderBy(p => p.Name).ToPagedList(numPage, sizePage));
            }
            catch (Exception)
            {
                this.CreateMessage("Error searching.", "Error", 1);
                throw;
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(int? p_numPage, string searchName, string opc)
        {
            try
            {
                ViewData["orders"] = GetOrdersHelper();

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
                        return View(list.Where(p => p.Name.StartsWith(searchName)).OrderBy(p => p.Birthday).ToPagedList(numPage, sizePage));
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
                        return View(list.OrderBy(p => p.Birthday).ToPagedList(numPage, sizePage));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        public ActionResult Create()
        {
            this.SetDate18();
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, Name, Email, Salary, Birthday, Gender")] PhysicalPersonViewModel pp, HttpPostedFileBase picture)
        {
            try
            {
                ModelState.Remove("Picture");

                if (ModelState.IsValid)
                {
                    if (picture != null)
                    {
                        pp.Picture = new byte[picture.ContentLength];
                        picture.InputStream.Read(pp.Picture, 0, picture.ContentLength);
                    }
                    else
                    {
                        ModelState.AddModelError("Picture", "The Picture is required.");
                        return View(pp);
                    }

                    this.Create(pp);
                    this.CreateMessage("Successfully created.", "Information", 1);
                    return RedirectToAction("Index");
                }
                return View(pp);
            }
            catch (Exception)
            {
                this.CreateMessage("Error creating.", "Error", 1);
                throw;
            }
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

                    ViewData["Genders"] = this.GetGenders();

                    PhysicalPersonViewModel pp = this.GetPhysicalPersonForID(id);

                    if (pp == null)
                    {
                        return HttpNotFound();
                    }

                    ViewData["picture"] = this.ConvertPictureToString(pp);

                    return View(pp);
                }
                catch (Exception)
                {
                    this.CreateMessage("Error editing.", "Error", 1);
                    throw;
                }
            }
            else
            {
                try
                {
                    PhysicalPersonViewModel pp = this.GetPhysicalPersonForID(id);

                    if (pp == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        this.CreateMessage("Successfully deleted.", "Information", 1);
                        this.Delete(pp);
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    this.CreateMessage("Error deleting.", "Error", 1);
                    throw;
                }
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name, Email, Salary, Birthday, Gender")] PhysicalPersonViewModel pp, HttpPostedFileBase picture)
        {
            try
            {
                ModelState.Remove("Picture");

                PhysicalPersonViewModel test = this.GetPhysicalPersonForID(pp.Id);

                if(test.Email == pp.Email)
                {
                    ModelState.Remove("Email");
                }

                if (ModelState.IsValid)
                {
                    if (picture != null)
                    {
                        pp.Picture = new byte[picture.ContentLength];
                        picture.InputStream.Read(pp.Picture, 0, picture.ContentLength);
                    }
                    
                    this.Edit(pp);
                    this.CreateMessage("Successfully edited.", "Information", 1);
                    return RedirectToAction("Index");
                }
                return View(pp);
            }
            catch (Exception)
            {
                this.CreateMessage("Error editing.", "Error", 1);
                throw;
            }
        }

        #region Helpers
        private List<SelectListItem> GetOrdersHelper()
        {
            List<SelectListItem> orders = new List<SelectListItem>();
            orders.Add(new SelectListItem() { Text = "Name", Value = "name", Selected = true });
            orders.Add(new SelectListItem() { Text = "Birthday", Value = "date" });
            return orders;
        }

        private List<SelectListItem> GetGenders()
        {
            List<SelectListItem> genders = new List<SelectListItem>();
            genders.Add(new SelectListItem() { Value = "M", Text = "Male", Selected = true });
            genders.Add(new SelectListItem() { Value = "F", Text = "Female" });
            return genders;
        }

        private void SetDate18()
        {
            ViewData["date18"] = DateTime.Now.AddYears(-18);
        }

        private object ConvertPictureToString(PhysicalPersonViewModel pp)
        {
            var base64 = Convert.ToBase64String(pp.Picture);
            var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
            return imgSrc;
        }
        #endregion

        #region Queries
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

        public List<PhysicalPersonViewModel> GetAllPhysicalPerson()
        {
            try
            {
                var query = from p in context.PERSON
                            join pp in context.PHYSICALPERSON
                            on p.ID equals pp.PERSON_ID
                            select p;

                if (query != null)
                {
                    list = new List<PhysicalPersonViewModel>();

                    foreach (var q in query)
                    {
                        list.Add(new PhysicalPersonFactory(
                            q.ID, q.NAME, q.EMAIL, q.PHYSICALPERSON.FirstOrDefault().SALARY,
                            DateTime.Parse(q.PHYSICALPERSON.FirstOrDefault().BIRTHDAY.ToString().Replace("00:00:00", "")),
                            q.PHYSICALPERSON.FirstOrDefault().GENDER,
                            q.PHYSICALPERSON.FirstOrDefault().PICTURE).
                            GetPhysicalPersonComplete());
                    }

                    return list;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private PhysicalPersonViewModel GetPhysicalPersonForID(int? id)
        {
            try
            {
                IQueryable<PERSON> query = from p in context.PERSON
                                           join pp in context.PHYSICALPERSON
                                           on p.ID equals pp.PERSON_ID
                                           where p.ID == id
                                           select p;

                if (query != null)
                {
                    return new PhysicalPersonFactory(
                        query.FirstOrDefault().ID, query.FirstOrDefault().NAME,
                        query.FirstOrDefault().EMAIL,
                        query.FirstOrDefault().PHYSICALPERSON.FirstOrDefault().SALARY,
                        query.FirstOrDefault().PHYSICALPERSON.FirstOrDefault().BIRTHDAY.Date,
                        query.FirstOrDefault().PHYSICALPERSON.FirstOrDefault().GENDER,
                        query.FirstOrDefault().PHYSICALPERSON.FirstOrDefault().PICTURE).GetPhysicalPersonComplete();
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Editions
        private bool Create(PhysicalPersonViewModel pp)
        {
            try
            {
                pp.Id = this.GenerateId();
                context.PERSON.Add(new PhysicalPersonFactory(pp).GetPERSON());
                context.PHYSICALPERSON.Add(new PhysicalPersonFactory(pp).GetPHYSICALPERSON());
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Edit(PhysicalPersonViewModel pp)
        {
            try
            {
                // REVER CÓDIGO:

                PERSON P = context.PERSON.Find(pp.Id);
                P.ID = pp.Id;
                P.NAME = pp.Name;
                P.EMAIL = pp.Email;
                P.PHYSICALPERSON.FirstOrDefault().GENDER = pp.Gender;
                if(pp.Picture != null)
                {
                    P.PHYSICALPERSON.FirstOrDefault().PICTURE = pp.Picture;
                }
                P.PHYSICALPERSON.FirstOrDefault().BIRTHDAY = pp.Birthday;
                P.PHYSICALPERSON.FirstOrDefault().SALARY = pp.Salary;
                
                context.Entry(P).State = EntityState.Modified;
                context.Entry(P.PHYSICALPERSON.FirstOrDefault()).State = EntityState.Modified;

                context.SaveChanges();
               
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Delete(PhysicalPersonViewModel pp)
        {
            try
            {
                context.PHYSICALPERSON.Remove(context.PHYSICALPERSON.Find(pp.Id));
                context.PERSON.Remove(context.PERSON.Find(pp.Id));
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

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

        private void CreateMessage(string message, string title, int show)
        {
            TempData["message"] = message;
            TempData["title"] = title;
            TempData["show"] = show;
            PhysicalPersonController.statusClearMessage = false;
        }
    }
}
