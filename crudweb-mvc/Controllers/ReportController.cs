using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using crudweb_mvc.ViewModels;
using crudweb_mvc.Models;
using crudweb_mvc.Reports;
using crudweb_mvc.Factories;

namespace crudweb_mvc.Controllers
{
    public class ReportController : Controller
    {
        [Authorize]
        public class SumGender
        {
            public string Gender
            {
                get;
                set;
            }

            public int Sum
            {
                get;
                set;
            }
        }

        [Authorize]
        public ActionResult Index()
        {
            ViewData["OpcAVG"] = this.GetOptionsAVG();
            ViewData["OpcSalary"] = this.GetOptionsSalary();
            ViewData["OpcMonth"] = this.GetMonths();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(int opt, int? dpAVG, int? dpSalary, int? dpMonth, float? InitialSal, float? FinalSal)
        {
            if (opt == 1)
            {
                return Redirect(String.Format("~/Reports/Report.aspx?opt=1&InitialSal={0}&FinalSal={1}", InitialSal, FinalSal));
            }

            if (opt == 2)
            {
                return Redirect(String.Format("~/Reports/Report.aspx?opt=2&opc_sal={0}", dpSalary));
            }

            if (opt == 3)
            {
                return Redirect(String.Format("~/Reports/Report.aspx?opt=3&opc_avg={0}", dpAVG));
            }

            if (opt == 4)
            {
                return Redirect(String.Format("~/Reports/Report.aspx?opt=4&month={0}", dpMonth));
            }

            if (opt == 5)
            {
                return Redirect(String.Format("~/Reports/Report.aspx?opt=5"));
            }

            if (opt == 6)
            {
                return Redirect(String.Format("~/Reports/Report.aspx?opt=6"));
            }

            return HttpNotFound();
        }

        #region Queries
        [Authorize]
        public List<PhysicalPersonViewModel> GetPhysicalPerson()
        {
            try
            {
                using (dbRegistrationContext context = new dbRegistrationContext())
                {
                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                select p;

                    /*
                    List<PhysicalPersonViewModel> list = new List<PhysicalPersonViewModel>();

                    foreach (var item_p in query)
                    {
                        PhysicalPersonViewModel ppr = new PhysicalPersonViewModel();

                        ppr.Id = item_p.ID;
                        ppr.Name = item_p.NAME;
                        ppr.Email = item_p.EMAIL;

                        foreach (var item_pp in item_p.PHYSICALPERSON)
                        {
                            ppr.Salary = item_pp.SALARY;
                            ppr.Birthday = item_pp.BIRTHDAY;
                            ppr.Gender = item_pp.GENDER;
                        }

                        list.Add(ppr);
                    }
                    return list;
                    */

                    if (query != null)
                    {
                        return this.GetList(query);
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        public List<PhysicalPersonViewModel> GetPhysicalPerson_SalaryAboveAVG()
        {
            try
            {
                using (dbRegistrationContext context = new dbRegistrationContext())
                {
                    /*
                    decimal avg_sal = context.PHYSICALPERSON.Average(p => p.SALARY);

                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                where pp.SALARY > avg_sal
                                select p;
                    */

                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                where pp.SALARY > context.PHYSICALPERSON.Average(_p => _p.SALARY)
                                select p;


                    /*
                    List<PhysicalPersonViewModel> list = new List<PhysicalPersonViewModel>();

                    foreach (var item_p in query)
                    {
                        PhysicalPersonViewModel ppr = new PhysicalPersonViewModel();

                        ppr.Id = item_p.ID;
                        ppr.Name = item_p.NAME;
                        ppr.Email = item_p.EMAIL;

                        foreach (var item_pp in item_p.PHYSICALPERSON)
                        {
                            ppr.Salary = item_pp.SALARY;
                            ppr.Birthday = item_pp.BIRTHDAY;
                            ppr.Gender = item_pp.GENDER;
                        }

                        list.Add(ppr);
                    }
                    return list;
                    */

                    if (query != null)
                    {
                        return this.GetList(query);
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        public List<PhysicalPersonViewModel> GetPhysicalPerson_SalaryEqualAVG()
        {
            try
            {
                using (dbRegistrationContext context = new dbRegistrationContext())
                {
                    /*
                    decimal avg_sal = context.PHYSICALPERSON.Average(p => p.SALARY);

                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                where pp.SALARY == avg_sal
                                select p;
                    */

                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                where pp.SALARY == context.PHYSICALPERSON.Average(_p => _p.SALARY)
                                select p;

                    /*
                    List<PhysicalPersonViewModel> list = new List<PhysicalPersonViewModel>();

                    foreach (var item_p in query)
                    {
                        PhysicalPersonViewModel ppr = new PhysicalPersonViewModel();

                        ppr.Id = item_p.ID;
                        ppr.Name = item_p.NAME;
                        ppr.Email = item_p.EMAIL;

                        foreach (var item_pp in item_p.PHYSICALPERSON)
                        {
                            ppr.Salary = item_pp.SALARY;
                            ppr.Birthday = item_pp.BIRTHDAY;
                            ppr.Gender = item_pp.GENDER;
                        }

                        list.Add(ppr);
                    }
                    return list;
                    */

                    if (query != null)
                    {
                        return this.GetList(query);
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        public List<PhysicalPersonViewModel> GetPhysicalPerson_SalaryUnderAVG()
        {
            {
                try
                {
                    using (dbRegistrationContext context = new dbRegistrationContext())
                    {
                        /*
                        decimal avg_sal = context.PHYSICALPERSON.Average(p => p.SALARY);

                        var query = from p in context.PERSON
                                    join pp in context.PHYSICALPERSON
                                    on p.ID equals pp.PERSON_ID
                                    where pp.SALARY < avg_sal
                                    select p;
                        */

                        var query = from p in context.PERSON
                                    join pp in context.PHYSICALPERSON
                                    on p.ID equals pp.PERSON_ID
                                    where pp.SALARY < context.PHYSICALPERSON.Average(_p => _p.SALARY)
                                    select p;

                        /*
                        List<PhysicalPersonViewModel> list = new List<PhysicalPersonViewModel>();

                        foreach (var item_p in query)
                        {
                            PhysicalPersonViewModel ppr = new PhysicalPersonViewModel();

                            ppr.Id = item_p.ID;
                            ppr.Name = item_p.NAME;
                            ppr.Email = item_p.EMAIL;

                            foreach (var item_pp in item_p.PHYSICALPERSON)
                            {
                                ppr.Salary = item_pp.SALARY;
                                ppr.Birthday = item_pp.BIRTHDAY;
                                ppr.Gender = item_pp.GENDER;
                            }

                            list.Add(ppr);
                        }
                        return list;
                        */

                        if (query != null)
                        {
                            return this.GetList(query);
                        }
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [Authorize]
        public List<PhysicalPersonViewModel> GetPhysicalPerson_ByBirthMonth(int month)
        {
            try
            {
                using (dbRegistrationContext context = new dbRegistrationContext())
                {
                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                where pp.BIRTHDAY.Month == month
                                select p;

                    /*
                    List<PhysicalPersonViewModel> list = new List<PhysicalPersonViewModel>();

                    foreach (var item_p in query)
                    {
                        PhysicalPersonViewModel ppr = new PhysicalPersonViewModel();

                        ppr.Id = item_p.ID;
                        ppr.Name = item_p.NAME;
                        ppr.Email = item_p.EMAIL;

                        foreach (var item_pp in item_p.PHYSICALPERSON)
                        {
                            ppr.Salary = item_pp.SALARY;
                            ppr.Birthday = item_pp.BIRTHDAY;
                            ppr.Gender = item_pp.GENDER;
                        }
                        list.Add(ppr);
                    }
                    return list;
                    */

                    if (query != null)
                    {
                        return this.GetList(query);
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        public List<PhysicalPersonViewModel> GetPhysicalPerson_BySalaryRange(decimal sal1, decimal sal2)
        {
            try
            {
                using (dbRegistrationContext context = new dbRegistrationContext())
                {
                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                where pp.SALARY >= sal1 && pp.SALARY <= sal2
                                select p;

                    /*
                    List<PhysicalPersonViewModel> list = new List<PhysicalPersonViewModel>();

                    foreach (var item_p in query)
                    {
                        PhysicalPersonViewModel ppr = new PhysicalPersonViewModel();

                        ppr.Id = item_p.ID;
                        ppr.Name = item_p.NAME;
                        ppr.Email = item_p.EMAIL;

                        foreach (var item_pp in item_p.PHYSICALPERSON)
                        {
                            ppr.Salary = item_pp.SALARY;
                            ppr.Birthday = item_pp.BIRTHDAY;
                            ppr.Gender = item_pp.GENDER;
                        }

                        list.Add(ppr);
                    }
                    return list;
                    */

                    if (query != null)
                    {
                        return this.GetList(query);
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        public PhysicalPersonViewModel GetPhysicalPerson_HigherSalary()
        {
            try
            {
                using (dbRegistrationContext context = new dbRegistrationContext())
                {
                    /*
                    decimal highSal = context.PHYSICALPERSON.Max(p => p.SALARY);

                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                where pp.SALARY == highSal
                                select p;
                    */

                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                where pp.SALARY == context.PHYSICALPERSON.Max(_p => _p.SALARY)
                                select p;

                    /*
                    PhysicalPersonViewModel ppr = new PhysicalPersonViewModel();

                    foreach (var item_p in query)
                    {
                        ppr.Id = item_p.ID;
                        ppr.Name = item_p.NAME;
                        ppr.Email = item_p.EMAIL;

                        foreach (var item_pp in item_p.PHYSICALPERSON)
                        {
                            ppr.Salary = item_pp.SALARY;
                            ppr.Birthday = item_pp.BIRTHDAY;
                            ppr.Gender = item_pp.GENDER;
                        }
                    }
                    return ppr;
                    */

                    return this.GetUnique(query);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        public PhysicalPersonViewModel GetPhysicalPerson_LowerSalary()
        {
            try
            {
                using (dbRegistrationContext context = new dbRegistrationContext())
                {
                    /*
                    decimal lowSal = context.PHYSICALPERSON.Min(p => p.SALARY);

                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                where pp.SALARY == lowSal
                                select p;
                    */

                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                where pp.SALARY == context.PHYSICALPERSON.Min(_p => _p.SALARY)
                                select p;

                    /*
                    PhysicalPersonViewModel ppr = new PhysicalPersonViewModel();

                    foreach (var item_p in query)
                    {
                        ppr.Id = item_p.ID;
                        ppr.Name = item_p.NAME;
                        ppr.Email = item_p.EMAIL;

                        foreach (var item_pp in item_p.PHYSICALPERSON)
                        {
                            ppr.Salary = item_pp.SALARY;
                            ppr.Birthday = item_pp.BIRTHDAY;
                            ppr.Gender = item_pp.GENDER;
                        }
                    }
                    return ppr;
                    */

                    return this.GetUnique(query);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        public List<SumGender> GetCountPhysicalPerson_ByGenre()
        {
            try
            {
                List<SumGender> data = new List<SumGender>();

                using (dbRegistrationContext context = new dbRegistrationContext())
                {
                    var countM = context.PHYSICALPERSON.Count(p => p.GENDER == "M");
                    var countF = context.PHYSICALPERSON.Count(p => p.GENDER == "F");
                    data.Add(new SumGender() { Gender = "M", Sum = countM });
                    data.Add(new SumGender() { Gender = "F", Sum = countF });
                }
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Helpers
        private List<SelectListItem> GetOptionsAVG()
        {
            List<SelectListItem> listAVG = new List<SelectListItem>();
            listAVG.Add(new SelectListItem() { Text = "Above", Value = "1", Selected = true });
            listAVG.Add(new SelectListItem() { Text = "Equal", Value = "2" });
            listAVG.Add(new SelectListItem() { Text = "Under", Value = "3" });
            return listAVG;
        }

        private List<SelectListItem> GetOptionsSalary()
        {
            List<SelectListItem> listSalary = new List<SelectListItem>();
            listSalary.Add(new SelectListItem() { Text = "Highest", Value = "1", Selected = true });
            listSalary.Add(new SelectListItem() { Text = "Lowest", Value = "2" });
            return listSalary;
        }

        private List<SelectListItem> GetMonths()
        {
            List<SelectListItem> listMonth = new List<SelectListItem>();
            listMonth.Add(new SelectListItem() { Text = "January", Value = "1", Selected = true });
            listMonth.Add(new SelectListItem() { Text = "February", Value = "2" });
            listMonth.Add(new SelectListItem() { Text = "March", Value = "3" });
            listMonth.Add(new SelectListItem() { Text = "April", Value = "4" });
            listMonth.Add(new SelectListItem() { Text = "May", Value = "5" });
            listMonth.Add(new SelectListItem() { Text = "June", Value = "6" });
            listMonth.Add(new SelectListItem() { Text = "July", Value = "7" });
            listMonth.Add(new SelectListItem() { Text = "August", Value = "8" });
            listMonth.Add(new SelectListItem() { Text = "September", Value = "9" });
            listMonth.Add(new SelectListItem() { Text = "October", Value = "10" });
            listMonth.Add(new SelectListItem() { Text = "November", Value = "11" });
            listMonth.Add(new SelectListItem() { Text = "December", Value = "12" });
            return listMonth;
        }

        private List<PhysicalPersonViewModel> GetList(IQueryable<PERSON> query)
        {
            List<PhysicalPersonViewModel> list = new List<PhysicalPersonViewModel>();

            foreach (var q in query)
            {
                list.Add(new PhysicalPersonFactory(
                    q.ID, q.NAME, q.EMAIL, q.PHYSICALPERSON.FirstOrDefault().SALARY,
                    q.PHYSICALPERSON.FirstOrDefault().BIRTHDAY.Date,
                    q.PHYSICALPERSON.FirstOrDefault().GENDER,
                    q.PHYSICALPERSON.FirstOrDefault().PICTURE).
                    GetPhysicalPersonComplete());
            }
            return list;
        }

        private PhysicalPersonViewModel GetUnique(IQueryable<PERSON> query)
        {
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
        #endregion
    }
}