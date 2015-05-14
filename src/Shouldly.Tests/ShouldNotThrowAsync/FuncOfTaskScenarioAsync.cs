using Shouldly.Tests.TestHelpers;
#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Shouldly.Tests.ShouldNotThrowAsync
{
    
    [TestFixture]
    public class FuncOfTaskScenarioAsync
    {
        [Test]
        public void ShouldThrowAWobbly()
        {
            try
            {
                Should.NotThrowAsync(() =>
                {
                    var task = Task.Factory.StartNew(() =>
                    {
                        throw new RankException();
                        return  1 + 1;
                        
                    }, CancellationToken.None, TaskCreationOptions.None,
                        TaskScheduler.Default);
                    return task;
                }, TimeSpan.FromSeconds(2), () => "Some additional context").Wait();
            }
            catch (AggregateException e)
            {
                var inner = e.Flatten().InnerException; 
                Console.WriteLine(inner.GetType());
               // var ex = inner.ShouldBeOfType<RankException>();
                inner.Message.ShouldContainWithoutWhitespace(@"
                            The provided expression
                                < not throw async>b__29 
                            System.RankException
                                but does
                            Additional Info:
                            Some additional context");
            }
        }

        [Test]
        public void ShouldPass()
        {
            Should.NotThrowAsync(() =>
            {
                var task = Task.Factory.StartNew(() => 1 + 1,
                    CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default);
                return task;
            },TimeSpan.FromSeconds(1000),()=>"blah").Wait();
        }
    }
}
#endif