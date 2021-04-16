using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssignmentWebPhone.Areas.Accuracy.Data
{
    public class RegisterModel
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage ="Please input username")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Please input fullname")]
        public string FullName { set; get; }
        [Required(ErrorMessage = "Please input email")]
        public string EmailAddress { set; get; }
        [Required(ErrorMessage = "Please input address")]
        public string Address { set; get; }
        [Required(ErrorMessage = "Please input password")]
        [Compare("Password",ErrorMessage ="Compare not correct")]
        public string Password { set; get; }
        [Required(ErrorMessage = "Please input confirmpassword")]
        public string ConfirmPassword { set; get; }
    }
}