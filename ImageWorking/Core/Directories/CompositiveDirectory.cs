using System.Collections.Generic;
using ImageWorking.Abstract.Classes;
using ImageWorking.Abstract.Interfaces;
using ImageWorking.DTO;
using System;
using System.Linq;

namespace ImageWorking.Core
{
    /// <summary>
    /// Class which represents directory with sub folders
    /// </summary>
    /// <typeparam name="ImageContentType"></typeparam>
    public class CompositiveDirectory<ImageContentType> : Directory<ImageContentType>
    {
        #region Fields

        protected List<Directory<ImageContentType>> _subDirectories;

        #endregion

        #region Constructors 

        /// <summary>
        /// Creates instance of compositive directory
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when one of input parameters is null</exception>
        public CompositiveDirectory(IImageContentProvider<ImageContentType> imageContentProvider, IFileNameProvider fileNameProvider, string path)
            : base(imageContentProvider, fileNameProvider, path)
        {
            _subDirectories = new List<Directory<ImageContentType>>();

        }

        #endregion

        #region Public Methods 

        /// <summary>
        /// <see cref="Directory{ImageContentType}.GetImages"/>
        /// </summary>
        public override IEnumerable<Image<ImageContentType>> GetImages(IEnumerable<string> imageFormats)
        {
            List<Image<ImageContentType>> result = new List<Image<ImageContentType>>();

            result.AddRange(GetCurrentDirectoryImages(imageFormats));

            foreach (var directory in _subDirectories)
                result.AddRange(directory.GetImages(imageFormats));

            return result;
        }

        /// <summary>
        /// <see cref="Directory{ImageContentType}.IsCompositive"/>
        /// </summary>
        public override bool IsCompositive() => _subDirectories.Any();

        /// <summary>
        /// Adds directory to current directory
        /// </summary>
        /// <param name="directory"></param>
        public void AddSubdirectory(Directory<ImageContentType> directory)
        {
            if (directory == null)
                throw new ArgumentNullException(nameof(directory));

            _subDirectories.Add(directory);
        }

        /// <summary>
        /// Adds range of subdirectories for current directory
        /// </summary>
        /// <param name="directories"></param>
        public void AddRangeOfSubdirectories(IEnumerable<Directory<ImageContentType>> directories)
        {
            if (directories == null)
                throw new ArgumentNullException(nameof(directories));

            foreach (var directory in directories)
                AddSubdirectory(directory);
        }

        #endregion

        #region Static

        /// <summary>
        /// Returns new instance of <see cref="CompositiveDirectory{ImageContentType}"/>
        /// </summary>
        /// <param name="imageContentProvider"><see cref="IImageContentProvider{ContentType}"/></param>
        /// <param name="fileNameProvider"><see cref="IFileNameProvider"/></param>
        /// <param name="path">Path to current directory</param>
        /// <returns><see cref="CompositiveDirectory{ImageContentType}"/></returns>
        public static CompositiveDirectory<ImageContentType> New(IImageContentProvider<ImageContentType> imageContentProvider, IFileNameProvider fileNameProvider, string path) => 
            new CompositiveDirectory<ImageContentType>(imageContentProvider, fileNameProvider, path);

       

        #endregion

    }
}
