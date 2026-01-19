using AutoMapper;
using LibraryManagementSystems.Application.Dtos.Request.Book;
using LibraryManagementSystems.Application.Dtos.Response.Book;
using LibraryManagementSystems.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystems.Application.Automapper
{
    public class Automapping : Profile
    {
        public Automapping()
        {
            CreateMap<Book, CreateBookRequestModel>().ReverseMap();
            CreateMap<Book, UpdateBookRequestModel>().ReverseMap();
            CreateMap<Book, BookResponseModel>();
        }
    }
}
