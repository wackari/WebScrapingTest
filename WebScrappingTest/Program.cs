using System;
using System.IO;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace WebScrappingTest
{
    class MainClass
    {
        public static void Main(string[] args)
        {


            Console.WriteLine("Iniciando");
            int number = (2022-2008)+1;
           
            var startdate = new DateTime(2008, 1, 1);
            for (int i = 1; i <=
                (number); i++)
            {
               


                Console.WriteLine(startdate.Year);
                Console.WriteLine("-------------------------");

                string fullurl = "https://www.goodsmile.info/en/products/category/figma/released/" + startdate.Year.ToString();
                var html = GetHtmlContent(fullurl);
                var FullPage = new HtmlDocument();
                FullPage.LoadHtml(html.ToString());

                var nodes = FullPage.DocumentNode.SelectNodes("//*[@class='hitItem figma   ']");
                foreach (var node in nodes)
                {

                    var htmlDocument2 = new HtmlDocument();
                    var lol = node.InnerHtml.ToString();
                    //  Console.WriteLine(lol);
                    htmlDocument2.LoadHtml(lol);
                    var nodes2 = htmlDocument2.DocumentNode.SelectNodes("//span");
                    var img = htmlDocument2.DocumentNode.SelectNodes("//img");
                    var htmlDocument3 = new HtmlDocument();
                    htmlDocument3.LoadHtml(img.ToString());
                    var nodes3 = htmlDocument3.DocumentNode.SelectNodes("//a");
                    string image = img[0].OuterHtml.ToString().Replace("<img data-original=\"//", String.Empty);
                    int text = image.IndexOf(".jpg\"") + 4;
                    var imgurl = image.Remove(text);
                   
                    if (nodes2.Count == 2)
                    {
                    }
                    else
                    {
                        DownloadImage(imgurl, nodes2[2].InnerText.ToString());
                        Console.WriteLine("Figma Number: " + nodes2[2].InnerText.Replace("\r\n", string.Empty) + " , " + "Name: " + nodes2[1].InnerText.Replace("\r\n", string.Empty));
                        Console.WriteLine("URl Image: " + imgurl);
                        Console.WriteLine("----------------------------------");



                    }
                }
                //date1.AddYears(i);
                startdate = startdate.AddYears(1);
            }
            Console.WriteLine(startdate);
            Console.ReadLine();
        }

        private static void DownloadImage(string imageurl,string nombre)
        {
            imageurl = "http://" + imageurl;
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(imageurl, "/Users/marcorodriguez/Downloads/img/"+nombre+".jpg");
            }
        }

        private static object GetHtmlContent(string v)
        {
            var request = (HttpWebRequest)WebRequest.Create(v);

            var response = (HttpWebResponse)request.GetResponse();
            var responseStream = response.GetResponseStream();
            request.Method = "GET";
            var ms = new MemoryStream();
            responseStream?.CopyTo(ms);

            var htmlContent = Encoding.UTF8.GetString(ms.ToArray());

            return htmlContent;
        }
    }
}
