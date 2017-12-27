using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YzEfDemo.Model;

namespace YzEfDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var testModel = new TestModel
            {
                Name="Yz",
                Age=26
            };
            YzEfDemoDbContext yzEfDemoDbContext = new YzEfDemoDbContext();
            yzEfDemoDbContext.TestModels.Add(testModel);

            //同一个dbContext实例，未保存之前是否能查到，查不到
            var queryResults = yzEfDemoDbContext.TestModels.Where(tm => tm.Age == 26).FirstOrDefault();



            yzEfDemoDbContext.SaveChanges();

            //发送sql请求吗？不会，缓存机制呢？
            queryResults = yzEfDemoDbContext.TestModels.Where(tm => tm.Age == 26).FirstOrDefault();



        }
    }
}
