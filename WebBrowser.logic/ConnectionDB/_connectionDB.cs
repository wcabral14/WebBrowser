using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrowserUI.ConnectionDB
{
    public partial class _connectionDB
    {
        public static SqlConnection Connection;
        public string connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                        AttachDbFilename=C:\Users\wcabr\OneDrive\Desktop\CPSC_2713\wjc0027_module5project\WebBrowser.Data\WebBrowserDataDB.mdf;
                                        Integrated Security = True";
        protected DateTime last_use;                                                                                          //when was the connection last used
                                                                                                                              //reconnect in _ minutes

        public void RecreateConnection()
        {
            Connection = new SqlConnection(connetionString);
            last_use = DateTime.Now;
        }
        public void CloseConnection()
        {
            Connection.Close();
        }
        public void OpenConnection()
        {
            Connection.Open();
            last_use = DateTime.Now;
        }
        public _connectionDB()
        {         
            RecreateConnection();
        }
    }
}
