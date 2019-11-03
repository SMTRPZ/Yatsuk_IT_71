using FluentAssertions;
using ImageWorking.Abstract.Interfaces;
using ImageWorking.DTO;
using ImageWorking.Services;
using ImageWorking_Tests.TestsCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace ImageWorking_Tests.Services_Tests
{
    [TestClass]
    public class ImageService_Tests
    {
        #region Exceptions Testsing

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Throws_WhenSubdirectoriesPathProviderIsNull()
        {
            //arrange
            var imageContentProvider = new Mock<IImageContentProvider<string>>();
            var fileNameProvider = new Mock<IFileNameProvider>();

            //act 
            var sut = new ImagesService<string>(null, imageContentProvider.Object, fileNameProvider.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Throws_WhenImageContentProviderIsNull()
        {
            //arrange
            var subdirectoriesPathProvider = new Mock<ISubdirectoriesPathProvider>();
            var fileNameProvider = new Mock<IFileNameProvider>();

            //act 
            var sut = new ImagesService<string>(subdirectoriesPathProvider.Object, null, fileNameProvider.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Throws_WhenFileNameProviderIsNull()
        {
            //arrange
            var subdirectoriesPathProvider = new Mock<ISubdirectoriesPathProvider>();
            var imageContentProvider = new Mock<IImageContentProvider<string>>();

            //act 
            var sut = new ImagesService<string>(subdirectoriesPathProvider.Object, imageContentProvider.Object, null);
        }

        #endregion

        #region Constructors Testing

        [TestMethod]
        public void Construct_NoExceptions_WhenAllIsOk()
        {
            //arrange
            var subdirectoriesPathProvider = new Mock<ISubdirectoriesPathProvider>();
            var imageContentProvider = new Mock<IImageContentProvider<string>>();
            var fileNameProvider = new Mock<IFileNameProvider>();

            //act 
            var sut = new ImagesService<string>(subdirectoriesPathProvider.Object, imageContentProvider.Object, fileNameProvider.Object);
        }

        #endregion

        #region Methods Testing

        [TestMethod]
        public void  GetImages_ReturnsEmptyList_WhenThereWereNoImages()
        {
            //arange

            //act
            var sut = MockedItemsFactory.ProvideDefaultImagesService();
            var result = sut.GetImages(string.Empty);

            //assert
            result.Any().Should().BeFalse();
        }

        [TestMethod]
        public void GetImages_ReturnsListWithOneElement_WhenThereWasImage()
        {
            //arange
            var expectedImages = 1;
            var expectedContent = "TEST_CONTENT";

            //act
            var sut = MockedItemsFactory.ProvideImagesServiceWithImages();
            var result = sut.GetImages(string.Empty);

            //assert
            result.Count().Should().Be(expectedImages);
            result.First().Should().BeOfType(typeof(Image<string>));
            result.First().Content.Should().Be(expectedContent);
        }

        #endregion
    }
}
