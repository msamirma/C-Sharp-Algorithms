namespace C_Sharp_Algorithms.AlgorithmsTests
{
    using System;
    using System.Diagnostics;

    using NUnit.Framework;
    using Algorithms.Strings;

    [TestFixture]
    public class StringSearchTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            
        }

        [Test]
        public void FindSubString()
        {
            var source = "The quick brown fox jumps over the lazy dog";
            var pattern = "n f";
                        
            // prime the extension method ~5000 ticks
            Assert.IsTrue(source.Has("n f"));

            var swMine = Stopwatch.StartNew();
            Assert.IsTrue(source.Has(" "));
            swMine.Stop();

            Assert.IsTrue(source.Has("The quick brown fox jumps over the lazy dog"));
            Assert.IsTrue(source.Has("dog"));
            Assert.IsTrue(source.Has("over the "));
            Assert.IsTrue(source.Has("f"));            
            Assert.IsFalse(source.Has("b f"));
            Assert.IsFalse(source.Has("b f"));
            Assert.IsFalse(source.Has(null));           
            
            var swTheirs = Stopwatch.StartNew();
            Assert.IsTrue(source.Contains(pattern));
            swTheirs.Stop();


            var diff = swMine.ElapsedTicks - swTheirs.ElapsedTicks;
            Console.WriteLine(diff);
        }
    }
}
