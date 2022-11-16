using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using WebProjectExam.Database;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels;
using WebProjectExam.Models.ViewModels.UserVMs;

namespace WebProjectExam.Services.ShoeServices
{
    public class ShoeServices : IShoeServices
    {
        HttpWebRequest request;

        HttpWebResponse response = null;
        private readonly ShoeStoreDbContext _context;
        public ShoeServices(ShoeStoreDbContext context)
        {
            _context = context;
        }

        public void Delete(int Id)
        {
            var shoeToDelete = _context.Shoes.FirstOrDefault(x => x.Id == Id);
            if(shoeToDelete != null)
            {
                _context.Shoes.Remove(shoeToDelete);
                _context.SaveChanges();
            }
        }

        public void StreamToRead(EditShoeVM shoemodel)
        {
            string fileName = $"{shoemodel.Id}.png";
            //Create an object of FileInfo for specified path            
            FileInfo fi = new FileInfo(fileName);

            //Open a file for Read\Write
            FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);

            //Create an object of StreamReader by passing FileStream object on which it needs to operates on
            StreamReader sr = new StreamReader(fs);

            //Use the ReadToEnd method to read all the content from file
            string fileContent = sr.ReadToEnd();

            if (fileName == $"{shoemodel.Id}.png")
            {
                File.Delete(fileName);
            }
            //Close the StreamReader object after operation
            sr.Close();
            fs.Close();
        }

        public void Edit(EditShoeVM shoemodel)
        {
            if(shoemodel.Id == 0)
            {
                var path = Stream(shoemodel.uri);
                var shoeToCreate = new Shoe()
                {
                    Size = shoemodel.Size,
                    Price = new Price {
                        price = shoemodel.Price,
                        Shoe_Id = shoemodel.Id
                    },
                    Brand = new Brand {
                        Name = shoemodel.Brand,
                        Shoe_Id = shoemodel.Id },

                    Colour = shoemodel.Colour,
                    uri = new Image
                    {
                        uri = path,
                        Shoe_Id = shoemodel.Id
                    }

                };
               
                _context.Shoes.Add(shoeToCreate);
              
            }
            else
            {
                var ShoeToEdit = _context.Shoes.FirstOrDefault(s => s.Id == shoemodel.Id);
                var shoePriceToEdit = _context.Prices.FirstOrDefault(p => p.Shoe_Id == shoemodel.Id);
                var shoeBrandToEdit = _context.Brands.FirstOrDefault(p => p.Shoe_Id == shoemodel.Id);
                var shoeImageToEdit = _context.Image.FirstOrDefault(I => I.Shoe_Id == shoemodel.Id);
                StreamToRead(shoemodel);
                var path =  Stream(shoemodel.uri);
                
                if (ShoeToEdit != null)
                {

                    ShoeToEdit.Size = shoemodel.Size ;
                    ShoeToEdit.Colour = shoemodel.Colour;
                    shoePriceToEdit.price = shoemodel.Price;
                    shoeBrandToEdit.Name = shoemodel.Brand;
                    shoeImageToEdit.uri = path;

                    _context.Image.Update(shoeImageToEdit);
                    _context.Shoes.Update(ShoeToEdit);
                    _context.Prices.Update(shoePriceToEdit);
                    _context.Brands.Update(shoeBrandToEdit);
                }
            }
            _context.SaveChanges();
        }

        private static Expression<Func<Shoe, ShoeVM>> MapToShoeVM()
        {
            return x => new ShoeVM()
            {
                Id = x.Id,
                
                Size = x.Size,
                Colour = x.Colour
            };
        }


        public List<ShoeVM> GetAllShoes()
        {
            var shoes = _context.Shoes.Select(MapToShoeVM()).ToList();

            return shoes;
        }

        public Image GetImageByShoe(ShoeVM shoe)
        {
            var image = _context.Image.FirstOrDefault(p => p.Shoe_Id == shoe.Id);
            return image;
        }

        public Price GetPriceByShoe(ShoeVM shoe)
        {


            var price = _context.Prices.FirstOrDefault(p  => p.Shoe_Id == shoe.Id);
            return price;
        }

        public Brand GetBrandByShoe(ShoeVM shoe)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Shoe_Id == shoe.Id);
            return brand;
        }

        private int GetImagesCount()
        {
            var count = _context.Image.Count();
            return count;
        }

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
                string fileName = $"{GetImagesCount()}.png";
                FileStream fs = new FileStream($"./wwwroot/Images/{fileName}" ,FileMode.Create, FileAccess.ReadWrite);

                byte[] read = new byte[256];

                int count = s.Read(read, 0, read.Length);

                while (count > 0)

                {

                    fs.Write(read, 0, count);

                    count = s.Read(read, 0, read.Length);

                }
                string path = fs.Name;
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
}