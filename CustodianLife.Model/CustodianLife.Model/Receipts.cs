using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustodianLife.Model
{
    public class Receipts
    {
        public virtual int? rtId { get; set; }
        public virtual string CompanyCode{ get; set; }
        public virtual Int32 BatchNo { get; set; }
        public virtual Int64 SerialNo { get; set; }
        public virtual Int32 SubSerialNo { get; set; }
        public virtual string TransId { get; set; }
        public virtual String TransType { get; set; }
        public virtual String TellerNo { get; set; }
        public virtual DateTime TransDate { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual String TransMode { get; set; }
        public virtual String ReceiptType { get; set; }
        public virtual String ReferenceNo { get; set; }
        public virtual String ProductCode { get; set; }
        public virtual String ProductClass { get; set; }
        public virtual String ProductPlanNo { get; set; }
        public virtual String ProductCoverNo { get; set; }
        public virtual String DocNo { get; set; }
        public virtual String CurrencyType { get; set; }
        public virtual String ChequeTellerNo { get; set; }
        public virtual String ChequeInwardNo { get; set; }
        public virtual DateTime ChequeDate { get; set; }
        public virtual String PayeeName { get; set; }
        public virtual String TranDescription1 { get; set; }
        public virtual String TranDescription2 { get; set; }
        public virtual String BranchCode { get; set; }
        public virtual String BankCode { get; set; }
        public virtual decimal TotalAmountLC { get; set; }
        public virtual decimal TotalAmountFC { get; set; }
        public virtual String InsuredCode { get; set; }
        public virtual String AgentCode { get; set; }
        public virtual String CommissionApplicable { get; set; }
        public virtual decimal PolicyRegularContribution { get; set; }
        public virtual String PolicyPaymentMode { get; set; }
        public virtual String MainAccountDebit { get; set; }
        public virtual String SubAccountDebit { get; set; }
        public virtual String MainAccountCredit { get; set; }
        public virtual String SubAccountCredit { get; set; }
        public virtual String LedgerTypeCredit { get; set; }
        public virtual String Flag { get; set; }
        public virtual String OperId { get; set; }
        public virtual String FileNo { get; set; }
        public virtual string ProposalNo { get; set; }
        public virtual string PolicyNo { get; set; }
        public virtual String ProcDate { get; set; }
        public virtual String TempPolicyNo { get; set; }
        public virtual String TempInsuredName { get; set; }
        public virtual String TempProdCode { get; set; }
        public virtual String TempProdCat { get; set; }
        public virtual string PostStatus { get; set; }
        public virtual string ApprovalStatus { get; set; } 
        public virtual string Remarks { get; set; }
        public virtual string DetailTransStatus { get; set; }
        public virtual String DetailDocNo { get; set; }
        public virtual decimal DetailTransAmountLC { get; set; }
        public virtual decimal DetailTransAmountFC { get; set; }
        public virtual DateTime DetailTransDate { get; set; }

    }
}
