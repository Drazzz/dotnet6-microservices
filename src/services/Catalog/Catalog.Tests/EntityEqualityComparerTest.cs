using Catalog.Domain;
using NUnit.Framework;
using System;

namespace Catalog.Tests
{
    [TestFixture]
    public class EntityEqualityComparerTest
    {
        [Test]public void CompareTwoObjectShouldReturnFalse()
        {
            var product1 = Product.From("Product1", "Description1", "Summary1", "pln", 1, (short)CategoryType.SmartBand);
            var product2 = Product.From("Product2", "Description2", "Summary2", "pln", 2, (short)CategoryType.Tablet);

            Assert.IsFalse(product1.Equals(product2));
        }

        [Test]
        public void CompareTwoObjectShouldReturnTrue()
        {
            var product1 = Product.From("Product1", "Description1", "Summary1", "pln", 1, (short)CategoryType.SmartBand);
            var product2 = Product.From(product1.Id, "Product1", "Description1", "Summary1", new Money("pln", 1), CategoryType.SmartBand);
            Assert.IsTrue(product1.Equals(product2));
        }

        [Test]
        public void Two_entities_needs_always_be_compared_by_id()
        {
            //arrange
            var id1 = Guid.NewGuid();
            var product1 = Product.From(id1, "name1", "description1", "summary1", new Money("usd", 1), CategoryType.SmartBand);
            var product2 = Product.From(id1, "name2", "description2", "summary2", new Money("pln", 2), CategoryType.Smartphone);

            //assert
            Assert.That(product1, Is.EqualTo(product2));
        }
    }
}
