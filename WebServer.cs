using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;

namespace SimpleWebServer
{
    class WebServer
    {
        HttpListener Listener;

        HttpListenerContext Context;
        HttpListenerRequest Request;
        HttpListenerResponse Response;
        StreamReader SR;

        public void Start()
        {

            string siteFolder = @"C:\site\";

            Listener = new HttpListener();
            Listener.Prefixes.Add("http://localhost:7070/");

            Listener.Start();

            //Read HTML file//
            string file = siteFolder + "index.html";
            SR = new StreamReader(file);
            file = SR.ReadToEnd();

            while (true)
            {
                Console.WriteLine("Waiting...");
                Context = Listener.GetContext();

                Console.WriteLine("Connected...");

                Request = Context.Request;
                Response = Context.Response;
                Console.WriteLine("Address ==> " + Request.Url);

                string data = "Welcome! Time: " + DateTime.Now.ToLongDateString();
                data += "<br />";
                data += Request.RawUrl;

                data = file;

                byte[] sendData = Encoding.UTF8.GetBytes(data);
                Response.AddHeader("Content-Type","text/html; charset=utf-8");
                Response.OutputStream.Write(sendData,0,sendData.Length);

                Context.Response.Close();



            }

        }
    }
}
