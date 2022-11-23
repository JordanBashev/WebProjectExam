using System.Collections;
using System.Collections.Generic;
using WebProjectExam.Models.Entities;

namespace WebProjectExam.Models.ViewModels.TagVMs
{
    public class ShowAllTagsVM
    {
        public IEnumerable<Tag> Tags { get; set; }
    }
}
