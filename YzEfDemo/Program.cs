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
                Age=22
            };
            YzEfDemoDbContext yzEfDemoDbContext = new YzEfDemoDbContext();
            yzEfDemoDbContext.TestModels.Add(testModel);

            //同一个dbContext实例，未保存之前是否能查到，查不到，不查缓存吗？
            var queryResults = yzEfDemoDbContext.TestModels.Where(tm => tm.Age == 23).FirstOrDefault();

            //不同dbContext实例，未保存之前能查到吗？查不到
            YzEfDemoDbContext yzEfDemoDbContext2 = new YzEfDemoDbContext();
            var queryResults2 = yzEfDemoDbContext2.TestModels.Where(tm => tm.Age == 22).FirstOrDefault();

            yzEfDemoDbContext.SaveChanges();

            //发送sql请求吗？不会，缓存机制呢？
            queryResults = yzEfDemoDbContext.TestModels.Where(tm => tm.Age == 22).FirstOrDefault();

            //不同dbContext实例，另一个实例保存的，能查到吗？能查到
            queryResults2 = yzEfDemoDbContext2.TestModels.Where(tm => tm.Age == 22).FirstOrDefault();


            //更新实体两种方式，观察sql更新
            //exec sp_executesql N'UPDATE [dbo].[TestModels]
            //SET [Name] = @0
            //WHERE ([Id] = @1)
            //',N'@0 nvarchar(max) ,@1 int',@0=N'Yz2',@1=7
            queryResults.Name = "Yz2";

            //exec sp_executesql N'UPDATE [dbo].[TestModels]
            //SET [Name] = @0, [Age] = @1
            //WHERE ([Id] = @2)
            //',N'@0 nvarchar(max) ,@1 int,@2 int',@0=N'Yz2',@1=22,@2=8
            //yzEfDemoDbContext.Entry(queryResults).State = System.Data.Entity.EntityState.Modified;

            yzEfDemoDbContext.SaveChanges();

        }
    }
}
