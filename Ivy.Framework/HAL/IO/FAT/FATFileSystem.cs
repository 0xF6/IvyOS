using Ivy.HAL;

namespace Ivy.Framework.HAL.IO.FAT
{
    using System;
    using System.Collections.Generic;
    using Cosmos.Common.Extensions;
    using Cosmos.HAL.BlockDevice;
    using Cosmos.System.FileSystem;
    using Cosmos.System.FileSystem.Listing;

    public class FatFileSystem : FileSystem
    {
        internal class Fat
        {
            private readonly FatFileSystem mFileSystem;

            private readonly ulong mFatSector;

            /// <summary>
            /// Initializes a new instance of the <see cref="Fat"/> class.
            /// </summary>
            /// <param name="aFileSystem">The file system.</param>
            /// <param name="aFatSector">The first sector of the FAT table.</param>
            public Fat(FatFileSystem aFileSystem, ulong aFatSector)
            {
                if (aFileSystem == null)
                {
                    throw new ArgumentNullException(nameof(aFileSystem));
                }

                mFileSystem = aFileSystem;
                mFatSector = aFatSector;
            }

            /// <summary>
            /// Gets the size of a fat entry in bytes.
            /// </summary>
            /// <returns>The size of a fat entry in bytes.</returns>
            /// <exception cref="NotSupportedException">Can not get the FAT entry size for an unknown FAT type.</exception>
            private uint GetFatEntrySizeInBytes()
            {
                switch (mFileSystem.mFatType)
                {
                    case FatTypeEnum.Fat32:
                        return 4;
                    case FatTypeEnum.Fat16:
                        return 2;
                    case FatTypeEnum.Fat12:
                        // TODO:
                        break;
                }

                throw new NotSupportedException("Can not get the FAT entry size for an unknown FAT type.");
            }

            /// <summary>
            /// Gets the fat chain.
            /// </summary>
            /// <param name="aFirstEntry">The first entry.</param>
            /// <param name="aDataSize">Size of a data to be stored in bytes.</param>
            /// <returns>An array of cluster numbers for the fat chain.</returns>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            public uint[] GetFatChain(uint aFirstEntry, long aDataSize = 0)
            {
                var xReturn = new uint[0];
                uint xCurrentEntry = aFirstEntry;
                uint xValue;

                long xEntriesRequired = aDataSize / mFileSystem.BytesPerCluster;
                if (aDataSize % mFileSystem.BytesPerCluster != 0)
                {
                    xEntriesRequired++;
                }

                GetFatEntry(xCurrentEntry, out xValue);
                Array.Resize(ref xReturn, xReturn.Length + 1);
                xReturn[xReturn.Length - 1] = xCurrentEntry;
                if (xEntriesRequired > 0)
                {
                    while (!FatEntryIsEof(xValue))
                    {
                        xCurrentEntry = xValue;
                        GetFatEntry(xCurrentEntry, out xValue);
                        Array.Resize(ref xReturn, xReturn.Length + 1);
                        if (!FatEntryIsEof(xValue))
                        {
                            xReturn[xReturn.Length - 1] = xValue;
                        }
                        else
                        {
                            xReturn[xReturn.Length - 1] = xCurrentEntry;
                        }
                    }

                    if (xEntriesRequired > xReturn.Length)
                    {
                        long xNewClusters = xReturn.Length - xEntriesRequired;
                        for (int i = 0; i < xNewClusters; i++)
                        {
                            xCurrentEntry = GetNextUnallocatedFatEntry();
                            uint xLastFatEntry = xReturn[xReturn.Length - 1];
                            SetFatEntry(xLastFatEntry, xCurrentEntry);
                            Array.Resize(ref xReturn, xReturn.Length + 1);
                            xReturn[xReturn.Length - 1] = xCurrentEntry;
                        }
                    }
                }

                SetFatEntry(xCurrentEntry, FatEntryEofValue());

                return xReturn;
            }

