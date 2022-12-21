using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hey_url_challenge_code_dotnet.Models
{
    public class Click
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string Platform { get; set; }
        [Required]
        public string Browser { get; set; }
        public string Date { get; set; }

        //Relations
        public Guid UrlID { get; set; }
        [ForeignKey("UrlID")]
        public Url Url { get; set; }
    }
}
