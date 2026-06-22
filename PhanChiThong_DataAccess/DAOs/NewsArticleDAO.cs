using PhanChiThong_DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PhanChiThong_DataAccess.DAOs
{
    public class NewsArticleDAO
    {
        private static NewsArticleDAO instance = null;
        private static readonly object instanceLock = new object();
        private NewsArticleDAO() { }
        public static NewsArticleDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new NewsArticleDAO();
                    }
                    return instance;
                }
            }
        }

        public List<NewsArticle> GetAll()
        {
            using var context = new FUNewsManagementContext();
            return context.NewsArticles.Include(n => n.Category).Include(n => n.CreatedBy).Include(n => n.Tags).ToList();
        }

        public NewsArticle GetById(object id)
        {
            using var context = new FUNewsManagementContext();
            return context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.Tags)
                .FirstOrDefault(n => n.NewsArticleId == id.ToString());
        }

        public void Add(NewsArticle entity)
        {
            using var context = new FUNewsManagementContext();
            if (entity.Tags != null && entity.Tags.Count > 0)
            {
                var tagIds = entity.Tags.Select(t => t.TagId).ToList();
                entity.Tags.Clear();
                var trackedTags = context.Tags.Where(t => tagIds.Contains(t.TagId)).ToList();
                foreach (var t in trackedTags) entity.Tags.Add(t);
            }
            context.NewsArticles.Add(entity);
            context.SaveChanges();
        }

        public void Update(NewsArticle entity)
        {
            using var context = new FUNewsManagementContext();
            var trackedEntity = context.NewsArticles.Include(n => n.Tags).FirstOrDefault(n => n.NewsArticleId == entity.NewsArticleId);
            if (trackedEntity != null)
            {
                trackedEntity.NewsTitle = entity.NewsTitle;
                trackedEntity.Headline = entity.Headline;
                trackedEntity.NewsContent = entity.NewsContent;
                trackedEntity.NewsSource = entity.NewsSource;
                trackedEntity.CategoryId = entity.CategoryId;
                trackedEntity.NewsStatus = entity.NewsStatus;
                trackedEntity.ModifiedDate = entity.ModifiedDate;
                trackedEntity.UpdatedById = entity.UpdatedById;
                trackedEntity.ViewCount = entity.ViewCount;
                if (!string.IsNullOrEmpty(entity.ImageUrl)) trackedEntity.ImageUrl = entity.ImageUrl;

                if (entity.Tags != null)
                {
                    var newTagIds = entity.Tags.Select(t => t.TagId).ToList();
                    var currentTagIds = trackedEntity.Tags.Select(t => t.TagId).ToList();

                    // Remove deselected tags
                    var tagsToRemove = trackedEntity.Tags.Where(t => !newTagIds.Contains(t.TagId)).ToList();
                    foreach (var t in tagsToRemove) trackedEntity.Tags.Remove(t);

                    // Add newly selected tags
                    var tagsToAddIds = newTagIds.Except(currentTagIds).ToList();
                    var tagsToAdd = context.Tags.Where(t => tagsToAddIds.Contains(t.TagId)).ToList();
                    foreach (var t in tagsToAdd) trackedEntity.Tags.Add(t);
                }
                
                context.SaveChanges();
            }
        }

        public void Delete(NewsArticle entity)
        {
            using var context = new FUNewsManagementContext();
            var trackedEntity = context.NewsArticles.Include(n => n.Tags).FirstOrDefault(n => n.NewsArticleId == entity.NewsArticleId);
            if (trackedEntity != null)
            {
                trackedEntity.Tags.Clear();
                context.SaveChanges();

                context.NewsArticles.Remove(trackedEntity);
                context.SaveChanges();
            }
        }

        public Dictionary<string, int> GetViewsByCategory()
        {
            using var context = new FUNewsManagementContext();
            return context.NewsArticles
                .Where(n => n.Category != null)
                .GroupBy(n => n.Category.CategoryName)
                .Select(g => new { Key = g.Key, Value = g.Sum(n => n.ViewCount ?? 0) })
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public Dictionary<string, int> GetArticlesByStaff()
        {
            using var context = new FUNewsManagementContext();
            return context.NewsArticles
                .Where(n => n.CreatedBy != null)
                .GroupBy(n => n.CreatedBy.AccountName)
                .Select(g => new { Key = g.Key, Value = g.Count() })
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
