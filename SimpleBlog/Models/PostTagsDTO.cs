using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    [Table("post_tags")]
    public class PostTagsDTO
    {
        [Key, Column(Order = 0)]
        public int tag_id { get; set; }
        [Key, Column(Order = 1)]
        public int post_id { get; set; }

        [ForeignKey("tag_id")]
        public virtual TagDTO Tag { get; set; }
        [ForeignKey("post_id")]
        public virtual PostDTO Post { get; set; }
    }
}