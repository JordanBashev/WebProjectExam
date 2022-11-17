using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Security.Claims;
using WebProjectExam.Database;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels;
using WebProjectExam.Models.ViewModels.TagVMs;
using WebProjectExam.Models.ViewModels.UserVMs;
using Microsoft.AspNetCore.Http;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace WebProjectExam.Services.ShoeServices
{
    public class ShoeServices : IShoeServices
    {
        HttpWebRequest request;
        HttpWebResponse response = null;
        HttpContext context;

        private readonly ShoeStoreDbContext _context;
        private readonly SignInManager<User> _signInManager;
        public ShoeServices(ShoeStoreDbContext context, SignInManager<User> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public void Delete(int Id)
        {
            //GETS ALL THE PROPERTIES TO DELETE
            var shoeToDelete = _context.Shoes.FirstOrDefault(x => x.Id == Id);
            var brandToDelete = _context.Brands.FirstOrDefault(x => x.Shoe_Id == Id);
            var priceToDelete = _context.Prices.FirstOrDefault(x => x.Shoe_Id == Id);
            var imageToDelete = _context.Image.FirstOrDefault(x => x.Shoe_Id == Id);
            //IF SUCH SHOE EXISTS DELETE 
            if (shoeToDelete != null)
            {
                _context.Brands.Remove(brandToDelete);
                _context.Prices.Remove(priceToDelete);
                _context.Image.Remove(imageToDelete);
                _context.Shoes.Remove(shoeToDelete);
                StreamToReadAndDeleteFile(Id);
                _context.SaveChanges();
            }
        }


        public void Edit(EditShoeVM shoemodel)
        {
            //GETS SELECTED TAG
            var getTag = _context.Tags.FirstOrDefault(x => x.Name == shoemodel.Tag.Name);

            //CHECKS IF SUCH SHOE EXISTS
            if (shoemodel.Id == 0)
            {
                //DOWNLOAD TO SPECIFIED FILE PATH
                var path = Stream(shoemodel.uri);
                //PREPARE OBJECTS TO CREATE
                var shoeToTags = new ShoeToTag();
                var shoeToCreate = new Shoe()
                {
                    Size = shoemodel.Size,
                    Price = new Price
                    {
                        price = shoemodel.Price,
                        Shoe_Id = shoemodel.Id
                    },
                    Brand = new Brand
                    {
                        Name = shoemodel.Brand,
                        Shoe_Id = shoemodel.Id
                    },

                    Colour = shoemodel.Colour,
                    uri = new Image
                    {
                        uri = path,
                        Shoe_Id = shoemodel.Id
                    }
                };

                //ADD SHOE TO DB AND THEN GET ITS ID
                _context.Shoes.Add(shoeToCreate);
                _context.SaveChanges();
                var getCreatedUser = _context.Shoes.FirstOrDefault(x => x.Colour == shoemodel.Colour);

                //USES THE ID WE GET FROM ABOVE CODE AND SETS VALUES
                shoeToTags.TagId = getTag.Id;
                shoeToTags.ShoeId = getCreatedUser.Id;
                _context.ShoeToTags.Add(shoeToTags);
            }
            else
            {
                //GET THE SHOE AND ALL OF IT PROPERTIES TO EDIT
                var ShoeToEdit = _context.Shoes.FirstOrDefault(s => s.Id == shoemodel.Id);
                var shoePriceToEdit = _context.Prices.FirstOrDefault(p => p.Shoe_Id == shoemodel.Id);
                var shoeBrandToEdit = _context.Brands.FirstOrDefault(p => p.Shoe_Id == shoemodel.Id);
                var shoeImageToEdit = _context.Image.FirstOrDefault(x => x.Shoe_Id == shoemodel.Id);
                var ShoeTagToEdit = _context.ShoeToTags.FirstOrDefault(x => x.ShoeId == shoemodel.Id);
                var TagToEdit = _context.Tags.FirstOrDefault(x => x.Id == getTag.Id);


                //CHECKS IF THERE's A SHOE TO EDIT
                if (ShoeToEdit != null)
                {
                    var newpath = "";
                    if (shoemodel.uri != "")
                    {
                        //DELETES THE IMG SET TO THE CURR SHOE AND REPLACE WITH NEW
                        StreamToReadAndDeleteFile(shoemodel.Id);
                        newpath = Stream(shoemodel.uri, shoemodel.Id);
                    }

                    //SET VALUES
                    ShoeToEdit.Size = shoemodel.Size;
                    ShoeToEdit.Colour = shoemodel.Colour;
                    shoePriceToEdit.price = shoemodel.Price;
                    shoeBrandToEdit.Name = shoemodel.Brand;

                    //UPDATE IF NEW PATH GIVEN
                    if (newpath != null)
                    {
                        shoeImageToEdit.uri = newpath;
                        _context.Image.Update(shoeImageToEdit);
                    }

                    //UPDATE
                    _context.Shoes.Update(ShoeToEdit);
                    _context.Prices.Update(shoePriceToEdit);
                    _context.Brands.Update(shoeBrandToEdit);

                    //UPDATE MANY TO MANY TABLE
                    if (ShoeTagToEdit != null)
                    {
                        _context.ShoeToTags.Remove(ShoeTagToEdit);
                        _context.SaveChanges();
                        var ShoeToTagUpdate = new ShoeToTag();
                        ShoeToTagUpdate.TagId = TagToEdit.Id;
                        ShoeToTagUpdate.ShoeId = ShoeToEdit.Id;
                        _context.ShoeToTags.Add(ShoeToTagUpdate);
                    }
                }
            }
            _context.SaveChanges();
        }

        //ADD TO ORDER
        public void AddToOrder(int Id)
        {
            var getShoe = GetShoeById(Id);
            var userId = _signInManager.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderExists = _context.orders.FirstOrDefault(x => x.shoe_Id == getShoe.Id);
            if (getShoe != null && userId != null && orderExists == null)
            {
                var orderToAdd = new Order()
                {
                    shoe_Id = getShoe.Id,
                    user_Id = userId,
                    Status = "Proccesing"
                };

                _context.orders.Add(orderToAdd);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("How did we get here");
            }
            
        }

        //MAPPING
        private static Expression<Func<Shoe, ShoeVM>> MapToShoeVM()
        {
            return x => new ShoeVM()
            {
                Id = x.Id,

                Size = x.Size,
                Colour = x.Colour
            };
        }

        //MAPPING
        private static Expression<Func<Tag, TagVM>> MapToTagVM()
        {
            return x => new TagVM()
            {
                Id = x.Id,
                Name = x.Name
            };
        }

        //RETURNS ALL SHOES
        public List<ShoeVM> GetAllShoes()
        {
            var shoes = _context.Shoes.Select(MapToShoeVM()).ToList();

            return shoes;
        }

        //GETS SHOE BY ID
        public Shoe GetShoeById(int Id)
        {
            var shoe = _context.Shoes.FirstOrDefault(x => x.Id == Id);
            return shoe;
        }

        public IEnumerable<TagVM> GetAllTags()
        {
            var tags = _context.Tags.Select(MapToTagVM()).ToList();
            return tags;
        }

        //METHODS THAT GET PROPERTIES VALUES BY SHOESVIEWMODEL
        public Image GetImageByShoeVM(ShoeVM shoe)
        {
            var image = _context.Image.FirstOrDefault(p => p.Shoe_Id == shoe.Id);
            return image;
        }

        public Price GetPriceByShoeVM(ShoeVM shoe)
        {
            var price = _context.Prices.FirstOrDefault(p => p.Shoe_Id == shoe.Id);
            return price;
        }

        public Brand GetBrandByShoeVM(ShoeVM shoe)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Shoe_Id == shoe.Id);
            return brand;
        }

        public Tag GetTagByShoeVM(ShoeVM shoe)
        {
            var shoeToTag = _context.ShoeToTags.FirstOrDefault(x => x.ShoeId == shoe.Id);
            if (shoeToTag != null)
            {
                var tag = _context.Tags.FirstOrDefault(x => x.Id == shoeToTag.TagId);
                return tag;
            }
            return new Tag();
        }

        //METHODS THAT GET PROPERTIES VALUES BY SHOES
        public Tag GetTagByShoe(Shoe shoe)
        {
            var shoeToTag = _context.ShoeToTags.FirstOrDefault(x => x.ShoeId == shoe.Id);
            if (shoeToTag != null)
            {
                var tag = _context.Tags.FirstOrDefault(x => x.Id == shoeToTag.TagId);
                return tag;
            }
            return null;
        }

        public Image GetImageByShoe(Shoe shoe)
        {
            var image = _context.Image.FirstOrDefault(p => p.Shoe_Id == shoe.Id);
            return image;
        }

        public Price GetPriceByShoe(Shoe shoe)
        {
            var price = _context.Prices.FirstOrDefault(x => x.Shoe_Id == shoe.Id);
            return price;
        }

        public Brand GetBrandByShoe(Shoe shoe)
        {
            var brand = _context.Brands.FirstOrDefault(x => x.Shoe_Id == shoe.Id);
            return brand;
        }

        //GETS THE IMG TABLE COUNT
        private int GetImagesCount()
        {
            var count = _context.Image.Count();
            return count;
        }

        //DOWNLOADS FILE IN SPECIFIED PATH FROM INTERNET WITH GIVEN URL
        public string Stream(string link)
        {
            try
            {
                request = (HttpWebRequest)WebRequest.Create(link);
                request.Timeout = 1000;
                request.AllowWriteStreamBuffering = false;
                response = (HttpWebResponse)request.GetResponse();

                Stream s = response.GetResponseStream();

                //Write to disk
                string fileName = $"{(GetImagesCount() + 1)}.png";
                FileStream fs = new FileStream($"./wwwroot/Images/{fileName}", FileMode.Create, FileAccess.ReadWrite);

                byte[] read = new byte[256];

                int count = s.Read(read, 0, read.Length);

                while (count > 0)

                {
                    fs.Write(read, 0, count);

                    count = s.Read(read, 0, read.Length);

                }
                //Close everything

                fs.Close();

                s.Close();

                response.Close();

                return fileName;
            }

            catch (System.Net.WebException)

            {
                if (response != null)

                    response.Close();
                return null;
            }
        }

        public string Stream(string link, int Id)
        {
            if (link == null)
            {
                return null;
            }
            else
            {
                try
                {
                    request = (HttpWebRequest)WebRequest.Create(link);
                    request.Timeout = 1000;
                    request.AllowWriteStreamBuffering = false;
                    response = (HttpWebResponse)request.GetResponse();

                    Stream s = response.GetResponseStream();

                    //Write to disk
                    string fileName = $"{Id}.png";
                    FileStream fs = new FileStream($"./wwwroot/Images/{fileName}", FileMode.Create, FileAccess.ReadWrite);

                    byte[] read = new byte[256];

                    int count = s.Read(read, 0, read.Length);

                    while (count > 0)

                    {
                        fs.Write(read, 0, count);

                        count = s.Read(read, 0, read.Length);

                    }
                    //Close everything

                    fs.Close();

                    s.Close();

                    response.Close();

                    return fileName;
                }

                catch (System.Net.WebException)

                {
                    if (response != null)

                        response.Close();
                    return null;
                }
            }
        }
        public void StreamToReadAndDeleteFile(int Id)
        {
            string fileName = $"./wwwroot/Images/{Id}.png";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        public void Dispose()
        {
            
        }
    }
}