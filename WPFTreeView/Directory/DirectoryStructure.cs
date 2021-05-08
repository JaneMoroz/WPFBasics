using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTreeView.Directory.Data;

namespace WPFTreeView.Directory
{
    /// <summary>
    /// A helper class to query information about directories
    /// </summary>
    public static class DirectoryStructure
    {
        /// <summary>
        /// Gets all logical drives on the machine
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            // Get every logical drive on the machine
            return System.IO.Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
        }

        /// <summary>
        /// Gets the directories top-level content
        /// </summary>
        /// <param name="fullPath">The full path to the directory</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            // Crate empty list
            var items = new List<DirectoryItem>();

            #region Get Folders

            // Try and get directories from the folder
            // ignoring any issues doing so
            try
            {
                var dirs = System.IO.Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                {
                    items.AddRange(dirs.Select(dir => new DirectoryItem {FullPath = dir, Type = DirectoryItemType.Folder }));
                }

            }
            catch
            {
            }

            #endregion

            #region Get Files
            
            // Try and get files from the folder
            // ignoring any issues doing so
            try
            {
                var fs = System.IO.Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                {
                    items.AddRange(fs.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File}));
                }

            }
            catch
            {
            }

            return items;
            #endregion
        }

        #region Helpers
        /// <summary>
        /// Find the file or folder name for a full path
        /// </summary>
        /// <param name="path">The full path</param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            // If we have no path return empty
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // Make all slashes back slashes
            var normalizedPath = path.Replace('/', '\\');

            // Find the last backslash in the path
            var lastIndex = normalizedPath.LastIndexOf('\\');

            // if we don't find a backslash, return the path itself
            if (lastIndex <= 0)
            {
                return path;
            }

            // Return name after the last backslash
            return path.Substring(lastIndex + 1);
        }
        #endregion
    }
}
