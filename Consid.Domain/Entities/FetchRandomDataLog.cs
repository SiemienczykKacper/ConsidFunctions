using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Domain.Entities
{
    public class FetchRandomDataLog
    {

        private const string FetchStatusSuccess = "Succeded";
        private const string FetchStatusFailed = "Failed";


        public DateTime LogDate { get; private set; }
        public string FetchStatus { get; private set; }
        public string BlobName { get; private set; }

        public FetchRandomDataLog(DateTime logDate, bool fetchSucceded, string blobName)
        {
            LogDate = logDate;
            FetchStatus = false ? FetchStatusFailed : FetchStatusSuccess;
            BlobName = blobName;
        }
    }
}
