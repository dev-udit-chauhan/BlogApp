using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Entities
{
    [Table("Blog")]
    public class Blog
    {
        /// <summary>
        /// Gets or Sets BlogID
        /// </summary>
        [Key]
        [Column(name: "BlogId", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogId { get; set; }

        /// <summary>
        /// Gets or Sets Blog Title
        /// </summary>
        [Column(name: "BlogTitle", TypeName = "varchar")]
        [StringLength(250)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets Blog Content
        /// </summary>
        [Column(name: "BlogContent", TypeName = "varchar")]
        [StringLength(1000)]
        public string Content { get; set; }

        /// <summary>
        /// Gets or Sets Blog Tags
        /// </summary>
        [Column(name: "BlogTags")]
        [StringLength(250)]
        public string Tag { get; set; }

        /// <summary>
        /// Gets or Sets Author
        /// </summary>
        [Column(name: "Author", TypeName = "varchar")]
        [StringLength(250)]
        public string Author { get; set; }


        /// <summary>
        /// Gets or Sets Blog CreationDate 
        /// </summary>
        [Column(name: "BlogCreationDate", TypeName = "date")]
        [StringLength(250)]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or Sets Type of Blog
        /// </summary>
        [Column(name:"TypeOfBlog")]
        public Type TypeOfBlog { get; set; }

        public User User { get; set; }
    }
}
