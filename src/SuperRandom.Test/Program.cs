//using System;
//using System.Linq;

//namespace Anduin.SuperRandom.Test
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            var startTime = DateTime.UtcNow;
//            new Randomizer().PrimeNumbers().Take(1000000).ToList();
//            var endTime = DateTime.UtcNow;
//            var timeCosts = endTime - startTime;
//            Console.WriteLine(timeCosts);

//            //while (true)
//            //{
//            //    Console.WriteLine("How many unique random numbers do you want?");
//            //    if (!int.TryParse(Console.ReadLine(), out int counts))
//            //    {
//            //        break;
//            //    }
//            //    var array = new int[counts];
//            //    foreach (var rand in new Randomizer().GetRandomNumbers(counts))
//            //    {
//            //        Console.WriteLine("Got random number: " + rand);
//            //        array[rand] += 1;
//            //    }
//            //    foreach (var bit in array)
//            //    {
//            //        if (bit > 1)
//            //        {
//            //            Console.WriteLine("Bit set twice! Wrong code!");
//            //        }
//            //        if (bit < 1)
//            //        {
//            //            Console.WriteLine("Bit not set! Wrong code!");
//            //        }
//            //    }
//            //}
//        }
//    }
//}
