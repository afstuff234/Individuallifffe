using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustodianLife.Model
{
    public class IndLifeCodes
    {
        public virtual int? icId { get; set; }
        public virtual string CodeTabId { get; set; }
        public virtual string CodeType { get; set; }
        public virtual string CodeItem { get; set; }
        public virtual string CodeLongDesc { get; set; }
        public virtual string CodeShortDesc { get; set; }
        public virtual string CodeCatTag { get; set; }
        public virtual string Flag { get; set; }
        public virtual string OperId { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual string Status { get; set; }

        //public virtual LoanInterest LoanInterest { get; set; }
        
    }
}
