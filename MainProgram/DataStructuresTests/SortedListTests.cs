using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace C_Sharp_Algorithms.DataStructuresTests
{
    using DataStructures.Lists;

    [TestFixture]
    public class SortedListTests
    {
        [TestFixtureSetUp]
        public void Init()
        {            
        }

        [Test]
        public void TestAdd()
        {
            var list = new SortedList<int> { 5, 10, 1, 1 };

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Assert.AreEqual("1,1,5,10", string.Join(",",list));
            Assert.AreEqual(4, list.Count);
        }
    }
}
