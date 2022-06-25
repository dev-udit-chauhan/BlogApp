using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Mapper
{
    public class BlogMapper : Profile
    {
        public BlogMapper()
        {
            this.CreateMap<Models.Blog, Infrastructure.Entities.Blog>().ReverseMap();
            this.CreateMap<Models.User, Infrastructure.Entities.User>().ReverseMap();
        }
    }
}
