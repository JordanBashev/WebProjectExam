using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjectExam.Models.Entities
{
    public class ShoeToTag
    {
        public int Id { get; set; }
        public int ShoeId { get; set; }
        public int TagId { get; set; }

        [ForeignKey(nameof(ShoeId))]
        public Shoe Shoe { get; set; }

        [ForeignKey(nameof(TagId))]
        public Tag Tag { get; set; }
    }
}
