using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace hey_url_challenge_code_dotnet.Models
{
    [Index(nameof(ShortUrl), IsUnique = true)]
    public class Url
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        [StringLength(maximumLength:5)]
        public string ShortUrl { get; set; }
        [Required]
        public string FullUrl { get; set; }
        public int Count { get; set; }
        public string DateCreation { get; set; }
    }
}
