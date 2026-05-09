using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Core.Enums;

namespace Core.Core
{
    public class FileStorageOptions
    {

        public StorageLocation DefaultStorageLocation { get; set; }
        internal LocalDiskStorageOptions? Local { get; set; }
        internal AzureBlobStorageOptions? AzureBlob { get; set; }
    }

    internal class LocalDiskStorageOptions
    {
        public string? RootPath { get; set; }
    }

    internal class AzureBlobStorageOptions
    {
        public string? ConnectionString { get; set; }
        public string? DefaultContainerName { get; set; }
    }
}
