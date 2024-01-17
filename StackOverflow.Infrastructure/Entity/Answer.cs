using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure.Entity
{
    public class Answer
    {
        public virtual int Id { get; set; }
        public virtual string AnswerText { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
