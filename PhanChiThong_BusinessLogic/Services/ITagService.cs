using PhanChiThong_DataAccess.Models;
using System.Collections.Generic;

namespace PhanChiThong_BusinessLogic.Services
{
    public interface ITagService
    {
        List<Tag> GetAll();
        Tag GetById(object id);
        void Add(Tag entity);
        void Update(Tag entity);
        void Delete(Tag entity);
    }
}
