﻿using Game;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class RebelBasePageTests
    {

        // Base Constructor
        //public RebelBasePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new RebelBasePage();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App           app;
        private RebelBasePage page;

        [Test]
        public void RebelBasePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void RebelBasePage_ItemsButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.ItemsButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RebelBasePage_CharactersButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.CharactersButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RebelBasePage_MonstersButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.MonstersButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RebelBasePage_ScoresButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.ScoresButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}