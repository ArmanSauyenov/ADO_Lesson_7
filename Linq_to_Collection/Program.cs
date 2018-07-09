using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;




namespace Linq_to_Collection
{
    class Program
    {

        private static crcms db = new crcms();


        static void Main(string[] args)
        {
           // Exmpl01();
            Exmpl02();


        }
        //фильтрация  
        //where
        //take
        //Skip
        //TakeWhile
        //SkipWhile
        //Distinct

        static void Exmpl01()
        {
            //where
            var q1 = db.Area
                .Where(w => w.ParentId == 0)
                .Where(w=>!string.IsNullOrEmpty(w.IP))
                .ToList();
            PrintInfo(q1);


            var qq1 = (from a in db.Area
                       where a.ParentId == 0
                       && !string.IsNullOrEmpty(a.IP)
                       select a).ToList();

            PrintInfo(qq1);


            //Take возвращает первые n-элементов и игнорирует остальные
            var q2 = db.Area.Take(5);
            PrintInfo(q2);



            //Skip - игнорирует первые n-элементов
            Console.WriteLine("Skip");
            var q3 = db.Area
                 .Where(w => !string.IsNullOrEmpty(w.IP))
                 .ToList().Skip(3);
            PrintInfo(q3.ToList());


            var q4 = db.Timer.Where(w => w.DateFinish != null)
                .Take(10)
                .ToList()
                .Skip(10)
                .ToList();
           PrintInfo(q4);



            //TakeWhile
            Console.WriteLine("TakeWhile");
            //var q5 = db.Timer
            //    .ToList()
            //    .TakeWhile(s => s.DateFinish != null)
            //    .ToList();
            //   PrintInfo(q5);



            //SkipWhile

            Console.WriteLine("SkipWhile");

            //var q6 = db.Timer.ToList()
            //    .SkipWhile(s => s.DateFinish != null)
            //    .ToList();
            //    PrintInfo(q6);



            //Distinct
            Console.WriteLine("===================================================================");
            Console.WriteLine("Distinct");
           
            var q7 = db.Area.Select(s => new { s.IP }).Distinct();
            Console.WriteLine("Distinct : " + q7.Count());

            var q7_1 = db.Area.Select(s => new { s.IP });
            Console.WriteLine("Total : " + q7_1.Count());

        }



        //Проецирование

        //Select
        //SelectMany

        static void Exmpl02()
        {

            DirectoryInfo []dirs = new DirectoryInfo(@"\\dc\\Студенты\ПКО").GetDirectories();

            //Select

            var q1 = from d in dirs
                     where (d.Attributes & FileAttributes.System) == 0
                     select new
                     {
                         MyDirectoryName = d.FullName,
                         MyCreated = d.CreationTime
                     };


            foreach (var file in q1)
            {
                Console.WriteLine("{0, -40}\t {1}", file.MyDirectoryName, file.MyCreated);

            }


            var q2 = from d in dirs
                     where (d.Attributes & FileAttributes.System) == 0
                     select new
                     {
                         MyDirectoryName = d.FullName,
                         MyCreated = d.CreationTime,
                         Files = from f in d.GetFiles()
                                 select new
                                 {
                                     FileName = f.FullName,
                                     f.Length
                                 }
                     };


            foreach (var file in q2)
            {
                Console.WriteLine("{0, -40}\t {1}", file.MyDirectoryName, file.MyCreated);
                foreach (var f in file.Files)
                {
                    Console.WriteLine("\t-->{0} ({1})", f.FileName, f.Length);
                }
            }



        }



        static void PrintInfo(List<Area> areas)
        {
            foreach (Area area in areas)
            {
                Console.WriteLine("{0, -50}\t{1}", area.Name, area.IP);
            }
            Console.WriteLine("===================================================================");
        }

        static void PrintInfo(IQueryable<Area> areas) //передадим сам запрос (IQueryable хранит сам запрос)
        {
            foreach (Area area in areas) //тут срабботает запрос
            {
                Console.WriteLine("{0, -50}\t{1}", area.Name, area.IP);
            }
            Console.WriteLine("===================================================================");
        }

        static void PrintInfo(List<Timer> timers) //передадим сам запрос (IQueryable хранит сам запрос)
        {
            foreach (Timer timer in timers) //тут срабботает запрос
            {
                Console.WriteLine("{0, -50}\t{1}:{2}", timer.DocumentId, timer.DateStart, timer.DateFinish);
            }
            Console.WriteLine("===================================================================");
        }





    }
}
