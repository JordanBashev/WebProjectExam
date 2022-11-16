using Microsoft.AspNetCore.Mvc;
using WebProjectExam.Models.ViewModels.TagVMs;
using WebProjectExam.Services.TagServices;

namespace WebProjectExam.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagServices _tagServices;

        public TagController(ITagServices tagServices)
        {
            _tagServices = tagServices;
        }

        public IActionResult AllTags(ShowAllTagsVM allTags)
        {
            allTags.Tags = _tagServices.GetAll();
            return View(allTags);
        }

        [HttpGet]
        public IActionResult Create()
        {          
            return View();
        }

        [HttpPost]
        public IActionResult Create(TagVM tag)
        {
            if(ModelState.IsValid)
            {
                _tagServices.Edit(tag);
            }
            return RedirectToAction(nameof(AllTags));
        }

        [HttpGet]
        public IActionResult Edit(TagVM tag, int id)
        {
            if(ModelState.IsValid)
            {
                var getTagById = _tagServices.GetTagById(id);
                if(getTagById != null)
                {
                    tag.Name = getTagById.Name;
                }
            }
            return View(tag);
        }

        [HttpPost]
        public IActionResult Edit(TagVM tag)
        {
            if (ModelState.IsValid)
            {
                _tagServices.Edit(tag);
            }
            return RedirectToAction(nameof(AllTags));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _tagServices.Delete(id);
            return RedirectToAction(nameof(AllTags));
        }
    }
}
