﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure.Entity
{
    public class Answer : IEntity<Guid>
    {
        public virtual Guid Id { get; set; }
        public virtual string AnswerText { get; set; }
        public virtual bool AcceptedAnswer { get; set; } = false;
        public virtual DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public virtual IList<AnswerVote> Votes { get; set; } = new List<AnswerVote>();
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
