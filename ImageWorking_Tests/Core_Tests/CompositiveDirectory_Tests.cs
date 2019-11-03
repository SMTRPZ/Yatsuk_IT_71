using FluentAssertions;
using ImageWorking.Abstract.Interfaces;
using ImageWorking.Core;
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
    public class CompositiveDirectory_Tests
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
            var sut = new CompositiveDirectory<string>(null, fileNameProvider.Object, path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Throws_WhenFileNameProviderIsNull()
        {
            //arrange
            var imageContentProvider = new Mock<IImageContentProvider<string>>();
            var path = string.Empty;

            //act 
            var sut = new CompositiveDirectory<string>(imageContentProvider.Object, null, path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Throws_WhenPathIsNull()
        {
            //arrange
            var imageContentProvider = new Mock<IImageContentProvider<string>>();
            var fileNameProvider = new Mock<IFileNameProvider>();

            //act 
            var sut = new CompositiveDirectory<string>(imageContentProvider.Object, fileNameProvider.Object, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddSubdirectory_Throws_WhenSubDirectoryIsNull()
        {
            //arrange
            CompositiveDirectory<string> subDirectory = null;

            //act
            var sut = MockedItemsFactory.ProvideDefaultDirectory((x, y, z) => new CompositiveDirectory<string>(x, y, z));
            sut.AddSubdirectory(subDirectory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddSubdirectories_Throws_WhenListOfSubdirectoriesIsNull()
        {
            //arrange
            List<CompositiveDirectory<string>> subDirectories = null;

            //act
            var sut = MockedItemsFactory.ProvideDefaultDirectory((x, y, z) => new CompositiveDirectory<string>(x, y, z));
            sut.AddRangeOfSubdirectories(subDirectories);
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
            var sut = new CompositiveDirectory<string>(imageContentProvider.Object, fileNameProvider.Object, path);
        }

        #endregion

        #region Methods Tests

        [TestMethod]
        public void IsCompositive_ReturnsFalse_WhenNoSubDirectoriesWereAdded()
        {
            //arrange

            //act 
            var sut = MockedItemsFactory.ProvideDefaultDirectory((x, y, z) => new CompositiveDirectory<string>(x, y, z));
            var result = sut.IsCompositive();

            //asserts
            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsCompositive_ReturnsTrue_WhenOneSubDirectoryWasAdded()
        {
            //arrange
            var subDirectory = MockedItemsFactory.ProvideDefaultDirectory((x, y, z) => new CompositiveDirectory<string>(x, y, z));

            //act
            var sut = MockedItemsFactory.ProvideDefaultDirectory((x, y, z) => new CompositiveDirectory<string>(x, y, z));
            sut.AddSubdirectory(subDirectory);
            var result = sut.IsCompositive();

            //asserts
            result.Should().BeTrue();
        }

        [TestMethod]
        public void AddSubdirectory_AllIsOk_WhenSubdirectoryWasAdded()
        {
            //arrange
            var subDirectory = MockedItemsFactory.ProvideDefaultDirectory((x, y, z) => new CompositiveDirectory<string>(x, y, z));

            //act
            var sut = MockedItemsFactory.ProvideDefaultDirectory((x, y, z) => new CompositiveDirectory<string>(x, y, z));
            sut.AddSubdirectory(subDirectory);
        }

        [TestMethod]
        public void AddSubdirectories_AllIsOk_WhenListOfSubdirectoriesWasAdded()
        {
            //arrange
            var subDirectories = new List<CompositiveDirectory<string>>();

            //act
            var sut = MockedItemsFactory.ProvideDefaultDirectory((x, y, z) => new CompositiveDirectory<string>(x, y, z));
            sut.AddRangeOfSubdirectories(subDirectories);
        }

        [TestMethod]
        public void GetImages_ReturnsEmptyList_WhenThereWereNoImages()
        {
            //arrange
            var imageFormats = MockedItemsFactory.ProvideImageFormats();

            //act 
            var sut = MockedItemsFactory.ProvideDefaultDirectory((x, y, z) => new CompositiveDirectory<string>(x, y, z));
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
            var sut = MockedItemsFactory.ProvideDirectoryWithImages((x, y, z) => new CompositiveDirectory<string>(x, y, z));
            var result = sut.GetImages(imageFormats);

            //assert
            result.Count().Should().Be(expectedImages);
            result.First().Should().BeOfType(typeof(Image<string>));
            result.First().Content.Should().Be(expectedContent);
        }

        [TestMethod]
        public void GetImges_ReturnsListWithOneElement_WhenThereWasOneImageInSubdirectory()
        {
            //arrange
            var imageFormats = MockedItemsFactory.ProvideImageFormats();
            var subDirectory = MockedItemsFactory.ProvideDirectoryWithImages((x, y, z) => new CompositiveDirectory<string>(x, y, z));
            var expectedImages = 1;
            var expectedContent = "TEST_CONTENT";

            //act
            var sut = MockedItemsFactory.ProvideDefaultDirectory((x, y, z) => new CompositiveDirectory<string>(x, y, z));
            sut.AddSubdirectory(subDirectory);
            var result = sut.GetImages(imageFormats);

            //assert
            result.Count().Should().Be(expectedImages);
            result.First().Should().BeOfType(typeof(Image<string>));
            result.First().Content.Should().Be(expectedContent);
        }

        #endregion
    }
}
