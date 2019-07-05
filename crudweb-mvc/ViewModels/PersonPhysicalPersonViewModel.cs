using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crudweb_mvc.ViewModels
{
    public class PersonPhysicalPersonViewModel
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
        [Required(ErrorMessage = "E-mail is empty.")]
        [StringLength(100, ErrorMessage = "The amount of character in E-mail is above the allowed value that is 100.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid E-mail.")]
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
        [DisplayName("Date Of Birth")]
        [Required(ErrorMessage = "Date Of Birth is empty.")]
        [ValidateDate]
        public DateTime DateBirth
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Genre is empty.")]
        [StringLength(1, ErrorMessage = "The amount of character in Genre is above the allowed value that is 1.")]
        [RegularExpression(@"[MF]{1}", ErrorMessage = "Invalid genre.")]
        public String Genre
        {
            get;
            set;
        }
    }
}