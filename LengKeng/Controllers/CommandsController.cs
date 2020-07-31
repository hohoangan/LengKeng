using AutoMapper;
using LengKeng.Data;
using LengKeng.Dtos;
using LengKeng.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LengKeng.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;
        private readonly CommanderContext _context;

        public CommandsController(ICommanderRepo reponsitory, IMapper mapper, CommanderContext context)
        {
            _repository = reponsitory;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("test")]
        public ActionResult<IEnumerable<Student>> test()
        {
            var students = _context.Students;
            return Ok(students);
        }

        [HttpGet("students")]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            var students = _context.Students;
            return Ok(students);
        }

        [HttpGet("sinhvien")]
        public ActionResult<IEnumerable<SinhVien>> GetAllSinhViens()
        {
            var sinhViens = _context.SinhViens;
            return Ok(sinhViens);
        }

        [HttpGet("sinhvien/{id}")]
        public ActionResult<SinhVien> GetSinhViens(int id)
        {
            //var sinhViens = _context.SinhViens.Find(id);  
            var sinhViens = _context.SinhViens.Include(r=>r.Students).Where(s=>s.Id==id).First();

            if (sinhViens != null)
            {
                return Ok(sinhViens.Students);
            }
            return NotFound();
        }

        [HttpPut("sinhvien/{id}")]
        public ActionResult UpdateSinhViens(int id, SinhVien sinhVien)
        {
            var sinhViens = _context.SinhViens.Find(id);
            if (sinhVien == null)
                return NotFound();

            _context.Entry(sinhVien).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        //private readonly MockCommanderRepo _repository = new MockCommanderRepo();
        //GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        //GET api/commands/5
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult <CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if(commandItem != null)
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            return NotFound();
        }

        //POST api/commands/
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();
            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            //tao thanh cong, tra ve thong tin trong Header: 201 Created
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
            //return Ok(commandReadDto);
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFormRepo = _repository.GetCommandById(id);
            if (commandModelFormRepo == null)
                return NotFound();

            //vi dieu
            _mapper.Map(commandUpdateDto, commandModelFormRepo);
            _repository.UpdateCommand(commandModelFormRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult ParticalCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFormRepo = _repository.GetCommandById(id);
            if (commandModelFormRepo == null)
                return NotFound();
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFormRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);
            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            //vi dieu
            _mapper.Map(commandToPatch, commandModelFormRepo);
            _repository.UpdateCommand(commandModelFormRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFormRepo = _repository.GetCommandById(id);
            if (commandModelFormRepo == null)
                return NotFound();
            _repository.DeleteCommand(commandModelFormRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