            /// <summary>
            /// Gets the next unallocated fat entry.
            /// </summary>
            /// <returns>The index of the next unallocated FAT entry.</returns>
            /// <exception cref="Exception">Failed to find an unallocated FAT entry.</exception>
            public uint GetNextUnallocatedFatEntry()
            {
                uint xTotalEntries = mFileSystem.FatSectorCount * mFileSystem.BytesPerSector / GetFatEntrySizeInBytes();
                for (uint i = mFileSystem.RootCluster; i < xTotalEntries; i++)
                {
                    uint xEntryValue;
                    GetFatEntry(i, out xEntryValue);
                    if (!FatEntryIsEof(xEntryValue))
                    {
                        return i;
                    }
                }

                throw new Exception("Failed to find an unallocated FAT entry.");
            }

            /// <summary>
            /// Clears a fat entry.
            /// </summary>
            /// <param name="aEntryNumber">The entry number.</param>
            public void ClearFatEntry(ulong aEntryNumber)
            {
                SetFatEntry(aEntryNumber, 0);
            }

            private void ReadFatSector(ulong aSector, out byte[] aData)
            {
                aData = mFileSystem.NewBlockArray();
                var xSector = mFatSector + aSector;
                mFileSystem.mDevice.ReadBlock(xSector, mFileSystem.SectorsPerCluster, aData);
            }

            private void WriteFatSector(ulong aSector, byte[] aData)
            {
                if (aData == null)
                {
                    throw new ArgumentNullException(nameof(aData));
                }

                var xSector = mFatSector + aSector;
                mFileSystem.mDevice.WriteBlock(xSector, mFileSystem.SectorsPerCluster, aData);
            }

            /// <summary>
            /// Gets a fat entry.
            /// </summary>
            /// <param name="aEntryNumber">The entry number.</param>
            /// <param name="aValue">The entry value.</param>
            private void GetFatEntry(uint aEntryNumber, out uint aValue)
            {
                uint xEntrySize = GetFatEntrySizeInBytes();
                ulong xEntryOffset = aEntryNumber * xEntrySize;
                ulong xSector = xEntryOffset / mFileSystem.BytesPerSector;
                ulong xSectorOffset = (xSector * mFileSystem.BytesPerSector) - xEntryOffset;

                byte[] xData = mFileSystem.NewBlockArray();
                ReadFatSector(xSector, out xData);

                switch (mFileSystem.mFatType)
                {
                    case FatTypeEnum.Fat12:
                        // We now access the FAT entry as a WORD just as we do for FAT16, but if the cluster number is
                        // EVEN, we only want the low 12-bits of the 16-bits we fetch. If the cluster number is ODD
                        // we want the high 12-bits of the 16-bits we fetch.
                        uint xResult = xData.ToUInt16(xEntryOffset);
                        if ((aEntryNumber & 0x01) == 0)
                        {
                            aValue = xResult & 0x0FFF; // Even
                        }
                        else
                        {
                            aValue = xResult >> 4; // Odd
                        }
                        break;
                    case FatTypeEnum.Fat16:
                        aValue = xData.ToUInt16(xEntryOffset);
                        break;
                    case FatTypeEnum.Fat32:
                        aValue = xData.ToUInt32(xEntryOffset) & 0x0FFFFFFF;
                        break;
                    default:
                        throw new NotSupportedException("Unknown FAT type.");
                }
            }

            /// <summary>
            /// Sets a fat entry.
            /// </summary>
            /// <param name="aEntryNumber">The entry number.</param>
            /// <param name="aValue">The value.</param>
            private void SetFatEntry(ulong aEntryNumber, ulong aValue)
            {
                uint xEntrySize = GetFatEntrySizeInBytes();
                ulong xEntryOffset = aEntryNumber * xEntrySize;

                ulong xSector = xEntryOffset / mFileSystem.BytesPerSector;
                ulong xSectorOffset = (xSector * mFileSystem.BytesPerSector) - xEntryOffset;

                byte[] xData = mFileSystem.NewBlockArray();
                ReadFatSector(xSector, out xData);

                switch (mFileSystem.mFatType)
                {
                    case FatTypeEnum.Fat12:
                        xData.SetUInt16(xEntryOffset, (ushort)aValue);
                        break;
                    case FatTypeEnum.Fat16:
                        xData.SetUInt16(xEntryOffset, (ushort)aValue);
                        break;
                    case FatTypeEnum.Fat32:
                        xData.SetUInt32(xEntryOffset, (uint)aValue);
                        break;
                    default:
                        throw new NotSupportedException("Unknown FAT type.");
                }

                WriteFatSector(xSector, xData);
            }

