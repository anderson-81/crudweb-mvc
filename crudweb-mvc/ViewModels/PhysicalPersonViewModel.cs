using crudweb_mvc.ViewModels.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crudweb_mvc.ViewModels
{
    public class PhysicalPersonViewModel
    {
        public int Id
        {
            get;
            set;
        }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Name is empty.")]
        [StringLength(100, ErrorMessage = "The amount of character in Name is above the allowed value that is 100.")]
        public string Name
        {
            get;
            set;
        }

        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        //[Required(ErrorMessage = "E-mail is empty.")]
        //[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid E-mail.")]
        //[UniqueEmail(ErrorMessage = "E-mail is already registered.")]
        [StringLength(100, ErrorMessage = "The amount of character in E-mail is above the allowed value that is 100.")]
        [CheckEmail]
        public string Email
        {
            get;
            set;
        }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Required(ErrorMessage = "Salary is empty.")]
        [Range(0, 9999999999, ErrorMessage = "Salary outside the salary range.")]
        public decimal Salary
        {
            get;
            set;
        }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayName("Birthday")]
        [Required(ErrorMessage = "Birthday is empty.")]
        [CheckBirthday]
        public DateTime Birthday
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Gender is empty.")]
        [StringLength(1, ErrorMessage = "The amount of character in Gender is above the allowed value that is 1.")]
        [RegularExpression(@"[MF]{1}", ErrorMessage = "Invalid Gender.")]
        public String Gender
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Picture is empty.")]
        public byte[] Picture
        {
            get;
            set;
        }

        public PhysicalPersonViewModel()
        {
        }

        public PhysicalPersonViewModel(int id, string name, string email, decimal salary, DateTime birthday, string gender, byte[] picture)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Salary = salary;
            this.Birthday = birthday;
            this.Gender = gender;
            this.Picture = picture;
        }
    }
}