using PhanChiThong_DataAccess.Models;
using PhanChiThong_DataAccess.Repositories;
using System.Collections.Generic;

namespace PhanChiThong_BusinessLogic.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }

        public List<Tag> GetAll() => _repository.GetAll();
        public Tag GetById(object id) => _repository.GetById(id);
        public void Add(Tag entity) => _repository.Add(entity);
        public void Update(Tag entity) => _repository.Update(entity);
        public void Delete(Tag entity) => _repository.Delete(entity);
    }
}
