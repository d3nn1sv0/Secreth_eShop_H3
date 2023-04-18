using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop_DAL.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(EShopDbContext context) : base(context)
        {
        }
    }
}
