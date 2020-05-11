using BackToTheCheckoutDll;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace BackToTheCheckoutTests.Processors
{
    [TestFixture]
    public class CItemProcessorTests
    {
        [TestCase(1, 20)]
        [TestCase(2, 40)]
        [TestCase(3, 60)]
        [TestCase(4, 80)]
        [TestCase(5, 100)]
        [TestCase(6, 120)]
        public void ShouldGiveCorrectTotalForNoOfItems(int noOfItems, int expectedAnswer)
        {
            var sut = CreateSut(new Mock<IItemProcessor>().Object);

            Enumerable.Range(0, noOfItems).ToList().ForEach(f => sut.Scan("C"));

            Assert.That(sut.Total(), Is.EqualTo(expectedAnswer));
        }

        [TestCase(1, 10, 30)]
        [TestCase(2, 20, 60)]
        [TestCase(3, 30, 90)]
        [TestCase(4, 40, 120)]
        [TestCase(5, 50, 150)]
        [TestCase(6, 60, 180)]
        public void ShouldGiveCorrectTotalForNoOfItemsWhenNextReturnsAGivenValue(int noOfItems, int nextMockTotal, int expectedAnswer)
        {
            var nextMock = new Mock<IItemProcessor>();
            nextMock.Setup(s => s.Total()).Returns(nextMockTotal);
            var sut = CreateSut(nextMock.Object);

            Enumerable.Range(0, noOfItems).ToList().ForEach(f => sut.Scan("C"));

            Assert.That(sut.Total(), Is.EqualTo(expectedAnswer));
        }

        [TestCase("A", 0, 1)]
        [TestCase("ACCD", 40, 2)]
        public void ShouldCallNextForNonCItems(string Items, int expectedTotal, int expectedNoOfNexts)
        {
            var nextMock = new Mock<IItemProcessor>();
            var sut = CreateSut(nextMock.Object);

            Items.ToCharArray().ToList().ForEach(f => sut.Scan(f.ToString()));

            Assert.That(sut.Total(), Is.EqualTo(expectedTotal));
            nextMock.Verify(n => n.Scan(It.IsAny<string>()), Times.Exactly(expectedNoOfNexts));
        }

        private CItemProcessor CreateSut(IItemProcessor next)
        {
            return new CItemProcessor(next);
        }
    }
}
