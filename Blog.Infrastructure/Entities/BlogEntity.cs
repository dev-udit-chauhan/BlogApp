using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Entities
{
    [Table("Blog")]
    public class BlogEntity
    {
        /// <summary>
        /// Gets or Sets BlogID
        /// </summary>
        public int BlogId { get; set; }

        /// <summary>
        /// Gets or Sets Blog Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets Blog Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or Sets Blog Tags
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Gets or Sets Author
        /// </summary>
        public string Author { get; set; }


        /// <summary>
        /// Gets or Sets Blog CreationDate 
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}
