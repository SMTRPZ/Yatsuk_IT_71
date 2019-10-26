using System.Collections.Generic;

namespace ImageWorking.Abstract.Interfaces
{
    /// <summary>
    /// Abstraction which provides images content
    /// </summary>
    public interface IImageContentProvider<ContentType> 
    {
        /// <summary>
        /// Returns supported image formats
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetSupportedImageFormats();

        /// <summary>
        /// Adds image format to supported formats
        /// </summary>
        void AddImageFormat();

        /// <summary>
        /// Provides content of image with specified <paramref name="pathToImage"/>
        /// </summary>
        /// <param name="pathToImage">Path to image</param>
        /// <returns>Image content with specified <typeparamref name="ContentType"/></returns>
        ContentType ProvideImageContent(string pathToImage);
    }
}
