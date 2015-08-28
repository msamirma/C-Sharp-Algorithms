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

    using NUnit.Framework.Constraints;

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
            var list = new SortedList<int>();
            list.Add(1);
            list.Add(3);
            list.Add(5);
            list.Add(4);
            list.Add(8);                      

            Assert.AreEqual("1,3,4,5,8", string.Join(",", list));
            Assert.AreEqual(5, list.Count);
        }
    }
}
