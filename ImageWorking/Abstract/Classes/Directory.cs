using ImageWorking.DTO;
using System.Collections.Generic;
using ImageWorking.Abstract.Interfaces;
using System;

namespace ImageWorking.Abstract.Classes
{
    /// <summary>
    /// Abstractions to work with directories
    /// </summary>
    public abstract class Directory<ImageContentType>
    {
        #region Fields

        protected readonly IImageContentProvider<ImageContentType> _imageContentProvider;
        protected readonly IFileNameProvider _fileNameProvider;

        #endregion

        #region Properties

        /// <summary>
        /// Contains path to current directory;
        /// </summary>
        public string Path { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default costructor 
        /// </summary>
        /// <param name="imageContentProvider"><see cref="IImageContentProvider{ContentType}"/></param>
        /// <param name="fileNameProvider"><see cref="IFileNameProvider"/></param>
        /// <param name="path">Path to current directory</param>
        /// <exception cref="ArgumentNullException"> If one of input parameters is null</exception>
        public Directory(IImageContentProvider<ImageContentType> imageContentProvider, IFileNameProvider fileNameProvider, string path)
        {
            _imageContentProvider = imageContentProvider
                ?? throw new ArgumentNullException(nameof(imageContentProvider));

            _fileNameProvider = fileNameProvider
                ?? throw new ArgumentNullException(nameof(fileNameProvider));

            Path = path
                ?? throw new ArgumentNullException(nameof(path));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to get all images from current directory 
        /// </summary>
        /// <param name="imageFormats">Formats of supported images</param>
        /// <returns>Collections of founded <see cref="Image"/></returns>
        /// <remarks>
        ///     Looks for images in all subfolders
        /// </remarks>
        public abstract IEnumerable<Image<ImageContentType>> GetImages(IEnumerable<string> imageFormats);

        /// <summary>
        /// Determines weather directory contains subdirectories
        /// </summary>
        /// <returns>Boolean value which represents status of directory</returns>
        public abstract bool IsCompositive();

        #endregion

        #region Private Methods

        protected IEnumerable<Image<ImageContentType>> GetCurrentDirectoryImages(IEnumerable<string> imageFormats)
        {
            var result = new List<Image<ImageContentType>>();
            foreach (var fileName in _fileNameProvider.ProvideFileNames(Path, imageFormats))
            {
                var path = $"{Path}\\{fileName}";
                var imageContent = _imageContentProvider.ProvideImageContent(path);

                var image = new Image<ImageContentType>() { Path = path, Content = imageContent };

                result.Add(image);
            }

            return result;
        }

        #endregion
    }
}
