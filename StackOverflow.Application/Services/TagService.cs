using StackOverflow.Infrastructure.Entity;
using StackOverflow.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application.Services
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddTag(Tag entity)
        {
            _unitOfWork.Tag.Add(entity);
            _unitOfWork.Commit();
        }

        public IList<Tag> GetAllTag()
        {
            return _unitOfWork.Tag.All().ToList();
        }

        public Tag GetById(int id)
        {
            return _unitOfWork.Tag.FindBy(id);
        }
    }
}
