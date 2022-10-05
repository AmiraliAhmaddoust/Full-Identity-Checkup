using pesmissionbase.Data;
using pesmissionbase.Services.AddPermissions;

namespace pesmissionbase.Services.IRenameNode
{
    public interface IRenameNode
    {
        ResultDto<ResultReNameNodeDto>Excute(RequsetReNameNodeDto requset);
    }
    public class RenameNode : IRenameNode
    {
        DataBaseContext _context;
        public RenameNode(DataBaseContext context)
        {
            _context = context;

        }
        public ResultDto<ResultReNameNodeDto> Excute(RequsetReNameNodeDto requset)
        {
            bool Issucces = true;
            string Massage = "";
            var query = _context.Grouping.AsQueryable();
            var node = _context.Grouping.Find(requset.NodeID);
            if ( node.Name!=requset.NewName &&  query.Any(p => p.Name == requset.NewName))
            {
                Issucces = false;
                Massage = " اسم انتخابی تکراری میباشد";
            }
            //else if (node.Link !=requset.Nodetitle && query.Any(p=>p.Link==requset.Nodetitle))
            //{
            //    Issucces = false;
            //    Massage = " لینک  انتخابی تکراری میباشد";
            //}t
            else if (requset.Nodetitle == null)
            {
                Issucces = false;
                Massage = " لینک  انتخابی خالی میباشد";
            }
            else
            {
                Issucces = true;
                node.Name = requset.NewName;
                node.Link = requset.Nodetitle;
                _context.SaveChanges();
            }
            return new ResultDto<ResultReNameNodeDto>
            {
                IsSucces = Issucces,
                Massage = Massage,

            };
        }
    }

    public class RequsetReNameNodeDto{
     
        public long NodeID { get; set; }
        public string NewName { get; set; }
        public string Nodetitle { get; set; }



    }
    public class ResultReNameNodeDto
    {
        public long Id { get; set; }
    }
}
