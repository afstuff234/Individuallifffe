﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using CustodianLife.Model;
using CustodianLife.Repositories;
using NHibernate;
using System.Runtime.InteropServices;


namespace CustodianLife.Data
{
    public class ReceiptsRepository : IReceiptsRepository
    {
        private static ISession GetSession()
        {
            return SessionProvider.SessionFactory.OpenSession();
        }

        public void Save(Receipts saveObj)
        {
            using (var session = GetSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.FlushMode = FlushMode.Commit;
                    session.SaveOrUpdate(saveObj);
                    trans.Commit();
                    session.Flush();
                    //}
                }
            }
        }
        public void Delete(Receipts delObj)
        {
            using (var session = GetSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.Delete(delObj);
                    trans.Commit();
                }
            }
        }
        public IList<Receipts> ReceiptDetailInfo()
        {
            using (var session = GetSession())
            {
                var intr = session.CreateCriteria<Receipts>()

                                     .List<Receipts>();
                return intr;
            }
        }

        public Receipts GetById(String _key)
        {
            //the _key is an array of string values (3). Split into individual values and fill the parameters
            Char[] seperator = new char[] { ',' };
            string[] keys = _key.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

            string hqlOptions = "from Receipts i where i.rtId = " + keys[0]
                              + " and i.CompanyCode = '" + keys[1] + "'"
                              + " and i.BatchNo = " + keys[2]
                                  + " and i.SerialNo = " + keys[3];

            using (var session = GetSession())
            {

                return (Receipts)session.CreateQuery(hqlOptions).UniqueResult();
            }
        }
        public IList<Receipts> GetByReceiptNo(String _receiptno)
        {
            string hqlOptions = "from Receipts i where i.DocNo = '" + _receiptno + "'";
            using (var session = GetSession())
            {
                return session.CreateQuery(hqlOptions).SetMaxResults(1).List<Receipts>();
            }

         }
                 

        public IList<Receipts> GetById(String _key, String _value)
        {
            _value = _value.Trim();
            String fCriteria = string.Empty;
            string hqlOptions = string.Empty;
            //the _key is a code or name with which to filter the data
            if (_key == "Code")
                fCriteria = "ReferenceNo";
            else if (_key == "TDate")
                fCriteria = "TransDate";
            else if (_key == "PayerName")
                fCriteria = "PayeeName";
            else if (_key == "Receipt")
                fCriteria = "DocNo";
            else if (_key == "All")
                fCriteria = "";

            switch (_key)
            {
                case "Code":
                    hqlOptions = "from Receipts r where r." + fCriteria + " like '%" + _value + "%' Order by EntryDate Desc";
                    // hqlOptions = "from Receipts r where r.ReferenceNo like '%" + _value + "%' Order by EntryDate Desc";
                    using (var session = GetSession())
                    {
                        return session.CreateQuery(hqlOptions).List<Receipts>();
                    }

                case "TDate":
                    //change user date to server format
                    _value = hashHelper.DateToServerSetting(_value);

                    hqlOptions = "from Receipts r where r." + fCriteria + " = '" + _value + "' Order by EntryDate Desc";
                    using (var session = GetSession())
                    {

                        return session.CreateQuery(hqlOptions).List<Receipts>();
                    }

                case "PayerName":
                    hqlOptions = "from Receipts r where r." + fCriteria + " like '%" + _value + "%' Order by EntryDate Desc";
                    using (var session = GetSession())
                    {
                        return session.CreateQuery(hqlOptions).List<Receipts>();
                    }

                case "Receipt":
                    hqlOptions = "from Receipts r where r." + fCriteria + " like '%" + _value + "%' Order by EntryDate Desc";
                    
                  

                        using (var session = GetSession())
                        {
                            return session.CreateQuery(hqlOptions).List<Receipts>();
                        }
                    
                case "All":

                    hqlOptions = "from Receipts r order by EntryDate Desc";
                    using (var session = GetSession())
                    {
                        var r = session.CreateQuery(hqlOptions).List<Receipts>().Count();
                        return session.CreateQuery(hqlOptions).List<Receipts>();
                    }

                default:
                    return null;
                // break;

            }
        }



        public IList<ChartOfAccounts> GetAccountChartList(String _key, String _value, String _parentcode)
        {
            String fCriteria = string.Empty;
            string hqlOptions = string.Empty;
            //the _key is a code or name with which to filter the data
            if (_key == "All")
                fCriteria = "";
            else
                if (_key == "Code")
                    fCriteria = "MainAcctsCode";
                else
                    if (_key == "Name")
                        fCriteria = "FullDescription";
                    else
                        if (_key == "Name1")
                            fCriteria = "ShortDescription";
                        else
                            if (_key == "SbCode")
                                fCriteria = "SubAcctsCode";

            switch (_key)
            {
                case "Code":
                case "Name":
                case "SbCode":
                    hqlOptions = "from ChartOfAccounts c where c." + fCriteria + " like '%" + _value + "%'";
                    using (var session = GetSession())
                    {
                        return session.CreateQuery(hqlOptions).List<ChartOfAccounts>();
                    }
                case "Name1":

                    hqlOptions = " from ChartOfAccounts c where c." + fCriteria + " like '%" + _value + "%' and c.MainAcctsCode like '%" + _parentcode + "%'";
                    using (var session = GetSession())
                    {

                        return session.CreateQuery(hqlOptions).List<ChartOfAccounts>();
                    }

                case "All":

                    hqlOptions = "from ChartOfAccounts c";
                    using (var session = GetSession())
                    {

                        return session.CreateQuery(hqlOptions).List<ChartOfAccounts>();
                    }

                default:
                    return null;
                // break;

            }
        }
        //public IList<PolicyInfo> GetPolicyList(String _key, String _value)
        //{
        //    String fCriteria = string.Empty;
        //    string hqlOptions = string.Empty;
        //    //the _key is a code or name with which to filter the data
        //    if (_key == "All")
        //        fCriteria = "";
        //    else
        //        if (_key == "Policy")
        //            fCriteria = "PolicyNumber";
        //        else
        //            if (_key == "Proposal")
        //                fCriteria = "ProposalNumber";
        //    switch (_key)
        //    {
        //        case "Policy":
        //        case "Proposal":
        //            hqlOptions = "from PolicyInfo p where p." + fCriteria + " like '%" + _value + "%'";
        //            using (var session = GetSession())
        //            {

        //                return session.CreateQuery(hqlOptions).List<PolicyInfo>();
        //            }
        //        case "All":
        //            hqlOptions = "from PolicyInfo p" ;
        //            using (var session = GetSession())
        //            {

        //                return session.CreateQuery(hqlOptions).List<PolicyInfo>();
        //            }

        //        default:
        //            return null;
        //        // break;

        //    }
        //}

        public DataSet GetPolicyList(String _key, String _value)
        {

            string query = "SELECT * "
                          + "FROM CiFn_GetPolicyList('"
                          + _key + "','" + _value + "')";

            return GetDataSet(query);

        }

        public IList<Agents> GetAgentsAccountList(String _key, String _value)
        {
            String fCriteria = string.Empty;
            string hqlOptions = string.Empty;
            //the _key is a code or name with which to filter the data
            if (_key == "All")
                fCriteria = "";
            else
                if (_key == "Code")
                    fCriteria = "AgentCode";
                else
                    if (_key == "Name")
                        fCriteria = "AgentName";

            switch (_key)
            {
                case "Code":
                case "Name":
                    hqlOptions = "from Agents c where c." + fCriteria + " like '%" + _value + "%'";
                    using (var session = GetSession())
                    {
                        return session.CreateQuery(hqlOptions).List<Agents>();
                    }
                case "All":

                    hqlOptions = "from Agents c";
                    using (var session = GetSession())
                    {

                        return session.CreateQuery(hqlOptions).List<Agents>();
                    }

                default:
                    return null;
                // break;

            }
        }

        public IList<Brokers> GetBrokersList(String _key, String _value)
        {
            String fCriteria = string.Empty;
            string hqlOptions = string.Empty;
            //the _key is a code or name with which to filter the data
            if (_key == "All")
                fCriteria = "";
            else
                if (_key == "Code")
                    fCriteria = "BrokerCode";
                else
                    if (_key == "Name")
                        fCriteria = "LongDescription";

            switch (_key)
            {
                case "Code":
                case "Name":
                    hqlOptions = "from Brokers c where c." + fCriteria + " like '%" + _value + "%'";
                    using (var session = GetSession())
                    {
                        return session.CreateQuery(hqlOptions).List<Brokers>();
                    }
                case "All":

                    hqlOptions = "from Brokers c";
                    using (var session = GetSession())
                    {

                        return session.CreateQuery(hqlOptions).List<Brokers>();
                    }

                default:
                    return null;
                // break;

            }
        }
        public String GetBranchInfo(String _branchcode)
        {
            //queries the generic lifecodes table and extract info for the branches only -- L02, 003
            string query = "SELECT * "
                          + "FROM CiFn_GetLifeCodeDetails('"
                          + _branchcode + "','L02','003',NULL)";

            return GetDataSet(query).GetXml();
        }

        public String GetCurrencyType(String _currencycode)
        {
            //queries the generic lifecodes table and extract info for the branches only -- L02, 003
            string query = "SELECT * "
                          + "FROM TBIL_LIFE_CODES WHERE (TBIL_COD_TAB_ID='L02' AND TBIL_COD_TYP='017'"
                           + "AND TBIL_COD_ITEM='" + _currencycode + "')";


            return GetDataSet(query).GetXml();
        }


        public Receipts GetById(Int32? id)
        {
            using (var session = GetSession())
            {
                return session.Get<Receipts>(id);
            }
        }
        ///// <summary>
        ///// Gets the policy info object using the nhibernate object
        ///// this is available to be used in serialization to an xml object
        ///// </summary>
        ///// <param name="criteriaValue">Either the policy or the proposal number </param>
        ///// <param name="searchtype">the search switch (P = Policy, D = Proposal)</param>
        ///// <returns></returns>
        //public PolicyInfo GetPolicyInfo(String criteriaValue, String searchType)
        //{
        //    String fld = String.Empty;
        //    if (searchType == "P")
        //        fld = "PolicyNumber";
        //    else if (searchType == "D")
        //        fld = "ProposalNumber";

        //    string hqlOptions = "from PolicyInfo p where p." + fld
        //                      + " = '" + criteriaValue + "'";

        //    using (var session = GetSession())
        //    {

        //        return (PolicyInfo)session.CreateQuery(hqlOptions).UniqueResult();
        //    }
        //}
        /// <summary>
        /// Gets the policy info in a dataset. 
        /// this is to be converted to the xml format transferred to the jquery function
        /// </summary>
        /// <param name="criteriaValue">Either the policy or the proposal number </param>
        /// <param name="searchtype">the search switch (P = Policy, D = Proposal)</param>
        /// <returns></returns>
        public String GetPolicyInfo(String criteriaValue, String searchType)
        {
            String fld = String.Empty;
            if (criteriaValue.StartsWith("P/2/"))
                fld = "[TBIL_POLY_PROPSAL_NO]";
            else if (searchType == "P")
                fld = "[TBIL_POLY_POLICY_NO]";
            else if (searchType == "D")
                fld = "[TBIL_POLY_PROPSAL_NO]";
            string query = "SELECT "
                           + " [TBIL_POLY_PROPSAL_NO]"
                          + ",[TBIL_POLY_POLICY_NO]"
                          + ",[TBIL_POLY_ASSRD_CD]"
                          + ",(SELECT TOP 1 [TBIL_INSRD_SURNAME] + ' ' + ISNULL([TBIL_INSRD_FIRSTNAME],' ')"
                          + " FROM [TBIL_INS_DETAIL] b WHERE b.[TBIL_INSRD_CODE] = p.[TBIL_POLY_ASSRD_CD]) as Insured_Name"
                          + "	                    ,	(SELECT TOP 1 "
                          + "    [TBIL_INSRD_ADRES1] + ' ' + ISNULL([TBIL_INSRD_ADRES2],' ') "
                          + "  FROM [TBIL_INS_DETAIL] y "
                          + "  WHERE y.[TBIL_INSRD_CODE] = p.[TBIL_POLY_ASSRD_CD]) as Insured_Address "
                          + ",[TBIL_POLY_AGCY_CODE], [TBIL_POLY_PRDCT_CD] as Product_Code "
                          + ",[TBIL_POLY_FILE_NO] as File_No"
                          + ",(SELECT TOP 1 [TBIL_AGCY_AGENT_NAME] FROM [TBIL_AGENCY_CD] d WHERE d.[TBIL_AGCY_AGENT_CD]= p.[TBIL_POLY_AGCY_CODE]) as Agent_Name"
                          + ", [TBIL_POL_PRM_DTL_MOP_PRM_LC]"
                          + ",[TBIL_POL_PRM_MODE_PAYT] as Payment_Mode "
                          + ",(SELECT CASE [TBIL_POL_PRM_MODE_PAYT] WHEN 'M' THEN 'MONTHLY' WHEN 'A' THEN 'ANNUALLY' WHEN 'H' THEN 'HALF YEARLY' WHEN 'Q' THEN 'QUARTERLY' END) as Payment_Mode_Desc"
                          + ",convert(varchar, [TBIL_POLICY_EFF_DT], 102) as TBIL_POLICY_EFF_DT"
                          + " FROM [TBIL_POLICY_DET] p INNER JOIN [TBIL_POLICY_PREM_DETAILS] q "
                          + "ON p.[TBIL_POLY_PROPSAL_NO] = q.[TBIL_POL_PRM_DTL_PROP_NO] "
                          + "INNER JOIN [TBIL_POLICY_PREM_INFO] r "
                          + "ON p.[TBIL_POLY_PROPSAL_NO] = r.[TBIL_POL_PRM_PROP_NO]"
                          + " WHERE p." + fld + " = '" + criteriaValue + "'";
            return GetDataSet(query).GetXml();

        }

        public DataSet GetPolicyInfoDataSet(String criteriaValue, String searchType)
        {
            String fld = String.Empty;
            if (criteriaValue.StartsWith("P/2/"))
                fld = "[TBIL_POLY_PROPSAL_NO]";
            else if (searchType == "P")
                fld = "[TBIL_POLY_POLICY_NO]";
            else if (searchType == "D")
                fld = "[TBIL_POLY_PROPSAL_NO]";
            else
                fld = "[TBIL_POLY_POLICY_NO]";
            string query = "SELECT "
                          + " [TBIL_POLY_PROPSAL_NO]"
                         + ",[TBIL_POLY_POLICY_NO]"
                         + ",[TBIL_POLY_ASSRD_CD]"
                         + ",(SELECT TOP 1 [TBIL_INSRD_SURNAME] + ' ' + ISNULL([TBIL_INSRD_FIRSTNAME],' ')"
                         + " FROM [TBIL_INS_DETAIL] b WHERE b.[TBIL_INSRD_CODE] = p.[TBIL_POLY_ASSRD_CD]) as Insured_Name"
                         + "	                    ,	(SELECT TOP 1 "
                         + "    [TBIL_INSRD_ADRES1] + ' ' + ISNULL([TBIL_INSRD_ADRES2],' ') "
                         + "  FROM [TBIL_INS_DETAIL] y "
                         + "  WHERE y.[TBIL_INSRD_CODE] = p.[TBIL_POLY_ASSRD_CD]) as Insured_Address "
                         + ",[TBIL_POLY_AGCY_CODE], [TBIL_POLY_PRDCT_CD] as Product_Code "
                         + ",[TBIL_POLY_FILE_NO] as File_No"
                         + ",(SELECT TOP 1 [TBIL_AGCY_AGENT_NAME] FROM [TBIL_AGENCY_CD] d WHERE d.[TBIL_AGCY_AGENT_CD]= p.[TBIL_POLY_AGCY_CODE]) as Agent_Name"
                         + ", [TBIL_POL_PRM_DTL_MOP_PRM_LC]"
                         + ",[TBIL_POL_PRM_MODE_PAYT] as Payment_Mode "
                         + ",(SELECT CASE [TBIL_POL_PRM_MODE_PAYT] WHEN 'M' THEN 'MONTHLY' WHEN 'A' THEN 'ANNUALLY' WHEN 'H' THEN 'HALF YEARLY' WHEN 'Q' THEN 'QUARTERLY' END) as Payment_Mode_Desc"
                         + ",convert(varchar, [TBIL_POLICY_EFF_DT], 102) as TBIL_POLICY_EFF_DT"
                         + " FROM [TBIL_POLICY_DET] p INNER JOIN [TBIL_POLICY_PREM_DETAILS] q "
                         + "ON p.[TBIL_POLY_PROPSAL_NO] = q.[TBIL_POL_PRM_DTL_PROP_NO] "
                         + "INNER JOIN [TBIL_POLICY_PREM_INFO] r "
                         + "ON p.[TBIL_POLY_PROPSAL_NO] = r.[TBIL_POL_PRM_PROP_NO]"
                         + " WHERE p." + fld + " = '" + criteriaValue + "'";
            return GetDataSet(query);

        }
        /// <summary>
        /// 
        /// this returns a dataset to be converted to XML for serialization into a Json object 
        /// used to retrieve data from the back end to the html page using jQuery 
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        private static DataSet GetDataSet(string qry)
        {
            using (var session = GetSession())
            {
                using (var conn = session.Connection as SqlConnection)
                {
                    var adapter = new SqlDataAdapter(qry, conn);
                    var dataSet = new System.Data.DataSet();

                    adapter.Fill(dataSet);

                    return dataSet;
                }
            }
        }

        private static DataSet GetDataSet(string qry, SqlCommand com)
        {
            using (var session = GetSession())
            {
                using (var conn = session.Connection as SqlConnection)
                {
                    var adapter = new SqlDataAdapter();
                    var dataSet = new System.Data.DataSet();
                    com.Connection = conn;
                    adapter.SelectCommand = com;
                    adapter.Fill(dataSet);
                    return dataSet;

                }
            }
        }


        public String GetNextSerialNumber(string sys_id
                                           , String sys_type
                                           , String sys_branch
                                           , String sys_year
                                           , String sys_prefix
                                           , String sys_out_int
                                           , String sys_out_char)
        {
            var session = GetSession();
            session.BeginTransaction();
            IDbCommand command = session.Connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CiSP_GetAcctSerialNo";

            IDbDataParameter inputparameter1 = command.CreateParameter();
            inputparameter1.ParameterName = "@PARAM_SYS_ID";
            inputparameter1.DbType = DbType.String;
            inputparameter1.Size = 5;
            inputparameter1.Direction = ParameterDirection.Input;
            inputparameter1.Value = sys_id;

            command.Parameters.Add(inputparameter1);

            IDbDataParameter inputparameter2 = command.CreateParameter();
            inputparameter2.ParameterName = "@PARAM_SYS_TYPE";
            inputparameter2.DbType = DbType.String;
            inputparameter2.Direction = ParameterDirection.Input;
            inputparameter2.Size = 5;
            inputparameter2.Value = sys_type;
            command.Parameters.Add(inputparameter2);

            IDbDataParameter inputparameter3 = command.CreateParameter();
            inputparameter3.ParameterName = "@PARAM_SYS_BRANCH";
            inputparameter3.DbType = DbType.String;
            inputparameter3.Direction = ParameterDirection.Input;
            inputparameter3.Size = 5;
            inputparameter3.Value = sys_branch;
            command.Parameters.Add(inputparameter3);

            IDbDataParameter inputparameter4 = command.CreateParameter();
            inputparameter4.ParameterName = "@PARAM_SYS_YEAR";
            inputparameter4.DbType = DbType.String;
            inputparameter4.Direction = ParameterDirection.Input;
            inputparameter4.Size = 5;
            inputparameter4.Value = sys_year;
            command.Parameters.Add(inputparameter4);

            IDbDataParameter inputparameter5 = command.CreateParameter();
            inputparameter5.ParameterName = "@PARAM_SYS_PREFIX";
            inputparameter5.DbType = DbType.String;
            inputparameter5.Size = 6;
            inputparameter5.Direction = ParameterDirection.Input;
            inputparameter5.Value = sys_prefix;
            command.Parameters.Add(inputparameter5);

            IDbDataParameter inputparameter6 = command.CreateParameter();
            inputparameter6.ParameterName = "@PARAM_OUT_INT";
            inputparameter6.DbType = DbType.String;
            inputparameter6.Direction = ParameterDirection.Input;
            inputparameter6.Size = 20;
            inputparameter6.Value = sys_out_int;
            command.Parameters.Add(inputparameter6);

            IDbDataParameter outparameter = command.CreateParameter();
            outparameter.ParameterName = "@PARAM_OUT_CHAR";
            outparameter.DbType = DbType.String;
            outparameter.Size = 20;
            outparameter.Direction = ParameterDirection.Output;
            command.Parameters.Add(outparameter);

            session.Transaction.Enlist(command);
            command.ExecuteNonQuery();

            //retrieve value from out parameter
            IDbDataParameter outparameter4valu = command.CreateParameter();

            outparameter4valu = (IDbDataParameter)command.Parameters["@PARAM_OUT_CHAR"];
            string outval = (string)outparameter4valu.Value;

            session.Transaction.Commit();
            return outval;
        }

        /// <summary>
        /// Retrieves the periods band covered amount of premium paid. 
        /// </summary>
        /// <param name="_polnum">Policy Number</param>
        /// <param name="_mop">Mode of Payment</param>
        /// <param name="_effdate">Date of Payment</param>
        /// <param name="_contrib">Normal Contribution as required by Policy document</param>
        /// <param name="_amtpaid">Amount Paid</param>
        /// <returns></returns>
        public String GetPaymentCover(string _polnum
                                         , String _mop
                                         , String _effdate
                                         , String _contrib
                                        , String _amtpaid)
        {


            string query = "SELECT * "
                          + "FROM CiFn_ReceiptCoverPeriods('"
                          + _polnum + "','"
                          + _mop + "', '"
                          + _effdate + "', "
                          + _contrib + ","
                          + _amtpaid + ",NULL,NULL,NULL)";

            return GetDataSet(query).GetXml();
        }


        public DataSet GetPaymentCoverDataSet(string _polnum
                                        , String _mop
                                        , String _effdate
                                        , String _contrib
                                       , String _amtpaid)
        {


            string query = "SELECT * "
                          + "FROM CiFn_ReceiptCoverPeriods('"
                          + _polnum + "','"
                          + _mop + "', '"
                          + _effdate + "', "
                          + _contrib + ","
                          + _amtpaid + ",NULL,NULL,NULL)";

            return GetDataSet(query);
        }

        public String GetAccountChartDetails(string _accountsubcode
                                         , String _accountmaincode)
        {


            string query = "SELECT * "
                          + "FROM CiFn_GetChartOfAccountDetail('"
                          + _accountsubcode + "','"
                          + _accountmaincode + "',NULL,NULL,NULL)";

            return GetDataSet(query).GetXml();
        }

        public DataSet GetAccountChartDetailsDataSet(string _accountsubcode
                                         , String _accountmaincode)
        {


            string query = "SELECT * "
                          + "FROM CiFn_GetChartOfAccountDetail('"
                          + _accountsubcode + "','"
                          + _accountmaincode + "',NULL,NULL,NULL)";

            return GetDataSet(query);
        }


        /// <summary>
        /// Provides the receipt details to enable printing
        /// </summary>
        /// <param name="_receiptNo">the receipt number to be printed</param>
        /// <returns>Returns a dataset to be used by the print engine</returns>
        public DataSet GetReceiptDetails(string _receiptNo)
        {
            string query = "SELECT * "
                          + "FROM CiFn_ReceiptDetails('"
                          + _receiptNo + "',NULL,NULL,NULL)";

            return GetDataSet(query);

        }



        //public String GetPaymentCover1(string _polnum
        //                                 , String _mop
        //                                 , String _effdate
        //                                 , String _contrib
        //                                , String _amtpaid)
        //{

        //    var session = GetSession();
        //    session.BeginTransaction();
        //    SqlCommand command = (SqlCommand)session.Connection.CreateCommand();
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "CiFn_ReceiptCoverPeriods";

        //    SqlParameter inputparameter1 = command.CreateParameter();
        //    inputparameter1.ParameterName = "@pPolicyNo";
        //    inputparameter1.DbType = DbType.String;
        //    inputparameter1.Size = 25;
        //    inputparameter1.Direction = ParameterDirection.Input;
        //    inputparameter1.Value = _polnum;

        //    command.Parameters.Add(inputparameter1);

        //    SqlParameter inputparameter2 = command.CreateParameter();
        //    inputparameter2.ParameterName = "@pMOP";
        //    inputparameter2.DbType = DbType.String;
        //    inputparameter2.Direction = ParameterDirection.Input;
        //    inputparameter2.Size = 1;
        //    inputparameter2.Value = _mop;
        //    command.Parameters.Add(inputparameter2);

        //    SqlParameter inputparameter3 = command.CreateParameter();
        //    inputparameter3.ParameterName = "@pPolicyEffDate";
        //    inputparameter3.DbType = DbType.Date;
        //    inputparameter3.Direction = ParameterDirection.Input;
        //    //            inputparameter3.Size = 5;
        //    inputparameter3.Value = Convert.ToDateTime(_effdate);
        //    command.Parameters.Add(inputparameter3);

        //    SqlParameter inputparameter8 = command.CreateParameter();
        //    inputparameter8.ParameterName = "@pCONTRIB";
        //    inputparameter8.DbType = DbType.Decimal;
        //    inputparameter8.Direction = ParameterDirection.Input;
        //    //inputparameter4.Size = 5;
        //    inputparameter8.Value = Convert.ToDecimal(_contrib);
        //    command.Parameters.Add(inputparameter8);

        //    SqlParameter inputparameter4 = command.CreateParameter();
        //    inputparameter4.ParameterName = "@pAmountPaid";
        //    inputparameter4.DbType = DbType.Decimal;
        //    inputparameter4.Direction = ParameterDirection.Input;
        //    //inputparameter4.Size = 5;
        //    inputparameter4.Value = Convert.ToDecimal(_amtpaid);
        //    command.Parameters.Add(inputparameter4);

        //    SqlParameter inputparameter5 = command.CreateParameter();
        //    inputparameter5.ParameterName = "@Param1";
        //    inputparameter5.DbType = DbType.String;
        //    inputparameter5.Size = 100;
        //    inputparameter5.Direction = ParameterDirection.Input;
        //    inputparameter5.Value = null;
        //    command.Parameters.Add(inputparameter5);

        //    SqlParameter inputparameter6 = command.CreateParameter();
        //    inputparameter6.ParameterName = "@Param2";
        //    inputparameter6.DbType = DbType.String;
        //    inputparameter6.Direction = ParameterDirection.Input;
        //    inputparameter6.Size = 100;
        //    inputparameter6.Value = null;
        //    command.Parameters.Add(inputparameter6);

        //    SqlParameter inputparameter7 = command.CreateParameter();
        //    inputparameter7.ParameterName = "@Param3";
        //    inputparameter7.DbType = DbType.String;
        //    inputparameter7.Size = 100;
        //    inputparameter7.Direction = ParameterDirection.Input;
        //    command.Parameters.Add(inputparameter7);

        //    SqlParameter returnparameter = command.CreateParameter();
        //    returnparameter.ParameterName = "@RETURN_VALUE";
        //    returnparameter.DbType = DbType.String;
        //    returnparameter.Direction = ParameterDirection.ReturnValue;
        //    command.Parameters.Add(returnparameter);

        //    session.Transaction.Commit();
        //    return GetDataSet(" ", command).GetXml();
        //}

       //Not implemented in this solution
        public void UpdateUnCompletedRec(string criteriaValue)
        {
            String fld = String.Empty;
            if (criteriaValue.StartsWith("P/2/"))
                fld = "[TBIL_POLY_PROPSAL_NO]";
            else
                fld = "[TBIL_POLY_POLICY_NO]";
            SqlCommand com;
            using (var session = GetSession())
            {
                using (var conn = session.Connection as SqlConnection)
                {
                    string stryquery = "UPDATE TBIL_POLICY_DET SET TBIL_POLY_FLAG='M'";
                    stryquery += " WHERE " + fld + " = '" + criteriaValue + "'";
                    com = new SqlCommand(stryquery, conn);
                    com.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void GetUnCompletedReceiptRecords(int startBatchNo, int endBatchNo)
        {
            DateTime Eff_Date = DateTime.Now;
            string Insured_Code = "";
            string Agent_Code = "";
            decimal Poly_Contrib = 0;
            string Mop;
            DataSet ds;
            DataRow dr, dr2;
            ds = new DataSet();
            DataSet dt1 = new DataSet();

            string query = "SELECT * "
                          + "FROM CiFn_Get_Uncompleted_Receipt_Record("
                          + startBatchNo + ","
                          + endBatchNo + ","
                          + "NULL,NULL,NULL)";
            ds = GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    string refNo = dr["REF_NO"].ToString();
                    string recType = dr["RECEIPT_TYPE"].ToString();
                    dt1 = GetPolicyInfoDataSet(refNo, recType);
                    if (dt1.Tables[0].Rows.Count > 0)
                    {
                        dr2 = dt1.Tables[0].Rows[0];
                        Insured_Code = dr2["TBIL_POLY_ASSRD_CD"].ToString();
                        Agent_Code = dr2["TBIL_POLY_AGCY_CODE"].ToString().ToString();
                        Poly_Contrib = Convert.ToDecimal(dr2["TBIL_POL_PRM_DTL_MOP_PRM_LC"]);
                        Mop = dr2["Payment_Mode"].ToString();
                        if (dr2["TBIL_POLICY_EFF_DT"] != DBNull.Value)
                            Eff_Date = Convert.ToDateTime(dr2["TBIL_POLICY_EFF_DT"]);
                        else
                            Eff_Date = Convert.ToDateTime("01/01/2014");
                        UpdateReceiptFile(refNo, Insured_Code, Mop, Poly_Contrib, Agent_Code);
                    }
                }
            }
        }

        public void UpdateReceiptFile(string refNo, string insCode, string mop, decimal polyContrib, string agentCode)
        {
            SqlCommand com;
            using (var session = GetSession())
            {
                using (var conn = session.Connection as SqlConnection)
                {
                    string stryquery = "UPDATE TBFN_RECPT_FILE SET TBFN_ACCT_INS_CODE='" + insCode + "'";
                    stryquery += ", TBFN_ACCT_POLY_MOP='" + mop + "'";
                    stryquery += ", TBFN_ACCT_POLY_CONTRIB=" + polyContrib + "";
                    stryquery += ", TBFN_ACCT_AGENT_CODE='" + agentCode + "'";
                    stryquery += " WHERE TBFN_ACCT_REF_NO = '" + refNo + "'";
                    com = new SqlCommand(stryquery, conn);
                    com.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

    }
}
