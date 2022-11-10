using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjectExam.Models.Entities
{
    public class Brand
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int Shoe_Id { get; set; }

        [ForeignKey (nameof(Shoe_Id))]
        public Shoe Shoe { get; set; }

    }
}

