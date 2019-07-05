using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using crudweb_mvc.ViewModels;
using crudweb_mvc.Models;
using crudweb_mvc.Reports;

namespace crudweb_mvc.Controllers
{
    public class ReportController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            List<SelectListItem> listAVG = new List<SelectListItem>();
            listAVG.Add(new SelectListItem() { Text = "Above", Value = "1", Selected = true });
            listAVG.Add(new SelectListItem() { Text = "Equal", Value = "2" });
            listAVG.Add(new SelectListItem() { Text = "Under", Value = "3" });
            ViewData["OpcAVG"] = listAVG;

            List<SelectListItem> listSalary = new List<SelectListItem>();
            listSalary.Add(new SelectListItem() { Text = "Highest", Value = "1", Selected = true });
            listSalary.Add(new SelectListItem() { Text = "Lowest", Value = "2" });
            ViewData["OpcSalary"] = listSalary;

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
            ViewData["OpcMonth"] = listMonth;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(int opt, int? dpAVG, int? dpSalary, int? dpMonth, float? InitialSal, float? FinalSal)
        {
            if (opt == 1)
            {
                return Redirect("~/Reports/Report.aspx?opt=1&InitialSal=" + InitialSal + "&FinalSal=" + FinalSal);
            }

            if(opt == 3) 
            {
                return Redirect("~/Reports/Report.aspx?opt=3&opc_avg=" + dpAVG); 
            }

            if (opt == 4)
            {
                return Redirect("~/Reports/Report.aspx?opt=4&month=" + dpMonth);
            }

            if (opt == 5)
            {
                return Redirect("~/Reports/Report.aspx?opt=5&opc_sal=" + dpSalary);
            }

            return Redirect("~/Reports/Report.aspx?opt=" + opt);
        }

