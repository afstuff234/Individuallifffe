using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustodianLife.Model
{
    public class GroupDRCRNote
    {
        public virtual int? gdnId { get; set; }
        public virtual string SystemModule { get; set; }
        public virtual string FileNo { get; set; }
        public virtual string ProposalNo { get; set; }
        public virtual string PolicyNo { get; set; }
        public virtual string UnderwritingYear { get; set; }
        public virtual string ProductCode { get; set; }
        public virtual string MembBatchNo { get; set; }
        public virtual string ProcessingDate { get; set; }
        public virtual int BatchNo { get; set; } //Some records were stored as string (on staging) in the batch no column 'BN001' 
        public virtual string DrCrNo { get; set; }
        public virtual string DrCrNoteDesc { get; set; }
        public virtual string DrCr { get; set; }
        public virtual string BrokerCode { get; set; }
    }
}
