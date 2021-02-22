﻿using Game.Enums;
using Game.Models;

using NUnit.Framework;

namespace UnitTests.Models
{
    [TestFixture]
    public class MenuItemModelTests
    {
        [Test]
        public void MenuItemModel_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new MenuItemModel();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void MenuItemModel_Set_Default_Should_Pass()
        {
            // Arrange
            var result = new MenuItemModel();

            // Act

            // Test all the Setters
            result.Id = MenuItemEnum.Village;
            result.Title = "bogus title";

            // Reset

            // Assert

            // The Get is tested by retrieving it here as well.
            Assert.AreEqual("bogus title", result.Title);
            Assert.AreEqual(MenuItemEnum.Village, result.Id);
        }
    }
}