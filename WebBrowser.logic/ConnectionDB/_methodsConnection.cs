
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrowserUI.ConnectionDB
{
    public partial class _connectionDB
    {
        //Open connection
        public bool ReopenConnection()
        {
            last_use = DateTime.Now;
            try { CloseConnection(); } catch { }      
        again:
            try
            {
                OpenConnection();
                return true;
            }
            catch (Exception e)
            {
                if (MessageBox.Show("Server connection error. Please try again?", "Server connection",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                   
                    goto again;
                }
                else
                {
                    return false;
                
                    //return;
                    //throw;
                }
            }
  
        }

        //command SQL : SELECT
        public DataSet MakeDataSet(string SQL)
        {

            if (!ReopenConnection()) return null;
           

            SqlDataAdapter da = new SqlDataAdapter(SQL, Connection);
            DataSet result = new DataSet(SQL);
            try
            {
                da.Fill(result);
            }
            catch (SqlException)
            {
            again:
                try
                {
                    ReopenConnection();
                    da = new SqlDataAdapter(SQL, Connection);
                    da.Fill(result);
                }
                catch (SqlException e)
                {
                    if (MessageBox.Show("Server connection error. Please try again?", "Server connection",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                    {
                        System.Threading.Thread.Sleep(1000);
                        goto again;
                    }
                    else
                    {
                        return null;
                    }
                }

            }

            last_use = DateTime.Now;
            return result;
        }

        //comand SQL : INSERT UPDATE DELETE
        public int DoSQL2(string SQL)
        {


            if (!ReopenConnection()) return -999;

            SqlCommand cmd = null;
            try
            {
                last_use = DateTime.Now;
                cmd = new SqlCommand(SQL, Connection);
                int res = cmd.ExecuteNonQuery();
                return res;
            }
            catch (SqlException)
            {
            again:
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                try
                {
                    ReopenConnection();
                    cmd = new SqlCommand(SQL, Connection);
                    int res = cmd.ExecuteNonQuery();
                    return res;
                }
                catch (SqlException e)
                {
                    if (MessageBox.Show("Server connection error. Please try again?", "Server connection",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                    {
                        goto again;
                    }
                    else
                    {
                        return -999;
                    }

                }
            }
        }
    }
}
