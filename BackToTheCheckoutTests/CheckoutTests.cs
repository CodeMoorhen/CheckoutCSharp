using BackToTheCheckoutDll;
using BackToTheCheckoutDll.Processors;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheCheckoutTests
{
    [TestFixture]
    public class CheckoutTests
    {
        [Test]
        public void ShouldThrowAnExceptionWhenPricingRulesIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Checkout(null));

            Assert.That(exception, Is.Not.EqualTo(null));
            Assert.That(exception.Message, Is.EqualTo("Value cannot be null.\r\nParameter name: pricingRules"));
        }

        [Test]
        public void ShouldThrowAnExceptionWhenBadItemIsSuppliedToScan()
        {
            var sut = CreateSut();
            var exception = Assert.Throws<ApplicationException>(() => sut.Scan("E"));

            Assert.That(exception, Is.Not.EqualTo(null));
            Assert.That(exception.Message, Is.EqualTo("Something has gone wrong"));
        }

        [TestCase("A", 50)]
        [TestCase("AA", 100)]
        [TestCase("AAA", 130)]
        [TestCase("AB", 80)]
        [TestCase("ABC", 100)]
        [TestCase("ABCD", 115)]
        [TestCase("AABABABCDDDDCCC", 395)]
        public void ShouldReturnCorrectTotalToInputs(string inputs, int expectedResult)
        {
            var sut = CreateSut();
            inputs.ToCharArray().ToList().ForEach(f => sut.Scan(f.ToString()));
            
            Assert.That(sut.Total, Is.EqualTo(expectedResult));
        }

        private Checkout CreateSut()
        {
            var pricingRules = new AItemProcessor(new BItemProcessor(new CItemProcessor(new DItemProcessor(new EndItemProcessor()))));
            return new Checkout(pricingRules);
        }
    }
}
