using LengKeng.Models;
using Microsoft.EntityFrameworkCore;

namespace LengKeng.Data
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
        {

        }

        public DbSet<Command> Commands { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
