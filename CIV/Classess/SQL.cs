using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace CIV.Classess
{
	/// <summary>
	/// Summary description for SQL.
	/// </summary>
	public class SQL
	{
		public static DataSet ImportSubscribers(string languageId,string category)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("	select	");
			sb.Append("		*	");
			sb.Append("	from	");
			sb.Append("		subscribers_tmp	");
            sb.Append(" where   ");
            sb.AppendFormat("   language_id = {0}", languageId);
            sb.AppendFormat("   and category = '{0}'", category);
            
			
			DataSet ds = new DataSet();

			SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(),GlobalFn.GetConnString);
			oDA.Fill(ds, "subs");
			return ds;
		}
        /*public static void DeleteSubscribers(string langId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" delete from subscribers where language_id = {0}", langId);

            SqlConnection conn = new SqlConnection(GlobalFn.GetConnString);
            try
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }

        }*/
		public static DataSet ImportReceipts()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("	select	");
			sb.Append("		subscno,bill_no,bill_amt,convert(char(12),bill_date,103) bill_date	");
			sb.Append("	from	");
			sb.Append("		receipts_tmp	");
			
			DataSet ds = new DataSet();

			SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(),GlobalFn.GetConnString);
			oDA.Fill(ds, "receipts");
			return ds;
		}
		public static void ImportRecepts(string subCode,string languageId,string billAmt,string billNum,string billDate)
		{
			StringBuilder sb = new StringBuilder();
            sb.Append("	insert into	receipts(sub_code,language_id,amount,bill_num,payment_date,created_on)  ");
			sb.AppendFormat(" values ('{0}'	,",subCode);
			sb.AppendFormat(" {0}	,",languageId);
			sb.AppendFormat("  {0}  ,",billAmt);
			sb.AppendFormat("{0},",billNum);
            sb.AppendFormat(" convert(datetime,'{0}',103),",billDate);
			sb.Append("	getdate())	");
            //
			SqlConnection conn = new SqlConnection(GlobalFn.GetConnString);
			try
			{
				
				conn.Open();
				SqlCommand cmd = new SqlCommand(sb.ToString(),conn);
			
				cmd.ExecuteNonQuery();
			}
			finally
			{
				conn.Close();
			}
		}

		  public static DataSet SubscribersGetStates(string countryId)
      {
	      StringBuilder sb = new StringBuilder();
	      sb.Append("	select	");
	      sb.Append("		state_ID,name	");
	      sb.Append("	from	");
	      sb.Append("		states	");
        sb.Append("   where   ");
        sb.AppendFormat("       country_id = {0}", countryId);
  	    sb.Append(" order by  ");
  	    sb.Append("   name asc  ");
	      DataSet ds = new DataSet();

	      SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(),GlobalFn.GetConnString);
	      oDA.Fill(ds, "states");
	      return ds;
      }

        public static DataSet SubscribersGetCountries()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("	select	");
            sb.Append("		Country_ID,Country_name	");
            sb.Append("	from	");
            sb.Append("		Countries	");
            sb.Append(" order by    ");
            sb.Append("     country_name    ");

            DataSet ds = new DataSet();

            SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
            oDA.Fill(ds, "Countries");
            return ds;
        }


		public static DataSet SubscribersGetLanguages()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("	select	");
			sb.Append("		language_id,mag_name	");
			sb.Append("	from	");
			sb.Append("		languages	");
			
			DataSet ds = new DataSet();

			SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(),GlobalFn.GetConnString);
			oDA.Fill(ds, "languages");
			return ds;
		}
//		public static object SubscribersGetLanguage(int mag_id)
//		{
//			StringBuilder sb = new StringBuilder();
//			sb.Append("	select	");
//			sb.Append("		mag_name	");
//			sb.Append("	from	");
//			sb.Append("		languages	");
//			sb.AppendFormat(" where language_id = {0} ", mag_id);
//			
//			SqlConnection conn = new SqlConnection(GlobalFn.GetConnString);
//			try
//			{
//				
//				conn.Open();
//				SqlCommand cmd = new SqlCommand(sb.ToString(),conn);
//			
//				return cmd.ExecuteScalar();
//			}
//			finally
//			{
//				conn.Close();
//			};
//		}

