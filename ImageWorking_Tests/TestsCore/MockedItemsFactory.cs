using ImageWorking.Abstract.Classes;
using ImageWorking.Abstract.Interfaces;
using ImageWorking.Services;
using Moq;
using System;
using System.Collections.Generic;

namespace ImageWorking_Tests.TestsCore
{
    internal static class MockedItemsFactory
    {
        public static T ProvideDefaultDirectory<T>(Func<IImageContentProvider<string>, IFileNameProvider, string, T> instanceFunc)
            where T : Directory<string>
        {
            var imageContentProvider = new Mock<IImageContentProvider<string>>();
            var fileNameProvider = new Mock<IFileNameProvider>();
            var path = string.Empty;

            var directory = instanceFunc(imageContentProvider.Object, fileNameProvider.Object, path);

            return directory;
        }

        public static T ProvideDirectoryWithImages<T>(Func<IImageContentProvider<string>, IFileNameProvider, string, T> instanceFunc)
            where T : Directory<string>
        {
            var imageContentProvider = MockImageContentProvider();
            var fileNameProvider = MockIFileNameProvider();

            var path = string.Empty;

            var directory = instanceFunc(imageContentProvider.Object, fileNameProvider.Object, path);

            return directory;
        }

        public static ImagesService<string> ProvideDefaultImagesService()
        {
            var subdirectoriesPathProvider = MockSubdirectoriesPathProvider();
            var imageContentProvider = new Mock<IImageContentProvider<string>>();
            var fileNameProvider = new Mock<IFileNameProvider>();

            var servcie = new ImagesService<string>(subdirectoriesPathProvider.Object, imageContentProvider.Object, fileNameProvider.Object);

            return servcie;
        }

        public static ImagesService<string> ProvideImagesServiceWithImages()
        {
            var subdirectoriesPathProvider = MockSubdirectoriesPathProvider();
            var imageContentProvider = MockImageContentProvider();
            var fileNameProvider = MockIFileNameProvider();

            var servcie = new ImagesService<string>(subdirectoriesPathProvider.Object, imageContentProvider.Object, fileNameProvider.Object);

            return servcie;
        }

        public static IEnumerable<string> ProvideImageFormats()
        {
            return new List<string>() { "svg" };
        }

        private static Mock<IImageContentProvider<string>> MockImageContentProvider()
        {
            var imageContentProvider = new Mock<IImageContentProvider<string>>();
            imageContentProvider
                .Setup(x => x.ProvideImageContent(It.IsAny<string>()))
                .Returns("TEST_CONTENT");

            return imageContentProvider;
        }

        private static Mock<IFileNameProvider> MockIFileNameProvider()
        {
            var fileNameProvider = new Mock<IFileNameProvider>();
            fileNameProvider
                .Setup(x => x.ProvideFileNames(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()))
                .Returns(new List<string>() { "TEST.svg" });

            return fileNameProvider;
        }

        private static Mock<ISubdirectoriesPathProvider> MockSubdirectoriesPathProvider()
        {
            var subdirectoriesPathProvider = new Mock<ISubdirectoriesPathProvider>();
            subdirectoriesPathProvider.Setup(x => x.GetSubDirectoriesPath(It.IsAny<string>()))
                .Returns(null as IEnumerable<string>);

            return subdirectoriesPathProvider;

        }
    }
}