        [Authorize]
        public List<PersonPhysicalPersonViewModel> GetPhysicalPerson()
        {
            try
            {
                using (dbRegistrationContext context = new dbRegistrationContext())
                {
                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                select p;

                    List<PersonPhysicalPersonViewModel> list = new List<PersonPhysicalPersonViewModel>();

                    foreach (var item_p in query)
                    {
                        PersonPhysicalPersonViewModel ppr = new PersonPhysicalPersonViewModel();

                        ppr.Id = item_p.ID;
                        ppr.Name = item_p.NAME;
                        ppr.Email = item_p.EMAIL;

                        foreach (var item_pp in item_p.PHYSICALPERSON)
                        {
                            ppr.Salary = item_pp.SALARY;
                            ppr.DateBirth = item_pp.DATEBIRTH;
                            ppr.Genre = item_pp.GENRE;
                        }

                        list.Add(ppr);
                    }
                    return list;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        public List<PersonPhysicalPersonViewModel> GetPhysicalPerson_SalaryAboveAVG()
        {
            try
            {
                using (dbRegistrationContext context = new dbRegistrationContext())
                {
                    decimal avg_sal = context.PHYSICALPERSON.Average(p => p.SALARY);

                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                where pp.SALARY > avg_sal
                                select p;

                    List<PersonPhysicalPersonViewModel> list = new List<PersonPhysicalPersonViewModel>();

                    foreach (var item_p in query)
                    {
                        PersonPhysicalPersonViewModel ppr = new PersonPhysicalPersonViewModel();

                        ppr.Id = item_p.ID;
                        ppr.Name = item_p.NAME;
                        ppr.Email = item_p.EMAIL;

                        foreach (var item_pp in item_p.PHYSICALPERSON)
                        {
                            ppr.Salary = item_pp.SALARY;
                            ppr.DateBirth = item_pp.DATEBIRTH;
                            ppr.Genre = item_pp.GENRE;
                        }

                        list.Add(ppr);
                    }
                    return list;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        public List<PersonPhysicalPersonViewModel> GetPhysicalPerson_SalaryEqualAVG()
        {
            try
            {
                using (dbRegistrationContext context = new dbRegistrationContext())
                {
                    decimal avg_sal = context.PHYSICALPERSON.Average(p => p.SALARY);

                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                where pp.SALARY == avg_sal
                                select p;

                    List<PersonPhysicalPersonViewModel> list = new List<PersonPhysicalPersonViewModel>();

                    foreach (var item_p in query)
                    {
                        PersonPhysicalPersonViewModel ppr = new PersonPhysicalPersonViewModel();

                        ppr.Id = item_p.ID;
                        ppr.Name = item_p.NAME;
                        ppr.Email = item_p.EMAIL;

                        foreach (var item_pp in item_p.PHYSICALPERSON)
                        {
                            ppr.Salary = item_pp.SALARY;
                            ppr.DateBirth = item_pp.DATEBIRTH;
                            ppr.Genre = item_pp.GENRE;
                        }

                        list.Add(ppr);
                    }
                    return list;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        public List<PersonPhysicalPersonViewModel> GetPhysicalPerson_SalaryUnderAVG()
        {
            {
                try
                {
                    using (dbRegistrationContext context = new dbRegistrationContext())
                    {
                        decimal avg_sal = context.PHYSICALPERSON.Average(p => p.SALARY);

                        var query = from p in context.PERSON
                                    join pp in context.PHYSICALPERSON
                                    on p.ID equals pp.PERSON_ID
                                    where pp.SALARY < avg_sal
                                    select p;

                        List<PersonPhysicalPersonViewModel> list = new List<PersonPhysicalPersonViewModel>();

                        foreach (var item_p in query)
                        {
                            PersonPhysicalPersonViewModel ppr = new PersonPhysicalPersonViewModel();

                            ppr.Id = item_p.ID;
                            ppr.Name = item_p.NAME;
                            ppr.Email = item_p.EMAIL;

                            foreach (var item_pp in item_p.PHYSICALPERSON)
                            {
                                ppr.Salary = item_pp.SALARY;
                                ppr.DateBirth = item_pp.DATEBIRTH;
                                ppr.Genre = item_pp.GENRE;
                            }

                            list.Add(ppr);
                        }
                        return list;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [Authorize]
        public List<PersonPhysicalPersonViewModel> GetPhysicalPerson_ByBirthMonth(int month)
        {
            try
            {
                using (dbRegistrationContext context = new dbRegistrationContext())
                {
                    decimal avg_sal = context.PHYSICALPERSON.Average(p => p.SALARY);

                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                where pp.DATEBIRTH.Month == month
                                select p;

                    List<PersonPhysicalPersonViewModel> list = new List<PersonPhysicalPersonViewModel>();

                    foreach (var item_p in query)
                    {
                        PersonPhysicalPersonViewModel ppr = new PersonPhysicalPersonViewModel();

                        ppr.Id = item_p.ID;
                        ppr.Name = item_p.NAME;
                        ppr.Email = item_p.EMAIL;

                        foreach (var item_pp in item_p.PHYSICALPERSON)
                        {
                            ppr.Salary = item_pp.SALARY;
                            ppr.DateBirth = item_pp.DATEBIRTH;
                            ppr.Genre = item_pp.GENRE;
                        }

                        list.Add(ppr);
                    }
                    return list;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        public List<PersonPhysicalPersonViewModel> GetPhysicalPerson_BySalaryRange(decimal sal1, decimal sal2)
        {
            try
            {
                using (dbRegistrationContext context = new dbRegistrationContext())
                {
                    decimal avg_sal = context.PHYSICALPERSON.Average(p => p.SALARY);

                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                where pp.SALARY >= sal1 && pp.SALARY <= sal2
                                select p;

                    List<PersonPhysicalPersonViewModel> list = new List<PersonPhysicalPersonViewModel>();

                    foreach (var item_p in query)
                    {
                        PersonPhysicalPersonViewModel ppr = new PersonPhysicalPersonViewModel();

                        ppr.Id = item_p.ID;
                        ppr.Name = item_p.NAME;
                        ppr.Email = item_p.EMAIL;

                        foreach (var item_pp in item_p.PHYSICALPERSON)
                        {
                            ppr.Salary = item_pp.SALARY;
                            ppr.DateBirth = item_pp.DATEBIRTH;
                            ppr.Genre = item_pp.GENRE;
                        }

                        list.Add(ppr);
                    }
                    return list;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        public PersonPhysicalPersonViewModel GetPhysicalPerson_HigherSalary()
        {
            try
            {
                using (dbRegistrationContext context = new dbRegistrationContext())
                {
                    decimal highSal = context.PHYSICALPERSON.Max(p => p.SALARY);

                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                where pp.SALARY == highSal
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
                            ppr.DateBirth = item_pp.DATEBIRTH;
                            ppr.Genre = item_pp.GENRE;
                        }
                    }
                    return ppr;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        public PersonPhysicalPersonViewModel GetPhysicalPerson_LowerSalary()
        {
            try
            {
                using (dbRegistrationContext context = new dbRegistrationContext())
                {
                    decimal lowSal = context.PHYSICALPERSON.Min(p => p.SALARY);

                    var query = from p in context.PERSON
                                join pp in context.PHYSICALPERSON
                                on p.ID equals pp.PERSON_ID
                                where pp.SALARY == lowSal
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
                            ppr.DateBirth = item_pp.DATEBIRTH;
                            ppr.Genre = item_pp.GENRE;
                        }
                    }
                    return ppr;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        public class SumGenre
        {
            public string Genre
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
        public List<SumGenre> GetCountPhysicalPerson_ByGenre()
        {
            try
            {
                List<SumGenre> data = new List<SumGenre>();

                using (dbRegistrationContext context = new dbRegistrationContext())
                {
                    var countM = context.PHYSICALPERSON.Count(p => p.GENRE == "M");
                    var countF = context.PHYSICALPERSON.Count(p => p.GENRE == "F");
                    data.Add(new SumGenre(){Genre = "M", Sum = countM});
                    data.Add(new SumGenre(){Genre = "F", Sum = countF});
                }
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
	}
}