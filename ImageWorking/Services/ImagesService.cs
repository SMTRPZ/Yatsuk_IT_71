using System.Collections.Generic;
using ImageWorking.Abstract.Interfaces;
using ImageWorking.DTO;
using ImageWorking.Services.Interfaces;
using System;
using ImageWorking.Abstract.Classes;
using ImageWorking.Core;
using System.Linq;
using ImageWorking.Core.Directories;

namespace ImageWorking.Services
{
    public class ImagesService<ImageContentType> : IImagesService<ImageContentType>
    {
        #region Fields

        private readonly ISubdirectoriesPathProvider _subdirectoriesPathProvider;
        private readonly IImageContentProvider<ImageContentType> _imageContentProvider;
        private readonly IFileNameProvider _fileNameProvider;

        #endregion

        #region Constructors 

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="subdirectoriesPathProvider"><see cref="ISubdirectoriesPathProvider"/></param>
        /// <param name="imageContentProvider"><see cref="IImageContentProvider{ImageContentType}"/></param>
        /// <param name="fileNameProvider"><see cref="IFileNameProvider"/></param>
        /// <exception cref="ArgumentNullException">Thrown when one of input parameters is null</exception>
        public ImagesService(ISubdirectoriesPathProvider subdirectoriesPathProvider, IImageContentProvider<ImageContentType> imageContentProvider, IFileNameProvider fileNameProvider)
        {
            _subdirectoriesPathProvider = subdirectoriesPathProvider
                ?? throw new ArgumentNullException(nameof(subdirectoriesPathProvider));
            _imageContentProvider = imageContentProvider
                ?? throw new ArgumentNullException(nameof(imageContentProvider));
            _fileNameProvider = fileNameProvider
                ?? throw new ArgumentNullException(nameof(fileNameProvider));
        }

        #endregion

        #region IImagesService Methods 

        /// <summary>
        /// <see cref="IImagesService{ImageContentType}.GetImages(string)"/>
        /// </summary>
        public IEnumerable<Image<ImageContentType>> GetImages(string pathToDirectory)
        {
            var rootDirectory = _subdirectoriesPathProvider.ContainsSubFolders(pathToDirectory) ?
                        (Directory<ImageContentType>)CompositiveDirectory<ImageContentType>.New(_imageContentProvider, _fileNameProvider, pathToDirectory) : SimpleDirectory<ImageContentType>.New(_imageContentProvider, _fileNameProvider, pathToDirectory);

            BuildDirectoriesTree(rootDirectory);

            return rootDirectory.GetImages(_imageContentProvider.GetSupportedImageFormats());
        }

        #endregion

        #region Private Methods

        private void BuildDirectoriesTree(Directory<ImageContentType> directory)
        {
            IEnumerable<Directory<ImageContentType>> subDirectories = _subdirectoriesPathProvider.GetSubDirectoriesPath(directory.Path)?.Select(x =>
                _subdirectoriesPathProvider.ContainsSubFolders(x) ? 
                        (Directory<ImageContentType>)CompositiveDirectory<ImageContentType>.New(_imageContentProvider, _fileNameProvider, x) : SimpleDirectory<ImageContentType>.New(_imageContentProvider, _fileNameProvider, x)
            );
            
            if(subDirectories != null)
            {
                var root = directory as CompositiveDirectory<ImageContentType>
                    ?? throw new InvalidOperationException($"Simple directory cannot contain sub folders; Path to directory {directory.Path}");
                ((CompositiveDirectory<ImageContentType>)directory).AddRangeOfSubdirectories(subDirectories);
                foreach (var subDirectory in subDirectories)
                    BuildDirectoriesTree(subDirectory);
            }
        }

        #endregion
    }
}