//		public static object GetSubscriberStatus(string subcode)
//		{
//			StringBuilder sb = new StringBuilder();
//			sb.Append("  select "); 
//			sb.Append("		status = case when status ='A' then 'Active' else 'Stopped' end");
//			sb.Append("  from subscribers  ");
//			sb.AppendFormat(" where sub_code = '{0}'	",subcode);
//			
//			SqlConnection conn = new SqlConnection(GlobalFn.GetConnString);
//			try
//			{
//				
//				conn.Open();
//				SqlCommand cmd = new SqlCommand(sb.ToString(),conn);
//			
//				return cmd.ExecuteScalar();
//			}
//			finally
//			{
//				conn.Close();
//			}
//		}


    /*ALTER PROC [dbo].[P_Add_Subscribers](@i_sub_code varchar(10),@i_title varchar(10),@i_last_name varchar(250),@i_first_name varchar(250),@i_address_line1 varchar(500),@i_address_line2 varchar(500),@i_address_line3 varchar(500),@i_city varchar(250),@i_district varchar(100),@i_state_id int,@i_pin_code int,@i_country_id int,@i_status char(1),@i_remarks varchar(500),@i_start_date datetime,@i_bill_date datetime,@i_amount decimal(9,2),@i_language_id int,@i_num_copies int,@i_bill_num int,@i_category varchar(1),@i_discount decimal(9,2),@i_machine_name varchar(250),@i_make_receipt char(1),@o_subscriber_id int output)  

     */
		public static int AddNewSubscriber(string subCode,string title,string lastName,string firstName,
			string addressLine1,string addressLine2,string addressLine3,string city,string district,string state,
			string pinCode,string countryId,string status,string remarks,DateTime startDate,DateTime billDate,string amountPaid,string languageId,
			string numCopies,string billNum,string category,double discount,string makeReceipt)
		{
			StringBuilder oSb = new StringBuilder();
			SqlConnection myConn = new SqlConnection(GlobalFn.GetConnString);
			try
			{
				myConn.Open();
			
				SqlCommand oComm = new SqlCommand();
				oComm.CommandType = CommandType.StoredProcedure;
				oComm.CommandText = "P_Add_Subscribers";
				oComm.Connection = myConn;
										
				oComm.Parameters.Add("@i_sub_code", SqlDbType.VarChar,10);
				if (subCode.Length > 0)
					oComm.Parameters["@i_sub_code"].Value =  subCode;
				else
					oComm.Parameters["@i_sub_code"].Value =  DBNull.Value;
				oComm.Parameters["@i_sub_code"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_title", SqlDbType.VarChar,10);
				if (title.Length > 0)
					oComm.Parameters["@i_title"].Value =  title;
				else
					oComm.Parameters["@i_title"].Value =  DBNull.Value;
				oComm.Parameters["@i_title"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_last_name", SqlDbType.VarChar,250);
				oComm.Parameters["@i_last_name"].Value =  lastName;
				oComm.Parameters["@i_last_name"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_first_name", SqlDbType.VarChar,250);
				oComm.Parameters["@i_first_name"].Value =  firstName;
				oComm.Parameters["@i_first_name"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_address_line1", SqlDbType.VarChar,500);
				oComm.Parameters["@i_address_line1"].Value = addressLine1;
				oComm.Parameters["@i_address_line1"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_address_line2", SqlDbType.VarChar,500);
				if(addressLine2.Length > 0)
					oComm.Parameters["@i_address_line2"].Value =  addressLine2;
				else
					oComm.Parameters["@i_address_line2"].Value = DBNull.Value;
				oComm.Parameters["@i_address_line2"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_address_line3", SqlDbType.VarChar,500);
				if(addressLine3.Length > 0)
					oComm.Parameters["@i_address_line3"].Value =  addressLine3;
				else
					oComm.Parameters["@i_address_line3"].Value = DBNull.Value;
				oComm.Parameters["@i_address_line3"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_city", SqlDbType.VarChar,100);
				oComm.Parameters["@i_city"].Value =  city;
				oComm.Parameters["@i_city"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_district", SqlDbType.VarChar,100);
				if(district.Length > 0)
					oComm.Parameters["@i_district"].Value =  district;
				else
					oComm.Parameters["@i_district"].Value = DBNull.Value;
				oComm.Parameters["@i_district"].Direction = ParameterDirection.Input;
				
				oComm.Parameters.Add("@i_state_id", SqlDbType.VarChar,100);
				oComm.Parameters["@i_state_id"].Value =  state;
				oComm.Parameters["@i_state_id"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_pin_code", SqlDbType.Int);
				if(pinCode.Length > 0)
          oComm.Parameters["@i_pin_code"].Value =  pinCode;
				else
					oComm.Parameters["@i_pin_code"].Value = DBNull.Value;
				oComm.Parameters["@i_pin_code"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_country_id", SqlDbType.Int);
        oComm.Parameters["@i_country_id"].Value = countryId;
        oComm.Parameters["@i_country_id"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_status", SqlDbType.Char,1);
				oComm.Parameters["@i_status"].Value =  status;
				oComm.Parameters["@i_status"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_remarks", SqlDbType.VarChar,500);
				if(remarks.Length>0)
					oComm.Parameters["@i_remarks"].Value =  remarks;
				else
					oComm.Parameters["@i_remarks"].Value = DBNull.Value;
                oComm.Parameters["@i_remarks"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_start_date", SqlDbType.DateTime);
				oComm.Parameters["@i_start_date"].Value =  startDate;
				oComm.Parameters["@i_start_date"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_bill_date", SqlDbType.DateTime);
				oComm.Parameters["@i_bill_date"].Value =  billDate;
				oComm.Parameters["@i_bill_date"].Direction = ParameterDirection.Input;
				
				oComm.Parameters.Add("@i_amount", SqlDbType.Decimal);
				oComm.Parameters["@i_amount"].Value =  amountPaid;
				oComm.Parameters["@i_amount"].Direction = ParameterDirection.Input;
				
				oComm.Parameters.Add("@i_language_id", SqlDbType.Int);
				oComm.Parameters["@i_language_id"].Value =  languageId;
				oComm.Parameters["@i_language_id"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_num_copies", SqlDbType.Int);
				oComm.Parameters["@i_num_copies"].Value =  numCopies;
				oComm.Parameters["@i_num_copies"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_bill_num", SqlDbType.Int);
				if(billNum.Length>0)
                    oComm.Parameters["@i_bill_num"].Value =  billNum;
				else
					oComm.Parameters["@i_bill_num"].Value = DBNull.Value;
				oComm.Parameters["@i_bill_num"].Direction = ParameterDirection.Input;

                oComm.Parameters.Add("@i_category", SqlDbType.VarChar, 1);
                oComm.Parameters["@i_category"].Value = category;
                oComm.Parameters["@i_category"].Direction = ParameterDirection.Input;

                oComm.Parameters.Add("@i_discount", SqlDbType.Decimal);
                oComm.Parameters["@i_discount"].Value = discount;
                oComm.Parameters["@i_discount"].Direction = ParameterDirection.Input;

                oComm.Parameters.Add("@i_machine_name", SqlDbType.VarChar,250);
                oComm.Parameters["@i_machine_name"].Value = GlobalFn.GetMachineName();
                oComm.Parameters["@i_machine_name"].Direction = ParameterDirection.Input;

                oComm.Parameters.Add("@i_make_receipt", SqlDbType.Char, 1);
                oComm.Parameters["@i_make_receipt"].Value = makeReceipt;
                oComm.Parameters["@i_make_receipt"].Direction = ParameterDirection.Input;


				oComm.Parameters.Add("@o_subscriber_id", SqlDbType.Int);
				oComm.Parameters["@o_subscriber_id"].Direction = ParameterDirection.Output;
					
				int cmdResults = oComm.ExecuteNonQuery();
				if (oComm.Parameters["@o_subscriber_id"].Value != DBNull.Value)
					return Convert.ToInt32(oComm.Parameters["@o_subscriber_id"].Value);
				else
					return -99;
			}
			finally
			{
				myConn.Close();
			}
		}
		public static DataSet NewSubsciberGetSubCode(string FirstName,string langId,bool isBulk)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("	select	");
            if (isBulk)
                sb.Append("    max(right(sub_code,(len(sub_code)-2))) max_code	");
            else
        		sb.Append("    max(right(sub_code,(len(sub_code)-1))) max_code	");
			sb.Append("	from	");
			sb.Append("		subscribers	");
			sb.Append("		where	");
			sb.AppendFormat("		left(sub_code,1) = upper(left('{0}',1))  	",GlobalFn.FixQuotes(FirstName));
            sb.AppendFormat("   and language_id = {0}", langId);
            if (isBulk)
                sb.Append("         and category = 'B'  ");
            else
                sb.Append("         and category != 'B' ");

						
			DataSet ds = new DataSet();

			SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(),GlobalFn.GetConnString);
			oDA.Fill(ds, "subCode");
			return ds;
		}


		public static DataSet FindSubscribers(string subCode,string firstName,string City,string pinCode,string languageId,string address1,string status)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("		select	");
			sb.Append("			subscriber_id,	");
			sb.Append("			sub_code,	");
			sb.Append("			last_name,	");
			sb.Append("			first_name,		");
			sb.Append("			address_line1,	");
			sb.Append("			address_line2,	");
			sb.Append("			address_line3,	");
			sb.Append("			city,	");
            sb.Append("		    status = case when subscribers.status ='A' then 'Active' else 'Stopped' end,	");
			sb.Append("			district,	");
			sb.Append("			states.name state_name,	");
			sb.Append("			pin_code	");
			sb.Append("		from	");
			sb.Append("			subscribers	left outer");
			sb.Append("			join	states 	");
			sb.Append("			on	subscribers.state_ID = states.state_ID	");
			sb.Append("	where	");
			sb.AppendFormat("	language_id = {0}", languageId);
			if (subCode.Length > 0)
				sb.AppendFormat("	 and sub_code = '{0}'",subCode.Trim());
			else
			{
                if ((firstName.Length > 0) && (City.Length > 0))  
                {
                    sb.AppendFormat(" and (upper(first_name) like '%{0}%'", GlobalFn.FixQuotes(firstName).ToUpper().Trim());
                    sb.AppendFormat(" and upper(city) LIKE '%{0}%')", GlobalFn.FixQuotes(City).ToUpper().Trim());

                }
                else
                {
                    if (firstName.Length > 0)
                        sb.AppendFormat(" and upper(first_name) like '%{0}%'", GlobalFn.FixQuotes(firstName).ToUpper().Trim());
                    if (City.Length > 0)
                        sb.AppendFormat("	and upper(city) LIKE '%{0}%'", GlobalFn.FixQuotes(City).ToUpper().Trim());
                    if (pinCode.Length > 0)
                        sb.AppendFormat(" and pin_code = {0} ", pinCode.Trim());
                    if (address1.Length > 0)
                        sb.AppendFormat(" and upper(address_line1) like '%{0}%'", GlobalFn.FixQuotes(address1.Trim().ToUpper()));
                }
                if (!status.Equals("-1"))
                    sb.AppendFormat("   and subscribers.status = '{0}'", status);

			}
						
			DataSet ds = new DataSet();

			SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(),GlobalFn.GetConnString);
			oDA.Fill(ds, "findsub");
			return ds;
		}
      public static DataSet FindSubscribers(string languageId, string billNum,string status)
      {
          StringBuilder sb = new StringBuilder();
          sb.Append("		select	");
          sb.Append("			subscriber_id,	");
          sb.Append("			subscribers.sub_code,	");
          sb.Append("			last_name,	");
          sb.Append("			first_name,		");
          sb.Append("			address_line1,	");
          sb.Append("			address_line2,	");
          sb.Append("			address_line3,	");
          sb.Append("			city,	");
          sb.Append("		    status = case when subscribers.status ='A' then 'Active' else 'Stopped' end,	");
          sb.Append("			district,	");
          sb.Append("			states.name state_name,	");
          sb.Append("			pin_code	");
          sb.Append("		from	");
          sb.Append("			subscribers	left outer");
          sb.Append("			join	states 	");
          sb.Append("			on	subscribers.state_ID = states.state_ID	");
          sb.Append("         join receipts   ");
          sb.Append("         on subscribers.sub_code = receipts.sub_code ");
          
          sb.Append("	where	");
          sb.AppendFormat("	subscribers.language_id = {0}", languageId);
          if (!status.Equals("-1"))
              sb.AppendFormat("   and subscribers.status = '{0}'", status);
          if (billNum.Length > 0)
          {
              sb.AppendFormat("	 and ( bill_num = {0}", billNum);
              sb.AppendFormat("       or ( receipt_id = {0} and bill_num is null))", billNum);
          }

        
          DataSet ds = new DataSet();

          SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
          oDA.Fill(ds, "findsub");
          return ds;
      }
        public static DataSet NewSubscriberDupeRec(string subCode, string languageId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" select * from subscribers where sub_code='{0}'", subCode);
            sb.AppendFormat("   and language_id = {0}", languageId);

            DataSet ds = new DataSet();

            SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
            oDA.Fill(ds, "dupeRecs");
            return ds;
        }
    public static string NewSubscriptionReceiptId(string subCode, string languageId)
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(" select max(receipt_id) from receipts    ");
      sb.AppendFormat(" where  sub_code = '{0}'",subCode);
      sb.AppendFormat(" and language_id = {0}", languageId);

      SqlConnection conn = new SqlConnection(GlobalFn.GetConnString);
      try
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand(sb.ToString(), conn);

        return cmd.ExecuteScalar().ToString();
      }
      finally
      {
        conn.Close();
      }
    }
    public static DataSet MagazineCost()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("select year_part,cost from magazine_cost order by year_part  ");

      DataSet ds = new DataSet();

      SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
      oDA.Fill(ds, "magCost");
      return ds;
    }
    public static void AddNewCountry(string countryName, string continent)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("	insert into	countries(country_name,continent,created_on)");
      sb.AppendFormat("values ('{0}','{1}',getdate())", GlobalFn.FixQuotes(countryName), GlobalFn.FixQuotes(continent));
      SqlConnection oConn = new SqlConnection(GlobalFn.GetConnString);
      try
      {
        oConn.Open();
        SqlCommand oComm = new SqlCommand(sb.ToString(), oConn);
        oComm.ExecuteNonQuery();
      }
      finally
      {
        oConn.Close();
      }
    }
    public static DataSet GetCountries()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("select country_id,country_name,continent from countries order by country_name  ");

      DataSet ds = new DataSet();

      SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
      oDA.Fill(ds, "countries");
      return ds;
    }
    public static void AddNewState(string stateName, string abbr, string countryId)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("	insert into	states(name,abbr,country_id)");
      sb.AppendFormat("values ('{0}','{1}',{2})", GlobalFn.FixQuotes(stateName), GlobalFn.FixQuotes(abbr), countryId);
      SqlConnection oConn = new SqlConnection(GlobalFn.GetConnString);
      try
      {
        oConn.Open();
        SqlCommand oComm = new SqlCommand(sb.ToString(), oConn);
        oComm.ExecuteNonQuery();
      }
      finally
      {
        oConn.Close();
      }
    }

		public static DataSet GetSubscriberInfo(string subscriberId)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("	select	");
			sb.Append("		s.subscriber_id,	");
			sb.Append("		l.mag_name, 	");
      sb.Append("		l.Description, 	");
			sb.Append("		s.language_id,	");
			sb.Append("		s.sub_code, 	");
			sb.Append("		s.title,	");
			sb.Append("		s.last_name,	");
			sb.Append("		s.first_name,	");
			sb.Append("		s.address_line1,	");
			sb.Append("		s.address_line2,	");
			sb.Append("		s.address_line3,	");
			sb.Append("		s.city,	");
			sb.Append("		s.district,	");
			sb.Append("		s.pin_code,	");
			sb.Append("		status = case when s.status ='A' then 'Active' else 'Stopped' end,	");
			sb.Append("		s.remarks,	");
			sb.Append("		s.state_id,	");
      sb.Append("		s.country_id,	");
      sb.Append("		st.name as stateName,");
      sb.Append("		st.abbr as stateAbbr,");
			sb.Append("		s.start_date,	");
			sb.Append("		s.amount_paid,	");
			sb.Append("		s.num_copies,	");
      sb.Append("		s.category,	");
      sb.Append("		category_name = case when s.category ='B' then 'Bulk' when s.category = 'F' then 'Free' when s.category = 'G' then 'General' when s.category = 'S' then 'Student' end,	");
      sb.Append("		s.discount	");
			sb.Append("	from 	");
			sb.Append("		subscribers	s left outer join states st	");	
			sb.Append("			on	s.state_id = st.state_id	");
			sb.Append("		join languages l	");
			sb.Append("			on s.language_id = l.language_id	");
			sb.Append("	where	");
			sb.AppendFormat("		s.subscriber_id ='{0}'  	",subscriberId);
						
			DataSet ds = new DataSet();

			SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(),GlobalFn.GetConnString);
			oDA.Fill(ds, "subInfo");
			return ds;
		}

    public static DataSet GetReceiptInfo(String subCode,DateTime receiptDate,int langID)
    {
      DataSet ds = new DataSet();
      StringBuilder sb = new StringBuilder();

      //insert into	receipts(sub_code,language_id,amount,payment_date,bill_num,created_on)
      sb.Append("select receipt_id as BillNo, amount as Amount, payment_date as Date  from receipts ");
      sb.AppendFormat(" where sub_code = '{0}' and language_id = '{1}' and Convert(char(10),payment_date,112) = '{2}' order by payment_date desc", subCode, langID, receiptDate.ToString("yyyyMMdd"));

      SqlDataAdapter da = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
      da.Fill(ds, "Receipt");

      return ds;
    }

    
    public static DataSet GetPaymentHistory(string subCode, string langID)
    {
        DataSet ds = new DataSet();
        StringBuilder sb = new StringBuilder();
        
        sb.Append("select     ");
        sb.Append("     receipt_id, ");
        sb.Append("     payment_date, ");
        sb.Append("     bill_num, "); 
        sb.Append("     amount,    ");
        sb.Append("     created_on, "); 
        sb.Append("     last_mod_on   from receipts ");
        sb.AppendFormat(" where sub_code = '{0}' and language_id = '{1}'", subCode, langID);

        SqlDataAdapter da = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
        da.Fill(ds, "payment History");
        
        return ds;
    }
    public static DataSet GetPrintList(string languageId,string sqlScript, bool isPinCode)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("	select	");
        sb.Append("     s.subscriber_id, ");
        sb.Append("		s.language_id,	");
        sb.Append("		s.sub_code, 	");
        sb.Append("		s.title,	");
        sb.Append("		s.last_name,	");
        sb.Append("		s.first_name,	");
        sb.Append("		s.address_line1,	");
        sb.Append("		s.address_line2,	");
        sb.Append("		s.address_line3,	");
        sb.Append("		s.city,	");
        sb.Append("		s.district,	");
        sb.Append("		s.pin_code,	");
        sb.Append("		l.mag_name,	");
        sb.Append("		st.abbr  as stateAbbr,	"); 
        sb.Append("     st.name  as state,  ");
        sb.Append("		s.start_date,	");
        sb.Append("		s.amount_paid,	");
        sb.Append("		s.discount,	");
        sb.Append("		s.num_copies,	");
        sb.Append("		s.category	");
        sb.Append("	from	");
        sb.Append("		subscribers	s left outer join states st	");
        sb.Append("			on	s.state_id = st.state_id	");
        sb.Append("		join languages l	");
        sb.Append("			on s.language_id = l.language_id	");
        sb.Append("		where status = 'A'	");
        sb.Append("		and	");
        sb.AppendFormat("		s.language_id ={0}  	", languageId);
        sb.Append("     and subscriber_id not in (select distinct subscriber_id  ");
        sb.Append("   from  subscriber_print ");
        sb.Append("   where status = 1  ");
        sb.AppendFormat(" and datePart(month,print_date) = datePart(month,getdate())");
        sb.AppendFormat(" and datePart(yyyy,print_date) = datePart(yyyy,getdate()) )");    
        sb.AppendFormat("{0}", sqlScript);

        DataSet ds = new DataSet();

        SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
        oDA.Fill(ds, "PrintList");
        return ds;
    }

    public static DataSet GetOverseasPrintList(string languageId, string sqlScript)
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("	  select	");
      sb.Append("   s.subscriber_id, ");
      sb.Append("		s.language_id,	");
      sb.Append("		s.sub_code, 	");
      sb.Append("		s.title,	");
      sb.Append("		s.last_name,	");
      sb.Append("		s.first_name,	");
      sb.Append("		s.address_line1,	");
      sb.Append("		s.address_line2,	");
      sb.Append("		s.address_line3,	");
      sb.Append("		s.city,	");
      sb.Append("		s.district,	");				  
      sb.Append("		s.pin_code,	");
      sb.Append("		l.mag_name,	");
      sb.Append("   st.name  as state,  ");
      sb.Append("		st.abbr  as stateAbbr,	");
      sb.Append("		ct.country_name as country,	"); 
      sb.Append("		s.start_date,	");
      sb.Append("		s.amount_paid,	");
      sb.Append("		s.discount,	");
      sb.Append("		s.num_copies,	");
      sb.Append("		s.country_id,	");
      sb.Append("		s.category	");
      sb.Append("	  from	");
      sb.Append("		subscribers	s left outer join states st	");
      sb.Append("		on	s.state_id = st.state_id and s.country_id = st.country_id	");
      sb.Append("		join countries ct on s.country_id = ct.country_id	");
      sb.Append("		join languages l	");			 
      sb.Append("		on s.language_id = l.language_id	");
      sb.Append("		where status = 'A'	");
      sb.Append("		and	");
      sb.AppendFormat("		s.language_id ={0}  	", languageId);
      sb.Append("   and subscriber_id not in (select distinct subscriber_id ");
      sb.Append("   from  subscriber_print ");
      sb.Append("   where status = 1  ");
      sb.AppendFormat("   and datePart(month,print_date) = {0}  ", DateTime.Today.Month);
      sb.AppendFormat("   and datePart(yyyy,print_date) = {0})  ", DateTime.Today.Year);
      sb.AppendFormat("   {0}  order by s.country_id asc ", sqlScript);

      DataSet ds = new DataSet();

      SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
      oDA.Fill(ds, "PrintList");
      return ds;
    }
    
        public static DataSet DuesPrintList(string languageId, string sqlScript, bool isPinCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("	select	");
            sb.Append("     s.subscriber_id, ");
            sb.Append("		s.language_id,	");
            sb.Append("		s.sub_code, 	");
            sb.Append("		s.title,	");
            sb.Append("		s.last_name,	");
            sb.Append("		s.first_name,	");
            sb.Append("		s.address_line1,	");
            sb.Append("		s.address_line2,	");
            sb.Append("		s.address_line3,	");
            sb.Append("		s.city,	");
            sb.Append("		s.district,	");
            sb.Append("		s.pin_code,	");
            sb.Append("		l.mag_name,	");
            if (isPinCode)
                sb.Append("		st.abbr  as state,	");
            else
                sb.Append("     st.name  as state,  ");
            sb.Append("		s.start_date,	");
            sb.Append("		s.amount_paid,	");
            sb.Append("		s.discount,	");
            sb.Append("		s.num_copies,	");
            sb.Append("		s.category	");
            sb.Append("	from	");
            sb.Append("		subscribers	s left outer join states st	");
            sb.Append("			on	s.state_id = st.state_id	");
            sb.Append("		join languages l	");
            sb.Append("			on s.language_id = l.language_id	");
            sb.Append("		where status = 'A'	");
            sb.Append("		and	");
            sb.AppendFormat("		s.language_id ={0}  	", languageId);
            sb.Append("     and subscriber_id not in (select distinct subscriber_id  ");
            sb.Append("                               from  dues_print    ");
            sb.Append("                               where status = 1  ");
            sb.Append(" and datePart(month,print_date) = datePart(month,getdate()) ");
            sb.Append(" and datePart(yyyy,print_date) = datePart(yyyy,getdate()) ");
            sb.AppendFormat("{0}", sqlScript);
           
            DataSet ds = new DataSet();

            SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
            oDA.Fill(ds, "PrintList");
            return ds;
        }
        public static void PrintStatusUpdateStaus(string subscriberId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" update  ");
            sb.Append("     subscriber_print    ");
            sb.Append(" set ");
            sb.Append("     status = 1  ");
            sb.Append(" where   ");
            sb.Append("         status = 0  ");
            sb.AppendFormat("     and subscriber_id = {0}", subscriberId);
            sb.AppendFormat("     and upper(machine_name) = '{0}'", GlobalFn.GetMachineName());

            SqlConnection conn = new SqlConnection(GlobalFn.GetConnString);
            try
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void DuesPrintUpdateStaus(string subscriberId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" update  ");
            sb.Append("     dues_print    ");
            sb.Append(" set ");
            sb.Append("     status = 1  ");
            sb.Append(" where   ");
            sb.Append("         status = 0  ");
            sb.AppendFormat("     and subscriber_id = {0}", subscriberId);
            sb.AppendFormat("     and upper(machine_name) = '{0}'", GlobalFn.GetMachineName());

            SqlConnection conn = new SqlConnection(GlobalFn.GetConnString);
            try
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void PrintStatusDeleteRec()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" delete from  ");
            sb.Append("     subscriber_print    ");
            sb.Append(" where   ");
            sb.AppendFormat(" status = 0  and upper(machine_name) = '{0}'", GlobalFn.GetMachineName());
           
            SqlConnection conn = new SqlConnection(GlobalFn.GetConnString);
            try
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void DuesPrintDeleteRec()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" delete from  ");
            sb.Append("     dues_print    ");
            sb.Append(" where   ");
            sb.AppendFormat(" status = 0  and upper(machine_name) = '{0}'", GlobalFn.GetMachineName());

            SqlConnection conn = new SqlConnection(GlobalFn.GetConnString);
            try
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void AddressLabelsInsertSubPrint(String subID)
        {
            SqlConnection conn = new SqlConnection(GlobalFn.GetConnString);
            String sqlScript = "insert into Subscriber_Print (subscriber_id,machine_name) values(" + subID + ",'"+ GlobalFn.GetMachineName() +"')";
            try
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlScript, conn);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void DuesListInsertDuesPrint(String subID)
        {
            SqlConnection conn = new SqlConnection(GlobalFn.GetConnString);
            String sqlScript = "insert into dues_Print (subscriber_id,machine_name) values(" + subID + ",'" + GlobalFn.GetMachineName() + "')";
            try
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlScript, conn);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

