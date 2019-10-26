using System.Collections.Generic;
using System;

namespace ImageWorking.Abstract.Interfaces
{
    /// <summary>
    /// Abstraction to work with file names
    /// </summary>
    public interface IFileNameProvider
    {
        /// <summary>
        /// Provides all file names in directory with specified <paramref name="pathToDirectory"/>
        /// </summary>
        /// <param name="pathToDirectory">Path to directory</param>
        /// <param name="fileFormatsToSearch">Formats of files to search</param>
        /// <returns>All file names in directory with specified <paramref name="pathToDirectory"/></returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="pathToDirectory"/> is null</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="pathToDirectory"/> has wrong format</exception>
        IEnumerable<string> ProvideFileNames(string pathToDirectory, IEnumerable<string> fileFormatsToSearch);
    }
}
