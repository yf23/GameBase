using System.Linq;

using NUnit.Framework;

using Game.Models;
using Game.GameRules;

namespace UnitTests.GameRules
{
    [TestFixture]
    public class LevelTableHelperTests
    {
        [Test]
        public void LevelTableHelper_Valid_ClearAndLoadDataTable_Should_Pass()
        {
            // Arrange

            // Act
            var result = LevelTableHelper.ClearAndLoadDataTable();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void LevelTableHelper_Valid_LoadLevelData_Should_Pass()
        {
            // Arrange

            // Act
            var result = LevelTableHelper.LoadLevelData();

            // Reset

            // Assert
            Assert.AreEqual(44, result);
        }

        [Test]
        public void LevelTableHelper_GetLevelAttribute_Valid_Level_Should_Pass()
        {
            // Arrange

            // Act
            var result = LevelTableHelper.GetLevelAttribute(1);

            // Reset

            // Assert
            Assert.AreEqual(1, result.Level);
        }

        [Test]
        public void LevelTableHelper_GetLevelAttribute_InValid_Level_0_Should_ReturnLast()
        {
            // Arrange

            // Act
            var result = LevelTableHelper.GetLevelAttribute(0);

            // Reset

            // Assert
            Assert.AreEqual(0, result.Level);
        }

        [Test]
        public void LevelTableHelper_GetLevelAttribute_InValid_Level_Neg1_Should_ReturnLast()
        {
            // Arrange

            // Act
            var result = LevelTableHelper.GetLevelAttribute(-1);

            // Reset

            // Assert
            Assert.AreEqual(21, result.Level);
        }

        [Test]
        public void LevelTableHelper_GetLevelAttribute_InValid_Level_25_Should_ReturnLast()
        {
            // Arrange

            // Act
            var result = LevelTableHelper.GetLevelAttribute(25);

            // Reset

            // Assert
            Assert.AreEqual(21, result.Level);
        }
    }
}