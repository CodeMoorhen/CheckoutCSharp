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
    public class EndItemProcessorTests
    {
        [TestCase("A")]
        [TestCase("B")]
        [TestCase("C")]
        [TestCase("D")]
        [TestCase("E")]
        public void ShouldThrowExceptionRegardlessOfInput(string item)
        {
            var sut = CreateSut();

            var exception = Assert.Throws<ApplicationException>(() => sut.Scan(item));

            Assert.That(exception, Is.Not.EqualTo(null));
            Assert.That(exception.Message, Is.EqualTo("Something has gone wrong"));
        }

        [Test]
        public void ShouldReturnZeroForTotal()
        {
            var sut = CreateSut();

            Assert.That(sut.Total(), Is.EqualTo(0));
        }

        private EndItemProcessor CreateSut()
        {
            return new EndItemProcessor();
        }
    }
}
