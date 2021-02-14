using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserUI.Bookmark
{
    
    public  class BookmarkItem
    {
        /*
         Here we have implemented one of the OOP paradigms: polymorphism

         namely, we put the fields of our class in a "capsule"
        */


        //Fields are only available in this class
        private string url { get; set; }
        private string title { get; set; }

        //this is the constructor of the class
        public BookmarkItem(string url, string title)
        {
            this.url = url;
            this.title = title;
        }

        //Fields are available to everyone
        public string Url { get { return url; } set { url = value; } }
        public string Title { get { return title; } set { title = value; } }

        //Overridden base method, lambda declaration operator =>
        public override string ToString() => String.Format("{0} ({1}) ", title, url);
    }
}
