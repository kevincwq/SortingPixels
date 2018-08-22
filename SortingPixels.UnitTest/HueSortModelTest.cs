using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingPixels.Models;

namespace SortingPixels.UnitTest
{
    [TestClass]
    public class HueSortModelTest
    {
        [TestMethod]
        public void TestZeroInit()
        {
            var hueSortModel = new HueSortModel(0,0);
            Assert.IsNotNull(hueSortModel);
            Assert.AreEqual(0, hueSortModel.Height);
            Assert.AreEqual(0, hueSortModel.Width);
        }
    }
}
