using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserUI.History
{
    public class HistoryItem
    {
        //Fields are only available in this class
        private string url { get; set; }
        private string title { get; set;  }

        private DateTime date { get; set; } = DateTime.Now;

        //this is the constructor of the class INSERT
        public HistoryItem( string url, string title)
        {
            this.url = url;
            this.title = title;
        }

        public HistoryItem(DateTime date, string title, string url) : this(url, title)
        {
            this.date = date;
 
        }


        //Fields are available to everyone
        public string Url { get { return url; } set { url = value; } }
        public string Title { get { return title; } set { title = value; } }
        public DateTime Date { get { return date; } set { date = value; } }

        //Overridden base method, lambda declaration operator =>
        public override string ToString() => String.Format("{0} {1} {2}", date, title, url);
    }
}
