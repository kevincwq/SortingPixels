using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingPixels.ViewModels;

namespace SortingPixels.UnitTest
{
    [TestClass]
    public class HueSortVmTest
    {
        [TestMethod]
        public void TestVmInit()
        {
            var hueSortVm = new HueSortViewModel();
            Assert.AreEqual(0d, hueSortVm.RenderWidth);
            Assert.AreEqual(0d, hueSortVm.RenderHeight);
            Assert.IsNotNull( hueSortVm.Randomize);
            Assert.IsNotNull(hueSortVm.Sort);
            Assert.IsNull(hueSortVm.Image);
        }
    }
}
