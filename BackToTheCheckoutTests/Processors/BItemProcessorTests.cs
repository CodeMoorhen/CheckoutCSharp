using BackToTheCheckoutDll;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheCheckoutTests.Processors
{
    [TestFixture]
    public class BItemProcessorTests
    {
        [TestCase(1, 30)]
        [TestCase(2, 45)]
        [TestCase(3, 75)]
        [TestCase(4, 90)]
        [TestCase(5, 120)]
        [TestCase(6, 135)]
        public void ShouldGiveCorrectTotalForNoOfItems(int noOfItems, int expectedAnswer)
        {
            var sut = CreateSut(new Mock<IItemProcessor>().Object);

            Enumerable.Range(0, noOfItems).ToList().ForEach(f => sut.Scan("B"));

            Assert.That(sut.Total(), Is.EqualTo(expectedAnswer));
        }

        [TestCase(1, 10, 40)]
        [TestCase(2, 20, 65)]
        [TestCase(3, 30, 105)]
        [TestCase(4, 40, 130)]
        [TestCase(5, 50, 170)]
        [TestCase(6, 60, 195)]
        public void ShouldGiveCorrectTotalForNoOfItemsWhenNextReturnsAGivenValue(int noOfItems, int nextMockTotal, int expectedAnswer)
        {
            var nextMock = new Mock<IItemProcessor>();
            nextMock.Setup(s => s.Total()).Returns(nextMockTotal);
            var sut = CreateSut(nextMock.Object);

            Enumerable.Range(0, noOfItems).ToList().ForEach(f => sut.Scan("B"));

            Assert.That(sut.Total(), Is.EqualTo(expectedAnswer));
        }

        [TestCase("A", 0, 1)]
        [TestCase("ABBC", 45, 2)]
        public void ShouldCallNextForNonBItems(string Items, int expectedTotal, int expectedNoOfNexts)
        {
            var nextMock = new Mock<IItemProcessor>();
            var sut = CreateSut(nextMock.Object);

            Items.ToCharArray().ToList().ForEach(f => sut.Scan(f.ToString()));

            Assert.That(sut.Total(), Is.EqualTo(expectedTotal));
            nextMock.Verify(n => n.Scan(It.IsAny<string>()), Times.Exactly(expectedNoOfNexts));
        }

        private BItemProcessor CreateSut(IItemProcessor next)
        {
            return new BItemProcessor(next);
        }
    }
}
