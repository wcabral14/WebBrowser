
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
        public void ReopenConnection()
        {
            try { CloseConnection(); } catch { }      
        again:
            try
            {
                OpenConnection();
            }
            catch (Exception e)
            {
                if (MessageBox.Show("Server connection error. Please try again? \nMessage:\n" + e.Message, "Server connection",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    System.Threading.Thread.Sleep(1000);
                    goto again;
                }
                else
                {
                    Application.Exit();
                    //return;
                    //throw;
                }
            }
            last_use = DateTime.Now;
        }

        //command SQL : SELECT
        public DataSet MakeDataSet(string SQL)
        {
           
            ReopenConnection();
           

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
                    if (MessageBox.Show("Server connection error. Please try again?  \nMessage:\n" + e.Message, "Server connection",
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
       
            ReopenConnection();
            
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
                    if (MessageBox.Show("Server connection error" + e.Message + ". Please try again?", "Server connection",
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
