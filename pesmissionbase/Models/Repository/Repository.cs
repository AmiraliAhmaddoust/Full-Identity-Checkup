using pesmissionbase.Data;
using pesmissionbase.Services.GetCategory;

namespace pesmissionbase.Models.Repository
{
    public class Repository
    {
        private readonly IGetCategory _getCategory;
        private  List<Grouping>menu=new List<Grouping>();
        private readonly DataBaseContext context;

      
        public Repository(DataBaseContext context)
        {
            this.context = context;
        
        }
        public List<Grouping> Get(IGetCategory getCategory)
        {
             
            menu = getCategory.Excute().Data;
            return menu;
        }

    }
}
