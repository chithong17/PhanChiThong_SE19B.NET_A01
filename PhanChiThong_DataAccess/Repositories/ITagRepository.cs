using PhanChiThong_DataAccess.Models;
using System.Collections.Generic;

namespace PhanChiThong_DataAccess.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAll();
        Tag GetById(object id);
        void Add(Tag entity);
        void Update(Tag entity);
        void Delete(Tag entity);
    }
}