//		public static DataSet GetReceiptInfo(string subscriberId,string langId)
//		{
//			StringBuilder sb = new StringBuilder();
//			sb.Append("	select	");
//			sb.Append("  sum(amount) total_amt	");
//			sb.Append("	from	");
//			sb.Append("		receipts	");
//			sb.Append("		where	");
//			sb.AppendFormat("		sub_code ='{0}'",subscriberId);
//			sb.AppendFormat("	and ");
//			sb.AppendFormat("	language_id='{0}'",langId);
//						
//			DataSet ds = new DataSet();
//
//			SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(),GlobalFn.GetConnString);
//			oDA.Fill(ds, "receiptInfo");
//			return ds;
//		}

		public static object RenewalExpiryDate(string year)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("	select	");
			sb.Append("     cost	");
			sb.Append("	from	");
			sb.Append("		magazine_cost	");
			sb.Append("	where	");
			sb.AppendFormat("	year_part = {0}",year);
						
			SqlConnection conn = new SqlConnection(GlobalFn.GetConnString);
			try
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(sb.ToString(),conn);
			
				return cmd.ExecuteScalar();
			}
			finally
			{
				conn.Close();
			}
		}
        /*CREATE PROC [P_Add_Subscribers_tmp](@i_sub_code varchar(10),@i_title varchar(10),@i_last_name varchar(250),@i_first_name varchar(250),
@i_address_line1 varchar(500),@i_address_line2 varchar(500),@i_address_line3 varchar(500),@i_city varchar(250),
@i_district varchar(100),@i_state_id int,@i_pin_code int,@i_status char(1),@i_remarks varchar(500),@i_start_date datetime,
@i_amount decimal(9,2),@i_language_id int,@i_num_copies int,@i_category varchar(1),@i_discount decimal(9,2),@o_subscriber_id int output) 
AS */

		public static int ImportAddNewSubscriber(string subCode,string title,string lastName,string firstName,
			string addressLine1,string addressLine2,string addressLine3,string city,string district,string state,
			string pinCode,string status,string remarks,DateTime startDate,decimal amountPaid,string languageId,
			string numCopies,string category,decimal discount)
		{
			StringBuilder oSb = new StringBuilder();
			SqlConnection myConn = new SqlConnection(GlobalFn.GetConnString);
			try
			{
				myConn.Open();
			
				SqlCommand oComm = new SqlCommand();
				oComm.CommandType = CommandType.StoredProcedure;
				oComm.CommandText = "P_Add_Subscribers_tmp";
				oComm.Connection = myConn;
										
				oComm.Parameters.Add("@i_sub_code", SqlDbType.VarChar,10);
				oComm.Parameters["@i_sub_code"].Value =  subCode;
				oComm.Parameters["@i_sub_code"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_title", SqlDbType.VarChar,10);
				if (title.Length > 0)
					oComm.Parameters["@i_title"].Value =  title;
				else
					oComm.Parameters["@i_title"].Value =  DBNull.Value;
				oComm.Parameters["@i_title"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_last_name", SqlDbType.VarChar,250);
				if (lastName.Length > 0)
					oComm.Parameters["@i_last_name"].Value =  lastName;
				else
					oComm.Parameters["@i_last_name"].Value =  DBNull.Value;
				oComm.Parameters["@i_last_name"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_first_name", SqlDbType.VarChar,250);
				oComm.Parameters["@i_first_name"].Value =  firstName;
				oComm.Parameters["@i_first_name"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_address_line1", SqlDbType.VarChar,500);
				oComm.Parameters["@i_address_line1"].Value = addressLine1;
				oComm.Parameters["@i_address_line1"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_address_line2", SqlDbType.VarChar,500);
				if(addressLine2.Length > 0)
					oComm.Parameters["@i_address_line2"].Value =  addressLine2;
				else
					oComm.Parameters["@i_address_line2"].Value = DBNull.Value;
				oComm.Parameters["@i_address_line2"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_address_line3", SqlDbType.VarChar,500);
				if(addressLine3.Length > 0)
					oComm.Parameters["@i_address_line3"].Value =  addressLine3;
				else
					oComm.Parameters["@i_address_line3"].Value = DBNull.Value;
				oComm.Parameters["@i_address_line3"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_city", SqlDbType.VarChar,100);
				oComm.Parameters["@i_city"].Value =  city;
				oComm.Parameters["@i_city"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_district", SqlDbType.VarChar,100);
				if(district.Length > 0)
					oComm.Parameters["@i_district"].Value =  district;
				else
					oComm.Parameters["@i_district"].Value = DBNull.Value;
				oComm.Parameters["@i_district"].Direction = ParameterDirection.Input;
				
				oComm.Parameters.Add("@i_state_id", SqlDbType.VarChar,100);
				if (state.Length > 0)
					oComm.Parameters["@i_state_id"].Value =  state;
				else
					oComm.Parameters["@i_state_id"].Value =  DBNull.Value;
				oComm.Parameters["@i_state_id"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_pin_code", SqlDbType.Int);
				oComm.Parameters["@i_pin_code"].Value =  pinCode;
				oComm.Parameters["@i_pin_code"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_status", SqlDbType.Char,1);
				if (status.Length > 0)
                    oComm.Parameters["@i_status"].Value =  status;
				else
					oComm.Parameters["@i_status"].Value =  DBNull.Value;
				oComm.Parameters["@i_status"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_remarks", SqlDbType.VarChar,500);
				if(remarks.Length>0)
					oComm.Parameters["@i_remarks"].Value =  remarks;
				else
					oComm.Parameters["@i_remarks"].Value = DBNull.Value;
				oComm.Parameters["@i_remarks"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_start_date", SqlDbType.DateTime);
				oComm.Parameters["@i_start_date"].Value =  startDate;
				oComm.Parameters["@i_start_date"].Direction = ParameterDirection.Input;
				
				oComm.Parameters.Add("@i_amount", SqlDbType.Decimal);
				oComm.Parameters["@i_amount"].Value =  amountPaid;
				oComm.Parameters["@i_amount"].Direction = ParameterDirection.Input;
				
				oComm.Parameters.Add("@i_language_id", SqlDbType.Int);
				oComm.Parameters["@i_language_id"].Value =  languageId;
				oComm.Parameters["@i_language_id"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_num_copies", SqlDbType.Int);
				oComm.Parameters["@i_num_copies"].Value =  numCopies;
				oComm.Parameters["@i_num_copies"].Direction = ParameterDirection.Input;

                oComm.Parameters.Add("@i_category", SqlDbType.VarChar,1);
                oComm.Parameters["@i_category"].Value = category;
                oComm.Parameters["@i_category"].Direction = ParameterDirection.Input;

                oComm.Parameters.Add("@i_discount", SqlDbType.Decimal);
                oComm.Parameters["@i_discount"].Value = discount;
                oComm.Parameters["@i_discount"].Direction = ParameterDirection.Input;
				
				oComm.Parameters.Add("@o_subscriber_id", SqlDbType.Int);
				oComm.Parameters["@o_subscriber_id"].Direction = ParameterDirection.Output;
					
				int cmdResults = oComm.ExecuteNonQuery();
				if (oComm.Parameters["@o_subscriber_id"].Value != DBNull.Value)
					return Convert.ToInt32(oComm.Parameters["@o_subscriber_id"].Value);
				else
					return -99;
			}
			finally
			{
				myConn.Close();
			}
		}
		public static void ImportDeleteRec(string subscribersTmpId)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("	delete from	");
			sb.Append("		subscribers_tmp	");
			sb.Append("		where	");
            sb.AppendFormat("	subscribers_tmp_id = {0}", subscribersTmpId);
			
			
			SqlConnection conn = new SqlConnection(GlobalFn.GetConnString);
			try
			{
				
				conn.Open();
				SqlCommand cmd = new SqlCommand(sb.ToString(),conn);
			
				cmd.ExecuteNonQuery();
			}
			finally
			{
				conn.Close();
			}
		}
    /*CREATE PROC [dbo].[P_Edit_Subscribers](@i_subscriber_id int,@i_sub_code varchar(10),@i_title varchar(10),
@i_last_name varchar(250),@i_first_name varchar(250),@i_address_line1 varchar(500),
@i_address_line2 varchar(500),@i_address_line3 varchar(500),@i_city varchar(250),
@i_district varchar(100),@i_state_id int,@i_pin_code int,@i_country_id int,@i_status char(1),
@i_remarks varchar(500),@i_language_id int,@i_num_copies int,
@i_category varchar(1),@i_discount decimal(9,2),@o_return int output) 
     */
    public static int EditSubscriber(int subscriberId, string subCode, string title, string lastName, string firstName, string addressLine1, string addressLine2, string addressLine3, string city, string district, int stateId, int pinCode, string countryId, string status, string remarks, string languageId, int numCopies,    string category, decimal discount)
    {
      status = (status.Equals("Active")) ? "A" : "X";

      StringBuilder oSb = new StringBuilder();
      SqlConnection myConn = new SqlConnection(GlobalFn.GetConnString);
      try
      {
        myConn.Open();

        SqlCommand oComm = new SqlCommand();
        oComm.CommandType = CommandType.StoredProcedure;
        oComm.CommandText = "P_Edit_Subscribers";
        oComm.Connection = myConn;

        oComm.Parameters.Add("@i_subscriber_id", SqlDbType.Int);
        oComm.Parameters["@i_subscriber_id"].Value = subscriberId;
        oComm.Parameters["@i_subscriber_id"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_sub_code", SqlDbType.VarChar, 10);
        oComm.Parameters["@i_sub_code"].Value = subCode;
        oComm.Parameters["@i_sub_code"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_title", SqlDbType.VarChar, 10);
        if (title.Length > 0)
          oComm.Parameters["@i_title"].Value = title;
        else
          oComm.Parameters["@i_title"].Value = DBNull.Value;
        oComm.Parameters["@i_title"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_last_name", SqlDbType.VarChar, 250);
        if (lastName.Length > 0)
          oComm.Parameters["@i_last_name"].Value = lastName;
        else
          oComm.Parameters["@i_last_name"].Value = DBNull.Value;
        oComm.Parameters["@i_last_name"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_first_name", SqlDbType.VarChar, 250);
        oComm.Parameters["@i_first_name"].Value = firstName;
        oComm.Parameters["@i_first_name"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_address_line1", SqlDbType.VarChar, 500);
        oComm.Parameters["@i_address_line1"].Value = addressLine1;
        oComm.Parameters["@i_address_line1"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_address_line2", SqlDbType.VarChar, 500);
        if (addressLine2.Length > 0)
          oComm.Parameters["@i_address_line2"].Value = addressLine2;
        else
          oComm.Parameters["@i_address_line2"].Value = DBNull.Value;
        oComm.Parameters["@i_address_line2"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_address_line3", SqlDbType.VarChar, 500);
        if (addressLine3.Length > 0)
          oComm.Parameters["@i_address_line3"].Value = addressLine3;
        else
          oComm.Parameters["@i_address_line3"].Value = DBNull.Value;
        oComm.Parameters["@i_address_line3"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_city", SqlDbType.VarChar, 100);
        oComm.Parameters["@i_city"].Value = city;
        oComm.Parameters["@i_city"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_district", SqlDbType.VarChar, 100);
        if (district.Length > 0)
          oComm.Parameters["@i_district"].Value = district;
        else
          oComm.Parameters["@i_district"].Value = DBNull.Value;
        oComm.Parameters["@i_district"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_state_id", SqlDbType.Int);
        if (stateId > 0)
          oComm.Parameters["@i_state_id"].Value = stateId;
        else
          oComm.Parameters["@i_state_id"].Value = DBNull.Value;
        oComm.Parameters["@i_state_id"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_pin_code", SqlDbType.Int);
        oComm.Parameters["@i_pin_code"].Value = pinCode;
        oComm.Parameters["@i_pin_code"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_country_id", SqlDbType.Int);
        oComm.Parameters["@i_country_id"].Value = countryId;
        oComm.Parameters["@i_country_id"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_status", SqlDbType.Char, 1);
        oComm.Parameters["@i_status"].Value = status;
        oComm.Parameters["@i_status"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_remarks", SqlDbType.VarChar, 500);
        if (remarks.Length > 0)
          oComm.Parameters["@i_remarks"].Value = remarks;
        else
          oComm.Parameters["@i_remarks"].Value = DBNull.Value;
        oComm.Parameters["@i_remarks"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_language_id", SqlDbType.Int);
        oComm.Parameters["@i_language_id"].Value = languageId;
        oComm.Parameters["@i_language_id"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_num_copies", SqlDbType.Int);
        oComm.Parameters["@i_num_copies"].Value = numCopies;
        oComm.Parameters["@i_num_copies"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_category", SqlDbType.Char, 1);
        oComm.Parameters["@i_category"].Value = category;
        oComm.Parameters["@i_category"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_discount", SqlDbType.Decimal);
        oComm.Parameters["@i_discount"].Value = discount;
        oComm.Parameters["@i_discount"].Direction = ParameterDirection.Input;
        
        oComm.Parameters.Add("@o_return", SqlDbType.Int);
        oComm.Parameters["@o_return"].Direction = ParameterDirection.Output;

        int cmdResults = oComm.ExecuteNonQuery();
        if (oComm.Parameters["@o_return"].Value != DBNull.Value)
          return Convert.ToInt32(oComm.Parameters["@o_return"].Value);
        else
          return -99;
      }
      finally
      {
        myConn.Close();
      }
    }
    /*ALTER PROC [dbo].[P_Update_Subscribers](@i_subscriber_id int,@i_sub_code varchar(10),@i_title varchar(10),@i_last_name varchar(250),@i_first_name varchar(250),@i_address_line1 varchar(500),@i_address_line2 varchar(500),@i_address_line3 varchar(500),@i_city varchar(250),@i_district varchar(100),@i_state_id int,@i_pin_code int,@i_country_id int,@i_status char(1),@i_remarks varchar(500),@i_language_id int,@i_amount decimal(9,2),@i_num_copies int,@i_bill_date datetime,@i_bill_num int,@i_category varchar(1),@i_discount decimal(9,2),@i_machine_name varchar(250),@i_make_receipt char(1),@o_return int output)
         */ 

		public static int UpdateModifySubscriber(int subscriberId,string subCode,string title,string lastName,string firstName,string addressLine1,string addressLine2,string addressLine3,string city,string district,int stateId,int pinCode, string countryId,string status,string remarks,string languageId,decimal amountPaid,int numCopies,DateTime billDate,int billNum,string category,decimal discount,string makeReceipt)
		{
			status = (status.Equals("Active"))? "A":"X";
			
			StringBuilder oSb = new StringBuilder();
			SqlConnection myConn = new SqlConnection(GlobalFn.GetConnString);
			try
			{
				myConn.Open();
			
				SqlCommand oComm = new SqlCommand();
				oComm.CommandType = CommandType.StoredProcedure;
				oComm.CommandText = "P_Update_Subscribers";
				oComm.Connection = myConn;
				
				oComm.Parameters.Add("@i_subscriber_id", SqlDbType.Int);
				oComm.Parameters["@i_subscriber_id"].Value =  subscriberId;
				oComm.Parameters["@i_subscriber_id"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_sub_code", SqlDbType.VarChar,10);
				oComm.Parameters["@i_sub_code"].Value =  subCode;
				oComm.Parameters["@i_sub_code"].Direction = ParameterDirection.Input;
										
				oComm.Parameters.Add("@i_title", SqlDbType.VarChar,10);
				if (title.Length > 0)
					oComm.Parameters["@i_title"].Value =  title;
				else
					oComm.Parameters["@i_title"].Value =  DBNull.Value;
				oComm.Parameters["@i_title"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_last_name", SqlDbType.VarChar,250);
				if (lastName.Length > 0)
					oComm.Parameters["@i_last_name"].Value =  lastName;
				else
					oComm.Parameters["@i_last_name"].Value =  DBNull.Value;
				oComm.Parameters["@i_last_name"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_first_name", SqlDbType.VarChar,250);
				oComm.Parameters["@i_first_name"].Value =  firstName;
				oComm.Parameters["@i_first_name"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_address_line1", SqlDbType.VarChar,500);
				oComm.Parameters["@i_address_line1"].Value = addressLine1;
				oComm.Parameters["@i_address_line1"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_address_line2", SqlDbType.VarChar,500);
				if(addressLine2.Length > 0)
					oComm.Parameters["@i_address_line2"].Value =  addressLine2;
				else
					oComm.Parameters["@i_address_line2"].Value = DBNull.Value;
				oComm.Parameters["@i_address_line2"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_address_line3", SqlDbType.VarChar,500);
				if(addressLine3.Length > 0)
					oComm.Parameters["@i_address_line3"].Value =  addressLine3;
				else
					oComm.Parameters["@i_address_line3"].Value = DBNull.Value;
				oComm.Parameters["@i_address_line3"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_city", SqlDbType.VarChar,100);
				oComm.Parameters["@i_city"].Value =  city;
				oComm.Parameters["@i_city"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_district", SqlDbType.VarChar,100);
				if(district.Length > 0)
					oComm.Parameters["@i_district"].Value =  district;
				else
					oComm.Parameters["@i_district"].Value = DBNull.Value;
				oComm.Parameters["@i_district"].Direction = ParameterDirection.Input;
				
				oComm.Parameters.Add("@i_state_id", SqlDbType.Int);
				if (stateId > 0)
					oComm.Parameters["@i_state_id"].Value =  stateId;
				else
					oComm.Parameters["@i_state_id"].Value =  DBNull.Value;
				oComm.Parameters["@i_state_id"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_pin_code", SqlDbType.Int);
				oComm.Parameters["@i_pin_code"].Value =  pinCode;
				oComm.Parameters["@i_pin_code"].Direction = ParameterDirection.Input;

        oComm.Parameters.Add("@i_country_id", SqlDbType.Int);
        oComm.Parameters["@i_country_id"].Value = countryId;
        oComm.Parameters["@i_country_id"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_status", SqlDbType.Char,1);
				oComm.Parameters["@i_status"].Value =  status;
				oComm.Parameters["@i_status"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_remarks", SqlDbType.VarChar,500);
				if(remarks.Length>0)
					oComm.Parameters["@i_remarks"].Value =  remarks;
				else
					oComm.Parameters["@i_remarks"].Value = DBNull.Value;
				oComm.Parameters["@i_remarks"].Direction = ParameterDirection.Input;
				
				oComm.Parameters.Add("@i_language_id", SqlDbType.Int);
				oComm.Parameters["@i_language_id"].Value =  languageId;
				oComm.Parameters["@i_language_id"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@i_amount", SqlDbType.Decimal);
				oComm.Parameters["@i_amount"].Value =  amountPaid;
				oComm.Parameters["@i_amount"].Direction = ParameterDirection.Input;
				
				oComm.Parameters.Add("@i_num_copies", SqlDbType.Int);
				oComm.Parameters["@i_num_copies"].Value =  numCopies;
				oComm.Parameters["@i_num_copies"].Direction = ParameterDirection.Input;
				
				oComm.Parameters.Add("@i_bill_date", SqlDbType.DateTime);
				oComm.Parameters["@i_bill_date"].Value =  billDate;
				oComm.Parameters["@i_bill_date"].Direction = ParameterDirection.Input;

                oComm.Parameters.Add("@i_bill_num", SqlDbType.Int);
                oComm.Parameters["@i_bill_num"].Value = billNum;
                oComm.Parameters["@i_bill_num"].Direction = ParameterDirection.Input;

                oComm.Parameters.Add("@i_category", SqlDbType.Char, 1);
                oComm.Parameters["@i_category"].Value = category;
                oComm.Parameters["@i_category"].Direction = ParameterDirection.Input;

                oComm.Parameters.Add("@i_discount", SqlDbType.Decimal);
                oComm.Parameters["@i_discount"].Value = discount;
                oComm.Parameters["@i_discount"].Direction = ParameterDirection.Input;

                oComm.Parameters.Add("@i_machine_name", SqlDbType.VarChar, 250);
                oComm.Parameters["@i_machine_name"].Value = GlobalFn.GetMachineName();
                oComm.Parameters["@i_machine_name"].Direction = ParameterDirection.Input;

                oComm.Parameters.Add("@i_make_receipt", SqlDbType.Char, 1);
                oComm.Parameters["@i_make_receipt"].Value = makeReceipt;
                oComm.Parameters["@i_make_receipt"].Direction = ParameterDirection.Input;

				oComm.Parameters.Add("@o_return", SqlDbType.Int);
				oComm.Parameters["@o_return"].Direction = ParameterDirection.Output;
					
				int cmdResults = oComm.ExecuteNonQuery();
				if (oComm.Parameters["@o_return"].Value != DBNull.Value)
					return Convert.ToInt32(oComm.Parameters["@o_return"].Value);
				else
					return -99;
			}
			finally
			{
				myConn.Close();
			}
		}
        public static DataSet RevenueReportGetRecs(DateTime dateFrom, DateTime dateTo, string langId,string machineType)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select  ");
            sb.Append("     convert(char(12),payment_date,103) payment_date_fmt,  ");
            sb.Append("     sum(case when l.mag_code = 'C' then amount else 0.0 end) civ,   ");
            sb.Append("     sum(case when l.mag_code = 'J' then amount else 0.0 end) jc, ");
            sb.Append("     sum(case when l.mag_code = 'K' then amount else 0.0 end) kv,  ");
            sb.Append("     sum(case when l.mag_code = 'M' then amount else 0.0 end) mjk  ");
            sb.Append(" from ");
            sb.Append("     receipts r,   ");
            sb.Append("     languages l  ");
            sb.Append(" where    ");
            sb.Append("         r.language_id = l.language_id    ");
            sb.AppendFormat("     and Convert(char(10),payment_date,112) between '{0}' and '{1}' ",dateFrom.ToString("yyyyMMdd"),dateTo.ToString("yyyyMMdd"));
            if (machineType.ToUpper().Equals("LOCAL"))
                sb.AppendFormat("     and r.machine_name = '{0}'",GlobalFn.GetMachineName());
            if (!langId.Equals("-1"))
                sb.AppendFormat("   and r.language_id = {0}", langId);
            sb.Append(" group by ");
            sb.Append("     convert(char(12),payment_date,103)   ");
            sb.Append(" order by  ");
            sb.Append("     convert(char(12),payment_date,103)  ");

            DataSet ds = new DataSet();

            SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
            oDA.Fill(ds, "revenueRep");
            return ds;

        }
        public static DataSet NewSubReportGetRecs(DateTime dateFrom, DateTime dateTo, string langId, string machineType)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select  ");
            sb.Append("     convert(char(12),start_date,103) start_date_fmt,  ");
            sb.Append("     sum(case when l.mag_code = 'C' then 1 else 0 end) civ,   ");
            sb.Append("     sum(case when l.mag_code = 'J' then 1 else 0 end) jc, ");
            sb.Append("     sum(case when l.mag_code = 'K' then 1 else 0 end) kv,  ");
            sb.Append("     sum(case when l.mag_code = 'M' then 1 else 0 end) mjk  ");
            sb.Append(" from ");
            sb.Append("     subscribers s,   ");
            if (machineType.ToUpper().Equals("LOCAL"))
            {
              sb.Append("     receipts r,  ");
            }
            sb.Append("     languages l  ");
            sb.Append(" where    ");
            sb.Append("         s.language_id = l.language_id    ");
            if (machineType.ToUpper().Equals("LOCAL"))
            {
              sb.Append("     and s.language_id = r.language_id   ");
              sb.Append("     and s.sub_code = r.sub_code ");
            }
            sb.Append("     and status = 'A'    ");
            sb.AppendFormat("     and Convert(char(10),start_date,112) between '{0}' and '{1}' ", dateFrom.ToString("yyyyMMdd"), dateTo.ToString("yyyyMMdd"));
            if (machineType.ToUpper().Equals("LOCAL"))
            {
              sb.AppendFormat("     and r.machine_name = '{0}'", GlobalFn.GetMachineName());
            }
            if (!langId.Equals("-1"))
            {
              sb.AppendFormat("   and s.language_id = {0}", langId);
            }
            sb.Append(" group by ");
            sb.Append("     convert(char(12),start_date,103)   ");
            sb.Append(" order by  ");
            sb.Append("     convert(char(12),start_date,103)  ");

            DataSet ds = new DataSet();

            SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
            oDA.Fill(ds, "NewSubRep");
            return ds;

        }
        public static DataSet RevenueReportGetLanguages()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select  ");
            sb.Append("     '-1' language_id, 'Choose All' mag_name ");
            sb.Append(" union all   ");
            sb.Append("	select	");
            sb.Append("		language_id,mag_name	");
            sb.Append("	from	");
            sb.Append("		languages	");

            DataSet ds = new DataSet();

            SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
            oDA.Fill(ds, "languages");
            return ds;
        }

        public static void CancelReceipt(int receiptID)
        { 
        
			SqlConnection myConn = new SqlConnection(GlobalFn.GetConnString);
            try
            {
                myConn.Open();

                SqlCommand oComm = new SqlCommand();
                oComm.CommandType = CommandType.StoredProcedure;
                oComm.CommandText = "p_bill_cancel";
                oComm.Connection = myConn;

                oComm.Parameters.Add("@i_receipt_id", SqlDbType.Int);
                oComm.Parameters["@i_receipt_id"].Value = receiptID;
                oComm.Parameters["@i_receipt_id"].Direction = ParameterDirection.Input;

                oComm.ExecuteNonQuery();
            }
            finally
            {
                myConn.Close();
            }

        
        }
        public static int GetDateDiff(string dueDate, string today)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" SELECT interval = DATEDIFF(day, '{0}', '{1}') ",today,dueDate);
            SqlConnection conn = new SqlConnection(GlobalFn.GetConnString);
            try
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            finally
            {
                conn.Close();
            }

        }
        public static DataSet DuesReportGetSubscribers(String langId, String sqlScript, bool incAllCat)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select  ");
            sb.Append("     *   ");
            sb.Append(" from    ");
            sb.Append("     subscribers ");
            sb.Append(" where   ");
            sb.AppendFormat("         language_id = {0}", langId);
            sb.Append("     and status = 'A'    ");
            if(incAllCat)
                sb.Append("     and category in ('G','S','B') ");
            sb.AppendFormat(" {0} ", sqlScript);

            DataSet ds = new DataSet();

            SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
            oDA.Fill(ds, "subscribers");
            return ds;
        }
        public static DataSet DuesReportGetAddressList(string subscribers)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select  ");
            sb.Append("     sub_code,    ");
            sb.Append("     title,  ");
            sb.Append("     ISNULL(last_name,'') + ' ' + first_name as name,    ");
            sb.Append("     address_line1,  ");
            sb.Append("     address_line2,  ");
            sb.Append("     address_line3,  ");
            sb.Append("     city,   ");
            sb.Append("     district,    ");
            sb.Append("     st.name state,  ");
            sb.Append("     pin_code,   ");
            sb.Append("     num_copies,  ");
            sb.Append("		amount_paid,	");
            sb.Append("		discount,	");
            sb.Append("		category,	");
            sb.Append("     start_Date,    ");
            sb.Append("     l.mag_name  ");
            sb.Append(" from    ");
            sb.Append("		subscribers	s left outer join states st	");
            sb.Append("			on	s.state_id = st.state_id	");
            sb.Append("		join languages l	");
            sb.Append("			on s.language_id = l.language_id	");
            sb.Append(" where   ");
            sb.Append("         status = 'A'    ");
            sb.AppendFormat("     and subscriber_id in ({0})", subscribers);

            DataSet ds = new DataSet();

            SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
            oDA.Fill(ds, "subs");
            return ds;

        }

        public static void MakeDbBackup(string path)
        {
//            backup DATABASE CIV to DISK = 'C:\civ_Backup.Bak'

            StringBuilder sb = new StringBuilder();
            sb.Append(" backup DATABASE CIV ");
            sb.AppendFormat(" to DISK = '{0}' ", path);

            SqlConnection conn = new SqlConnection(GlobalFn.GetConnString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataSet GetSubPrintHistory(String subCode, String langId)
        {
            //select subscriber_print_id, print_date, machine_name from subscriber_print where [status] = 'True' and subscriber_id in (select subscriber_id from subscribers where sub_code = and language_id = )

            StringBuilder sb = new StringBuilder();
            
            sb.Append(" select  ");
            sb.Append(" subscriber_print_id, print_date, ");
            sb.Append(" machine_name from subscriber_print ");
            sb.Append(" where [status] = 'True' and ");
            sb.Append(" subscriber_id in ");
            sb.Append(" (select subscriber_id from subscribers ");
            sb.AppendFormat(" where sub_code = '{0}'  ", subCode);
            sb.AppendFormat(" and language_id = {0} ) ", langId);

            DataSet ds = new DataSet();

            SqlDataAdapter oDA = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
            oDA.Fill(ds, "subPrint");
            return ds;
        }
        public static DataSet DeleteSubscriberGetRec(string subCode, string langId)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("    select  ");
            _sb.Append("        sub_code,   ");
            _sb.Append("        ISNULL(last_name,'') + ' ' + first_name sub_name,  ");
            _sb.Append("        address_line1,  ");
            _sb.Append("        city + ' ' + district city, ");
            _sb.Append("        status = case when status ='A' then 'Active' else 'Stopped' end,   ");
            _sb.Append("        amount_paid ");
            _sb.Append("    from    ");
            _sb.Append("        subscribers ");
            _sb.Append("    where   ");
            _sb.AppendFormat("        sub_code = '{0}'    ",subCode);
            _sb.AppendFormat("       and language_id = {0}   ",langId);
            _sb.Append("             and status = 'X' ");
            DataSet _ds = new DataSet();
              
            SqlDataAdapter _da = new SqlDataAdapter(_sb.ToString(),GlobalFn.GetConnString);
            _da.Fill(_ds);
                      
            return _ds;
        }
        /*[p_delete_subscriber](@i_sub_code varchar(10),
@i_language_id int, @o_return int output)
         */
        public static int DeleteSubDelete(string subCode,string langId)
        {
            SqlConnection myConn = new SqlConnection(GlobalFn.GetConnString);
            try
            {
                myConn.Open();

                SqlCommand oComm = new SqlCommand();
                oComm.CommandType = CommandType.StoredProcedure;
                oComm.CommandText = "p_delete_subscriber";
                oComm.Connection = myConn;

                oComm.Parameters.Add("@i_sub_code", SqlDbType.VarChar,10);
                oComm.Parameters["@i_sub_code"].Value = subCode;
                oComm.Parameters["@i_sub_code"].Direction = ParameterDirection.Input;
                oComm.Parameters.Add("@i_language_id", SqlDbType.Int);
                oComm.Parameters["@i_language_id"].Value = langId;
                oComm.Parameters["@i_language_id"].Direction = ParameterDirection.Input;
                oComm.Parameters.Add("@o_return", SqlDbType.Int);
                oComm.Parameters["@o_return"].Direction = ParameterDirection.Output;

                int cmdResults = oComm.ExecuteNonQuery();
                if (oComm.Parameters["@o_return"].Value != DBNull.Value)
                    return Convert.ToInt32(oComm.Parameters["@o_return"].Value);
                else
                    return -99;
            }
            finally
            {
                myConn.Close();
            }


        }
    }
}
       