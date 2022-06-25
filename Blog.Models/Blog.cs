using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    /// <summary>
    /// Blog Model
    /// </summary>
    public class Blog
    {
        /// <summary>
        /// Gets or Sets BlogId
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
        public string Tag { get; set; }

        /// <summary>
        /// Gets or Sets Author
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or Sets Blog CreationDate 
        /// </summary>
        public string DateCreated { get; set; } = DateTime.Now.ToString("d");

        /// <summary>
        /// Gets or Sets Type of Blog
        /// </summary>
        public Type TypeOfBlog { get; set; }

        /// <summary>
        /// Gets or Sets User
        /// </summary>
        public User User { get; set; }

    }
}
