using pesmissionbase.Data;
using pesmissionbase.Models;

namespace pesmissionbase.Services.AddCategory
{
    public interface IAddCategory
    {

        ResultsDto<ResultAddCatagoryDto> Excute(RequsestAddDto requsest);

    }




    public class AddCategory : IAddCategory
    {

        DataBaseContext _context;
        public AddCategory(DataBaseContext context)
        {
            _context = context;

        }



        private Grouping getParent(long? ParentId)
        {
            return _context.Grouping.Find(ParentId);
        }
        private double NewRank(long? ParentId)
        {
            if (ParentId.HasValue)
            {
                if (_context.Grouping.Where(p => p.ParentId == ParentId).FirstOrDefault() != null)
                {
                    return _context.Grouping.Where(p => p.ParentId == ParentId).OrderBy(p => p.Rank).LastOrDefault().Rank;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return _context.Grouping.Where(p => p.ParentId == null).OrderBy(p => p.Rank).LastOrDefault().Rank;
            }
        }

        public ResultsDto<ResultAddCatagoryDto> Excute(RequsestAddDto requsest)
        {
            bool IsSucces = true;

            Grouping catagory = new Grouping()
            {
                Parent = getParent(requsest.ParenetId),
                Link = requsest.link,
                Name = requsest.Name,
                Rank = NewRank(requsest.ParenetId),
            };

            var query = _context.Grouping.AsQueryable();
            //if(query.Any(p=>p.Name==catagory.Name))
            //{
            //IsSucces = false;
            //}
           // else
         //   {
                IsSucces=true;
                _context.Grouping.Add(catagory);
                _context.SaveChanges();
                long id=catagory.Id;

       //     }
  
          
            var result = new ResultsDto<ResultAddCatagoryDto>()
            {

                IsSucces = IsSucces,
                Id = id,
                
                
            };
            return result;
        }
    }

    public class ResultAddCatagoryDto
    {
        public long Id { get; set; }
    }

    public class ResultsDto<T>
    {
        public bool IsSucces { get; set; }
        public string Massage { get; set; }
        public T Data { get; set; }
        public long Id { get;set; }
    }


    public class RequsestAddDto
    {


        public long? ParenetId { get; set; }

        public string link { get; set; }

        public string Name { get; set; }



    }


}
