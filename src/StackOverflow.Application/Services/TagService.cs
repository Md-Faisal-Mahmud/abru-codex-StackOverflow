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
        public async Task AddTag(Tag entity)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.Tag.AddAsync(entity);
            await _unitOfWork.Commit();
        }

        public async Task DeleteTag(Guid id)
        {
            await _unitOfWork.BeginTransaction();

            var tagEntity = await GetById(id);

            if(tagEntity == null)
            {
                throw new InvalidOperationException("tag not found");
            }
            await _unitOfWork.Tag.DeleteAsync(tagEntity);
            await _unitOfWork.Commit();
        }

        public async Task<IList<Tag>> GetAllTag()
        {
            return await _unitOfWork.Tag.GetAllAsync();
        }

        public async Task<Tag?> GetById(Guid id)
        {
            return await _unitOfWork.Tag.GetSingleAsync(id);
        }
    }
}
