using AutoMapper;
using NWEC.P.L001_Task3.DataAccessLayer.Models;


public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Quiz, Dto>();
    }
}


