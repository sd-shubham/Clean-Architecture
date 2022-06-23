using App.Application.Exceptions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace ApiBenchMark
{
    //[SimpleJob(RunStrategy.ColdStart, launchCount: 1, warmupCount: 5, targetCount: 5, id: "FastAndDirtyJob")]
    [MemoryDiagnoser]
    public class StringHandlerBenchMark
    {
        //[Benchmark]
        //public void StringHandler()
        //{
        //    string.IsNullOrEmpty("jijij").IsTrue($"some error");
        //    var a = new StringHandlerBenchMark();
        //     (a is null).IsTrue("")
        //}
        //[Benchmark]
        //public void SimpleStringHandler()
        //{
        //    int num = 10;
        //    "shubham".NullOrEmpty($"shgdjgkjd {num}");
        //}
        [Benchmark]
        public void AdvancedStringHandler()
        {
            int num = 10;
            bool t = false;
            t.IsTrue($"shgdjgkjd {num}");
        }
    }
}
