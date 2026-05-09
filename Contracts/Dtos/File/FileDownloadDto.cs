using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.File
{
    public class FileDownloadDto
    {
        public FileStream FileStream { get; set; }
        public string FileDownloadName { get; set; }
        public bool EnableRangeProcessing { get; set; }
    }
}
