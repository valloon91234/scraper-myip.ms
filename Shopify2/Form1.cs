using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shopify2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void insert(Model m)
        {
            if (Dao.IsExist(m.Domain))
            {
                Console.Write(" - Already exists - ");
            }
            else
            {
                try
                {
                    Dao.Insert(m);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            Console.WriteLine(m.ToString());
        }

        public static String GetURLFromDomain(String domain)
        {
            if (!domain.StartsWith("http"))
            {
                domain = "https://" + domain;
            }
            return domain;
        }

        public static String GetURLFromPath(String href, String host, String url)
        {
            if (!href.StartsWith("http"))
            {
                if (href.StartsWith("/"))
                {
                    href = host + "/" + href;
                }
                else
                {
                    int k = url.LastIndexOf("/");
                    href = url.Substring(0, k + 1) + href;
                }
            }
            return href;
        }

        public static Boolean IsValidEmail(String source)
        {
            String sourceLower = source.ToLower();
            if (sourceLower.Equals("aaa@bbb.ccc")
                || sourceLower.Equals("email@email.com")
                || sourceLower.Equals("email@gmail.com")
                || sourceLower.EndsWith("-collection")
                || sourceLower.EndsWith(".css")
                || sourceLower.EndsWith(".png")
                || sourceLower.EndsWith(".jpg")
                || sourceLower.EndsWith(".gif")
                || sourceLower.EndsWith(".jpeg")
                || sourceLower.EndsWith(".js")
                || sourceLower.EndsWith(".account")
                || sourceLower.EndsWith(".checkout")
                || sourceLower.EndsWith(".pro")
                || sourceLower.StartsWith("vendors@")
                || sourceLower.StartsWith("u003e@")
                || sourceLower.StartsWith("u003c@")
                || sourceLower.Contains("@website")
                || sourceLower.Contains("@example")
                || sourceLower.Contains("@exemple")
                || sourceLower.Contains("@company")
                || sourceLower.Contains("@domain")
                || sourceLower.Contains("@ejemplo")
                || sourceLower.Contains("@website")
                || sourceLower.Contains("@beispiel")
                || sourceLower.Contains("you@")
                || sourceLower.Contains("your@")
                || sourceLower.Contains("@you")
                || sourceLower.Contains("@template")
                || sourceLower.Contains("@noreply")
                || sourceLower.Contains("@sentry")
                || sourceLower.Contains("@address")
                )
                return false;
            return new EmailAddressAttribute().IsValid(source);
        }

        public static String GetEmailOld(String input)
        {
            Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
            RegexOptions.IgnoreCase);
            //find items that matches with our pattern
            MatchCollection emailMatches = emailRegex.Matches(input);
            //StringBuilder sb = new StringBuilder();
            foreach (Match emailMatch in emailMatches)
            {
                String value = emailMatch.Value;
                if (IsValidEmail(value))
                {
                    //sb.AppendLine(emailMatch.Value);
                    return value;
                }
            }
            return null;
        }

        public static String GetEmail(String input)
        {
            const string MatchEmailPattern =
            @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
              + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})";
            Regex rx = new Regex(MatchEmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            // Find matches.
            MatchCollection matches = rx.Matches(input);
            // Report the number of matches found.
            int noOfMatches = matches.Count;
            // Report on each match.
            foreach (Match match in matches)
            {
                String value = match.Value;
                if (IsValidEmail(value))
                {
                    //sb.AppendLine(emailMatch.Value);
                    return value;
                }
            }
            return null;
        }

        private void parse(String text)
        {
            if (text.StartsWith("<"))
            {
                int i = text.IndexOf(@"<td class='row_name'>");
                while (i >= 0)
                {
                    Model m = new Model();
                    int j;

                    i = text.IndexOf("href", i);
                    i = text.IndexOf(">", i) + 1;
                    j = text.IndexOf("<", i);
                    m.Domain = text.Substring(i, j - i).Trim();

                    i = text.IndexOf("href", i);
                    i = text.IndexOf(">", i) + 1;
                    j = text.IndexOf("<", i);
                    m.IP = text.Substring(i, j - i).Trim();

                    i = text.IndexOf("href", i);
                    i = text.IndexOf(">", i) + 1;
                    j = text.IndexOf("<", i);
                    m.Company = text.Substring(i, j - i).Trim();

                    i = text.IndexOf("href", i);
                    i = text.IndexOf(">", i) + 1;
                    j = text.IndexOf("<", i);
                    m.Location = text.Substring(i, j - i).Trim();

                    i = text.IndexOf("href", i);
                    i = text.IndexOf(">", i) + 1;
                    j = text.IndexOf("<", i);
                    m.City = text.Substring(i, j - i).Trim();

                    i = text.IndexOf(">#", i) + 2;
                    j = text.IndexOf("<", i);
                    String rating = text.Substring(i, j - i).Trim();
                    m.Rating = Convert.ToInt32(Regex.Replace(rating, "[^0-9]", ""));
                    insert(m);

                    i = text.IndexOf(@"<td class='row_name'>", j);
                }
            }
            else
            {
                int i = text.IndexOf("No\tWeb Site");
                if (i < 0)
                {
                    Console.WriteLine("None");
                    return;
                }
                i = text.IndexOf("\r\n", i) + 2;
                while (true)
                {
                    int j = text.IndexOf("\r\n", i) + 2;
                    String t = text.Substring(i, j - i - 2);
                    String[] array = t.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (array.Length < 6) break;
                    Model m = new Model();
                    m.Domain = array[1].Trim();
                    m.IP = array[2].Trim();
                    m.Company = array[3].Trim();
                    m.Location = array[4].Trim();
                    m.City = array[5].Trim();
                    m.Rating = Convert.ToInt32(Regex.Replace(array[6], "[^0-9]", ""));
                    insert(m);
                    i = j;
                }
            }
        }

        public static String requestGet(String url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            httpWebRequest.Referer = "https://myip.ms/browse/sites/1/ownerID/376714/ownerID_A/1";
            //httpWebRequest.Headers.Add("Sec-Fetch-Site", "same-origin");
            //httpWebRequest.Headers.Add("Sec-Fetch-Mode:", "cors");
            //httpWebRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            String headers = @"Origin: https://myip.ms
X-Requested-With: XMLHttpRequest
Cache-Control: max-age=0
Upgrade-Insecure-Requests: 1
Sec-Fetch-User: ?1
Sec-Fetch-Mode: none
Sec-Fetch-Site: navigate
Accept-Language: en-US,en;q=0.9
If-None-Match: cacheable:2b787a51769b6ea09856f46ded73c5f8";
            List<string> listOfHeaders = new List<string>();
            listOfHeaders = headers.Split('\n').ToList();

            foreach (var header in listOfHeaders)
                httpWebRequest.Headers.Add(header);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream receiveStream = httpWebResponse.GetResponseStream();
            Boolean encodingInfoNotFound = string.IsNullOrEmpty(httpWebResponse.CharacterSet) || !Encoding.GetEncodings().Any(e => e.Name == httpWebResponse.CharacterSet);
            StreamReader streamReader = new StreamReader(receiveStream, encodingInfoNotFound ? Encoding.UTF8 : Encoding.GetEncoding(httpWebResponse.CharacterSet));
            String response = streamReader.ReadToEnd();
            streamReader.Close();
            receiveStream.Close();
            httpWebResponse.Close();
            return response;
        }

        public String requestPost(String url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json; charset=UTF-8";

            String cookie = textBox1.Text;
            httpWebRequest.Headers.Add("Cookie", cookie);

            httpWebRequest.Host = "myip.ms";
            httpWebRequest.Accept = "text/html, */*; q=0.01";
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            httpWebRequest.Referer = "https://myip.ms/browse/sites/1/ownerID/376714/ownerID_A/1";
            String headers = @"Origin: https://myip.ms
X-Requested-With: XMLHttpRequest
Sec-Fetch-Site: same-origin
Sec-Fetch-Mode: cors
Accept-Language: en-US,en;q=0.9";
            List<string> listOfHeaders = new List<string>();
            listOfHeaders = headers.Split('\n').ToList();
            foreach (var header in listOfHeaders)
                httpWebRequest.Headers.Add(header);
            String data = "getpage=yes&lang=en";
            if (data != null)
            {
                httpWebRequest.ContentLength = data.Length;
                StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                streamWriter.Write(data);
                streamWriter.Close();
                streamWriter.Dispose();
            }
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string Charset = httpWebResponse.CharacterSet;
            Stream receiveStream = httpWebResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(receiveStream, Encoding.GetEncoding(Charset));
            String response = streamReader.ReadToEnd();
            streamReader.Close();
            receiveStream.Close();
            httpWebResponse.Close();
            return response;
        }

        private void toolStripButton_Parse_Click(object sender, EventArgs e)
        {
            String text = textBox1.Text.Trim();
            parse(text);
        }


        private void toolStripButton_List_Click(object sender, EventArgs e)
        {
            int p = Convert.ToInt32(toolStripTextBox1.Text);
            while (true)
            {
                String url = "https://myip.ms/ajax_table/sites/" + p + "/ownerID/376714/ownerID_A/1";
                String result = requestPost(url);
                if (result.StartsWith("Error"))
                {
                    Console.WriteLine("-------- " + p + " -------- ERROR");
                    StreamWriter writer = new StreamWriter("page.txt");
                    writer.Write(p.ToString());
                    writer.Close();
                    var dr = MessageBox.Show("Shut down?", p.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        Process.Start("shutdown", "/s /t 0");
                    }
                    toolStripTextBox1.Text = p.ToString();
                    break;
                }
                else
                {
                    Console.WriteLine("--- " + p + " --- OK");
                    p++;
                    parse(result);
                }
            }
        }

        private void toolStripButton_Email_Click(object sender, EventArgs e)
        {
            Model[] list = Dao.SelectAll(null, true, true).ToArray();
            int count = list.Length;
            int threadCount = 16;
            Console.WriteLine("\r\n" + count + " domains have no eamil.\r\n");
            List<Model>[] lists = new List<Model>[threadCount];
            for (int t = 0; t < threadCount; t++)
            {
                lists[t] = new List<Model>();
            }
            int i = Convert.ToInt32(toolStripTextBox2.Text);
            while (i < count)
            {
                Model m = list[i++];
                if (m.Error != null) continue;
                lists[i % threadCount].Add(m);
            }
            for (int t = 0; t < threadCount; t++)
            {
                startProcessEmailThread(lists[t].ToArray());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                StreamReader reader = File.OpenText("page.txt");
                String text = reader.ReadToEnd();
                toolStripTextBox1.Text = text.Trim();
                reader.Close();
                reader.Dispose();
                //toolStripButton_List_Click(sender, e);
            }
            catch (Exception) { }
        }

        private void toolStripButton_Merge_Click(object sender, EventArgs e)
        {
            {
                List<Model> list = Dao.SelectAll("_plus");
                int count = list.Count;
                int start = 0;
                for (int i = start; i < count; i++)
                {
                    Model m = list[i];
                    try
                    {
                        if (Dao.IsExist(m.Domain))
                        {
                            Console.WriteLine(i + " / " + count + "\t" + m.Domain + "\t<Already exists.>");
                        }
                        else
                        {
                            Dao.Insert(m);
                            Console.WriteLine(i + " / " + count + "\t" + m.Domain + "\t- Added -");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(i + " / " + count + "\t" + m.Domain + "\t" + ex.Message);
                    }
                }
                Console.WriteLine("--- finished ---");
            }
            {
                List<Model> list = Dao.SelectAll("_plus2");
                int count = list.Count;
                int start = 0;
                for (int i = start; i < count; i++)
                {
                    Model m = list[i];
                    try
                    {
                        if (Dao.IsExist(m.Domain))
                        {
                            Console.WriteLine(i + " / " + count + "\t" + m.Domain + "\t<Already exists.>");
                        }
                        else
                        {
                            Dao.Insert(m);
                            Console.WriteLine(i + " / " + count + "\t" + m.Domain + "\t- Added -");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(i + " / " + count + "\t" + m.Domain + "\t" + ex.Message);
                    }
                }
                Console.WriteLine("--- finished ---");
            }
        }

        private static int TheradCount = 0;

        public static void ProcessEmail(Object obj)
        {
            int threadIndex = ++TheradCount;
            Model[] mArray = (Model[])obj;
            foreach (Model m in mArray)
            {
                if (m.Domain == "sodoapparel.com")
                    m.Domain = "sodoapparel.com";
                try
                {
                    String response = requestGet(GetURLFromDomain(m.Domain));
                    String email = GetEmail(response);
                    if (email != null)
                    {
                        m.Email = email;
                        Dao.UpdateEmail(m);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(m.Id + "\t" + threadIndex + " / " + TheradCount + "\t\t" + m.Domain + "\t\t" + m.Email);
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    {
                        int i2 = 0;
                        List<String> hrefList = new List<string>();
                        while (true)
                        {
                            i2 = response.IndexOf(" href=", i2);
                            if (i2 < 0) break;
                            i2 += 7;
                            char c = response[i2 - 1];
                            int j2 = response.IndexOf(c, i2);
                            String href = response.Substring(i2, j2 - i2);
                            String hrefLower = href.ToLower();
                            if (href != "#" && !hrefLower.Contains(".css"))
                            {
                                //if (hrefLower.Contains("contact") || hrefLower.Contains("support") || hrefLower.Contains("about") || hrefLower.Contains("term"))
                                hrefList.Add(href);
                            }
                            i2 = j2 + 1;
                        }
                        foreach (String href in hrefList)
                        {
                            String url = GetURLFromPath(href, GetURLFromDomain(m.Domain), GetURLFromDomain(m.Domain) + "/");
                            if (!href.Contains(m.Domain)) continue;
                            try
                            {
                                String response2 = requestGet(url);
                                String email2 = GetEmail(response2);
                                if (email2 != null)
                                {
                                    m.Email = email2;
                                    Dao.UpdateEmail(m);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine(m.Id + "\t" + threadIndex + " / " + TheradCount + "\t\t" + m.Domain + "\t\t" + m.Email);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    goto break_1;
                                }
                            }
                            catch (Exception) { }
                        }
                    }
                    m.Error = "<EMAIL NOT FOUND>";
                    Dao.UpdateError(m);
                    Console.WriteLine(m.Id + "\t" + threadIndex + " / " + TheradCount + "\t\t" + m.Domain + "\t\t<Not found>");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(m.Id + "\t" + threadIndex + " / " + TheradCount + "\t\t" + m.Domain + "\t\t" + ex.Message);
                    m.Error = ex.Message;
                    Dao.UpdateError(m);
                }
            break_1:;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\r\n------------ Thread " + threadIndex + " Ended ------------\r\n");
            TheradCount--;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void startProcessEmailThread(Model[] mArray)
        {
            Thread thread = new Thread(ProcessEmail);
            thread.Start(mArray);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (Dao.Close())
                Application.Exit();
        }

        private void toolStripButton_Test_Click(object sender, EventArgs e)
        {
            Console.Clear();
            List<Model> list = Dao.SelectAll();
            foreach (Model m in list)
            {
                String email = m.Email;
                if (email == null) continue;

                if (email.StartsWith("u003e@", StringComparison.OrdinalIgnoreCase))
                {
                    m.Email = null;
                    Dao.UpdateEmail(m);
                    Console.WriteLine(m.ToString());
                }
                else if (email.StartsWith("u003e", StringComparison.OrdinalIgnoreCase) || email.StartsWith("u003c", StringComparison.OrdinalIgnoreCase))
                {
                    m.Email = email.Substring(5);
                    Dao.UpdateEmail(m);
                    Console.WriteLine(m.ToString());
                }

                if (email.EndsWith(".com", StringComparison.OrdinalIgnoreCase)) continue;
                int length = email.Length;
                if (char.IsDigit(email[length - 1]) || email.Contains("?"))
                {
                    Console.WriteLine(m.ToString());
                }
                String[] array = email.Split(new char[] { '.' });
                if (char.IsUpper(array[array.Length - 1][0]) && char.IsLower(email[length - 1]))
                {
                    Console.WriteLine(m.ToString());
                }

                if (email.EndsWith(".For", StringComparison.OrdinalIgnoreCase))
                {
                    m.Email = email.Substring(0, length - 4);
                    Dao.UpdateEmail(m);
                    Console.WriteLine(m.ToString());

                }
                else if (email.EndsWith(".We", StringComparison.OrdinalIgnoreCase))
                {
                    m.Email = email.Substring(0, length - 3);
                    Dao.UpdateEmail(m);
                    Console.WriteLine(m.ToString());
                }
                else if (email.EndsWith("phone", StringComparison.OrdinalIgnoreCase))
                {
                    m.Email = email.Substring(0, length - 5);
                    Dao.UpdateEmail(m);
                    Console.WriteLine(m.ToString());
                }


            }
            Console.WriteLine("--- finished ---");
        }
    }
}
