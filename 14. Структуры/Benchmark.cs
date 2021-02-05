using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
    {
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            task.Run();
            var stopWtch = new Stopwatch();
            
            GC.Collect();                  
            GC.WaitForPendingFinalizers(); 
            
            
            stopWtch.Start();
            for (int i = 0; i < repetitionCount; i++) 
                task.Run();
            stopWtch.Stop();
            return (double) stopWtch.ElapsedMilliseconds / repetitionCount;
        }
    }
    
    public class StringFromStringBuilder : ITask
    {
        public void Run()
        {
            var strBuild = new StringBuilder();
            for (int i = 0; i < 10000; i++)
                strBuild.Append('a');
            strBuild.ToString();
        }
    }
    
    public class StringFromString : ITask
    {
        public void Run()
        {
            new string('a', 10000);
        }
    }

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var benchmark = new Benchmark();
            var res1 = benchmark.MeasureDurationInMs(new StringFromStringBuilder(), 10000);
            var res2 = benchmark.MeasureDurationInMs(new StringFromString(), 10000);
            Assert.Less(res2, res1);
        }
    }
}