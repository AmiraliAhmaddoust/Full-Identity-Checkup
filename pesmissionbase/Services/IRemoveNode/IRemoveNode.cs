using pesmissionbase.Data;
using pesmissionbase.Services.AddPermissions;

namespace pesmissionbase.Services.IRemoveNode
{
    public interface IRemoveNode
    {
        ResultDto1 Excute(RequsetRemoveNodeDto requset);

    }

    public class ResultDto1
    {
        public bool IsSucces { get; set; }
        public string Massage { get; set; }
    }

    public class RemoveNode : IRemoveNode
    {
        bool isSucces = true;
        DataBaseContext _context;
        public RemoveNode(DataBaseContext context)
        {
            _context = context;

        }

        public ResultDto1 Excute(RequsetRemoveNodeDto requset)
        {
            var node = _context.Grouping.ToList().Where(p=>p.Id == requset.Id).FirstOrDefault();
            if (node.Childs == null)
            {
                _context.Grouping.Remove(node);
                _context.SaveChanges();
                isSucces = true;

            }
            else
            {
                isSucces=false;
            }
            return new ResultDto1
            {
                IsSucces = isSucces,
                Massage = "",

            };
        }
    }

    public class RequsetRemoveNodeDto
    {
        public long Id { get; set; }
    }
}
