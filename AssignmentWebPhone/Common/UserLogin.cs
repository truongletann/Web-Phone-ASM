using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssignmentWebPhone.Common
{
    [Serializable]
    public class UserLogin
    {
        public string UserID { set; get; }
        public string UserName { set; get; }
        public string Address { set; get; }
        public string RoleID { set; get; }
    }
}