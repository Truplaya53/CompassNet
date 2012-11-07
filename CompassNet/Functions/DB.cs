using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace CompassNet.Functions
{
    public class DB
    {//Driver={Pervasive ODBC Client Interface};ServerName=192.168.1.206;dbq=CompassIES;

#region "Pervasive Queries"


        public static List<BusinessObjects.Quote> getQuotes()
        {
            string sql = @"SELECT Top 100 ID, REFERENCE_NO, CONTROLLER, EFFECTIVE_DATE, EXPIRATION_DATE, SHIPPER, SHIPPER_NAME, SHIPPER_CONTACT, SHIPPER_PHONE, 
                            SHIPPER_FAX, COMMODITY, MOVE_TYPE, PAYMENT, P_O_NUMBER, PLR, POL, POD, SUPPLIER_STATE, SUPPLIER_ZIP_CODE, CONSIGNEE_STATE, 
                            CONSIGNEE_ZIP_CODE, CONSIGNEE_COUNTRY, CARRIER, BUS_LINE, Incoterms, IncotermsLocation, ContainerHandeling, Booking_UID
,Voyage,FinalDestino,QuotedBY,Vessel
                            FROM QUOTES_DAT
                            order by effective_date desc";
            //string sql = @"select top 100 * from Quotes";
            DataTable dt = new DataTable();
            using (OdbcDataAdapter adp = new OdbcDataAdapter(sql, Properties.Settings.Default.IESDBConn))
            {
                adp.Fill(dt);
            };

            List<BusinessObjects.Quote> lq = new List<BusinessObjects.Quote>();
            foreach (DataRow row in dt.Rows)
            {
                BusinessObjects.Quote q = populateQuoteFromDataRow(row);
                lq.Add(q);
            }
            return lq;
        }
        public static List<BusinessObjects.Quote> getQuotes(string criteria, string category)
        {
            string sql = @"SELECT Top 100 ID, REFERENCE_NO, CONTROLLER, EFFECTIVE_DATE, EXPIRATION_DATE, SHIPPER, SHIPPER_NAME, SHIPPER_CONTACT, SHIPPER_PHONE, 
                            SHIPPER_FAX, COMMODITY, MOVE_TYPE, PAYMENT, P_O_NUMBER, PLR, POL, POD, SUPPLIER_STATE, SUPPLIER_ZIP_CODE, CONSIGNEE_STATE, 
                            CONSIGNEE_ZIP_CODE, CONSIGNEE_COUNTRY, CARRIER, BUS_LINE, Incoterms, IncotermsLocation, ContainerHandeling, Booking_UID
,Voyage,FinalDestino,QuotedBY,Vessel
                            FROM QUOTES_DAT where REFERENCE_NO=?
                            order by effective_date desc";

            DataTable dt = new DataTable();
            using (OdbcDataAdapter adp = new OdbcDataAdapter(sql, Properties.Settings.Default.IESDBConn))
            {
                adp.SelectCommand.Parameters.Add(new OdbcParameter("@QuoteNumber", criteria));
                adp.Fill(dt);
            };

            List<BusinessObjects.Quote> lq = new List<BusinessObjects.Quote>();
            foreach (DataRow row in dt.Rows)
            {
                BusinessObjects.Quote q = populateQuoteFromDataRow(row);
                lq.Add(q);
            }
            return lq;
        }
        public static BusinessObjects.Quote getQuote(string quoteno)
        {
            string sql = string.Empty;
            if (quoteno == null || quoteno == string.Empty) 
            {
                sql = @"SELECT Top 0 ID, REFERENCE_NO, CONTROLLER, EFFECTIVE_DATE, EXPIRATION_DATE, SHIPPER, SHIPPER_NAME, SHIPPER_CONTACT, SHIPPER_PHONE, 
                            SHIPPER_FAX, COMMODITY, MOVE_TYPE, PAYMENT, P_O_NUMBER, PLR, POL, POD, SUPPLIER_STATE, SUPPLIER_ZIP_CODE, CONSIGNEE_STATE, 
                            CONSIGNEE_ZIP_CODE, CONSIGNEE_COUNTRY, CARRIER, BUS_LINE, IncotermsLocation, ContainerHandeling, Booking_UID
,Voyage,FinalDestino,QuotedBY
                            FROM QUOTES_DAT
                            order by effective_date desc";
            }
            else
            {
                sql = @"SELECT Top 1 ID, REFERENCE_NO, CONTROLLER, EFFECTIVE_DATE, EXPIRATION_DATE, SHIPPER, SHIPPER_NAME, SHIPPER_CONTACT, SHIPPER_PHONE, 
                            SHIPPER_FAX, COMMODITY, MOVE_TYPE, PAYMENT, P_O_NUMBER, PLR, POL, POD, SUPPLIER_STATE, SUPPLIER_ZIP_CODE, CONSIGNEE_STATE, 
                            CONSIGNEE_ZIP_CODE, CONSIGNEE_COUNTRY, CARRIER, BUS_LINE, Incoterms, IncotermsLocation, ContainerHandeling, Booking_UID
,Voyage,FinalDestino,QuotedBY,Vessel
                            FROM QUOTES_DAT where REFERENCE_NO=?
                            order by effective_date desc";
            }

            DataTable dt = new DataTable();
            using (OdbcDataAdapter adp = new OdbcDataAdapter(sql, Properties.Settings.Default.IESDBConn))
            {
                adp.SelectCommand.Parameters.Add(new OdbcParameter("@ID", quoteno));
                adp.Fill(dt);
            }

            BusinessObjects.Quote q = new BusinessObjects.Quote();
            if (dt.Rows.Count > 0)
            {
                q = populateQuoteFromDataRow(dt.Rows[0]);
                return q;
            }
            return null;
        }
        private static BusinessObjects.Quote populateQuoteFromDataRow(DataRow row)
        {
            BusinessObjects.Quote q = new BusinessObjects.Quote();
                q.ID = int.Parse(row["ID"].ToString());
                q.QuoteNumber = row["REFERENCE_NO"].ToString();
                q.Customer = row["SHIPPER_NAME"].ToString().Trim();
                q.CustomerCode = row["SHIPPER"].ToString().Trim();
                q.CustomerContact = row["SHIPPER_CONTACT"].ToString();
                q.CustomerPhone = row["SHIPPER_PHONE"].ToString();
                q.CustomerFax = row["SHIPPER_FAX"].ToString();
                q.CustomerReferenceNo = row["P_O_NUMBER"].ToString();
                q.PickupCity = row["PLR"].ToString().Trim();
                q.CustomerState = row["SUPPLIER_STATE"].ToString().Trim();
                q.CustomerZip = row["SUPPLIER_ZIP_CODE"].ToString();
                q.PortOfLoad = row["POL"].ToString().Trim();
                q.PortOfDispatch = row["POD"].ToString().Trim();
                q.FinalDestination = row["FinalDestino"].ToString();
                q.DestinationZip = row["CONSIGNEE_ZIP_CODE"].ToString();
                q.DestinationCountry = row["CONSIGNEE_COUNTRY"].ToString().Trim();
                q.TypeOfMove = row["MOVE_TYPE"].ToString().Trim();
                q.Division = row["PAYMENT"].ToString().Trim();
                q.Businessline = row["BUS_LINE"].ToString().Trim();
                q.INCOTerms = row["Incoterms"].ToString();
                q.INCOTermsLocation = row["IncotermsLocation"].ToString();
                DateTime effdate;
                DateTime.TryParse(row["EFFECTIVE_DATE"].ToString(), out effdate);
                q.Effectivdate = effdate;
                DateTime expdate;
                DateTime.TryParse(row["EXPIRATION_DATE"].ToString(), out expdate);
                q.ExpirationDate = expdate;
                q.QuotedBy = row["QuotedBy"].ToString();
                q.SalesRep = row["CONTROLLER"].ToString();
                q.PreferredCarrier = row["CARRIER"].ToString();
                q.Voyage = row["Voyage"].ToString();
                q.Vessel = row["Vessel"].ToString();
                q.ContainerHandling = row["ContainerHandeling"].ToString();
                q.Containers = GetContainers(q.QuoteNumber);

                return q;
        }

        public static List<BusinessObjects.Container> GetContainers(string QuoteNumber)
        {
            if (QuoteNumber==null || QuoteNumber == string.Empty) { QuoteNumber = "0"; }
            DataTable dt = new DataTable();
            string sql = string.Format( @"SELECT * FROM Containers where QuoteNumber={0}",QuoteNumber);
            OdbcDataAdapter adp = new OdbcDataAdapter(sql, Properties.Settings.Default.IESDBConn);

            adp.Fill(dt);

            List<BusinessObjects.Container> lc = new List<BusinessObjects.Container>();
            foreach (DataRow row in dt.Rows)
            {
                BusinessObjects.Container c = new BusinessObjects.Container();
                c.ID = int.Parse(row["ID"].ToString());
                c.ContainerNumber = row["ContainerNumer"].ToString();
                c.ContainerType = row["ContainerType"].ToString();
                c.Seal1 = row["Seal1"].ToString();
                c.Seal2 = row["Seal2"].ToString();
                c.Seal3 = row["Seal3"].ToString();
                c.TareWeight = row["TareWeight"].ToString();
                int i = 1; int.TryParse(row["TareWeightUnit"].ToString(), out i);
                c.TareWeightUnit =(BusinessObjects.Units.WeightUnits)i ;
                DateTime d=DateTime.MinValue;
                c.DischargeDate=  DateTime.TryParse(row["DischargeDate"].ToString(),out d)?d:d;
                c.PickupDate= DateTime.TryParse(row["PickupDate"].ToString(),out d)?d:d;
                c.ReturnDate= DateTime.TryParse(row["ReturnDate"].ToString(),out d)?d:d;
                c.CustomerPickupLastDate= DateTime.TryParse(row["CustomerPickupDate"].ToString(),out d)?d:d;
                c.CustomerReturnLastDate= DateTime.TryParse(row["CustomerReturnDate"].ToString(),out d)?d:d;
                c.CarrierPickUpLastDate= DateTime.TryParse(row["CarrierPickupDate"].ToString(),out d)?d:d;
                c.CarrierReturnLastDate = DateTime.TryParse(row["CarrierReturnDate"].ToString(), out d) ? d : d;
                c.Cargo = GetCargo(c.ID);
                lc.Add(c);
            }
            return lc;

        }
        public static List<BusinessObjects.ContainerCargo> GetCargo(int ContainerID)
        {
            DataTable dt = new DataTable();
            string sql = string.Format(@"SELECT * FROM Cargo where ContainerID={0}", ContainerID);
            OdbcDataAdapter adp = new OdbcDataAdapter(sql, Properties.Settings.Default.IESDBConn);

            adp.Fill(dt);

            List<BusinessObjects.ContainerCargo> lc = new List<BusinessObjects.ContainerCargo>();
            foreach (DataRow row in dt.Rows)
            {
                BusinessObjects.ContainerCargo c = new BusinessObjects.ContainerCargo();
                c.ID = int.Parse(row["ID"].ToString());
                c.Description = row["Description"].ToString();
                int p;
                int.TryParse(row["Pieces"].ToString(),out p);
                c.Pieces = p; p = 0;
                c.UOM = row["UOM"].ToString();
                int.TryParse(row["GrossWeight"].ToString(), out p);
                c.GrossWeight = p; p = 0;
                int.TryParse(row["GrossWeight"].ToString(), out p);
                c.NetWeight = p;
                bool h; bool.TryParse(row["Hazardous"].ToString(),out h);
                c.Hazardous = h;
                lc.Add(c);
            }
            return lc;

        }

        public static List<string> GetQuoteNumers(string criteria, int count)
    {
        List<string> qs = new List<string>();
        string sql = string.Format(@"select top {0} REFERENCE_NO from QUOTES_DAT where REFERENCE_NO like '" + criteria + "%'", count);

        DataTable dt = new DataTable();
        using (OdbcDataAdapter adp = new OdbcDataAdapter(sql, Properties.Settings.Default.IESDBConn))
        {
            adp.SelectCommand.Parameters.Add(new OdbcParameter("@QuoteNumber", criteria));
            adp.Fill(dt);
        }

        foreach (DataRow r in dt.Rows)
        {
            qs.Add(r["REFERENCE_NO"].ToString());
           
        }
        return qs;
    }
        public static DataTable GetQuoteNumers(string text, int startOffset, int numberOfItems)
        {
            DataTable dt = new DataTable();

            string whereClause = " WHERE  [REFERENCE_NO] LIKE ?";
            string sortExpression = " ORDER BY [REFERENCE_NO]";
            string commandText = "SELECT TOP " + numberOfItems + " REFERENCE_NO FROM QUOTES_DAT";
            commandText += whereClause;
            if (startOffset != 0)
            {
                commandText += " AND REFERENCE_NO NOT IN (SELECT TOP " + startOffset + " REFERENCE_NO FROM QUOTES_DAT";
                commandText += whereClause + sortExpression + ")";
            }
            commandText += sortExpression;


            using (OdbcDataAdapter adp = new OdbcDataAdapter(commandText, Properties.Settings.Default.IESDBConn))
            {
                adp.SelectCommand.Parameters.Add("@name", OdbcType.VarChar).Value = text + '%';
                adp.Fill(dt);
            }
            return dt;
        }

        public static DataTable GetCustomers()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT  top 100      rtrim(ID) ID, TYPE, NAME
                                FROM            CLIENT_DAT
                                WHERE        (TYPE = 'Manufacturer')  order by id";
            OdbcDataAdapter adp = new OdbcDataAdapter(sql, Properties.Settings.Default.IESDBConn);

            adp.Fill(dt);
            return dt;

        }
        public static DataTable GetCustomers(string text, int startOffset, int numberOfItems)
        {
            DataTable dt = new DataTable();

            string whereClause = " WHERE  [NAME] LIKE '" + text + "%'";
            string sortExpression = " ORDER BY [NAME]";
            string commandText = "SELECT TOP " + numberOfItems + " rtrim(ID) ID, TYPE, NAME FROM CLIENT_DAT";
            commandText += whereClause;
            if (startOffset != 0)
            {
                commandText += " AND ID NOT IN (SELECT TOP " + startOffset + " ID FROM CLIENT_DAT";
                commandText += whereClause + sortExpression + ")";
            }
            commandText += sortExpression;


            using (OdbcDataAdapter adp = new OdbcDataAdapter(commandText, Properties.Settings.Default.IESDBConn))
            {
                //adp.SelectCommand.Parameters.Add("@name", OdbcType.VarChar).Value = text + '%';
                adp.Fill(dt);
            }

            return dt;
        }
        public static int GetCustomersCount(string text)
        {
            int dt;

            string whereClause = " WHERE [NAME] LIKE ?";
            string commandText = "SELECT COUNT(*) FROM CLIENT_DAT";
            commandText += whereClause;

            using (OdbcConnection conn = new OdbcConnection(Properties.Settings.Default.IESDBConn))
            {
                OdbcCommand cmd = new OdbcCommand(commandText, conn);
                cmd.Parameters.Add("@name", OdbcType.VarChar).Value = text + '%';
                conn.Open();
                dt = (int)cmd.ExecuteScalar();
            }

            return dt;
        }
        public static DataTable GetCarriers()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT  top 100      rtrim(ID) ID, TYPE, NAME
                            FROM            CLIENT_DAT
                            WHERE        (TYPE = 'Carrier') order by id";
            OdbcDataAdapter adp = new OdbcDataAdapter(sql, Properties.Settings.Default.IESDBConn);

            adp.Fill(dt);
            return dt;

        }
        public static DataTable GetVessels()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT top 100 ID, NAME FROM  VESSELS_DAT";
            OdbcDataAdapter adp = new OdbcDataAdapter(sql, Properties.Settings.Default.IESDBConn);

            adp.Fill(dt);
            return dt;

        }
        public static BusinessObjects.Client GetClient(string clientid)
        {
            DataTable dt = new DataTable();
            BusinessObjects.Client c = new BusinessObjects.Client();
            string sql = @"SELECT rtrim(ID) ID, TYPE, NAME,[Contact],[Phone],[Fax],[ReferenceNo]
                                FROM  CLIENT_DAT WHERE  ID=?";
            using (OdbcDataAdapter adp = new OdbcDataAdapter(sql, Properties.Settings.Default.IESDBConn))
            {
                adp.SelectCommand.Parameters.Add(new OdbcParameter("@clientid", clientid));
                adp.Fill(dt);
            }
            if (dt.Rows.Count > 0)
            {
                c.ClientID = dt.Rows[0]["ID"].ToString();
                c.Type = dt.Rows[0]["TYPE"].ToString();
                c.Name = dt.Rows[0]["NAME"].ToString();
                c.Contact = dt.Rows[0]["Contact"].ToString();
                c.Phone = dt.Rows[0]["Phone"].ToString();
                c.Fax = dt.Rows[0]["Fax"].ToString();
                c.ReferenceNo = dt.Rows[0]["ReferenceNo"].ToString();

                return c;
            }
            return null;
        }

        public static void SaveQuote(BusinessObjects.Quote q)
        {
            string sql=string.Empty;

            if (q.ID <= 0)
            {
                sql = @"Insert into QUOTES_DAT ([SHIPPER_NAME],[SHIPPER],[SHIPPER_CONTACT],[SHIPPER_PHONE],[SHIPPER_FAX]
                                            ,[P_O_NUMBER],[PLR],[SUPPLIER_STATE],[SUPPLIER_ZIP_CODE],[POL],[POD]
                                            ,[FinalDestino],[CONSIGNEE_ZIP_CODE],[CONSIGNEE_COUNTRY],[MOVE_TYPE],[PAYMENT],[BUS_LINE],[Incoterms]
                                            ,[INCOTermsLocation],[EFFECTIVE_DATE],[EXPIRATION_DATE],[QuotedBy],[CONTROLLER],[CARRIER]
                                            ,[Voyage],[Vessel],[ContainerHandeling])
                                            values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            }
            else
            {
                sql = string.Format(@"Update QUOTES_DAT SET [SHIPPER_NAME]=?,[SHIPPER]=?,[SHIPPER_CONTACT]=?,[SHIPPER_PHONE]=?,[SHIPPER_FAX]=?
                                        ,[P_O_NUMBER]=?,[PLR]=?,[SUPPLIER_STATE]=?,[SUPPLIER_ZIP_CODE]=?,[POL]=?,[POD]=?
                                        ,[FinalDestino]=?,[CONSIGNEE_ZIP_CODE]=?,[CONSIGNEE_COUNTRY]=?,[MOVE_TYPE]=?,[PAYMENT]=?,[BUS_LINE]=?,[INCOTerms]=?
                                        ,[INCOTermsLocation]=?,[EFFECTIVE_DATE]=?,[EXPIRATION_DATE]=?,[QuotedBy]=?,[CONTROLLER]=?,[CARRIER]=?
                                        ,[Voyage]=?,[Vessel]=?,[ContainerHandeling]=?
                        Where ID={0}", q.ID);
            }

            using (OdbcConnection conn = new OdbcConnection(Properties.Settings.Default.IESDBConn))
            {
                OdbcCommand cmd = new OdbcCommand(sql,conn);
                cmd.Parameters.Add(new OdbcParameter("@Customer", q.Customer));
                cmd.Parameters.Add(new OdbcParameter("@CustomerCode", q.CustomerCode));
                cmd.Parameters.Add(new OdbcParameter("@CustomerContact", q.CustomerContact));
                cmd.Parameters.Add(new OdbcParameter("@CustomerPhone", q.CustomerPhone));
                cmd.Parameters.Add(new OdbcParameter("@CustomerFax", q.CustomerFax));
                cmd.Parameters.Add(new OdbcParameter("@CustomerReferenceNo", q.CustomerReferenceNo));
                cmd.Parameters.Add(new OdbcParameter("@PickupCity", q.PickupCity));
                cmd.Parameters.Add(new OdbcParameter("@CustomerState", q.CustomerState));
                cmd.Parameters.Add(new OdbcParameter("@CustomerZip", q.CustomerZip));
                cmd.Parameters.Add(new OdbcParameter("@PortOfLoad", q.PortOfLoad));
                cmd.Parameters.Add(new OdbcParameter("@PortOfDispatch", q.PortOfDispatch));
                cmd.Parameters.Add(new OdbcParameter("@FinalDestination", q.FinalDestination));
                cmd.Parameters.Add(new OdbcParameter("@DestinationZip", q.DestinationZip));
                cmd.Parameters.Add(new OdbcParameter("@DestinationCounty", q.DestinationCountry));
                cmd.Parameters.Add(new OdbcParameter("@TypeOfMove", q.TypeOfMove));
                cmd.Parameters.Add(new OdbcParameter("@Division", q.Division));
                cmd.Parameters.Add(new OdbcParameter("@Businessline", q.Businessline));
                cmd.Parameters.Add(new OdbcParameter("@INCOTerms", q.INCOTerms));
                cmd.Parameters.Add(new OdbcParameter("@INCOTermsLocation", q.INCOTermsLocation));
                cmd.Parameters.Add(new OdbcParameter("@Effectivdate", q.Effectivdate));
                cmd.Parameters.Add(new OdbcParameter("@ExpirationDate", q.ExpirationDate));
                cmd.Parameters.Add(new OdbcParameter("@QuotedBy", q.QuotedBy));
                cmd.Parameters.Add(new OdbcParameter("@SalesRep", q.SalesRep));
                cmd.Parameters.Add(new OdbcParameter("@PreferredCarrier", q.PreferredCarrier));
                cmd.Parameters.Add(new OdbcParameter("@Voyage", q.Voyage));
                cmd.Parameters.Add(new OdbcParameter("@Vessel", q.Vessel));
                cmd.Parameters.Add(new OdbcParameter("@ContainerHandling", q.ContainerHandling));

                conn.Open();
                OdbcTransaction trans = conn.BeginTransaction();
                cmd.Transaction = trans;
                try
                {
                cmd.ExecuteNonQuery();

                foreach (BusinessObjects.Container c in q.Containers)
                {
                    SaveContainer(cmd, c, q.QuoteNumber);
                }
                trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        
        }
        private static void SaveContainer(OdbcCommand cmd, BusinessObjects.Container c, string QuoteNumber)
        { 
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new OdbcParameter("@QuoteNumber", OdbcType.Int));
            cmd.Parameters.Add(new OdbcParameter("@ContainerNumer", OdbcType.VarChar, 50));
            cmd.Parameters.Add(new OdbcParameter("@ContainerType", OdbcType.VarChar, 50));
            cmd.Parameters.Add(new OdbcParameter("@Seal1", OdbcType.VarChar, 50));
            cmd.Parameters.Add(new OdbcParameter("@Seal2", OdbcType.VarChar, 50));
            cmd.Parameters.Add(new OdbcParameter("@Seal3", OdbcType.VarChar, 50));
            cmd.Parameters.Add(new OdbcParameter("@TareWeight", OdbcType.VarChar, 50));
            cmd.Parameters.Add(new OdbcParameter("@TareWeightUnit", OdbcType.VarChar, 50));
            cmd.Parameters.Add(new OdbcParameter("@DischargeDate",OdbcType.DateTime));
            cmd.Parameters.Add(new OdbcParameter("@PickupDate", OdbcType.DateTime));
            cmd.Parameters.Add(new OdbcParameter("@ReturnDate", OdbcType.DateTime));
            cmd.Parameters.Add(new OdbcParameter("@CustomerPickupDate", OdbcType.DateTime));
            cmd.Parameters.Add(new OdbcParameter("@CustomerReturnDate", OdbcType.DateTime));
            cmd.Parameters.Add(new OdbcParameter("@CarrierPickupDate", OdbcType.DateTime));
            cmd.Parameters.Add(new OdbcParameter("@CarrierReturnDate", OdbcType.DateTime));

            if (c.ID == 0)
            {
                cmd.CommandText = @"Insert into Containers ([QuoteNumber],[ContainerNumer],[ContainerType],[Seal1],[Seal2],[Seal3]
                                                        ,[TareWeight],[TareWeightUnit]) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            }
            else
            {
                cmd.CommandText = string.Format(@"Update Containers SET [QuoteNumber]=?,[ContainerNumer]=?,[ContainerType]=?,[Seal1]=?,[Seal2]=?,[Seal3]=?
                                                        ,[TareWeight]=?,[TareWeightUnit]=? ,[DischargeDate]=?,[PickupDate]=?,[ReturnDate]=?
                                                        ,[CustomerPickupDate]=?,[CustomerReturnDate]=?,[CarrierPickupDate]=?,[CarrierReturnDate]=?
                                                        where ID={0}", c.ID);
            }
            cmd.Parameters["@QuoteNumber"].Value = QuoteNumber;
            cmd.Parameters["@ContainerNumer"].Value = c.ContainerNumber;
            cmd.Parameters["@ContainerType"].Value = c.ContainerType;
            cmd.Parameters["@Seal1"].Value = c.Seal1;
            cmd.Parameters["@Seal2"].Value = c.Seal2;
            cmd.Parameters["@Seal3"].Value = c.Seal3;
            cmd.Parameters["@TareWeight"].Value = c.TareWeight;
            cmd.Parameters["@TareWeightUnit"].Value = c.TareWeightUnit;

            cmd.Parameters["@DischargeDate"].Value = DBNull.Value;
            cmd.Parameters["@PickupDate"].Value = DBNull.Value;
            cmd.Parameters["@ReturnDate"].Value = DBNull.Value;
            cmd.Parameters["@CustomerPickupDate"].Value = DBNull.Value;
            cmd.Parameters["@CustomerReturnDate"].Value = DBNull.Value;
            cmd.Parameters["@CarrierPickupDate"].Value = DBNull.Value;
            cmd.Parameters["@CarrierReturnDate"].Value = DBNull.Value;
            if (c.DischargeDate > DateTime.MinValue) cmd.Parameters["@DischargeDate"].Value = c.DischargeDate;
            if (c.PickupDate > DateTime.MinValue) cmd.Parameters["@PickupDate"].Value = c.PickupDate;
            if (c.ReturnDate > DateTime.MinValue) cmd.Parameters["@ReturnDate"].Value = c.ReturnDate;
            if (c.CustomerPickupLastDate > DateTime.MinValue) cmd.Parameters["@CustomerPickupDate"].Value = c.CustomerPickupLastDate;
            if (c.CustomerReturnLastDate > DateTime.MinValue) cmd.Parameters["@CustomerReturnDate"].Value = c.CustomerReturnLastDate;
            if (c.CarrierPickUpLastDate > DateTime.MinValue) cmd.Parameters["@CarrierPickupDate"].Value = c.CarrierPickUpLastDate;
            if (c.CarrierReturnLastDate > DateTime.MinValue) cmd.Parameters["@CarrierReturnDate"].Value = c.CarrierReturnLastDate;
            cmd.ExecuteNonQuery();
                           
        }
        public static void SaveContainer(BusinessObjects.Container c, string QuoteNumber)
        {
            string sql = string.Empty;

            using (OdbcConnection conn = new OdbcConnection(Properties.Settings.Default.IESDBConn))
            {
                OdbcCommand cmd = new OdbcCommand(sql, conn);
                conn.Open();
                SaveContainer(cmd, c, QuoteNumber);
            }

        }
        public static void SaveClient(BusinessObjects.Client c)
        {
            string sql = string.Empty;


            using (OdbcConnection conn = new OdbcConnection(Properties.Settings.Default.IESDBConn))
            {
                sql = string.Format(@"select 1 from Client_DAT where ID='{0}'", c.ClientID);
                OdbcCommand cmd = new OdbcCommand(sql, conn);
                conn.Open();
                if (cmd.ExecuteScalar()!=null)
                {
                    sql = string.Format(@"Update QUOTES_DAT SET [ID]=?,[Type]=?,[Name]=?,[Contact]=?,[Phone]=?,[Fax]=?,[ReferenceNo]=?
                            Where ID={0}", c.ClientID);
                }
                else
                {
                    sql = @"Insert into Client_DAT ([ID],[Type],[Name],[Contact],[Phone],[Fax],[ReferenceNo])
                                                values (?,?,?,?,?,?,?)";
                }
                cmd.CommandText = sql;
                cmd.Parameters.Add(new OdbcParameter("@ID", c.ClientID));
                cmd.Parameters.Add(new OdbcParameter("@Type", c.Type));
                cmd.Parameters.Add(new OdbcParameter("@Name", c.Name));
                cmd.Parameters.Add(new OdbcParameter("@Contact", c.Contact));
                cmd.Parameters.Add(new OdbcParameter("@Phone", c.Phone));
                cmd.Parameters.Add(new OdbcParameter("@Fax", c.Fax));
                cmd.Parameters.Add(new OdbcParameter("@ReferenceNo", c.ReferenceNo));

                OdbcTransaction trans = conn.BeginTransaction();
                cmd.Transaction = trans;
                try
                {
                    cmd.ExecuteNonQuery();

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

#endregion

#region "SQL Queries"
        public static DataTable GetPlaces()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT [POR CODE],[POR PORT_NAME],[POR COUNTRY],[POR STATE],[POR PORTORPLACE],[UN Code]
                                FROM [PortsPlaces]
                                where [POR PORTORPLACE]='place' and [por country]='US' order by [POR PORT_NAME]";
            SqlDataAdapter adp = new SqlDataAdapter(sql, Properties.Settings.Default.CompassSQL);

            adp.Fill(dt);
            return dt;

        }
        public static DataTable GetPlaces(string text, int startOffset, int numberOfItems)
        {
            DataTable dt = new DataTable();

            string whereClause = " WHERE [POR PORTORPLACE]='place' and [POR PORT_NAME] LIKE @name";
            string sortExpression = " ORDER BY [POR PORT_NAME]";
            string commandText = "SELECT TOP " + numberOfItems + " [POR CODE],[POR PORT_NAME],[POR COUNTRY],[POR STATE],[POR PORTORPLACE],[UN Code] FROM PortsPlaces";
            commandText += whereClause;
            if (startOffset != 0)
            {
                commandText += " AND [POR CODE] NOT IN (SELECT TOP " + startOffset + " [POR CODE] FROM PortsPlaces";
                commandText += whereClause + sortExpression + ")";
            }
            commandText += sortExpression;


            using (SqlDataAdapter adp = new SqlDataAdapter(commandText, Properties.Settings.Default.CompassSQL))
            {
                adp.SelectCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = text + '%';
                adp.Fill(dt);
            }

            return dt;
        }
        public static int GetPlacesCount(string text)
        {
            int dt;

            string whereClause = " WHERE [POR PORTORPLACE]='place' and [POR PORT_NAME] LIKE @name";
            string commandText = "SELECT COUNT(*) FROM PortsPlaces";
            commandText += whereClause;

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.CompassSQL))
            {
                SqlCommand cmd = new SqlCommand(commandText,conn);
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = text + '%';
                conn.Open();
                dt=(int)cmd.ExecuteScalar();
            }

            return dt;
        }
        public static DataTable GetOrigins(string text, int startOffset, int numberOfItems)
        {
            DataTable dt = new DataTable();

            string whereClause = " WHERE Origin LIKE @name or CODE LIKE @name";
            string sortExpression = " order by Origin, Code";
            string commandText = "SELECT TOP " + numberOfItems + @" CODE, Origin from (
                                    select z.Zipcode CODE,po.Name Origin from ShippingRoutes r
                                    join PlacesZipcodes z on z.PlaceID = r.origin
                                    join PortsAndPlaces po on po.Id = r.origin
                                    join PortsAndPlaces pd on pd.Id = r.Destination
                                    union
                                    select isnull(z.code,'00000') CODE,po.Name Origin from ShippingRoutes r
                                    left join PortsAndPlaces z on z.ID = r.origin
                                    join PortsAndPlaces po on po.Id = r.origin
                                    join PortsAndPlaces pd on pd.Id = r.Destination
                                    ) SRoutes ";
            commandText += whereClause;
            if (startOffset != 0)
            {
                commandText += " AND [CODE] NOT IN (SELECT TOP " + startOffset + @" CODE from (
                                    select z.Zipcode CODE,po.Name Origin from ShippingRoutes r
                                    join PlacesZipcodes z on z.PlaceID = r.origin
                                    join PortsAndPlaces po on po.Id = r.origin
                                    join PortsAndPlaces pd on pd.Id = r.Destination
                                    union
                                    select isnull(z.code,'00000') CODE,po.Name Origin from ShippingRoutes r
                                    left join PortsAndPlaces z on z.ID = r.origin
                                    join PortsAndPlaces po on po.Id = r.origin
                                    join PortsAndPlaces pd on pd.Id = r.Destination
                                    ) SRoutes";
                commandText += whereClause + sortExpression + ")";
            }
            commandText += sortExpression;


            using (SqlDataAdapter adp = new SqlDataAdapter(commandText, Properties.Settings.Default.CompassSQL))
            {
                adp.SelectCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = text + '%';
                adp.Fill(dt);
            }

            return dt;
        }
        public static int GetOriginsCount(string text)
        {
            int dt;

            string whereClause = " WHERE Origin LIKE @name or CODE LIKE @name";
            string commandText = @"SELECT COUNT(*) FROM (
                                    select z.Zipcode CODE,po.Name Origin from ShippingRoutes r
                                    join PlacesZipcodes z on z.PlaceID = r.origin
                                    join PortsAndPlaces po on po.Id = r.origin
                                    join PortsAndPlaces pd on pd.Id = r.Destination
                                    union
                                    select isnull(z.code,'00000') CODE,po.Name Origin from ShippingRoutes r
                                    left join PortsAndPlaces z on z.ID = r.origin
                                    join PortsAndPlaces po on po.Id = r.origin
                                    join PortsAndPlaces pd on pd.Id = r.Destination
                                    ) SRoutes ";
            commandText += whereClause;

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.CompassSQL))
            {
                SqlCommand cmd = new SqlCommand(commandText, conn);
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = text + '%';
                conn.Open();
                dt = (int)cmd.ExecuteScalar();
            }

            return dt;
        }
        public static DataTable GetDestinations(string text, string origin, int startOffset, int numberOfItems)
        {
            DataTable dt = new DataTable();

            string whereClause = " WHERE (Destination LIKE @name or CODE LIKE @name)";
            string orignfilterzip = string.Empty;
            string orignfilterport = string.Empty;
            if (!string.IsNullOrEmpty(origin))
            {
                orignfilterzip = " AND (z.Zipcode=@originzip)";
                orignfilterport = " AND (po.Name=@originport)";
            }
            string sortExpression = " order by Destination, Code";
            string commandText =string.Format( "SELECT TOP " + numberOfItems + @" CODE, Destination,ID from (
                                    select z.Zipcode CODE,pd.Name Destination,pd.ID from ShippingRoutes r
                                    join PlacesZipcodes z on z.PlaceID = r.Destination
                                    join PortsAndPlaces po on po.Id = r.origin {0}
                                    join PortsAndPlaces pd on pd.Id = r.Destination
                                    union
                                    select isnull(z.code,'00000') CODE,pd.Name Destination,pd.ID from ShippingRoutes r
                                    left join PortsAndPlaces z on z.ID = r.Destination
                                    join PortsAndPlaces po on po.Id = r.origin {1}
                                    join PortsAndPlaces pd on pd.Id = r.Destination
                                    ) SRoutes ", orignfilterzip,orignfilterport);
            commandText += whereClause;
            if (startOffset != 0)
            {
                commandText += string.Format(" AND [CODE] NOT IN (SELECT TOP " + startOffset + @" CODE from (
                                    select z.Zipcode CODE,pd.Name Destination,pd.ID from ShippingRoutes r
                                    join PlacesZipcodes z on z.PlaceID = r.Destination
                                    join PortsAndPlaces po on po.Id = r.origin {0}
                                    join PortsAndPlaces pd on pd.Id = r.Destination
                                    union
                                    select isnull(z.code,'00000') CODE,pd.Name Destination,pd.ID from ShippingRoutes r
                                    left join PortsAndPlaces z on z.ID = r.Destination
                                    join PortsAndPlaces po on po.Id = r.origin {1}
                                    join PortsAndPlaces pd on pd.Id = r.Destination
                                    ) SRoutes", orignfilterzip,orignfilterport);
                commandText += whereClause + sortExpression + ")";
            }
            commandText += sortExpression;


            using (SqlDataAdapter adp = new SqlDataAdapter(commandText, Properties.Settings.Default.CompassSQL))
            {
                adp.SelectCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = text + '%';
                if (!string.IsNullOrEmpty(origin))
                {
                    adp.SelectCommand.Parameters.Add("@originzip", SqlDbType.VarChar).Value = origin;
                    adp.SelectCommand.Parameters.Add("@originport", SqlDbType.VarChar).Value = origin;
                }
                adp.Fill(dt);
            }

            return dt;
        }
        public static int GetDestinationsCount(string text, string origin)
        {
            int dt;

            string whereClause = " WHERE (Destination LIKE @name or CODE LIKE @name)";
            string orignfilterzip = string.Empty;
            string orignfilterport = string.Empty;
            if ( !string.IsNullOrEmpty(origin))
            {
                orignfilterzip = " AND (z.Zipcode=@originzip)";
                orignfilterport = " AND (po.Name=@originport)";
            }
            string commandText = string.Format(@"SELECT COUNT(*) FROM (
                                    select z.Zipcode CODE,pd.Name,pd.ID Destination from ShippingRoutes r
                                    join PlacesZipcodes z on z.PlaceID = r.Destination
                                    join PortsAndPlaces po on po.Id = r.origin {0}
                                    join PortsAndPlaces pd on pd.Id = r.Destination
                                    union
                                    select isnull(z.code,'00000') CODE,pd.Name,pd.ID Destination from ShippingRoutes r
                                    left join PortsAndPlaces z on z.ID = r.Destination
                                    join PortsAndPlaces po on po.Id = r.origin {1}
                                    join PortsAndPlaces pd on pd.Id = r.Destination
                                    ) SRoutes ",orignfilterzip,orignfilterport);
            commandText += whereClause;

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.CompassSQL))
            {
                SqlCommand cmd = new SqlCommand(commandText, conn);
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = text + '%';
                if (!string.IsNullOrEmpty(origin))
                {
                    cmd.Parameters.Add("@originzip", SqlDbType.VarChar).Value = origin;
                    cmd.Parameters.Add("@originport", SqlDbType.VarChar).Value = origin;
                }
                conn.Open();
                dt = (int)cmd.ExecuteScalar();
            }

            return dt;
        }

        public static DataTable GetPorts()
        {
            string top = string.Empty;
            string andclause = string.Empty;
            //if (country != string.Empty) { andclause = "and [por country]=@crit"; }
            //if (count > 0) { top = "top " + count; }

            DataTable dt = new DataTable();
            string sql =string.Format( @"SELECT {0} [POR CODE],[POR PORT_NAME],[POR COUNTRY],[POR STATE],[POR PORTORPLACE],[UN Code]
                                FROM [PortsPlaces]
                                where [POR PORTORPLACE]='port' {1} order by [POR PORT_NAME]",top,andclause);
            SqlDataAdapter adp = new SqlDataAdapter(sql, Properties.Settings.Default.CompassSQL);
            //adp.SelectCommand.Parameters.Add(new SqlParameter("@crit",country));
            adp.Fill(dt);
            return dt;

        }
        public static DataTable GetPorts(string text, int startOffset, int numberOfItems)
        {
            DataTable dt = new DataTable();

            string whereClause = " WHERE  [POR PORTORPLACE]='port' and [POR PORT_NAME] LIKE @name";
            string sortExpression = " ORDER BY [POR PORT_NAME]";
            string commandText = "SELECT TOP " + numberOfItems + " [POR CODE],[POR PORT_NAME],[POR COUNTRY],[POR STATE],[POR PORTORPLACE],[UN Code] FROM PortsPlaces";
            commandText += whereClause;
            if (startOffset != 0)
            {
                commandText += " AND [POR CODE] NOT IN (SELECT TOP " + startOffset + " [POR CODE] FROM PortsPlaces";
                commandText += whereClause + sortExpression + ")";
            }
            commandText += sortExpression;


            using (SqlDataAdapter adp = new SqlDataAdapter(commandText, Properties.Settings.Default.CompassSQL))
            {
                adp.SelectCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = text + '%';
                adp.Fill(dt);
            }

            return dt;
        }
        public static int GetPortsCount(string text)
        {
            int dt;

            string whereClause = " WHERE [POR PORTORPLACE]='port' and [POR PORT_NAME] LIKE @name";
            string commandText = "SELECT COUNT(*) FROM PortsPlaces";
            commandText += whereClause;

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.CompassSQL))
            {
                SqlCommand cmd = new SqlCommand(commandText, conn);
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = text + '%';
                conn.Open();
                dt = (int)cmd.ExecuteScalar();
            }

            return dt;
        }

        public static DataTable GetStates()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT StateCode, StateCode FROM States";
            SqlDataAdapter adp = new SqlDataAdapter(sql, Properties.Settings.Default.CompassSQL);

            adp.Fill(dt);
            return dt;

        }
        public static DataTable GetCountries()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT [CountryCode],[CountryName] FROM [CompassForwarding].[dbo].[Countries]";
            SqlDataAdapter adp = new SqlDataAdapter(sql, Properties.Settings.Default.CompassSQL);

            adp.Fill(dt);
            return dt;

        }        
        public static DataTable GetSalesReps()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT ID, Code, NAME   FROM SalesReps where Active=1";
            SqlDataAdapter adp = new SqlDataAdapter(sql, Properties.Settings.Default.CompassSQL);

            adp.Fill(dt);
            return dt;

        }
        public static DataTable GetDivisions()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT Code, Description FROM Divisions";
            SqlDataAdapter adp = new SqlDataAdapter(sql, Properties.Settings.Default.CompassSQL);

            adp.Fill(dt);
            return dt;

        }
        public static DataTable GetBusinessLines()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT Code, Description FROM BusinessLines";
            SqlDataAdapter adp = new SqlDataAdapter(sql, Properties.Settings.Default.CompassSQL);

            adp.Fill(dt);
            return dt;

        }
        public static DataTable GetServiceTypes()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT ID, ServiceID, ServiceType FROM ServiceTypes";
            SqlDataAdapter adp = new SqlDataAdapter(sql, Properties.Settings.Default.CompassSQL);

            adp.Fill(dt);
            return dt;

        }
        public static DataTable GetServiceTypes(string text, int startOffset, int numberOfItems)
        {
            DataTable dt = new DataTable();

            string whereClause = " WHERE ServiceType LIKE @ServiceType";
            string sortExpression = " ORDER BY ServiceType";
            string commandText = "SELECT TOP " + numberOfItems + " ID, ServiceID, ServiceType FROM ServiceTypes";
            commandText += whereClause;
            if (startOffset != 0)
            {
                commandText += " AND ID NOT IN (SELECT TOP " + startOffset + " ID FROM ServiceTypes";
                commandText += whereClause + sortExpression + ")";
            }
            commandText += sortExpression;


            using (SqlDataAdapter adp = new SqlDataAdapter(commandText, Properties.Settings.Default.CompassSQL))
            {
                DataSet ds = new DataSet();
                adp.SelectCommand.Parameters.Add("@ServiceType", SqlDbType.VarChar).Value = text + '%';
                adp.Fill(dt);
            }

            return dt;
        }
        public static int GetServiceTypesCount(string text)
        {
            int dt;

            string whereClause = " WHERE ServiceType LIKE @ServiceType";
            string commandText = "SELECT COUNT(*) FROM ServiceTypes";
            commandText += whereClause;

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.CompassSQL))
            {
                SqlCommand cmd = new SqlCommand(commandText, conn);
                cmd.Parameters.Add("@ServiceType", SqlDbType.VarChar).Value = text + '%';
                conn.Open();
                dt = (int)cmd.ExecuteScalar();
            }

            return dt;
        }

        public static List<BusinessObjects.Rates.Rate> GetRates(string origin, string destination, DateTime shipdate,
                                        int? contractid, int? containertype, int? commodityType)
        {
            string sql = @"select contracts.ContractNumber,shppr.Name Shipper, orig.Name [from], dest.Name [to], cntr1.Name container, 
                            zones.Name zone, map.Zipcode,coms1.Name commodity, rates1.BaseRate,rates1.EffectiveDate,rates1.ExpirationDate
                            from dbo.ShippingRates rates1
                            join dbo.ShippingContracts contracts on contracts.ContractID=rates1.ContractID
                            join dbo.Shippers shppr on shppr.ShipperID=contracts.ShipperID
                            join dbo.ShippingRoutes rout on rout.RouteID=rates1.RouteID
                            join dbo.PortsAndPlaces orig on orig.ID=rout.Origin
                            join dbo.PortsAndPlaces dest on dest.ID=rout.Destination
                            join dbo.ContainerTypes cntr1 on cntr1.ID = rates1.ContainerTypeID
                            left join dbo.CommodityTypes coms1 on coms1.CommodityTypeID=rates1.CommodityTypeID
                            left join dbo.ShippingZipcodeZones zones on zones.ZoneID=rates1.ZoneID
                            left join dbo.ShippingZoneMap map on map.ZoneID=zones.ZoneID";
            string where = @" where (orig.Name=@origin or orig.Code=@origin or map.Zipcode=@origin) 
                                    and (dest.Name=@destination or dest.Code=@destination) 
                                    and (rates1.EffectiveDate <= @shipdt and rates1.ExpirationDate>=@shipdt)";
            if (contractid.HasValue){where+=" and (rates1.ContractID=@contratid)";}
            if (commodityType.HasValue) { where += @" and (coms1.CommodityTypeID=@commodity or coms1.CommodityTypeID is null)"; }
            if (containertype.HasValue){where+=" and (cntr1.ID=@containertype)";}

            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(sql + where + "order by commodity, container", Properties.Settings.Default.CompassSQL);
            adp.SelectCommand.Parameters.Add(new SqlParameter("@origin", origin));
            adp.SelectCommand.Parameters.Add(new SqlParameter("@destination", destination));
            adp.SelectCommand.Parameters.Add(new SqlParameter("@shipdt", shipdate));
            if (contractid.HasValue) { adp.SelectCommand.Parameters.Add(new SqlParameter("@contratid", contractid.Value)); }
            if (commodityType.HasValue) { adp.SelectCommand.Parameters.Add(new SqlParameter("@commodity", commodityType.Value)); }
            if (containertype.HasValue) { adp.SelectCommand.Parameters.Add(new SqlParameter("@containertype", containertype.Value)); }
            adp.Fill(dt);

            if (dt.Rows.Count == 0)
            {//TODO: Ask if cross contracts are allowed?
                sql= @"select distinct contracts1.ContractNumber,shppr1.Name Shipper, orig.Name [from],via.Name via, dest.Name [to], 
	                        cntr1.Name container, zones1.Name zone, map.Zipcode
							,case when coms1.Name is null then coms2.Name else coms1.Name end Commodity
                            ,rates1.BaseRate + rates2.BaseRate [BaseRate]
                            ,case when rates1.EffectiveDate>rates2.EffectiveDate then rates1.EffectiveDate else rates2.EffectiveDate end EffectiveDate
                            ,case when rates1.ExpirationDate>rates2.ExpirationDate then rates2.ExpirationDate else rates1.ExpirationDate end ExpirationDate
                            from dbo.ShippingRates rates1
                            join dbo.ShippingContracts contracts1 on contracts1.ContractID=rates1.ContractID
                            join dbo.Shippers shppr1 on shppr1.ShipperID=contracts1.ShipperID
                            join dbo.ShippingRoutes rout1 on rout1.RouteID=rates1.RouteID
                            join dbo.PortsAndPlaces orig on orig.ID=rout1.Origin
                            join dbo.ContainerTypes cntr1 on cntr1.ID = rates1.ContainerTypeID

                            join dbo.ShippingRoutes rout2 on rout2.Origin=rout1.Destination
                            join dbo.ShippingRates rates2 on rates2.RouteID=rout2.RouteID and (rates2.EffectiveDate <= @shipdt and rates2.ExpirationDate>=@shipdt) 
                            join dbo.PortsAndPlaces via on via.ID=rout2.Origin
                            join dbo.PortsAndPlaces dest on dest.ID=rout2.Destination
                            join dbo.ContainerTypes cntr2 on cntr2.ID = rates2.ContainerTypeID and cntr1.ID=cntr2.ID
                            left join dbo.CommodityTypes coms1 on coms1.CommodityTypeID=rates1.CommodityTypeID
                            left join dbo.CommodityTypes coms2 on coms2.CommodityTypeID=rates2.CommodityTypeID
                            left join dbo.ShippingZipcodeZones zones1 on zones1.ZoneID=rates1.ZoneID
                            left join dbo.ShippingZoneMap map on map.ZoneID=zones1.ZoneID";
                if (commodityType.HasValue) { where += " and (coms2.CommodityTypeID=@commodity or coms2.CommodityTypeID is null)";}
                if (containertype.HasValue) {where += " and (cntr2.ID=@containertype)";}

                adp.SelectCommand.CommandText = sql + where +"order by commodity, container";
                adp.Fill(dt);
            }

            List<BusinessObjects.Rates.Rate> rts = new List<BusinessObjects.Rates.Rate>();
            foreach (DataRow row in dt.Rows)
            {
                BusinessObjects.Rates.Rate r = new BusinessObjects.Rates.Rate();
                r.Commodity = row["commodity"].ToString();
                r.ContainerType = row["container"].ToString();
                r.ContractNumber = row["ContractNumber"].ToString();
                r.Destination = row["to"].ToString();
                r.DollarValue = double.Parse( row["BaseRate"].ToString());
                r.Origin = row["from"].ToString();
                r.Shipper = row["Shipper"].ToString();
                rts.Add(r);
            }

            return rts;
        }

        public static void SaveSalesRep(BusinessObjects.SalesRep h)
        {
            string sql=string.Empty;
            if (h.ID > 0)
            {
                sql = string.Format(@"update SalesReps set [Code]=@Code,[Name]=@Name,[Active]=@Active where ID={0}", h.ID);
            }
            else
            {
                sql = string.Format(@"Insert into SalesReps (@Code,@Name,@Active) ", h.Code, h.Name, h.Active);
            }
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.CompassSQL))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.Parameters.Add(new SqlParameter("@Code", h.Code));
                cmd.Parameters.Add(new SqlParameter("@Name", h.Name));
                cmd.Parameters.Add(new SqlParameter("@Active", h.Active));
                cmd.ExecuteNonQuery();
                
            }
        }




        public static System.Web.UI.WebControls.SqlDataSource GetSQLDataSource(string sql)
        {
            return new System.Web.UI.WebControls.SqlDataSource(Properties.Settings.Default.CompassSQL, sql);        
        }
        public static System.Web.UI.WebControls.SqlDataSource GetSQLDataSource2(string sql)
        {
            return new System.Web.UI.WebControls.SqlDataSource("System.Data.Odbc",Properties.Settings.Default.IESDBConn, sql);
        }
#endregion
    }
}