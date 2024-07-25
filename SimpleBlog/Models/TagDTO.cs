using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    [Table("tags")]
    public class TagDTO
    {
        [Key]
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }

        public IList<PostDTO> Posts { get; set; }

        public TagDTO()
        {
            Posts = new List<PostDTO>();
        }
    }
}