            /// <summary>
            /// Is the FAT entry EOF?
            /// </summary>
            /// <param name="aValue">The entry index.</param>
            /// <returns></returns>
            /// <exception cref="Exception">Unknown file system type.</exception>
            private bool FatEntryIsEof(ulong aValue)
            {
                switch (mFileSystem.mFatType)
                {
                    case FatTypeEnum.Fat12:
                        return aValue >= 0xFF8;
                    case FatTypeEnum.Fat16:
                        return aValue >= 0xFFF8;
                    case FatTypeEnum.Fat32:
                        return aValue >= 0xFFFFFF8;
                    default:
                        throw new Exception("Unknown file system type.");
                }
            }

            /// <summary>
            /// The the EOF value for a specific FAT type.
            /// </summary>
            /// <returns>The EOF value.</returns>
            /// <exception cref="Exception">Unknown file system type.</exception>
            private ulong FatEntryEofValue()
            {
                switch (mFileSystem.mFatType)
                {
                    case FatTypeEnum.Fat12:
                        return 0x0FFF;
                    case FatTypeEnum.Fat16:
                        return 0xFFFF;
                    case FatTypeEnum.Fat32:
                        return 0x0FFFFFFF;
                    default:
                        throw new Exception("Unknown file system type.");
                }
            }
        }

        public readonly uint BytesPerCluster;

        public readonly uint BytesPerSector;

        public readonly uint ClusterCount;

        public readonly uint DataSector; // First Data Sector

        public readonly uint DataSectorCount;

        public readonly uint FatSectorCount;

        private readonly FatTypeEnum mFatType;

        public readonly uint NumberOfFATs;

        public readonly uint ReservedSectorCount;

        public readonly uint RootCluster; // FAT32

        public readonly uint RootEntryCount;

        public readonly uint RootSector; // FAT12/16

        public readonly uint RootSectorCount; // FAT12/16, FAT32 remains 0

        public readonly uint SectorsPerCluster;

        public readonly uint TotalSectorCount;

        private readonly Fat[] mFats;

        /// <summary>
        /// Initializes a new instance of the <see cref="FatFileSystem"/> class.
        /// </summary>
        /// <param name="aDevice">The partition.</param>
        /// <param name="aRootPath">The root path.</param>
        /// <exception cref="Exception">FAT signature not found.</exception>
        public FatFileSystem(Partition aDevice, string aRootPath)
            : base(aDevice, aRootPath)
        {
            if (aDevice == null)
            {
                throw new ArgumentNullException(nameof(aDevice));
            }

            if (string.IsNullOrEmpty(aRootPath))
            {
                throw new ArgumentException("Argument is null or empty", nameof(aRootPath));
            }

            var xBPB = mDevice.NewBlockArray(1);

            mDevice.ReadBlock(0UL, 1U, xBPB);

            ushort xSig = xBPB.ToUInt16(510);
            if (xSig != 0xAA55)
            {
                throw new Exception("FAT signature not found.");
            }

            BytesPerSector = xBPB.ToUInt16(11);
            SectorsPerCluster = xBPB[13];
            BytesPerCluster = BytesPerSector * SectorsPerCluster;
            ReservedSectorCount = xBPB.ToUInt16(14);
            NumberOfFATs = xBPB[16];
            RootEntryCount = xBPB.ToUInt16(17);

            TotalSectorCount = xBPB.ToUInt16(19);
            if (TotalSectorCount == 0)
            {
                TotalSectorCount = xBPB.ToUInt32(32);
            }

            // FATSz
            FatSectorCount = xBPB.ToUInt16(22);
            if (FatSectorCount == 0)
            {
                FatSectorCount = xBPB.ToUInt32(36);
            }

            DataSectorCount = TotalSectorCount -
                              (ReservedSectorCount + NumberOfFATs * FatSectorCount + ReservedSectorCount);

            // Computation rounds down.
            ClusterCount = DataSectorCount / SectorsPerCluster;
            // Determine the FAT type. Do not use another method - this IS the official and
            // proper way to determine FAT type.
            // Comparisons are purposefully < and not <=
            // FAT16 starts at 4085, FAT32 starts at 65525
            if (ClusterCount < 4085)
            {
                mFatType = FatTypeEnum.Fat12;
            }
            else if (ClusterCount < 65525)
            {
                mFatType = FatTypeEnum.Fat16;
            }
            else
            {
                mFatType = FatTypeEnum.Fat32;
            }

            if (mFatType == FatTypeEnum.Fat32)
            {
                RootCluster = xBPB.ToUInt32(44);
            }
            else
            {
                RootSector = ReservedSectorCount + NumberOfFATs * FatSectorCount;
                RootSectorCount = (RootEntryCount * 32 + (BytesPerSector - 1)) / BytesPerSector;
            }
            DataSector = ReservedSectorCount + NumberOfFATs * FatSectorCount + RootSectorCount;

            mFats = new Fat[NumberOfFATs];
            for (ulong i = 0; i < NumberOfFATs; i++)
            {
                mFats[i] = new Fat(this, (ReservedSectorCount + i * FatSectorCount));
            }
        }

