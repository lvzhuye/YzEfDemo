using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YzEfDemo.Model;

namespace YzEfDemo
{
    public class YzEfDemoDbContext:DbContext
    {
        public DbSet<TestModel> TestModels { get; set; }
    }
}
