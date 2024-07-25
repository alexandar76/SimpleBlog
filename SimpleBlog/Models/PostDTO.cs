using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    [Table("posts")]
    public class PostDTO
    {
        [Key]
        public int Id { get; set; }
        //public UserDTO User { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Author { get; set; }

        public IList<TagDTO> Tags { get; set; }

        public PostDTO()
        {
            Tags = new List<TagDTO>();
        }

        public bool IsDeleted { get { return DeletedAt != null; } }

    }
}