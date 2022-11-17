using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjectExam.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string user_Id { get; set; }
        public int shoe_Id { get; set; }
        public string Status { get; set; }

        [ForeignKey(nameof(user_Id))]
        public User User { get; set; }

        [ForeignKey(nameof(shoe_Id))]
        public Shoe Shoe { get; set; }
    }
}