        internal Fat GetFat(int aTableNumber)
        {
            if (mFats.Length > aTableNumber)
            {
                return mFats[aTableNumber];
            }

            throw new Exception("The fat table number doesn't exist.");
        }

        internal byte[] NewBlockArray()
        {
            return new byte[BytesPerCluster];
        }

        internal void Read(long aCluster, out byte[] aData, long aSize = 0, long aOffset = 0)
        {
            if (aSize == 0)
            {
                aSize = BytesPerCluster;
            }

            if (mFatType == FatTypeEnum.Fat32)
            {
                aData = NewBlockArray();
                long xSector = DataSector + (aCluster - RootCluster) * SectorsPerCluster;
                mDevice.ReadBlock((ulong)xSector, SectorsPerCluster, aData);
            }
            else
            {
                aData = mDevice.NewBlockArray(1);
                mDevice.ReadBlock((ulong)aCluster, RootSectorCount, aData);
            }
        }

        internal void Write(long aCluster, byte[] aData, long aSize = 0, long aOffset = 0)
        {
            if (aData == null)
            {
                throw new ArgumentNullException(nameof(aData));
            }

            if (aSize == 0)
            {
                aSize = BytesPerCluster;
            }

            byte[] xData;

            Read(aCluster, out xData);
            Array.Copy(aData, 0, xData, aOffset, aData.Length);

            if (mFatType == FatTypeEnum.Fat32)
            {
                long xSector = DataSector + (aCluster - RootCluster) * SectorsPerCluster;
                mDevice.WriteBlock((ulong) xSector, SectorsPerCluster, xData);
            }
            else
            {
                mDevice.WriteBlock((ulong) aCluster, RootSectorCount, xData);
            }
        }

        public static bool IsDeviceFat(Partition aDevice)
        {
            if (aDevice == null)
            {
                throw new ArgumentNullException(nameof(aDevice));
            }

            var xBPB = aDevice.NewBlockArray(1);
            aDevice.ReadBlock(0UL, 1U, xBPB);
            ushort xSig = xBPB.ToUInt16(510);
            if (xSig != 0xAA55)
            {
                return false;
            }
            return true;
        }

        private string Format(uint i) => $"{i}{new string(' ', ("00000000").Length - i.ToString().Length)}";

