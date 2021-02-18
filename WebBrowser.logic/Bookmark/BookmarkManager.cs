using BrowserUI.Bookmark;
using BrowserUI.ConnectionDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrowserUI.Bookmark
{
    public static class BookmarkManager
    {
        static _connectionDB MainDB = new _connectionDB();
        public static bool DeleteItem(string URL, string title)
        {
            int result = MainDB.DoSQL2(String.Format("DELETE Bookmarks WHERE URL = '{0}' and TITLE = '{1}' ", URL, title));
            return result != -999 ? true : false;
        }

        //Method selects all records
        public static List<BookmarkItem> AllItems()
        {

            List<BookmarkItem> items = new List<BookmarkItem>();
            DataSet select_items = MainDB.MakeDataSet("SELECT * FROM BOOKMARKS");

            if (select_items != null)
            {
                foreach (DataRow dataRow in select_items.Tables[0].Rows)
                {
                    BookmarkItem _item = new BookmarkItem(dataRow["URL"].ToString(), dataRow["TITLE"].ToString());
                    items.Add(_item);
                }
            }
            else
            {

            }
            return items;
        }

        //Method insert records
        public static bool InsertItems(string URL, string title)
        {
            var chek_dublicat = AllItems();
            int count = chek_dublicat.Where(x => x.Url == URL).Where(x => x.Title == title).Count();

            if (count == 0)
            {
                int result = MainDB.DoSQL2(String.Format("INSERT INTO BOOKMARKS(URL,Title) VALUES ('{0}', N'{1}');", URL, title));
                return result != -999 ? true : false;
            }
            else
            {
                MessageBox.Show("The bookmark already exists");
                return false;
            }
        }
    }
}
