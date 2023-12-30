using crudweb_mvc.Models;
using crudweb_mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crudweb_mvc.Factories
{
    public class PhysicalPersonFactory
    {
        private PhysicalPersonViewModel pp = null;

        private PERSON P = null;

        private PHYSICALPERSON PP = null;


        public PhysicalPersonFactory()
        {
            pp = new PhysicalPersonViewModel();
        }

        public PhysicalPersonFactory(int id, string name, string email, decimal salary, DateTime birthday, string gender, byte[] picture)
        {
            pp = new PhysicalPersonViewModel(id, name, email, salary, birthday, gender, picture);
        }

        public PhysicalPersonFactory(PhysicalPersonViewModel pp)
        {
            this.pp = pp;
        }

        public PhysicalPersonFactory(PERSON P)
        {
            this.P = P;
        }

        public PhysicalPersonFactory(PHYSICALPERSON PP)
        {
            this.PP = PP;
        }

        public PERSON GetPERSON()
        {
            return new PERSON()
            {
                ID = pp.Id,
                NAME = pp.Name,
                EMAIL = pp.Email
            };
        }

        public PHYSICALPERSON GetPHYSICALPERSON()
        {
            return new PHYSICALPERSON()
            {
                PERSON_ID = pp.Id,
                ID = pp.Id,
                SALARY = pp.Salary,
                BIRTHDAY = pp.Birthday,
                GENDER = pp.Gender,
                PICTURE = pp.Picture != null ? pp.Picture : null
            };
        }

        public PhysicalPersonViewModel GetPhysicalPersonComplete()
        {
            return this.pp;
        }
    }
}