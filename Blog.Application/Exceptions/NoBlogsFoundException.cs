using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Exceptions
{
    public class NoBlogsFoundException : ApplicationException
    {
        public NoBlogsFoundException() { }

        public NoBlogsFoundException(string message) : base(message) { } 
    }
}
