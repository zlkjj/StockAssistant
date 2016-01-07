using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace StockAssistant
{
    public class WebUtil
    {
        #region PostUrl(String url, String paramList)
        /// <summary>
        /// POST方法获取页面
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramList">格式: a=xxx&b=xxx&c=xxx</param>
        /// <returns></returns>
        private static string PostUrl(String url, String paramList)
        {
            HttpWebResponse res = null;
            string strResult = "";
            try
            {

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Proxy = WebRequest.GetSystemWebProxy();
                req.Method = "POST";
                req.KeepAlive = true;
                req.ContentType = "application/x-www-form-urlencoded";
                //req.UserAgent="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322)";
                CookieContainer cookieCon = new CookieContainer();
                req.CookieContainer = cookieCon;
                StringBuilder UrlEncoded = new StringBuilder();
                Char[] reserved = { '?', '=', '&' };
                byte[] SomeBytes = null;
                if (paramList != null)
                {
                    int i = 0, j;
                    while (i < paramList.Length)
                    {
                        j = paramList.IndexOfAny(reserved, i);
                        if (j == -1)
                        {
                            //							UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, paramList.Length-i)));
                            UrlEncoded.Append((paramList.Substring(i, paramList.Length - i)));
                            break;
                        }
                        //						UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, j-i)));
                        UrlEncoded.Append((paramList.Substring(i, j - i)));
                        UrlEncoded.Append(paramList.Substring(j, 1));
                        i = j + 1;
                    }
                    SomeBytes = Encoding.Default.GetBytes(UrlEncoded.ToString());
                    req.ContentLength = SomeBytes.Length;
                    Stream newStream = req.GetRequestStream();
                    newStream.Write(SomeBytes, 0, SomeBytes.Length);
                    newStream.Close();
                }
                else
                {
                    req.ContentLength = 0;
                }
                res = (HttpWebResponse)req.GetResponse();
                Stream ReceiveStream = res.GetResponseStream();
                //				Encoding encode = System.Text.Encoding.Default;//GetEncoding("utf-8");
                string encodeheader = res.ContentType;
                string encodestr = System.Text.Encoding.Default.HeaderName;
                if ((encodeheader.IndexOf("charset=") >= 0) && (encodeheader.IndexOf("charset=GBK") == -1) && (encodeheader.IndexOf("charset=gbk") == -1))
                {
                    int i = encodeheader.IndexOf("charset=");
                    encodestr = encodeheader.Substring(i + 8);
                }
                Encoding encode = System.Text.Encoding.GetEncoding(encodestr);//GetEncoding("utf-8");

                StreamReader sr = new StreamReader(ReceiveStream, encode);
                Char[] read = new Char[256];
                int count = sr.Read(read, 0, 256);
                while (count > 0)
                {
                    String str = new String(read, 0, count);
                    strResult += str;
                    count = sr.Read(read, 0, 256);
                }
            }
            catch (Exception e)
            {
                strResult = e.ToString();
            }
            finally
            {
                if (res != null)
                {
                    res.Close();
                }
            }
            return strResult;
        }

        #endregion

        public static decimal GetCurrentPrice(string stocknumber)
        {
            decimal price = 0;

            string url;
            if (stocknumber.StartsWith("6"))
            {
                url = "http://hq.sinajs.cn/list=sh" + stocknumber;
            }
            else
            {
                url = "http://hq.sinajs.cn/list=sz" + stocknumber;
            }

            string strResult = PostUrl(url, "");
            
            if (strResult.Length - stocknumber.Length == 17)
            {
                return price;
            }
            strResult = strResult.Replace("\"", "");
            strResult = strResult.Replace(";", "");
            int equPosition = strResult.IndexOf('=');
            strResult = strResult.Substring(equPosition + 1);
           // MessageBox.Show(strResult);
            try
            {

                string[] strlist = strResult.Split(',');

                price = Convert.ToDecimal(strlist[3]);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine("exception is " + e.ToString());
            }

            return price;
        }
    }
}
