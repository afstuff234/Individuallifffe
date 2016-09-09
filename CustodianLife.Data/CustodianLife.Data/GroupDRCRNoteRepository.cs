using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustodianLife.Model;
using CustodianLife.Repositories;
using NHibernate;
using System.Data.SqlClient;
using System.Data;

namespace CustodianLife.Data
{
    public class GroupDRCRNoteRepository
    {
        private static ISession GetSession()
        {
            return SessionProvider.SessionFactory.OpenSession();
        }

        public IList<GroupDRCRNote> GetDebitNoteList(String _key, String _value)
        {
            String fCriteria = string.Empty;
            string hqlOptions = string.Empty;
            //the _key is a code or name with which to filter the data
           if (_key == "Scheme")
               fCriteria = "DrCrNoteDesc";// Scheme Name not save in dnote table
                else
                    if (_key == "Policy")
                        fCriteria = "PolicyNo";
                   

            switch (_key)
            {
                case "Policy":
                    hqlOptions = "from GroupDRCRNote c where c." + fCriteria + " like '%" + _value + "%' and DrCr='D'";
                    using (var session = GetSession())
                    {
                        return session.CreateQuery(hqlOptions).List<GroupDRCRNote>();
                    }

                case "Scheme":
                    hqlOptions = "from GroupDRCRNote c where c." + fCriteria + " like '%" + _value + "%' and DrCr='D'";
                    using (var session = GetSession())
                    {
                        return session.CreateQuery(hqlOptions).List<GroupDRCRNote>();
                    }
                default:
                    return null;
                // break;

            }
        }
    }
}
