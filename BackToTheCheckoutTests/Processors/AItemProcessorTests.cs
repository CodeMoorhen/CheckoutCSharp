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
    public class AItemProcessorTests
    {
        [TestCase(1, 50)]
        [TestCase(2, 100)]
        [TestCase(3, 130)]
        [TestCase(4, 180)]
        [TestCase(5, 230)]
        [TestCase(6, 260)]
        public void ShouldGiveCorrectTotalForNoOfItemsWhenNextReturnsNothing(int noOfItems, int expectedAnswer)
        {
            var sut = CreateSut(new Mock<IItemProcessor>().Object);

            Enumerable.Range(0, noOfItems).ToList().ForEach(f => sut.Scan("A"));
            
            Assert.That(sut.Total(), Is.EqualTo(expectedAnswer));
        }

        [TestCase(1, 10, 60)]
        [TestCase(2, 20, 120)]
        [TestCase(3, 30, 160)]
        [TestCase(4, 40, 220)]
        [TestCase(5, 50, 280)]
        [TestCase(6, 60, 320)]
        public void ShouldGiveCorrectTotalForNoOfItemsWhenNextReturnsAGivenValue(int noOfItems, int nextMockTotal, int expectedAnswer)
        {
            var nextMock = new Mock<IItemProcessor>();
            nextMock.Setup(s => s.Total()).Returns(nextMockTotal);
            var sut = CreateSut(nextMock.Object);

            Enumerable.Range(0, noOfItems).ToList().ForEach(f => sut.Scan("A"));

            Assert.That(sut.Total(), Is.EqualTo(expectedAnswer));
        }

        [TestCase("B", 0, 1)]
        [TestCase("ABAC", 100, 2)]
        public void ShouldCallNextForNonAItems(string Items, int expectedTotal, int expectedNoOfNexts)
        {
            var nextMock = new Mock<IItemProcessor>();
            var sut = CreateSut(nextMock.Object);

            Items.ToCharArray().ToList().ForEach(f => sut.Scan(f.ToString()));

            Assert.That(sut.Total(), Is.EqualTo(expectedTotal));
            nextMock.Verify(n => n.Scan(It.IsAny<string>()), Times.Exactly(expectedNoOfNexts));
        }

        private AItemProcessor CreateSut(IItemProcessor next)
        {
            return new AItemProcessor(next);
        }
    }
}
