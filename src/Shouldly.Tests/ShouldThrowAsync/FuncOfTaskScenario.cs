using NUnit.Framework;
#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrowAsync
{
   
    public class FuncOfTaskScenario : ShouldlyShouldTestScenario
    {
      
        public void ShouldPasss()
        {
            var ex = Should.ThrowAsync<InvalidOperationException>(() =>
            {
                var task = Task.Factory.StartNew(() =>
                {
                    throw new InvalidOperationException();
                },
                    CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default);
                return task;
            });
            ex.ShouldNotBe(null);
            ex.ShouldBeOfType<Task<InvalidOperationException>>();
        }

        protected override void ShouldThrowAWobbly()
        {
            Should.ThrowAsync<InvalidOperationException>(() =>
            {
                throw new RankException();
            }).TimeoutAfter(TimeSpan.FromSeconds(1)).Wait();


            //  Should.ThrowAsync(...).TimeoutAfter(Timespan.FromSeconds(1)).Wait();
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Base"; }
        }

        protected override void ShouldPass()
        {
            throw new NotImplementedException();
        }
    }

    [TestFixture]
    public class Tests
    {
        public class MyClass
        {
            public int Add(int a, int b)
            {
                if (a > 5)
                    throw new ArgumentException("a is greater than 5");
                return a + b;
            }
        }
        [Test]
        public void SampleTest()
        {
            var m = new MyClass();
            m.Add(3,4).ShouldBe(7);
        }

        [Test]
        public void ExceptionTest()
        {
            var m = new MyClass();
            Should.ThrowAsync<ArgumentException>(() =>
            {
                var t = Task.Factory.StartNew(() => m.Add(6, 1));
               // t.Wait();
                return t;
              
            }).Wait();
        }
    }
}
#endif