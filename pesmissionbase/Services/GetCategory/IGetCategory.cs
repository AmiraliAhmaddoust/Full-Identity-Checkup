using Microsoft.EntityFrameworkCore;
using pesmissionbase.Data;
using pesmissionbase.Models;
using pesmissionbase.Services.AddCategory;

namespace pesmissionbase.Services.GetCategory
{
    public interface IGetCategory
    {

        ResultsDto<List<Grouping>> Excute();

    }


    public class GetCategory : IGetCategory
    {
        DataBaseContext _context;
        public GetCategory(DataBaseContext context)
        {
            _context = context;

        }


        public ResultsDto<List<Grouping>> Excute()
        {


            var ans = _context.Grouping.OrderBy(x=>x.Rank).ToList().Where(x=>x.Parent==null).OrderBy(p=>p.Rank).ToList();
            //foreach (var item in ans)
            //{
            //  item.Childs=  item.Childs.OrderBy(item=>item.Rank).ToList();
            //}

            var resulat = new ResultsDto<List<Grouping>>()
            {

                IsSucces = true,
                Data = ans,

            };
            return resulat;
        }
    }




}
