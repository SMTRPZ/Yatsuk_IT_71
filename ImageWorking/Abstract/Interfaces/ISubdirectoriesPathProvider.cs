using System.Collections.Generic;

namespace ImageWorking.Abstract.Interfaces
{
    /// <summary>
    /// Abstraction to work with subdirectory names
    /// </summary>
    public interface ISubdirectoriesPathProvider
    {
        /// <summary>
        /// Returns pathes to subdirectories 
        /// </summary>
        /// <param name="pathToDirectory">Path to directory</param>
        /// <returns>Pathes to all subdirectories</returns>
        IEnumerable<string> GetSubDirectoriesPath(string pathToDirectory);

        /// <summary>
        /// Determines weather directory contains subfolders
        /// </summary>
        /// <param name="pathToFolder">Path ot folder</param>
        /// <returns></returns>
        bool ContainsSubFolders(string pathToFolder);
    }
}
