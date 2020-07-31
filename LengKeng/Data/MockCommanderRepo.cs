using LengKeng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LengKeng.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var command = new List<Command>
            {
                new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kettle and Pan" },
                new Command { Id = 1, HowTo = "Cut bread", Line = "Get a knife", Platform = "Knife and chopping board" },
                new Command { Id = 2, HowTo = "make cup of tea", Line = "Place teabag in cup", Platform = "Kettle and Cup" }
            };

            return command;
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kettle and Pan" };
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }
    }
}
