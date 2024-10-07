using System.ComponentModel.DataAnnotations;
using CatalogService.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CatalogService.Domain.UnitTests.Entitites
{
    [TestFixture]
    internal class CategoryTests
    {
        [Test]
        public void UpdateMethod_Works()
        {
            var testObject = new Category
            {
                Id = 1,
                Image = "url",
                Name = "name",
                Parent = new Category
                {
                    Id = 2,
                    Image = "url for parent",
                    Name = "Parent"
                }
            };

            var updatedObject = new Category
            {
                Image = "url new",
                Name = "New Name",
                Parent = null
            };

            // Act
            testObject.Update(updatedObject);

            // Assert
            testObject.Should().BeEquivalentTo(new { Id = 1, Image = "url new", Name = "New Name" });
        }
    }
}
