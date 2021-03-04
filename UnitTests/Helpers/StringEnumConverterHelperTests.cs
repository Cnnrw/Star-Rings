using Game.Converters;
using Game.Enums;
using Game.Models;

using NUnit.Framework;

namespace UnitTests.Helpers
{
    [TestFixture]
    class StringEnumConverterHelperTests
    {
        [Test]
        public void StringEnumConvert_String_Should_Pass()
        {
            var myConverter = new StringEnumConverter();

            var myObject = "Feet";
            var result = myConverter.Convert(myObject, typeof(ItemLocationEnum), null, null);
            var expected = ItemLocationEnum.Feet;

            Assert.AreEqual(expected, result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvert_Enum_Should_Pass()
        {
            var myConverter = new StringEnumConverter();

            var myObject = ItemLocationEnum.Feet;
            var result = myConverter.Convert(myObject, null, null, null);
            var expected = 40;

            Assert.AreEqual(expected, result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvert_Other_Should_Skip()
        {
            var myConverter = new StringEnumConverter();

            var myObject = new ItemModel();
            var result = myConverter.Convert(myObject, null, null, null);
            var expected = 0;

            Assert.AreEqual(expected, result, TestContext.CurrentContext.Test.Name);
        }

        // Convert Back
        [Test]
        public void IntEnumConvertBack_Should_Skip()
        {
            var myConverter = new IntEnumConverter();

            var myObject = "Bogus";
            var result = myConverter.ConvertBack(myObject, null, null, null);
            var expected = 0;

            Assert.AreEqual(expected, result, TestContext.CurrentContext.Test.Name);
        }

        // Convert Back
        [Test]
        public void IntEnumConvertBack_Int_Should_Pass()
        {
            var myConverter = new IntEnumConverter();

            var myObject = 40;
            var result = myConverter.ConvertBack(myObject, typeof(ItemLocationEnum), null, null);
            var expected = "Feet";

            Assert.AreEqual(expected, result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvertBack_String_Should_Pass()
        {
            var myConverter = new StringEnumConverter();

            var myObject = "Feet";
            var result = myConverter.ConvertBack(myObject, typeof(ItemLocationEnum), null, null);
            var expected = ItemLocationEnum.Feet;

            Assert.AreEqual(expected, result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvertBack_Enum_Should_Skip()
        {
            var myConverter = new StringEnumConverter();

            var myObject = ItemLocationEnum.Feet;
            var result = myConverter.ConvertBack(myObject, null, null, null);
            var expected = 0;

            Assert.AreEqual(expected, result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvertBack_Other_Should_Skip()
        {
            var myConverter = new StringEnumConverter();

            var myObject = new ItemModel();
            var result = myConverter.ConvertBack(myObject, null, null, null);
            var expected = 0;

            Assert.AreEqual(expected, result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvertBack_Int_Should_Pass()
        {
            var myConverter = new StringEnumConverter();

            var myObject = 40;
            var result = myConverter.ConvertBack(myObject, typeof(ItemLocationEnum), null, null);
            var expected = "Feet";

            Assert.AreEqual(expected, result, TestContext.CurrentContext.Test.Name);
        }
    }
}
