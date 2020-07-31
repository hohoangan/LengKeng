using AutoMapper;
using LengKeng.Dtos;
using LengKeng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LengKeng.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //Source -> Target
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command> ();
            CreateMap<CommandUpdateDto, Command> ();
            CreateMap<Command, CommandUpdateDto> ();
        }
    }
}
