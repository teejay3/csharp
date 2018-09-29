using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Win32;
using System.Threading;

namespace homework6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string temp_dir = "tmp";
        RootObject search;
        int position = 0;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s = textBox.Text;
            string url = "https://www.googleapis.com/customsearch/v1?key=AIzaSyDLh584TAPZrIkeT7728FrXxH_iyC3vBPQ&cx=009698661134167960273:pfkgk4hlfby&searchType=image&q=" + s;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "Mozilla/4.0+(compatible;+MSIE+6.0;+Windows+NT+5.1;+.NET+CLR+1.1.4322)";
            request.Timeout = 10000;
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RootObject));
            search = (RootObject)ser.ReadObject(resp.GetResponseStream());
            StreamReader sr = new StreamReader(resp.GetResponseStream());

            DownloadPics();
            position = 0;
            label.Content = "Complete";
            image2.Source = new BitmapImage(new Uri(search.items[position].link));
            position++;
            image1.Source = new BitmapImage(new Uri(search.items[position].link));
            resp.Close();
            sr.Close();
        }

        private void DownloadPics()
        {
            Thread[] t = new Thread[search.items.Count];
            for (int i = 0; i < t.Length; i++)
            {
                int n = i;
                t[i] = new Thread(() => {
                    WebClient wc = new WebClient();
                    wc.DownloadFileAsync(new Uri(search.items[n].link), "image" + n + ".jpg");
                    
                });
                t[i].Start();
            }
            for (int i = 0; i < t.Length; i++)
            {
                t[i].Join();
            }
        }

        private void Wc_DownloadFileCompleted1(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            label.Content = "Complete";
            System.Threading.Thread.Sleep(500);
            Uri path = new Uri(Directory.GetCurrentDirectory() + @"\image0.jpg", UriKind.Absolute);
            //image1.Source = new BitmapImage(path);
            //image2.Source = new BitmapImage(new Uri(search.items[1].link));
        }

        [DataContract]
        public class Url
        {
            public string type { get; set; }
            public string template { get; set; }
        }

        public class Request
        {
            public string title { get; set; }
            public string totalResults { get; set; }
            public string searchTerms { get; set; }
            public int count { get; set; }
            public int startIndex { get; set; }
            public string inputEncoding { get; set; }
            public string outputEncoding { get; set; }
            public string safe { get; set; }
            public string cx { get; set; }
            public string searchType { get; set; }
        }

        public class NextPage
        {
            public string title { get; set; }
            public string totalResults { get; set; }
            public string searchTerms { get; set; }
            public int count { get; set; }
            public int startIndex { get; set; }
            public string inputEncoding { get; set; }
            public string outputEncoding { get; set; }
            public string safe { get; set; }
            public string cx { get; set; }
            public string searchType { get; set; }
        }

        public class Queries
        {
            public List<Request> request { get; set; }
            public List<NextPage> nextPage { get; set; }
        }

        public class Context
        {
            public string title { get; set; }
        }

        public class SearchInformation
        {
            public double searchTime { get; set; }
            public string formattedSearchTime { get; set; }
            public string totalResults { get; set; }
            public string formattedTotalResults { get; set; }
        }

        public class Image
        {
            public string contextLink { get; set; }
            public int height { get; set; }
            public int width { get; set; }
            public int byteSize { get; set; }
            public string thumbnailLink { get; set; }
            public int thumbnailHeight { get; set; }
            public int thumbnailWidth { get; set; }
        }

        public class Item
        {
            public string kind { get; set; }
            public string title { get; set; }
            public string htmlTitle { get; set; }
            public string link { get; set; }
            public string displayLink { get; set; }
            public string snippet { get; set; }
            public string htmlSnippet { get; set; }
            public string mime { get; set; }
            public Image image { get; set; }
        }

        public class RootObject
        {
            public string kind { get; set; }
            public Url url { get; set; }
            public Queries queries { get; set; }
            public Context context { get; set; }
            public SearchInformation searchInformation { get; set; }
            public List<Item> items { get; set; }
        }

        private void button_previous_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All pics|*.jpg;*.jpeg";
            if (op.ShowDialog() == true)
            {
                image2.Source = new BitmapImage(new Uri(op.FileName));
                image3.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void button_next_Click(object sender, RoutedEventArgs e)
        {
            if (position < search.items.Count - 1)
            {
                image3.Source = image1.Source;
                image1.Source = image2.Source;
                position++;
                image2.Source = new BitmapImage(new Uri(search.items[position].link));
            }
            else
            {
                image3.Source = image1.Source;
                image1.Source = image2.Source;
                image2.Source = null;
            }
        }
    }
}
