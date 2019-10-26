using ImageWorking.Abstract.Classes;
using ImageWorking.Abstract.Interfaces;
using ImageWorking.DTO;
using System.Collections.Generic;

namespace ImageWorking.Core.Directories
{
    /// <summary>
    /// Class which represents directory without sub folders
    /// </summary>
    /// <typeparam name="ImageContentType"></typeparam>
    public class SimpleDirectory<ImageContentType> : Directory<ImageContentType>
    {
        #region Constructors 

        /// <summary>
        /// Creates instance of compositive directory
        /// </summary>
        /// <param name="imageContentProvider"><see cref="IImageContentProvider{ContentType}"/></param>
        /// <exception cref="ArgumentNullException">Thrown when one of input parameters is null</exception>
        public SimpleDirectory(IImageContentProvider<ImageContentType> imageContentProvider, IFileNameProvider fileNameProvider, string path)
            : base(imageContentProvider, fileNameProvider, path)
        { }

        #endregion

        #region Public Methods 

        /// <summary>
        /// <see cref="Directory{ImageContentType}.GetImages"/>
        /// </summary>
        public override IEnumerable<Image<ImageContentType>> GetImages(IEnumerable<string> imageFormats)
        {
            List<Image<ImageContentType>> result = new List<Image<ImageContentType>>();

            result.AddRange(GetCurrentDirectoryImages(imageFormats));

            return result;
        }

        /// <summary>
        /// <see cref="Directory{ImageContentType}.IsCompositive"/>
        /// </summary>
        public override bool IsCompositive() => false;

        #endregion

        #region Static

        /// <summary>
        /// Returns new instance of <see cref="SimpleDirectory{ImageContentType}"/>
        /// </summary>
        /// <param name="imageContentProvider"><see cref="IImageContentProvider{ContentType}"/></param>
        /// <param name="fileNameProvider"><see cref="IFileNameProvider"/></param>
        /// <param name="path">Path to current directory</param>
        /// <returns><see cref="SimpleDirectory{ImageContentType}"/></returns>
        public static SimpleDirectory<ImageContentType> New(IImageContentProvider<ImageContentType> imageContentProvider, IFileNameProvider fileNameProvider, string path) =>
            new SimpleDirectory<ImageContentType>(imageContentProvider, fileNameProvider, path);

    

        #endregion
    }
}
