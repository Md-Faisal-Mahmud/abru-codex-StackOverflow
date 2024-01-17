using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure.Entity
{
    public class Tag
    {
        public virtual int Id { get; set; }
        public virtual string TagName { get; set; }
        public virtual string TagDescription { get; set; }
        public virtual IList<TagPost> Posts { get; set; }
    }
}
