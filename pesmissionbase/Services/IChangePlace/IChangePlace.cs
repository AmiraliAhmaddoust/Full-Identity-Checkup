using pesmissionbase.Data;
using pesmissionbase.Services.AddPermissions;

namespace pesmissionbase.Services.IChangePlace
{
    public interface IChangePlace
    {
        ResultDto<ResultChangeRequsetDto> Excute(ChangeRequsetDto changeRequset);  
    }


    public class ChangePlace : IChangePlace
    {
        DataBaseContext _context;
        public ChangePlace(DataBaseContext context)
        {
            _context = context;

        }

        public ResultDto<ResultChangeRequsetDto> Excute(ChangeRequsetDto changeRequset)
        {



            var changeNode = _context.Grouping.Find(changeRequset.ChangingnodeId);

            if (changeRequset.Hitmode == "over")
            {
                double rank;
                var host = _context.Grouping.Find(changeRequset.HostId);
                if (changeRequset.onlevelAfter != 0)
                {
                    var nodeAfter = _context.Grouping.Find(changeRequset.onlevelAfter);
                    rank = nodeAfter.Rank + 1;
                }
                else
                {
                    rank = 1;

                }

                changeNode.Parent = host;
                changeNode.Rank = rank;

            }
            else if (changeRequset.Hitmode == "before")
            {


                if (changeRequset.HostId == null)
                {


                    var nodeBefore = _context.Grouping.Find(changeRequset.onlevelBfore);
                    double rank = (nodeBefore.Rank / 2);
                    changeNode.Parent = null;
                    changeNode.Rank = rank;
                }
                else
                {
                    var host = _context.Grouping.Find(changeRequset.HostId);

                    double rank;


                    var nodeBefore = _context.Grouping.Find(changeRequset.onlevelBfore);
                    if (changeRequset.onlevelAfter != 0)
                    {
                        var nodeAfter = _context.Grouping.Find(changeRequset.onlevelAfter);
                        rank = (nodeAfter.Rank + nodeBefore.Rank) / 2;
                    }
                    else
                    {
                        rank = nodeBefore.Rank / 2;
                    }

                    changeNode.Parent = host;
                    changeNode.Rank = rank;
                }

            }
            else if (changeRequset.Hitmode == "after")
            {

                if (changeRequset.HostId == null)
                {
                    var nodeAfter = _context.Grouping.Find(changeRequset.onlevelAfter);
                    double rank = nodeAfter.Rank + 1;
                    changeNode.Parent = null;
                    changeNode.Rank = rank;


                }
                else
                {




                    var host = _context.Grouping.Find(changeRequset.HostId);

                    double rank;
                    var nodeAfter = _context.Grouping.Find(changeRequset.onlevelAfter);

                    if (changeRequset.onlevelBfore == 0)
                    {
                        rank = nodeAfter.Rank + 1;

                    }
                    else
                    {
                        var nodeBefore = _context.Grouping.Find(changeRequset.onlevelBfore);

                        rank = (nodeAfter.Rank + nodeBefore.Rank) / 2;
                    }

                    changeNode.Parent = host;
                    changeNode.Rank = rank;
                }
            }
            _context.SaveChanges();
            return new ResultDto<ResultChangeRequsetDto>
            {

                IsSucces = true,
                Massage = "تغییرات اعمال شد"


            };

        }
    }



    public class ChangeRequsetDto
    {

        public long ChangingnodeId { get; set; }
        public long HostId { get; set; }

        public string Hitmode { get; set; }
        public long onlevelBfore { get; set; }
      
        public long onlevelAfter { get; set; }
    }
    public class ResultChangeRequsetDto
    {
        public long Id { get; set; }
    }

}
