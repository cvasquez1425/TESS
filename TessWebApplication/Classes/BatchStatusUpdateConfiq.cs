using System.Configuration;

namespace Greenspoon.Tess.Classes
{
    public sealed class BatchStatusUpdateConfiq
    {
        private static BatchStatusUpdateConfiq instance;
        public string MasterId { get; set; }
        public string BatchEscrowId { get; set; }
        public string DevK { get; set; }
        public string BatchCancelId { get; set; }

        private BatchStatusUpdateConfiq() { }

        public static BatchStatusUpdateConfiq Instance
        {
            get
            {
                return instance ??
                    ( instance = new BatchStatusUpdateConfiq {
                        MasterId = ConfigurationManager.AppSettings["Excel.MasterId"],
                        BatchEscrowId = ConfigurationManager.AppSettings["Excel.BatchEscrowId"],
                        DevK = ConfigurationManager.AppSettings["Excel.DevK"],
                        BatchCancelId = ConfigurationManager.AppSettings["Excel.BatchCancelId"]
                    } );
            }
        }
    }
}