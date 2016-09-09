﻿using System;
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
    public class ProductDetailsRepository:IProductDetailsRepository
    {
        private static ISession GetSession()
        {
            return SessionProvider.SessionFactory.OpenSession();
        }

        public void Save(ProductDetails saveObj)
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
        public void Delete(ProductDetails delObj)
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
        public IList<ProductDetails> ProductDetailInfo()
        {
            using (var session = GetSession())
            {
                var pDet = session.CreateCriteria<ProductDetails>()

                                     .List<ProductDetails>();

                return pDet;
            }
        }
        public ProductDetails GetById(Int32? id)
        {
            using (var session = GetSession())
            {
                return session.Get<ProductDetails>(id);
            }
        }
        public ProductDetails GetById(String _key)
        {
            //the _key is an array of string values (3). Split into individual values and fill the parameters
            Char[] seperator = new char[] { ',' };
            string[] keys = _key.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

            string hqlOptions = "from ProductDetails i where i.pdId = " + keys[0]
                              + " and i.ProductDetailsModule = '" + keys[1] + "'";

            using (var session = GetSession())
            {

                return (ProductDetails)session.CreateQuery(hqlOptions).UniqueResult();
            }
        }

        public IList<ProductDetails> GetProductByCatCode(string CatCode)
        {
            using (var session = GetSession())
            {
                var pDet = session.CreateCriteria<ProductDetails>()

                                     .List<ProductDetails>().Where(c=>c.ProductCategory==CatCode).ToList<ProductDetails>();

                return pDet;

            }
        }


        public String GetProductByCatCodeClient(string CatCode)
        {
            //queries the generic lifecodes table and extract info for the branches only -- L02, 003
            string query;
            switch(CatCode)
            {
                case "A":
                     query = "SELECT * "
                        + "FROM TBIL_PRODUCT_DETL WHERE (TBIL_PRDCT_DTL_CAT='" + CatCode + "')";
                    break;

                case "I":
                     query = "SELECT * "
                        + "FROM TBIL_PRODUCT_DETL WHERE TBIL_PRDCT_DTL_CODE='P004'";
                    break;

                case "E":
                    query = "SELECT * "
                       + "FROM TBIL_PRODUCT_DETL WHERE TBIL_PRDCT_DTL_CAT NOT IN('A','I','G') AND TBIL_PRDCT_DTL_CODE NOT IN('P004')";
                    break;
                default:
                    query = "SELECT * "
                      + "FROM TBIL_PRODUCT_DETL WHERE (TBIL_PRDCT_DTL_CAT='" + CatCode + "')";
                    break;
            }
               

            //string query = "SELECT * "
            //              + "FROM TBIL_PRODUCT_DETL WHERE (TBIL_PRDCT_DTL_CAT='" + CatCode + "')";


            return GetDataSet(query).GetXml();
        }

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
    }
}
