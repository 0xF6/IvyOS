
using Ivy.Framework.HAL.IO.FAT;

namespace IvyOS.IO
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.IO;
    using global::Cosmos.HAL.BlockDevice;
    using Cosmos.System.FileSystem;
    using Cosmos.System.FileSystem.Listing;
    using Cosmos.System.FileSystem.VFS;
    using Ivy.HAL;
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Cosmos default virtual file system.
    /// </summary>
    /// <seealso cref="Cosmos.System.FileSystem.VFS.VFSBase" />
    public class IvyVFS : VFSBase
    {
        private static bool isIniting = false;
        private List<Partition> mPartitions;
        private List<FileSystem> mFileSystems;
        private FileSystem mCurrentFileSystem;

        /// <summary>
        /// Initializes the virtual file system.
        /// </summary>
        public override void Initialize()
        {
            if (isIniting) return;
            mPartitions = new List<Partition>();
            mFileSystems = new List<FileSystem>();
            InitializePartitions();
            if (mPartitions.Count > 0) InitializeFileSystems();
            isIniting = true;
        }

        /// <summary>
        /// Creates a new file.
        /// </summary>
        /// <param name="aPath">The full path including the file to create.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException">aPath</exception>
        public override DirectoryEntry CreateFile(string aPath)
        {
            if (aPath == null)
                throw new ArgumentNullException(nameof(aPath));
            if (aPath.Length == 0)
                throw new ArgumentException("aPath");
            if (File.Exists(aPath))
                return GetFile(aPath);
            string xFileToCreate = Path.GetFileName(aPath);
            string xParentDirectory = aPath.Remove(aPath.Length - xFileToCreate.Length);
            DirectoryEntry xParentEntry = GetDirectory(xParentDirectory);
            if (xParentEntry == null)
                xParentEntry = CreateDirectory(xParentDirectory);
            var xFS = GetFileSystemFromPath(xParentDirectory);
            return xFS.CreateFile(xParentEntry, xFileToCreate);
        }

        /// <summary>
        /// Creates a directory.
        /// </summary>
        /// <param name="aPath">The full path including the directory to create.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException">aPath</exception>
        public override DirectoryEntry CreateDirectory(string aPath)
        {
            if (aPath == null) throw new ArgumentNullException(nameof(aPath));
            if (aPath.Length == 0) throw new ArgumentException("aPath");
            if (Directory.Exists(aPath)) return GetDirectory(aPath);
            string xDirectoryToCreate = Path.GetFileName(aPath);
            string xParentDirectory = aPath.Remove(aPath.Length - xDirectoryToCreate.Length);
            DirectoryEntry xParentEntry = GetDirectory(xParentDirectory) ?? CreateDirectory(xParentDirectory);
            var xFS = GetFileSystemFromPath(xParentDirectory);
            return xFS.CreateDirectory(xParentEntry, xDirectoryToCreate);
        }

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="aPath">The full path.</param>
        /// <returns></returns>
        public override bool DeleteFile(DirectoryEntry aPath)
        {
            try
            {
                var xFS = GetFileSystemFromPath(aPath.mFullPath);
                xFS.DeleteFile(aPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes an empty directory.
        /// </summary>
        /// <param name="aPath">The full path.</param>
        /// <returns></returns>
        public override bool DeleteDirectory(DirectoryEntry aPath)
        {
            try
            {
                if (GetDirectoryListing(aPath).Count > 0)
                    throw new Exception("Directory is not empty");

                var xFS = GetFileSystemFromPath(aPath.mFullPath);
                xFS.DeleteDirectory(aPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the directory listing for a path.
        /// </summary>
        /// <param name="aPath">The full path.</param>
        /// <returns></returns>
        public override List<DirectoryEntry> GetDirectoryListing(string aPath)
        {
            var xFS = GetFileSystemFromPath(aPath);
            var xDirectory = DoGetDirectoryEntry(aPath, xFS);
            return xFS.GetDirectoryListing(xDirectory);
        }

        /// <summary>
        /// Gets the directory listing for a directory entry.
        /// </summary>
        /// <param name="aDirectory">The directory entry.</param>
        /// <returns></returns>
        public override List<DirectoryEntry> GetDirectoryListing(DirectoryEntry aDirectory)
        {
            if (aDirectory == null || String.IsNullOrEmpty(aDirectory.mFullPath))
            {
                throw new ArgumentException("Argument is null or empty", nameof(aDirectory));
            }

            return GetDirectoryListing(aDirectory.mFullPath);
        }

        /// <summary>
        /// Gets the directory entry for a directory.
        /// </summary>
        /// <param name="aPath">The full path path.</param>
        /// <returns>A directory entry for the directory.</returns>
        /// <exception cref="Exception"></exception>
        public override DirectoryEntry GetDirectory(string aPath)
        {
            try
            {
                var xFileSystem = GetFileSystemFromPath(aPath);
                var xEntry = DoGetDirectoryEntry(aPath, xFileSystem);
                if ((xEntry != null) && (xEntry.mEntryType == DirectoryEntryTypeEnum.Directory))
                {
                    return xEntry;
                }
            }
            catch (Exception)
            {
                return null;
            }
            throw new Exception(aPath + " was found, but is not a directory.");
        }

        /// <summary>
        /// Gets the directory entry for a file.
        /// </summary>
        /// <param name="aPath">The full path.</param>
        /// <returns>A directory entry for the file.</returns>
        /// <exception cref="Exception"></exception>
        public override DirectoryEntry GetFile(string aPath)
        {
            try
            {
                var xFileSystem = GetFileSystemFromPath(aPath);
                var xEntry = DoGetDirectoryEntry(aPath, xFileSystem);
                if ((xEntry != null) && (xEntry.mEntryType == DirectoryEntryTypeEnum.File)) return xEntry;
            }
            catch (Exception)
            {
                return null;
            }
            throw new Exception(aPath + " was found, but is not a file.");
        }

        /// <summary>
        /// Gets the volumes for all registered file systems.
        /// </summary>
        /// <returns>A list of directory entries for all volumes.</returns>
        public override List<DirectoryEntry> GetVolumes()
        {
            List<DirectoryEntry> xVolumes = new List<DirectoryEntry>();
            foreach (FileSystem t in mFileSystems)
                xVolumes.Add(GetVolume(t));
            return xVolumes;
        }

        /// <summary>
        /// Gets the directory entry for a volume.
        /// </summary>
        /// <param name="aPath">The volume root path.</param>
        /// <returns>A directory entry for the volume.</returns>
        public override DirectoryEntry GetVolume(string aPath)
        {
            if (string.IsNullOrEmpty(aPath))
                return null;
            var xFileSystem = GetFileSystemFromPath(aPath);
            return xFileSystem != null ? GetVolume(xFileSystem) : null;
        }

        /// <summary>
        /// Initializes the partitions for all block devices.
        /// </summary>
        protected virtual void InitializePartitions()
        {
            foreach (BlockDevice t in BlockDevice.Devices)
            {
                var item = t as Partition;
                if (item == null) continue;
                mPartitions.Add(item);
                break;
            }
            if (mPartitions.Count > 0)
            for (int i = 0; i < mPartitions.Count; i++)
                WriteTerminal($"P#:{(i + 1)}. BS: {mPartitions[i].BlockSize}b. BC: {mPartitions[i].BlockCount}. S: {mPartitions[i].BlockCount * mPartitions[i].BlockSize / 1024 / 1024}Mb");
            else
                WriteTerminalError("No partitions found!");
        }

        private static void WriteTerminal(string text) => Terminal.WrapGreen("VFS").White(text).newLine();
        private static void WriteTerminalError(string text) => Terminal.WrapGreen("VFS").Red(text).newLine();
        private static void WriteTerminalWarn(string text) => Terminal.WrapGreen("VFS").Yellow(text).newLine();

        /// <summary>
        /// Initializes the file system for all partitions.
        /// </summary>
        protected virtual void InitializeFileSystems()
        {
            for (int i = 0; i < mPartitions.Count; i++)
            {
                string xRootPath = string.Concat(i, VolumeSeparatorChar, DirectorySeparatorChar);
                switch (FileSystem.GetFileSystemType(mPartitions[i]))
                {
                    case FileSystemType.FAT:
                        mFileSystems.Add(new FatFileSystem(mPartitions[i], xRootPath));
                        break;
                    default:
                        WriteTerminalError("Unknown filesystem type!");
                        return;
                }

                if ((mFileSystems.Count > 0) && (mFileSystems[mFileSystems.Count - 1].mRootPath == xRootPath))
                {
                    string xMessage = string.Concat("Initialized ", mFileSystems.Count, " filesystem(s)...");
                    WriteTerminal(xMessage);
                    mFileSystems[i].DisplayFileSystemInfo();
                    Directory.SetCurrentDirectory(xRootPath);
                }
                else
                {
                    string xMessage = string.Concat("No filesystem found on partition #", i);
                    WriteTerminalError(xMessage);
                }
            }
        }

        /// <summary>
        /// Gets the file system from a path.
        /// </summary>
        /// <param name="aPath">The path.</param>
        /// <returns>The file system for the path.</returns>
        /// <exception cref="Exception">Unable to determine filesystem for path:  + aPath</exception>
        private FileSystem GetFileSystemFromPath(string aPath)
        {
            //Global.mFileSystemDebugger.SendInternal("--- CosmosVFS.GetFileSystemFromPath ---");

            if (String.IsNullOrEmpty(aPath))
            {
                throw new ArgumentException("Argument is null or empty", nameof(aPath));
            }

            //Global.mFileSystemDebugger.SendInternal("aPath =");
            //Global.mFileSystemDebugger.SendInternal(aPath);

            string xPath = Path.GetPathRoot(aPath);
            //Global.mFileSystemDebugger.SendInternal("xPath after GetPathRoot =");
            //Global.mFileSystemDebugger.SendInternal(xPath);

            if ((mCurrentFileSystem != null) && (xPath == mCurrentFileSystem.mRootPath))
            {
                //Global.mFileSystemDebugger.SendInternal("Returning current file system.");
                return mCurrentFileSystem;
            }

            for (int i = 0; i < mFileSystems.Count; i++)
            {
                if (mFileSystems[i].mRootPath == xPath)
                {
                    //Global.mFileSystemDebugger.SendInternal("Found filesystem.");
                    mCurrentFileSystem = mFileSystems[i];
                    return mCurrentFileSystem;
                }
            }
            throw new Exception("Unable to determine filesystem for path: " + aPath);
        }

        /// <summary>
        /// Attempts to get a directory entry for a path in a file system.
        /// </summary>
        /// <param name="aPath">The path.</param>
        /// <param name="aFS">The file system.</param>
        /// <returns>A directory entry for the path.</returns>
        /// <exception cref="ArgumentNullException">aFS</exception>
        /// <exception cref="Exception">Path part ' + xPathPart + ' not found!</exception>
        private DirectoryEntry DoGetDirectoryEntry(string aPath, FileSystem aFS)
        {
            //Global.mFileSystemDebugger.SendInternal("--- CosmosVFS.DoGetDirectoryEntry ---");

            if (String.IsNullOrEmpty(aPath))
            {
                throw new ArgumentException("Argument is null or empty", nameof(aPath));
            }

            if (aFS == null)
            {
                throw new ArgumentNullException(nameof(aFS));
            }

            //Global.mFileSystemDebugger.SendInternal("aPath =");
            //Global.mFileSystemDebugger.SendInternal(aPath);

            string[] xPathParts = VFSManager.SplitPath(aPath);

            DirectoryEntry xBaseDirectory = GetVolume(aFS);

            if (xPathParts.Length == 1)
            {
                //Global.mFileSystemDebugger.SendInternal("Returning the volume.");
                return xBaseDirectory;
            }

            // start at index 1, because 0 is the volume
            for (int i = 1; i < xPathParts.Length; i++)
            {
                var xPathPart = xPathParts[i].ToLower();

                var xPartFound = false;
                var xListing = aFS.GetDirectoryListing(xBaseDirectory);

                for (int j = 0; j < xListing.Count; j++)
                {
                    var xListingItem = xListing[j];
                    string xListingItemName = xListingItem.mName.ToLower();
                    xPathPart = xPathPart.ToLower();

                    if (xListingItemName == xPathPart)
                    {
                        xBaseDirectory = xListingItem;
                        xPartFound = true;
                    }
                }

                if (!xPartFound)
                {
                    throw new Exception("Path part '" + xPathPart + "' not found!");
                }
            }
            return xBaseDirectory;
        }

        /// <summary>
        /// Gets the root directory entry for a volume in a file system.
        /// </summary>
        /// <param name="aFS">The file system containing the volume.</param>
        /// <returns>A directory entry for the volume.</returns>
        private DirectoryEntry GetVolume(FileSystem aFS)
        {
            //Global.mFileSystemDebugger.SendInternal("--- CosmosVFS.GetVolume ---");

            if (aFS == null)
            {
                //Global.mFileSystemDebugger.SendInternal("File system is null.");
                throw new ArgumentNullException(nameof(aFS));
            }

            return aFS.GetRootDirectory();
        }
    }
}