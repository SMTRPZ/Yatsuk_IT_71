using FluentAssertions;
using ImageWorking.Abstract.Interfaces;
using ImageWorking.Core;
using ImageWorking.Core.Directories;
using ImageWorking.DTO;
using ImageWorking_Tests.TestsCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageWorking_Tests.Core_Tests
{
    [TestClass]
    public class SimpleDirectory_Tests
    {
        #region Exceptions Testing 

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Throws_WhenImageContentProviderIsNull()
        {
            //arrange
            var fileNameProvider = new Mock<IFileNameProvider>();
            var path = string.Empty;

            //act 
            var sut = new SimpleDirectory<string>(null, fileNameProvider.Object, path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Throws_WhenFileNameProviderIsNull()
        {
            //arrange
            var imageContentProvider = new Mock<IImageContentProvider<string>>();
            var path = string.Empty;

            //act 
            var sut = new SimpleDirectory<string>(imageContentProvider.Object, null, path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Throws_WhenPathIsNull()
        {
            //arrange
            var imageContentProvider = new Mock<IImageContentProvider<string>>();
            var fileNameProvider = new Mock<IFileNameProvider>();

            //act 
            var sut = new SimpleDirectory<string>(imageContentProvider.Object, fileNameProvider.Object, null);
        }

        #endregion

        #region Constructor Tests 

        [TestMethod]
        public void Construct_NoExceptions_WhenAllIsOk()
        {
            //arrange
            var imageContentProvider = new Mock<IImageContentProvider<string>>();
            var fileNameProvider = new Mock<IFileNameProvider>();
            var path = string.Empty;

            //act
            var sut = new SimpleDirectory<string>(imageContentProvider.Object, fileNameProvider.Object, path);
        }

        #endregion

        #region Methods Tests

        [TestMethod]
        public void IsCompositive_ReturnsFalse_Always()
        {
            //arrange

            //act 
            var sut = MockedItemsFactory.ProvideDefaultDirectory((x, y, z) => new SimpleDirectory<string>(x, y, z));
            var result = sut.IsCompositive();

            //asserts
            result.Should().BeFalse();
        }

        [TestMethod]
        public void GetImages_ReturnsEmptyList_WhenThereWereNoImages()
        {
            //arrange
            var imageFormats = MockedItemsFactory.ProvideImageFormats();

            //act 
            var sut = MockedItemsFactory.ProvideDefaultDirectory((x, y, z) => new SimpleDirectory<string>(x, y, z));
            var result = sut.GetImages(imageFormats);

            //assert
            result.Any().Should().BeFalse();
        }

        [TestMethod]
        public void GetImges_ReturnsListWithOneElement_WhenThereWasOneImageInDirectory()
        {
            //arrange
            var imageFormats = MockedItemsFactory.ProvideImageFormats();
            var expectedImages = 1;
            var expectedContent = "TEST_CONTENT";

            //act
            var sut = MockedItemsFactory.ProvideDirectoryWithImages((x, y, z) => new SimpleDirectory<string>(x, y, z));
            var result = sut.GetImages(imageFormats);

            //assert
            result.Count().Should().Be(expectedImages);
            result.First().Should().BeOfType(typeof(Image<string>));
            result.First().Content.Should().Be(expectedContent);
        }


        #endregion
    }
}
