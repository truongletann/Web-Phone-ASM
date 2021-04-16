using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class UserDAO
    {
        OnlineShopPhoneDbContext db = null;
        public UserDAO()
        {
            db = new OnlineShopPhoneDbContext();
        }
        public string Insert(tblUser entity)
        {
            db.tblUsers.Add(entity);
            db.SaveChanges();
            return entity.userID;
        }
        public tblUser GetByID(string userID)
        {
            return db.tblUsers.SingleOrDefault(x => x.userID == userID);
        }
        public bool Login(string userID, string password)
        {
            var result = db.tblUsers.Count(x => x.userID == userID && x.password == password);
            if(result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckUserID(string userID)
        {
            return db.tblUsers.Count(x => x.userID == userID) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.tblUsers.Count(x => x.email == email) > 0;
        }
    }
}
