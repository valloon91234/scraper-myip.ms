using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shopify2
{
    class HttpClient
    {
        public String cookie = null;

        public static String convertFilename(String title)
        {
            return title.Replace('/', ' ').Replace('\\', ' ').Replace(':', '.').Replace('*', ' ').Replace('?', ' ').Replace('\"', ' ').Replace('<', ' ').Replace('>', ' ').Replace('|', ' ');
        }

        public String requestGet(String url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            if (cookie != null)
            {
                httpWebRequest.Headers.Add("Cookie", cookie);
            }
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            String cookieNew = httpWebResponse.Headers.Get("Set-Cookie");
            if (cookieNew != null)
                cookie = cookieNew;
            Stream receiveStream = httpWebResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(receiveStream, Encoding.UTF8);
            String response = streamReader.ReadToEnd();
            streamReader.Close();
            receiveStream.Close();
            httpWebResponse.Close();
            return response;
        }

        public String requestPost(String url, String data)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            if (cookie != null)
            {
                httpWebRequest.Headers.Add("Cookie", cookie);
            }
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json; charset=UTF-8";
            if (data != null) {
                httpWebRequest.ContentLength = data.Length;
                StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                streamWriter.Write(data);
                streamWriter.Close();
                streamWriter.Dispose();
            }
//            Access - Control - Allow - Credentials: true
//Access - Control - Allow - Headers: Accept, Origin, Content - Type, Content - Length
//Access - Control - Allow - Methods: GET, PUT, POST, DELETE, OPTIONS
//Access - Control - Allow - Origin: https://wetransfer.com
//            Cache - Control: no - cache, no - store, must - revalidate
//Connection: keep - alive
//Content - Encoding: gzip
//Content - Length: 8015
//Content - Type: application / json; charset = utf - 8
//Date: Tue, 22 Oct 2019 13:15:24 GMT
//ETag: W / "243be-I2rBuZ1FfAJcFiEqRPlkAQ"
//Expires: 0
//Pragma: no - cache
//Server: nginx / 1.14.1
//x - powered - by: adzerk bifrost/
//    x - served - by: engine - i - 09606602f9086a59f

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            String cookieNew = httpWebResponse.Headers.Get("Set-Cookie");
            if (cookieNew != null)
                cookie = cookieNew;
            Stream receiveStream = httpWebResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(receiveStream, Encoding.UTF8);
            String response = streamReader.ReadToEnd();
            streamReader.Close();
            receiveStream.Close();
            httpWebResponse.Close();
            return response;
        }

        public String requestFile(String url, String filepath, String filename)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            if (cookie != null)
            {
                httpWebRequest.Headers.Add("Cookie", cookie);
            }
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            String cookieNew = httpWebResponse.Headers.Get("Set-Cookie");
            if (cookieNew != null)
                cookie = cookieNew;
            Stream receiveStream = httpWebResponse.GetResponseStream();
            String fullname = null;
            if (filepath == null || filepath == "")
                fullname = filename;
            else if (filepath.EndsWith(@"\"))
                fullname += filepath + filename;
            else
                fullname += filepath + @"\" + filename;
            FileStream fileStream = File.OpenWrite(fullname);
            receiveStream.CopyTo(fileStream);
            receiveStream.Close();
            fileStream.Close();
            httpWebResponse.Close();
            return fullname;
        }
    }
}
