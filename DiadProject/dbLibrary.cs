using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace DiadProject
{
    public class dbLibrary
    {
        public string clr(string clrValues)
        {
            string value = "";
            value = clrValues;
            value = value.Replace("~", "");
            //value = value.Replace("é", "");
            return value;
        }
        public string insertValue(string tableName, string databaseFields, string databaseValues, string ConString)
        {
            string parameters = "", result = "";
            int i = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[ConString]);
            try
            {
                SqlConnection.ClearPool(con);
                SqlConnection.ClearAllPools();
                string[] fields = databaseFields.Split(',');
                string[] values = databaseValues.Split('~');
                foreach (string item in fields)
                {
                    parameters = parameters + "@" + item + ",";
                }
                parameters = parameters.Substring(0, parameters.Length - 1);
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into " + tableName + " (" + databaseFields + ")" + " Values (" + parameters + ")", con);
                foreach (string item in fields)
                {
                    if (values[i].StartsWith("é"))
                    {
                        StringBuilder sb = new StringBuilder();
                        string s = values[i].Replace("é", "");
                        DateTime dt = DateTime.Parse(s);
                        sb.Append(cmd.Parameters.AddWithValue("@" + item, dt));
                        i++;
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(cmd.Parameters.AddWithValue("@" + item, values[i]));
                        i++;
                    }
                }
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                result = "true";
                return result;
            }
            catch (Exception ex)
            {
                result = "false";
                return result;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
            }
        }
        public string updateValueWhere1(string tableName, string databaseFields, string databaseValues, string whereField, string whereOperator, string whereValue, string ConString)
        {
            string UpdateParameters = "", updateField = "", updateResult = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[ConString]);
            try
            {
                int i = 0;
                SqlConnection.ClearPool(con);
                SqlConnection.ClearAllPools();
                string[] fields = databaseFields.Split(',');
                string[] values = databaseValues.Split('~');
                foreach (string item in fields)
                {
                    UpdateParameters = UpdateParameters + "@" + item + ",";
                }
                UpdateParameters = UpdateParameters.Substring(0, UpdateParameters.Length - 1);
                con.Open();
                foreach (string item in fields)
                {
                    if (updateField == "")
                    {
                        updateField = item + "=" + "@" + item;
                    }
                    else
                    {
                        updateField = updateField + "," + item + "=" + "@" + item;
                    }
                }
                string where = "where " + whereField + whereOperator + "@" + whereField + "1";
                SqlCommand cmd = new SqlCommand("update " + tableName + " set " + updateField + " " + where, con);
                cmd.Parameters.AddWithValue("@" + whereField + "1", whereValue);
                foreach (string item in fields)
                {
                    if (values[i].StartsWith("é"))
                    {
                        StringBuilder sb = new StringBuilder();
                        string s = values[i].Replace("é", "");
                        DateTime dt = DateTime.Parse(s);
                        sb.Append(cmd.Parameters.AddWithValue("@" + item, dt));
                        i++;
                    }
                    else if (values[i] == "Né")
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(cmd.Parameters.AddWithValue("@" + item, DBNull.Value));
                        i++;
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(cmd.Parameters.AddWithValue("@" + item, values[i]));
                        i++;
                    }
                }
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();
                updateResult = "true";
                return updateResult;
            }
            catch (Exception ex)
            {
                updateResult = "false";
                return updateResult;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
            }
        }
        public string updateValueWhere2(string tableName, string databaseFields, string databaseValues, string whereField, string whereOperator, string whereValue, string whereAndOr, string whereField2, string whereOperator2, string whereValue2, string ConString)
        {
            string UpdateParameters = "", updateField = "", updateResult = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[ConString]);
            try
            {
                int i = 0;
                SqlConnection.ClearPool(con);
                SqlConnection.ClearAllPools();
                string[] fields = databaseFields.Split(',');
                string[] values = databaseValues.Split('~');
                foreach (string item in fields)
                {
                    UpdateParameters = UpdateParameters + "@" + item + ",";
                }
                UpdateParameters = UpdateParameters.Substring(0, UpdateParameters.Length - 1);
                con.Open();
                foreach (string item in fields)
                {
                    if (updateField == "")
                    {
                        updateField = item + "=" + "@" + item;
                    }
                    else
                    {
                        updateField = updateField + "," + item + "=" + "@" + item;
                    }
                }
                string where = "where " + whereField + whereOperator + "@" + whereField + "1" + " " + whereAndOr + " " + whereField2 + whereOperator2 + "@" + whereField2 + "2";
                SqlCommand cmd = new SqlCommand("update " + tableName + " set " + updateField + " " + where, con);
                cmd.Parameters.AddWithValue("@" + whereField + "1", whereValue);
                cmd.Parameters.AddWithValue("@" + whereField2 + "2", whereValue2);
                foreach (string item in fields)
                {
                    if (values[i].StartsWith("é"))
                    {
                        StringBuilder sb = new StringBuilder();
                        string s = values[i].Replace("é", "");
                        DateTime dt = DateTime.Parse(s);
                        sb.Append(cmd.Parameters.AddWithValue("@" + item, dt));
                        i++;
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(cmd.Parameters.AddWithValue("@" + item, values[i]));
                        i++;
                    }
                }
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();
                updateResult = "true";
                return updateResult;
            }
            catch (Exception ex)
            {
                updateResult = "false";
                return updateResult;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
            }
        }
        public string updateValueWhere3(string tableName, string databaseFields, string databaseValues, string whereField, string whereOperator, string whereValue, string whereAndOr, string whereField2, string whereOperator2, string whereValue2, string whereAndOr2, string whereField3, string whereOperator3, string whereValue3, string ConString)
        {
            string UpdateParameters = "", updateField = "", updateResult = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[ConString]);
            try
            {
                int i = 0;
                SqlConnection.ClearPool(con);
                SqlConnection.ClearAllPools();
                string[] fields = databaseFields.Split(',');
                string[] values = databaseValues.Split('~');
                foreach (string item in fields)
                {
                    UpdateParameters = UpdateParameters + "@" + item + ",";
                }
                UpdateParameters = UpdateParameters.Substring(0, UpdateParameters.Length - 1);
                con.Open();
                foreach (string item in fields)
                {
                    if (updateField == "")
                    {
                        updateField = item + "=" + "@" + item;
                    }
                    else
                    {
                        updateField = updateField + "," + item + "=" + "@" + item;
                    }
                }
                string where = "where " + whereField + whereOperator + "@" + whereField + "1" + " " + whereAndOr + " " + whereField2 + whereOperator2 + "@" + whereField2 + "2" + " " + whereAndOr2 + " " + whereField3 + whereOperator3 + "@" + whereField3 + "3";
                SqlCommand cmd = new SqlCommand("update " + tableName + " set " + updateField + " " + where, con);
                cmd.Parameters.AddWithValue("@" + whereField + "1", whereValue);
                cmd.Parameters.AddWithValue("@" + whereField2 + "2", whereValue2);
                cmd.Parameters.AddWithValue("@" + whereField3 + "3", whereValue3);
                foreach (string item in fields)
                {
                    if (values[i].StartsWith("é"))
                    {
                        StringBuilder sb = new StringBuilder();
                        string s = values[i].Replace("é", "");
                        DateTime dt = DateTime.Parse(s);
                        sb.Append(cmd.Parameters.AddWithValue("@" + item, dt));
                        i++;
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(cmd.Parameters.AddWithValue("@" + item, values[i]));
                        i++;
                    }
                }
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();
                updateResult = "true";
                return updateResult;
            }
            catch (Exception ex)
            {
                updateResult = "false";
                return updateResult;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
            }
        }
        public string deleteValueWhere1(string tableName, string whereField, string whereOperator, string whereValue, string ConString)
        {
            string result = "", where = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[ConString]);
            try
            {
                SqlConnection.ClearPool(con);
                SqlConnection.ClearAllPools();
                where = "where " + whereField + whereOperator + "@" + whereField + "1";
                SqlCommand cmd = new SqlCommand("Delete From " + tableName + " " + where, con);
                con.Open();
                cmd.Parameters.AddWithValue("@" + whereField + "1", whereValue);
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();
                result = "true";
            }
            catch (Exception ex)
            {
                result = "false";
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
            }
            return result;
        }
        public string deleteValueWhere2(string tableName, string whereField, string whereOperator, string whereValue, string whereAndOr, string whereField2, string whereOperator2, string whereValue2, string ConString)
        {
            string result = "", where = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[ConString]);
            try
            {
                SqlConnection.ClearPool(con);
                SqlConnection.ClearAllPools();
                where = "where " + whereField + whereOperator + "@" + whereField + "1 " + whereAndOr + " " + whereField2 + whereOperator2 + "@" + whereField2 + "2";
                SqlCommand cmd = new SqlCommand("Delete From " + tableName + " " + where, con);
                con.Open();
                cmd.Parameters.AddWithValue("@" + whereField + "1", whereValue);
                cmd.Parameters.AddWithValue("@" + whereField2 + "2", whereValue2);
                cmd.ExecuteNonQuery();
                con.Dispose();
                con.Close();
                result = "true";
            }
            catch (Exception ex)
            {
                result = "false";
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
            }
            return result;
        }
        public DataTable getDataTable(string selectQuery, string ConString)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[ConString]);
            try
            {
                SqlConnection.ClearPool(con);
                SqlConnection.ClearAllPools();
                SqlCommand cmd = new SqlCommand(selectQuery, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                con.Dispose();
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
            }
        }


        public DataTable getDataTableWhere1(string selectQuery, string whereField, string whereOperator, string whereValue, string orderBy, string ConString)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[ConString]);
            try
            {
                SqlConnection.ClearPool(con);
                SqlConnection.ClearAllPools();
                string sql = selectQuery + " Where " + whereField + " " + whereOperator + " " + "@" + whereField.Replace(".", "") + " " + orderBy;
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@" + whereField.Replace(".", ""), whereValue);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                con.Dispose();
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
            }
        }
        public DataTable getDataTableWhere2(string selectQuery, string whereField, string whereOperator, string whereValue, string whereAndOr, string whereField2, string whereOperator2, string whereValue2, string orderBy, string ConString)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[ConString]);
            try
            {
                SqlConnection.ClearPool(con);
                SqlConnection.ClearAllPools();
                string sql = selectQuery + " Where " + whereField + whereOperator + "@" + whereField.Replace(".", "").Replace(" ", "") + "1 " + whereAndOr + " " + whereField2 + whereOperator2 + "@" + whereField2.Replace(".", "") + "2" + " " + orderBy;
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@" + whereField.Replace(".", "").Replace(" ", "") + "1", whereValue);
                cmd.Parameters.AddWithValue("@" + whereField2.Replace(".", "") + "2", whereValue2);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                con.Dispose();
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
            }
        }
        public DataTable getDataTableWhere3(string selectQuery, string whereField, string whereOperator, string whereValue, string whereAndOr, string whereField2, string whereOperator2, string whereValue2, string whereAndOr2, string whereField3, string whereOperator3, string whereValue3, string orderBy, string ConString)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[ConString]);
            try
            {
                SqlConnection.ClearPool(con);
                SqlConnection.ClearAllPools();
                string sql = selectQuery + " Where " + whereField + whereOperator + "@" + SeoURL(whereField).Replace("-", "").Replace(".", "") + "1" + " " + whereAndOr + " " + whereField2 + whereOperator2 + "@" + SeoURL(whereField2).Replace("-", "").Replace(".", "") + "2" + " " + whereAndOr2 + " " + whereField3 + whereOperator3 + "@" + SeoURL(whereField3).Replace("-", "").Replace(".", "") + "3" + " " + orderBy;
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@" + SeoURL(whereField).Replace("-", "").Replace(".", "") + "1", whereValue);
                cmd.Parameters.AddWithValue("@" + SeoURL(whereField2).Replace("-", "").Replace(".", "") + "2", whereValue2);
                cmd.Parameters.AddWithValue("@" + SeoURL(whereField3).Replace("-", "").Replace(".", "") + "3", whereValue3);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                con.Dispose();
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
            }
        }
        public DataTable getDataTableWhere4(string selectQuery, string whereField, string whereOperator, string whereValue, string whereAndOr, string whereField2, string whereOperator2, string whereValue2, string whereAndOr2, string whereField3, string whereOperator3, string whereValue3, string whereAndOr3, string whereField4, string whereOperator4, string whereValue4, string orderBy, string ConString)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[ConString]);
            try
            {
                SqlConnection.ClearPool(con);
                SqlConnection.ClearAllPools();
                string sql = selectQuery + " Where " + whereField + whereOperator + "@" + SeoURL(whereField).Replace("-", "") + "1" + " " + whereAndOr + " " + whereField2 + whereOperator2 + "@" + SeoURL(whereField2).Replace("-", "") + "2" + " " + whereAndOr2 + " " + whereField3 + whereOperator3 + "@" + SeoURL(whereField3).Replace("-", "") + "3" + " " + whereAndOr3 + " " + whereField4 + whereOperator4 + "@" + SeoURL(whereField4).Replace("-", "") + "4" + " " + orderBy;
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@" + SeoURL(whereField).Replace(".", "").Replace("-", "") + "1", whereValue);
                cmd.Parameters.AddWithValue("@" + SeoURL(whereField2).Replace(".", "").Replace("-", "") + "2", whereValue2);
                cmd.Parameters.AddWithValue("@" + SeoURL(whereField3).Replace(".", "").Replace("-", "") + "3", whereValue3);
                cmd.Parameters.AddWithValue("@" + SeoURL(whereField4).Replace(".", "").Replace("-", "") + "4", whereValue4);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                con.Dispose();
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                { con.Close(); }
            }
        }
        public DataRow getValue(string selectQuery, string ConString)
        {
            DataTable table = getDataTable(selectQuery, ConString);
            try
            {
                if (table.Rows.Count == 0) return null;
                return table.Rows[0];
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataRow getValueWhere1(string selectQuery, string whereField, string whereOperator, string whereValue, string orderBy, string ConString)
        {
            DataTable table = getDataTableWhere1(selectQuery, whereField, whereOperator, whereValue, orderBy, ConString);
            try
            {
                if (table.Rows.Count == 0) return null;
                return table.Rows[0];
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataRow getValueWhere2(string selectQuery, string whereField, string whereOperator, string whereValue, string whereAndOr, string whereField2, string whereOperator2, string whereValue2, string orderBy, string ConString)
        {
            DataTable table = getDataTableWhere2(selectQuery, whereField, whereOperator, whereValue, whereAndOr, whereField2, whereOperator2, whereValue2, orderBy, ConString);
            try
            {
                if (table.Rows.Count == 0) return null;
                return table.Rows[0];
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataRow getValueWhere3(string selectQuery, string whereField, string whereOperator, string whereValue, string whereAndOr, string whereField2, string whereOperator2, string whereValue2, string whereAndOr2, string whereField3, string whereOperator3, string whereValue3, string orderBy, string ConString)
        {
            DataTable table = getDataTableWhere3(selectQuery, whereField, whereOperator, whereValue, whereAndOr, whereField2, whereOperator2, whereValue2, whereAndOr2, whereField3, whereOperator3, whereValue3, orderBy, ConString);
            try
            {
                if (table.Rows.Count == 0) return null;
                return table.Rows[0];
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataRow getValueWhere4(string selectQuery, string whereField, string whereOperator, string whereValue, string whereAndOr, string whereField2, string whereOperator2, string whereValue2, string whereAndOr2, string whereField3, string whereOperator3, string whereValue3, string whereAndOr3, string whereField4, string whereOperator4, string whereValue4, string orderBy, string ConString)
        {
            DataTable table = getDataTableWhere4(selectQuery, whereField, whereOperator, whereValue, whereAndOr, whereField2, whereOperator2, whereValue2, whereAndOr2, whereField3, whereOperator3, whereValue3, whereAndOr3, whereField4, whereOperator4, whereValue4, orderBy, ConString);
            try
            {
                if (table.Rows.Count == 0) return null;
                return table.Rows[0];
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        SqlDataReader te;
        public SqlDataReader getDataReader(string selectQuery, string ConString)
        {
            SqlConnection conn;
            SqlCommand comm;
            SqlDataReader reader;
            string connectionString = ConfigurationManager.AppSettings[ConString];
            conn = new SqlConnection(connectionString);
            try
            {
                SqlConnection.ClearPool(conn);
                SqlConnection.ClearAllPools();
                comm = new SqlCommand(selectQuery, conn);
                conn.Open();
                reader = comm.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                return te;
            }
            //finally
            //{
            //    if (conn.State == ConnectionState.Open)
            //    { conn.Close(); }
            //}
        }
        public SqlDataReader getDataReaderWhere2(string selectQuery, string whereField, string whereOperator, string whereValue, string whereAndOr, string whereField2, string whereOperator2, string whereValue2, string orderBy, string ConString)
        {
            SqlConnection conn;
            SqlCommand comm;
            SqlDataReader reader;
            string connectionString = ConfigurationManager.AppSettings[ConString];
            string sql = selectQuery + " Where " + whereField + whereOperator + "@" + whereField.Replace(".", "").Replace(" ", "") + "1 " + whereAndOr + " " + whereField2 + whereOperator2 + "@" + whereField2.Replace(".", "") + "2" + " " + orderBy;
            conn = new SqlConnection(connectionString);
            try
            {
                SqlConnection.ClearPool(conn);
                SqlConnection.ClearAllPools();
                comm = new SqlCommand(selectQuery, conn);
                comm.Parameters.AddWithValue("@" + whereField.Replace(".", "").Replace(" ", "") + "1", whereValue);
                comm.Parameters.AddWithValue("@" + whereField2.Replace(".", "") + "2", whereValue2);
                conn.Open();
                reader = comm.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                return te;
            }
            //finally
            //{
            //    if (conn.State == ConnectionState.Open)
            //    { conn.Close(); }
            //}
        }
        public SqlDataAdapter getDataAdapter(string selectQuery, string ConString)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[ConString]);
            SqlConnection.ClearPool(con);
            SqlConnection.ClearAllPools();
            SqlDataAdapter da = new SqlDataAdapter(selectQuery, con);
            return da;
        }
        public SqlDataAdapter getDataAdapterWhere1(string selectQuery, string whereField, string whereOperator, string whereValue, string orderBy, string ConString)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[ConString]);
            SqlConnection.ClearPool(con);
            SqlConnection.ClearAllPools();
            SqlDataAdapter da = new SqlDataAdapter();
            string sql = selectQuery + " where " + whereField + " " + whereOperator + " " + "@" + whereField + " " + orderBy;
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@" + whereField, whereValue);
            da.SelectCommand = cmd;
            return da;
        }
        public SqlDataAdapter getDataAdapterWhere2(string selectQuery, string whereField, string whereOperator, string whereValue, string whereAndOr, string whereField2, string whereOperator2, string whereValue2, string orderBy, string ConString)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[ConString]);
            SqlConnection.ClearPool(con);
            SqlConnection.ClearAllPools();
            SqlDataAdapter da = new SqlDataAdapter();
            string sql = selectQuery + " where " + whereField + " " + whereOperator + " @" + whereField + " " + whereAndOr + " " + whereField2 + " " + whereOperator2 + " @" + whereField2 + " " + orderBy;
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@" + whereField, whereValue);
            cmd.Parameters.AddWithValue("@" + whereField2, whereValue2);
            da.SelectCommand = cmd;
            return da;
            con.Close(); con.Dispose(); da.Dispose();
        }
        public SqlDataAdapter getDataAdapterWhere3(string selectQuery, string whereField, string whereOperator, string whereValue, string whereAndOr, string whereField2, string whereOperator2, string whereValue2, string whereAndOr2, string whereField3, string whereOperator3, string whereValue3, string OrderBy, string ConString)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[ConString]);
            SqlConnection.ClearPool(con);
            SqlConnection.ClearAllPools();
            SqlDataAdapter da = new SqlDataAdapter();
            string sql = selectQuery + " where " + whereField + " " + whereOperator + " @" + whereField + " " + whereAndOr + " " + whereField2 + " " + whereOperator2 + " @" + whereField2 + " " + whereAndOr2 + " " + whereField3 + " " + whereOperator3 + " @" + whereField3 + " " + OrderBy;
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@" + whereField, whereValue);
            cmd.Parameters.AddWithValue("@" + whereField2, whereValue2);
            cmd.Parameters.AddWithValue("@" + whereField3, whereValue3);
            da.SelectCommand = cmd;
            return da;
        }
        public SqlDataAdapter getDataAdapterWhere4(string selectQuery, string whereField, string whereOperator, string WhereDeger, string whereAndOr, string whereField2, string whereOperator2, string whereValue2, string whereAndOr2, string whereField3, string whereOperator3, string whereValue3, string whereAndOr3, string whereField4, string whereOperator4, string whereValue4, string orderBy, string ConString)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[ConString]);
            SqlConnection.ClearPool(con);
            SqlConnection.ClearAllPools();
            SqlDataAdapter da = new SqlDataAdapter();
            string sql = selectQuery + " where " + whereField + " " + whereOperator + " @" + whereField + " " + whereAndOr + " " + whereField2 + " " + whereOperator2 + " @" + whereField2 + " " + whereAndOr2 + " " + whereField3 + " " + whereOperator3 + " @" + whereField3 + " " + whereAndOr3 + " " + whereField4 + " " + whereOperator4 + " @" + whereField4 + " " + orderBy;
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@" + whereField, WhereDeger);
            cmd.Parameters.AddWithValue("@" + whereField2, whereValue2);
            cmd.Parameters.AddWithValue("@" + whereField3, whereValue3);
            cmd.Parameters.AddWithValue("@" + whereField4, whereValue4);
            da.SelectCommand = cmd;
            return da;
        }
        public string sendMail(string mailAdress, string subject, string maildetail, string yourmail, string yourpassword, string SMTPAdress, bool sslTrueFalse, int portNumber)
        {
            string sonuc = "";
            try
            {
                SmtpClient ss = new SmtpClient(SMTPAdress, portNumber);
                ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                ss.UseDefaultCredentials = false;
                ss.Credentials = new NetworkCredential(yourmail, yourpassword);
                MailMessage mm = new MailMessage(yourmail, mailAdress);
                mm.IsBodyHtml = true;
                mm.Subject = subject;
                mm.Body = maildetail;
                ss.EnableSsl = sslTrueFalse;
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                ss.Send(mm);
                sonuc = "true";
                return sonuc;
            }
            catch (Exception ex)
            {
                sonuc = "false";
                return sonuc;
            }

        }
        public string SeoURL(string adress)
        {
            string value = "";
            value = adress.ToLower();
            value = value.Replace("ı", "i");
            value = value.Replace("ö", "o");
            value = value.Replace("ü", "u");
            value = value.Replace("ş", "s");
            value = value.Replace("ç", "c");
            value = value.Replace("ğ", "g");
            value = value.Replace("İ", "i");
            value = value.Replace("Ö", "o");
            value = value.Replace("Ü", "u");
            value = value.Replace("Ş", "s");
            value = value.Replace("Ç", "c");
            value = value.Replace("Ğ", "g");
            value = value.Replace("!", "");
            value = value.Replace("'", "");
            value = value.Replace("£", "");
            value = value.Replace("^", "");
            value = value.Replace("#", "");
            value = value.Replace("+", "");
            value = value.Replace("$", "");
            value = value.Replace("%", "");
            value = value.Replace("½", "");
            value = value.Replace("&", "");
            value = value.Replace("/", "");
            value = value.Replace("{", "");
            value = value.Replace("(", "");
            value = value.Replace("[", "");
            value = value.Replace(")", "");
            value = value.Replace("]", "");
            value = value.Replace("=", "");
            value = value.Replace("}", "");
            value = value.Replace("?", "");
            value = value.Replace("\\", "");
            value = value.Replace("_", "");
            value = value.Replace(" ", "-");
            value = value.Replace("|", "");
            value = value.Replace(";", "");
            value = value.Replace("`", "");
            value = value.Replace("<", "");
            value = value.Replace(">", "");
            value = value.Replace("@", "");
            value = value.Replace(".", "");
            value = value.Replace(":", "");
            value = value.Replace("*", "");
            value = value.Replace("\"", "");
            value = value.Replace("'", "");
            value = value.Replace("''", "");
            return value.ToLower();
        }
        public string getIPAdress(string value)
        {
            string IpAdress = "";
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                IpAdress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                IpAdress = HttpContext.Current.Request.UserHostAddress;
            }
            return IpAdress;
        }
        public string MD5(string password)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");
        }
        public string sha512(string data)
        {
            SHA512 sha = new SHA512Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }
        #region Settings

        private static int _iterations = 2;
        private static int _keySize = 256;

        private static string _hash = "SHA1";
        private static string _salt = "aselriasGYhB0a32"; // Random
        private static string _vector = "89CFtg34awl34kjq"; // Random

        #endregion
        public string Encrypt(string value)
        {
            return Encrypt<AesManaged>(value);
        }
        public static string Encrypt<T>(string value)
                where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = ASCIIEncoding.ASCII.GetBytes(_vector);
            byte[] saltBytes = ASCIIEncoding.ASCII.GetBytes(_salt);
            byte[] valueBytes = ASCIIEncoding.ASCII.GetBytes(value);

            byte[] encrypted;
            using (T cipher = new T())
            {
                PasswordDeriveBytes _passwordBytes =
                    new PasswordDeriveBytes("aHygbmgY28umdcX", saltBytes, _hash, _iterations);
                byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                cipher.Mode = CipherMode.CBC;

                using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                {
                    using (MemoryStream to = new MemoryStream())
                    {
                        using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                        {
                            writer.Write(valueBytes, 0, valueBytes.Length);
                            writer.FlushFinalBlock();
                            encrypted = to.ToArray();
                        }
                    }
                }
                cipher.Clear();
            }
            return Convert.ToBase64String(encrypted);
        }
        public string Decrypt(string value)
        {
            return Decrypt<AesManaged>(value);
        }
        public static string Decrypt<T>(string value) where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = ASCIIEncoding.ASCII.GetBytes(_vector);
            byte[] saltBytes = ASCIIEncoding.ASCII.GetBytes(_salt);
            byte[] valueBytes = Convert.FromBase64String(value);

            byte[] decrypted;
            int decryptedByteCount = 0;

            using (T cipher = new T())
            {
                PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes("aHygbmgY28umdcX", saltBytes, _hash, _iterations);
                byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                cipher.Mode = CipherMode.CBC;

                try
                {
                    using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                    {
                        using (MemoryStream from = new MemoryStream(valueBytes))
                        {
                            using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                            {
                                decrypted = new byte[valueBytes.Length];
                                decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return String.Empty;
                }

                cipher.Clear();
            }
            return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
        }
        public string checkCookie(string sessionID)
        {
            string result;
            try
            {
                DataRow dr = getValueWhere1("select * from tbl_users", "usr_lastsession_id", "=", sessionID, "", "ConString");
                if (dr == null)
                { result = "false"; }
                else { result = dr["usr_name"].ToString() + " " + dr["usr_surname"].ToString() + "," + dr["usr_user_id"].ToString(); }
                return result;
            }
            catch (Exception ex)
            {
                result = "";
                return result;
            }
        }
      
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        public Image RezizeImage(Image img, int maxWidth, int maxHeight)
        {
            using (img)
            {
                Double xRatio = (double)img.Width / maxWidth;
                Double yRatio = (double)img.Height / maxHeight;
                Double ratio = Math.Max(xRatio, yRatio);
                int nnx = (int)Math.Floor(img.Width / ratio);
                int nny = (int)Math.Floor(img.Height / ratio);
                Bitmap cpy = new Bitmap(nnx, nny, PixelFormat.Format32bppArgb);
                using (Graphics gr = Graphics.FromImage(cpy))
                {
                    gr.Clear(Color.Transparent);

                    // This is said to give best quality when resizing images
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    gr.DrawImage(img,
                        new Rectangle(0, 0, nnx, nny),
                        new Rectangle(0, 0, img.Width, img.Height),
                        GraphicsUnit.Pixel);
                }
                return cpy;
            }
        }
    }
}