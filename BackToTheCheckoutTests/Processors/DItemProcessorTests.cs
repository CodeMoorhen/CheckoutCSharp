using BackToTheCheckoutDll;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace BackToTheCheckoutTests.Processors
{
    [TestFixture]
    public class DItemProcessorTests
    {
        [TestCase(1, 15)]
        [TestCase(2, 30)]
        [TestCase(3, 45)]
        [TestCase(4, 60)]
        [TestCase(5, 75)]
        [TestCase(6, 90)]
        public void ShouldGiveCorrectTotalForNoOfItems(int noOfItems, int expectedAnswer)
        {
            var sut = CreateSut(new Mock<IItemProcessor>().Object);

            Enumerable.Range(0, noOfItems).ToList().ForEach(f => sut.Scan("D"));

            Assert.That(sut.Total(), Is.EqualTo(expectedAnswer));
        }

        [TestCase(1, 10, 25)]
        [TestCase(2, 20, 50)]
        [TestCase(3, 30, 75)]
        [TestCase(4, 40, 100)]
        [TestCase(5, 50, 125)]
        [TestCase(6, 60, 150)]
        public void ShouldGiveCorrectTotalForNoOfItemsWhenNextReturnsAGivenValue(int noOfItems, int nextMockTotal, int expectedAnswer)
        {
            var nextMock = new Mock<IItemProcessor>();
            nextMock.Setup(s => s.Total()).Returns(nextMockTotal);
            var sut = CreateSut(nextMock.Object);

            Enumerable.Range(0, noOfItems).ToList().ForEach(f => sut.Scan("D"));

            Assert.That(sut.Total(), Is.EqualTo(expectedAnswer));
        }

        [TestCase("A", 0, 1)]
        [TestCase("ADDA", 30, 2)]
        public void ShouldCallNextForNonDItems(string Items, int expectedTotal, int expectedNoOfNexts)
        {
            var nextMock = new Mock<IItemProcessor>();
            var sut = CreateSut(nextMock.Object);

            Items.ToCharArray().ToList().ForEach(f => sut.Scan(f.ToString()));

            Assert.That(sut.Total(), Is.EqualTo(expectedTotal));
            nextMock.Verify(n => n.Scan(It.IsAny<string>()), Times.Exactly(expectedNoOfNexts));
        }

        private DItemProcessor CreateSut(IItemProcessor next)
        {
            return new DItemProcessor(next);
        }
    }
}
