using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebProjectExam.Database;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels.RoleVMs;
using WebProjectExam.Models.ViewModels.TagVMs;

namespace WebProjectExam.Services.TagServices
{
    public class TagService : ITagServices
    {
        private readonly ShoeStoreDbContext _context;

        public TagService(ShoeStoreDbContext context)
        {
            _context = context;
        }

        public void Edit(TagVM tag)
        {
            var getTagToEdit = _context.Tags.FirstOrDefault(x => x.Id == tag.Id);
            if (getTagToEdit != null)
            {
                getTagToEdit.Name = tag.Name;

                _context.Tags.Update(getTagToEdit);
            }
            else
            {
                var tagToCreate = new Tag()
                {
                    Name = tag.Name
                };

                _context.Tags.Add(tagToCreate);
            }
            _context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var findTagToDelete = _context.Tags.FirstOrDefault(x => x.Id == Id);
            _context.Tags.Remove(findTagToDelete);
            _context.SaveChanges(); 
        }


        public IEnumerable<Tag> GetAll()
        { 
            var allTags = _context.Tags.ToList();

            return allTags;
        }

        public Tag GetTagById(int Id)
        {
            var getTagById = _context.Tags.FirstOrDefault(x =>x.Id == Id);

            return getTagById;
        }

        private static Expression<Func<Tag, TagVM>> MapToTagVM()
        {
            return x => new TagVM()
            {
                Id = x.Id,
                Name = x.Name
            };
        }
    }
}
