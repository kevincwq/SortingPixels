using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingPixels.Models;
using System;

namespace SortingPixels.UnitTest
{
    [TestClass]
    public class HueSortModelTest
    {
        [TestMethod]
        public void TestInvalidInit()
        {
            var hueSortModel = new HueSortModel(0, 0);
            Assert.IsNotNull(hueSortModel);
            Assert.AreEqual(0, hueSortModel.Height);
            Assert.AreEqual(0, hueSortModel.Width);
            var rbm = hueSortModel.Randomize();
            Assert.IsNull(rbm);

            var sbm = hueSortModel.SortByHue();
            Assert.IsNull(sbm);

            var spbm = hueSortModel.SortByHueParallel();
            Assert.IsNull(spbm);
        }

        public void TestInitWithWidthAndHeight(int width, int height)
        {
            var hueSortModel = new HueSortModel(width, height);
            Assert.IsNotNull(hueSortModel);
            Assert.AreEqual(height, hueSortModel.Height);
            Assert.AreEqual(width, hueSortModel.Width);
            var rbm = hueSortModel.Randomize();
            Assert.IsNotNull(rbm);
            Assert.AreEqual(height, rbm.PixelHeight);
            Assert.AreEqual(width, rbm.PixelWidth);

            var sbm = hueSortModel.SortByHue();
            Assert.IsNotNull(sbm);
            Assert.AreEqual(height, sbm.PixelHeight);
            Assert.AreEqual(width, sbm.PixelWidth);

            var spbm = hueSortModel.SortByHueParallel();
            Assert.IsNotNull(spbm);
            Assert.AreEqual(height, spbm.PixelHeight);
            Assert.AreEqual(width, spbm.PixelWidth);
        }

        [TestMethod]
        public void TestValidInit()
        {
            TestInitWithWidthAndHeight(1920, 1080);
        }

        [TestMethod]
        public void TestBigInit()
        {
            TestInitWithWidthAndHeight(5000, 5000);
        }

        [TestMethod]
        public void TestOverflowInit()
        {
            Assert.ThrowsException<OverflowException>(() => TestInitWithWidthAndHeight(50000, 50000));
        }
    }
}
