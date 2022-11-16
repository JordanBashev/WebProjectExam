using System.Collections;
using System.Collections.Generic;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels.TagVMs;

namespace WebProjectExam.Services.TagServices
{
    public interface ITagServices
    {
        public void Edit(TagVM tag);

        public void Delete(int Id);

        public Tag GetTagById(int Id);

        public IEnumerable<TagVM> GetAll();

    }
}
