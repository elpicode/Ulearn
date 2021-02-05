using System.Collections.Generic;

namespace StructBenchmarking
{
    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            return DataBuilder.BuildChartData( "Create array",
                new ArrayCreationTask(), benchmark, repetitionsCount);
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            return DataBuilder.BuildChartData("Call method with argument", 
                new CallWithTask(), benchmark, repetitionsCount);
        }
    }
    
    public interface IFact
    {
        ITask CreateTask(int sz, string tskName);
    }

    public class ArrayCreationTask : IFact
    {
        public ITask CreateTask(int sz, string tskName)
        {
            if (tskName == "Class")
                return new ClassArrayCreationTask(sz);
            return new StructArrayCreationTask(sz);
        }
    }

    public class CallWithTask : IFact
    {
        public ITask CreateTask(int sz, string tskName)
        {
            if (tskName == "Class")
                return new MethodCallWithClassArgumentTask(sz);
            return new MethodCallWithStructArgumentTask(sz);
        }
    }

    public static class DataBuilder
    {
        public static ChartData BuildChartData( string title, IFact fact,  
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();
            foreach (var sz in Constants.FieldCounts)
            {
                classesTimes.Add(new ExperimentResult(sz,
                    (benchmark.MeasureDurationInMs(fact.CreateTask(sz, "Class"), repetitionsCount))));
                structuresTimes.Add(new ExperimentResult(sz,
                    benchmark.MeasureDurationInMs(fact.CreateTask(sz, "Structure"), repetitionsCount)));
            }
            return new ChartData
            {
                Title = title,
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
    }
}