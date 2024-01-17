using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure.Entity
{
    public class TagPost
    {
        public virtual int Id { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual Post Post { get; set; }
    }
}
