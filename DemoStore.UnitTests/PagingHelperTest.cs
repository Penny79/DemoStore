using System.Web.Mvc;
using DemoStore.WebUI.HtmlHelpers;
using DemoStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoStore.UnitTests
{
    [TestClass]
    public class PagingHelperTest
    {
        [TestMethod]
        public void WhenThereAreNoItems_Should_Not_Create_Any_Paging()
        {
            // Arrange
            HtmlHelper htmlHelper = null;

            // Act
            var result = htmlHelper.PageLinks(new PagingInfo() { TotalItems = 0, ItemsPerPage = 10, }, (i) => "s");

            // Assert
            Assert.IsTrue(result.ToString().Trim().Length == 0, "No paging details should be created");
        }

        [TestMethod]
        public void WhenThereIsOnlyOnePage_ItShouldBeSelected()
        {
            // Arrange
            HtmlHelper htmlHelper = null;
            string expectedResult = @"<a class=""selected"" href=""url"">1</a>";

            // Act
            var result = htmlHelper.PageLinks(new PagingInfo() { CurrentPage = 1, TotalItems = 4, ItemsPerPage = 10 }, (i) => "url");

            // Assert
            Assert.AreEqual(expectedResult, result.ToString(), "Should match the expected result.");
        }

        [TestMethod]
        public void WhenThereAre3Pages_SecondPage_ShouldBeSelected()
        {
            // Arrange
            HtmlHelper htmlHelper = null;
            string expectedResult = @"<a href=""url1"">1</a><a class=""selected"" href=""url2"">2</a><a href=""url3"">3</a>";

            // Act
            var actual = htmlHelper.PageLinks(new PagingInfo() { ItemsPerPage = 2, TotalItems = 5, CurrentPage = 2 }, (i) => "url" + i.ToString());

            // Assert
            Assert.AreEqual(expectedResult, actual.ToString(), "Only the second item should be selected");
        }
    }
}
