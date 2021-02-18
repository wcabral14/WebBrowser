
using BrowserUI.ConnectionDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BrowserUI.History
{
    public static class HistoryManager
    {
        static _connectionDB MainDB = new _connectionDB();

        //Method delete item
        public static int DeleteItem(string URL, string title)
        { 
            int result = MainDB.DoSQL2(String.Format("DELETE History WHERE URL = '{0}' and TITLE = N'{1}'", URL, title));
            return result;
        }
        public static bool Delete()
        {
            int result = MainDB.DoSQL2("DELETE History ");
            return result != -999 ? true : false;
        }

        //Method selects all records
        public static List<HistoryItem> AllItems()
        {
           
            List<HistoryItem> items = new List<HistoryItem>();
            DataSet select_items = MainDB.MakeDataSet("SELECT * FROM HISTORY");

            if (select_items != null)
            {
                foreach (DataRow dataRow in select_items.Tables[0].Rows)
                {
                    HistoryItem _item = new HistoryItem(Convert.ToDateTime(dataRow["DATE"]), dataRow["TITLE"].ToString(), dataRow["URL"].ToString());
                    items.Add(_item);
                }

            }
            else { 
            
            }
            return items;
        }

        //Method insert records
        public static bool InsertItems(string URL, string title)
        {        
            int result = MainDB.DoSQL2(String.Format("INSERT INTO History(URL,Title,Date) VALUES ('{0}', N'{1}', CURRENT_TIMESTAMP);", URL, title));
            return result != -999 ? true : false;
        }
    }
}
