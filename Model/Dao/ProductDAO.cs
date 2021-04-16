using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.Dao
{
    public class ProductDAO
    {
        OnlineShopPhoneDbContext db = null;
        public ProductDAO()
        {
            db = new OnlineShopPhoneDbContext();
        }
        public IEnumerable<tblProduct> ListAllPaging(int page, int pageSize, string search)
        {
            search = search ?? "";
           
            return db.tblProducts.OrderByDescending(x=>x.name)
                .Where(x => 
                                           x.name.Contains(search))
                .ToPagedList(page,pageSize);
        }
    }
}
