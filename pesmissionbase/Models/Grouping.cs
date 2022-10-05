using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pesmissionbase.Models
{
    public class Grouping
    {
     
        public long Id { get; set; }
        public string Name { get; set; }

        public string Link { get; set; }
       
        public double Rank { get; set; }   

        public virtual Grouping Parent { get; set; }
        public long? ParentId { get; set; }


        public virtual ICollection<Grouping> Childs { get; set; }
    }
}
