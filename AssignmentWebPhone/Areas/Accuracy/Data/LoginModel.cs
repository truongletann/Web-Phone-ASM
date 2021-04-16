using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssignmentWebPhone.Areas.Accuracy.Data
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please input Username")]
        public string UserID { set; get; }
        [Required(ErrorMessage = "Please input Password")]
        public string Password { set; get; }
    }
}