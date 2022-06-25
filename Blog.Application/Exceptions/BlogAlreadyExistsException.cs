using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Exceptions
{
    public class BlogAlreadyExistsException : ApplicationException
    {
        public BlogAlreadyExistsException() { }

        public BlogAlreadyExistsException(string message) : base(message)
        {

        }
    }
}