        public override void DisplayFileSystemInfo()
        {
            Terminal.WrapGreen("VFS")
                .White($"Bytes per Cluster     = {Format(BytesPerCluster)}, Bytes per Sector      = {Format(BytesPerSector)}")
                .newLine();
            Terminal.WrapGreen("VFS")
                .White($"Cluster Count         = {Format(ClusterCount)}, Data Sector           = {Format(DataSector)}")
                .newLine();
            Terminal.WrapGreen("VFS")
                .White($"Data Sector Count     = {Format(DataSectorCount)}, FAT Sector Count      = {Format(FatSectorCount)}")
                .newLine();
            Terminal.WrapGreen("VFS")
                .White($"FAT Type              = {Format((uint)mFatType)}, Number of FATS        = {Format(NumberOfFATs)}")
                .newLine();
            Terminal.WrapGreen("VFS")
                .White($"Reserved Sector Count = {Format(ReservedSectorCount)}, Root Cluster          = {Format(RootCluster)}")
                .newLine();
            Terminal.WrapGreen("VFS")
                .White($"Root Entry Count      = {Format(RootEntryCount)}, Root Sector           = {Format(RootSector)}")
                .newLine();
            Terminal.WrapGreen("VFS")
                .White($"Root Sector Count     = {Format(RootSectorCount)}, Sectors per Cluster   = {Format(SectorsPerCluster)}")
                .newLine();
            Terminal.WrapGreen("VFS")
                .White($"Total Sector Count    = {Format(TotalSectorCount)}, Keys                  = {Format(15)}")
                .newLine();
        }

        public override List<DirectoryEntry> GetDirectoryListing(DirectoryEntry baseDirectory)
        {
            if (baseDirectory == null)
            {
                throw new ArgumentNullException(nameof(baseDirectory));
            }

            var result = new List<DirectoryEntry>();
            var xEntry = (FatDirectoryEntry)baseDirectory;
            var fatListing = xEntry.ReadDirectoryContents();

            for (int i = 0; i < fatListing.Count; i++)
            {
                result.Add(fatListing[i]);
            }
            return result;
        }

        public override DirectoryEntry GetRootDirectory()
        {
            var xRootEntry = new FatDirectoryEntry(this, null, mRootPath, mRootPath, RootCluster);
            return xRootEntry;
        }

        public override DirectoryEntry CreateDirectory(DirectoryEntry aParentDirectory, string aNewDirectory)
        {
            if (aParentDirectory == null)
            {
                throw new ArgumentNullException(nameof(aParentDirectory));
            }

            if (string.IsNullOrEmpty(aNewDirectory))
            {
                throw new ArgumentNullException(nameof(aNewDirectory));
            }

            var xParentDirectory = (FatDirectoryEntry)aParentDirectory;
            var xDirectoryEntryToAdd = xParentDirectory.AddDirectoryEntry(aNewDirectory, DirectoryEntryTypeEnum.Directory);
            return xDirectoryEntryToAdd;
        }

        public override DirectoryEntry CreateFile(DirectoryEntry aParentDirectory, string aNewFile)
        {

            if (aParentDirectory == null)
            {
                throw new ArgumentNullException(nameof(aParentDirectory));
            }

            if (string.IsNullOrEmpty(aNewFile))
            {
                throw new ArgumentNullException(nameof(aNewFile));
            }

            var xParentDirectory = (FatDirectoryEntry)aParentDirectory;
            var xDirectoryEntryToAdd = xParentDirectory.AddDirectoryEntry(aNewFile, DirectoryEntryTypeEnum.File);
            return xDirectoryEntryToAdd;
        }

        public override void DeleteDirectory(DirectoryEntry aDirectoryEntry)
        {
            if (aDirectoryEntry == null)
            {
                throw new ArgumentNullException(nameof(aDirectoryEntry));
            }

            var xDirectoryEntry = (FatDirectoryEntry)aDirectoryEntry;

            xDirectoryEntry.DeleteDirectoryEntry();
        }

        public override void DeleteFile(DirectoryEntry aDirectoryEntry)
        {
            if (aDirectoryEntry == null)
            {
                throw new ArgumentNullException(nameof(aDirectoryEntry));
            }

            var xDirectoryEntry = (FatDirectoryEntry)aDirectoryEntry;

            var entries = xDirectoryEntry.GetFatTable();

            foreach (var entry in entries)
            {
                GetFat(0).ClearFatEntry(entry);
            }

            xDirectoryEntry.DeleteDirectoryEntry();
        }

        private enum FatTypeEnum
        {
            Unknown,

            Fat12,

            Fat16,

            Fat32
        }
    }
}