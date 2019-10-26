using ImageWorking.DTO;
using System.Collections.Generic;

namespace ImageWorking.Services.Interfaces
{
    public interface IImagesService<ImageContentType>
    {
        /// <summary>
        /// Returns collection of <see cref="Image{Type}"/> from directory with <paramref name="pathToDirectory"/>
        /// </summary>
        /// <param name="pathToDirectory">Path to directory</param>
        /// <returns>Collection of <see cref="Image{Type}"/></returns>
        IEnumerable<Image<ImageContentType>> GetImages(string pathToDirectory);
    }
}
