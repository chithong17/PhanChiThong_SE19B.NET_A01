using PhanChiThong_DataAccess.DAOs;
using PhanChiThong_DataAccess.Models;
using System.Collections.Generic;

namespace PhanChiThong_DataAccess.Repositories
{
    public class TagRepository : ITagRepository
    {
        public List<Tag> GetAll() => TagDAO.Instance.GetAll();
        public Tag GetById(object id) => TagDAO.Instance.GetById(id);
        public void Add(Tag entity) => TagDAO.Instance.Add(entity);
        public void Update(Tag entity) => TagDAO.Instance.Update(entity);
        public void Delete(Tag entity) => TagDAO.Instance.Delete(entity);
    }
}
