using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace LPS.Core
{

    /// <summary>
    /// Summary description for Common
    /// </summary>
    public class Common
    {
        public Common()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //CAC HAM HIEN THI NGAY THANG NAM
        //Ngaythang: Thứ ba, 22/06/2010, 15:49 GMT+7
        public string DisplayDateTimeVn_Type1(DateTime dateObject)
        {
            string dayofweek = string.Empty;
            string day = string.Empty;
            string month = string.Empty;
            string year = string.Empty;
            string hour = string.Empty;
            string minute = string.Empty;

            switch (dateObject.DayOfWeek.ToString().ToLower())
            {
                case "monday": dayofweek = "Thứ hai"; break;
                case "tuesday": dayofweek = "Thứ ba"; break;
                case "wednesday": dayofweek = "Thứ tư"; break;
                case "thursday": dayofweek = "Thứ năm"; break;
                case "friday": dayofweek = "Thứ sáu"; break;
                case "saturday": dayofweek = "Thứ bảy"; break;
                case "sunday": dayofweek = "Chủ nhật"; break;
            }


            day = dateObject.Day < 10 ? "0" + dateObject.Day.ToString() : dateObject.Day.ToString();
            month = dateObject.Month < 10 ? "0" + dateObject.Month.ToString() : dateObject.Month.ToString();
            year = dateObject.Year.ToString();
            hour = int.Parse(dateObject.ToString("HH")).ToString();
            hour = int.Parse(hour) < 10 ? "0" + hour : hour;
            minute = dateObject.Minute < 10 ? "0" + dateObject.Minute.ToString() : dateObject.Minute.ToString();

            //Thứ ba, 22/06/2010, 15:49 GMT+7
            StringBuilder str = new StringBuilder(string.Empty);
            str.Append(dayofweek);
            str.Append(", ");
            str.Append(day);
            str.Append("/");
            str.Append(month);
            str.Append("/");
            str.Append(year);
            str.Append(", ");
            str.Append(hour);
            str.Append(":");
            str.Append(minute);
            str.Append(" GMT+7");
            return str.ToString();
        }

        //Ngaythang: Monday, 06/22/2010, 15:49 GMT+7
        public string DisplayDateTimeUs_Type1(DateTime dateObject)
        {
            string dayofweek = string.Empty;
            string day = string.Empty;
            string month = string.Empty;
            string year = string.Empty;
            string hour = string.Empty;
            string minute = string.Empty;


            dayofweek = dateObject.DayOfWeek.ToString();
            day = dateObject.Day < 10 ? "0" + dateObject.Day.ToString() : dateObject.Day.ToString();
            month = dateObject.Month < 10 ? "0" + dateObject.Month.ToString() : dateObject.Month.ToString();
            year = dateObject.Year.ToString();
            hour = int.Parse(dateObject.ToString("HH")).ToString();
            hour = int.Parse(hour) < 10 ? "0" + hour : hour;
            minute = dateObject.Minute < 10 ? "0" + dateObject.Minute.ToString() : dateObject.Minute.ToString();

            //Monday, 06/22/2010, 15:49 GMT+7
            StringBuilder str = new StringBuilder(string.Empty);
            str.Append(dayofweek);
            str.Append(", ");
            str.Append(month);
            str.Append("/");
            str.Append(day);
            str.Append("/");
            str.Append(year);
            str.Append(", ");
            str.Append(hour);
            str.Append(":");
            str.Append(minute);
            str.Append(" GMT+7");
            return str.ToString();
        }

        public string DisplayDateTime_DD_MM_YYYY(DateTime dateObject)
        {
            return dateObject.ToString("dd/MM/yyyy");
        }

        public string DisplayDateTime_DD_MM_YYYY_dot(DateTime dateObject)
        {
            string str = dateObject.ToString("dd/MM/yyyy");
            return str.Replace("/", ".");
        }

        public string DisplayDateTime_DD_MM_YYYY_dash(DateTime dateObject)
        {
            string str = dateObject.ToString("dd/MM/yyyy");
            return str.Replace("/", "-");
        }

        public string DisplayDateTimeUs_MM_DD_YYYY(DateTime dateObject)
        {
            return dateObject.ToString("MM/dd/yyyy");
        }

        public string DisplayDateTimeUs_MM_DD_YYYY_dot(DateTime dateObject)
        {
            string str = dateObject.ToString("MM/dd/yyyy");
            return str.Replace("/", ".");
        }

        public string DisplayDateTimeUs_MM_DD_YYYY_dash(DateTime dateObject)
        {
            string str = dateObject.ToString("MM/dd/yyyy");
            return str.Replace("/", "-");
        }

        public string DisplayDayOfWeek(string enDayOfWeek)
        {
            string str = string.Empty;
            switch (enDayOfWeek.ToLower())
            {
                case "monday": str = "Thứ hai"; break;
                case "tuesday": str = "Thứ ba"; break;
                case "wednesday": str = "Thứ tư"; break;
                case "thursday": str = "Thứ năm"; break;
                case "friday": str = "Thứ sáu"; break;
                case "saturday": str = "Thứ bảy"; break;
                case "sunday": str = "Chủ nhật"; break;
            }

            return str;
        }

        //Insert & Update
        //vn 26/02/2010 04:44:17 PM --> us 02/26/2010 04:44:17 PM
        public string DisplayDateTimeUS(string vnDatetime)
        {
            IFormatProvider provider = new CultureInfo("vi-Vn", true);
            String datetime = vnDatetime.Trim();
            DateTime dt = DateTime.Parse(datetime, provider, DateTimeStyles.NoCurrentDateDefault);

            //vn 26/02/2010 04:44:17 PM --> us 02/26/2010 04:44:17 PM
            string day = string.Empty;
            string month = string.Empty;
            string year = string.Empty;
            string hour = string.Empty;
            string minute = string.Empty;
            string second = string.Empty;
            string ampm = string.Empty;

            day = dt.Day < 10 ? "0" + dt.Day.ToString() : dt.Day.ToString();
            month = dt.Month < 10 ? "0" + dt.Month.ToString() : dt.Month.ToString();
            year = dt.Year.ToString();
            hour = dt.Hour < 10 ? "0" + dt.Hour.ToString() : dt.Hour.ToString();
            minute = dt.Minute < 10 ? "0" + dt.Minute.ToString() : dt.Minute.ToString();
            second = dt.Second < 10 ? "0" + dt.Second.ToString() : dt.Second.ToString();
            ampm = dt.ToString("tt");

            StringBuilder str = new StringBuilder(string.Empty);
            str.Append(month);
            str.Append("/");
            str.Append(day);
            str.Append("/");
            str.Append(year);
            str.Append(" ");
            str.Append(hour);
            str.Append(":");
            str.Append(minute);
            str.Append(":");
            str.Append(second);
            str.Append(" ");
            str.Append(ampm);

            return str.ToString();
        }

        //Select
        //us 02/26/2010 04:44:17 PM --> vn 26/02/2010 04:44:17 PM
        public string DisplayDateTimeVN(DateTime usDatetime)
        {
            //us 02/26/2010 04:44:17 PM --> vn 26/02/2010 04:44:17 PM
            string day = string.Empty;
            string month = string.Empty;
            string year = string.Empty;
            string hour = string.Empty;
            string minute = string.Empty;
            string second = string.Empty;
            string ampm = string.Empty;

            day = usDatetime.Day < 10 ? "0" + usDatetime.Day.ToString() : usDatetime.Day.ToString();
            month = usDatetime.Month < 10 ? "0" + usDatetime.Month.ToString() : usDatetime.Month.ToString();
            year = usDatetime.Year.ToString();
            hour = usDatetime.Hour < 10 ? "0" + usDatetime.Hour.ToString() : usDatetime.Hour.ToString();
            minute = usDatetime.Minute < 10 ? "0" + usDatetime.Minute.ToString() : usDatetime.Minute.ToString();
            second = usDatetime.Second < 10 ? "0" + usDatetime.Second.ToString() : usDatetime.Second.ToString();
            ampm = usDatetime.ToString("tt");

            StringBuilder str = new StringBuilder(string.Empty);
            str.Append(day);
            str.Append("/");
            str.Append(month);
            str.Append("/");
            str.Append(year);
            str.Append(" ");
            str.Append(hour);
            str.Append(":");
            str.Append(minute);
            str.Append(":");
            str.Append(second);
            str.Append(" ");
            str.Append(ampm);

            return str.ToString();
        }



        //Kiem tra chuoi nhap vao co dung dinh dang dd/mm/yyyy
        public bool ValidDate_DD_MM_YYYY(string strDate)
        {
            DateTime date;
            if (DateTime.TryParseExact(strDate, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out date))
                return true;
            else
                return false;
        }


        //CAC HANG DINH DANG CHUOI

        //Loai bo dau ' cho kieu du lieu Nvarchar truong hop viet cau lenh sql truc tiep khong parameter
        public string TextFormat(string textInput)
        {
            return textInput.Trim().Replace("'", "''");
        }

        //Loai bo ma HTML
        public string RemoveHTML(string strHtml)
        {
            string resHTML = System.Text.RegularExpressions.Regex.Replace(strHtml, @"<(.|\n)*?>", string.Empty);
            return resHTML;
        }

        //Loai bo ma HTML
        public string RemoveHTML(string strHtml, int length)
        {
            string resHTML = System.Text.RegularExpressions.Regex.Replace(strHtml, @"<(.|\n)*?>", string.Empty);
            resHTML = (resHTML.Length >= length) ? resHTML.Substring(0, length) : resHTML;
            return resHTML;
        }


        //Kiem tra chuoi la kieu so
        public bool IsNumeric(string strVal)
        {
            try
            {
                double doub = double.Parse(strVal);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Kiem tra chuoi rong hoac khoang trang
        public static bool IsNullOrWhiteSpace(string s)
        {
            if (s == null || "".Equals(s.Trim()))
                return (true);
            return (false);
        }

        // Thay the Url ..  thanh ~ 
        public string ConvertToHtmlUrl(string aspnetUrl)
        {
            aspnetUrl = aspnetUrl.Replace("~", "..");
            return aspnetUrl;
        }

        // Thay the Url ~  thanh .. 
        public string ConvertToAspNetUrl(string htmlUrl)
        {
            htmlUrl = htmlUrl.Replace("..", "~");
            return htmlUrl;
        }


        //------------------ xu ly chu co dau

        //------------------#Loai bo dau tieng viet SEO Rewrite Url---
        public string GenerateRewriteUrl(string titleText)
        {
            string strTitle = titleText.Trim().ToString();

            //Trim Start and End Spaces.
            strTitle = strTitle.Trim();

            //Trim "-" Hyphen
            strTitle = strTitle.Trim('-');

            strTitle = strTitle.ToLower();
            //Replace . with - hyphen
            strTitle = strTitle.Replace(".", "-");

            //Loai bo ky tu dat biet.
            char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();

            //Loai bo dau tieng viet cac nguyen am a,e,o,i,u,y
            char[] char_a = @"àáạảãâầấậẩẫăằắặẳẵ".ToCharArray();
            char[] char_e = @"èéẹẻẽêềếệểễ".ToCharArray();
            char[] char_i = @"ìíịỉĩ".ToCharArray();
            char[] char_o = @"òóọỏõôồốộổỗơờớợởỡ".ToCharArray();
            char[] char_u = @"ùúụủũưừứựửữ".ToCharArray();
            char[] char_y = @"ỳýỵỷỹ".ToCharArray();
            char[] char_d = @"đ".ToCharArray();

            //Replace Special-Characters
            strTitle = RemoveSignVN(strTitle, chars, string.Empty);
            strTitle = RemoveSignVN(strTitle, char_a, "a");
            strTitle = RemoveSignVN(strTitle, char_e, "e");
            strTitle = RemoveSignVN(strTitle, char_i, "i");
            strTitle = RemoveSignVN(strTitle, char_o, "o");
            strTitle = RemoveSignVN(strTitle, char_u, "u");
            strTitle = RemoveSignVN(strTitle, char_y, "y");
            strTitle = RemoveSignVN(strTitle, char_d, "d");


            //Replace all spaces with one "-" hyphen
            strTitle = strTitle.Replace(" ", "-");

            //Replace multiple "-" hyphen with single "-" hyphen.
            strTitle = strTitle.Replace("--", "-");
            strTitle = strTitle.Replace("---", "-");
            strTitle = strTitle.Replace("----", "-");
            strTitle = strTitle.Replace("-----", "-");
            strTitle = strTitle.Replace("----", "-");
            strTitle = strTitle.Replace("---", "-");
            strTitle = strTitle.Replace("--", "-");

            //Run the code again...
            //Trim Start and End Spaces.
            strTitle = strTitle.Trim();

            //Trim "-" Hyphen
            strTitle = strTitle.Trim('-');


            return strTitle;
        }

        //-----------Loai bo dau tieng viet cho truong hop tim kiem
        public string GenerateANSI(string titleText)
        {
            string strTitle = titleText.Trim().ToString();

            //Trim Start and End Spaces.
            strTitle = strTitle.Trim();

            //Trim "-" Hyphen
            strTitle = strTitle.Trim('-');

            strTitle = strTitle.ToLower();
            //Replace . with - hyphen
            strTitle = strTitle.Replace(".", "-");

            //Loai bo ky tu dat biet.
            char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();

            //Loai bo dau tieng viet cac nguyen am a,e,o,i,u,y
            char[] char_a = @"àáạảãâầấậẩẫăằắặẳẵ".ToCharArray();
            char[] char_e = @"èéẹẻẽêềếệểễ".ToCharArray();
            char[] char_i = @"ìíịỉĩ".ToCharArray();
            char[] char_o = @"òóọỏõôồốộổỗơờớợởỡ".ToCharArray();
            char[] char_u = @"ùúụủũưừứựửữ".ToCharArray();
            char[] char_y = @"ỳýỵỷỹ".ToCharArray();
            char[] char_d = @"đ".ToCharArray();

            //Replace Special-Characters
            strTitle = RemoveSignVN(strTitle, chars, string.Empty);
            strTitle = RemoveSignVN(strTitle, char_a, "a");
            strTitle = RemoveSignVN(strTitle, char_e, "e");
            strTitle = RemoveSignVN(strTitle, char_i, "i");
            strTitle = RemoveSignVN(strTitle, char_o, "o");
            strTitle = RemoveSignVN(strTitle, char_u, "u");
            strTitle = RemoveSignVN(strTitle, char_y, "y");
            strTitle = RemoveSignVN(strTitle, char_d, "d");

            //Run the code again...
            //Trim Start and End Spaces.
            strTitle = strTitle.Trim();

            return strTitle;
        }

        public string RemoveSignVN(string strReplace, char[] char_vn, string str)
        {
            for (int i = 0; i < char_vn.Length; i++)
            {
                string strChar = char_vn.GetValue(i).ToString();
                if (strReplace.Contains(strChar))
                {
                    strReplace = strReplace.Replace(strChar, str);
                }
            }
            return strReplace;
        }

        //-----------------# xu ly chu co dau

        //Lay so tu trong mot doan van ban----------------
        private int RealCharNum(string strInput)
        {
            string[] a = strInput.Split(' ');
            int count = 0;

            for (int i = 0; i < a.Length; i++)
                if (!string.IsNullOrEmpty(a[i].ToString()))
                    count++;
            return count;
        }
        private string ClearBlank(string[] strInput, int pos)
        {
            int count = 0;
            string result = string.Empty;

            for (int i = 0; i < strInput.Length; i++)
            {
                if (!string.IsNullOrEmpty(strInput[i].ToString()))
                {
                    result += strInput[i].ToString() + " ";
                    count++;
                }

                if (count == pos)
                    break;
            }
            return result;
        }

        //Lay so tu
        public string StringSplitByNum(string strInput, int pos)
        {
            string[] a = strInput.Split(' ');
            string result = string.Empty;
            int count = RealCharNum(strInput);

            if (pos == 0)
                return result;

            result = ClearBlank(a, pos);

            if (count > pos)
                result += "...";

            return result;
        }

        //#---Lay so tu trong mot doan van ban----------------


        //Cat chuoi neu so ky tu vuot qua
        public string StringSplitByNumChar(string strInput, int numCharGet)
        {
            if (strInput.Length > numCharGet)
                strInput = strInput.Substring(0, numCharGet - 3) + "...";
            return strInput;
        }


        //--Dinh dang chuoi hien thi
        //Lay Code la chuoi ngay thang nam gio phut giay hien tai
        //YYYY-MM-DD-HH-MM-SS
        public string GetCodeIsDateTimeNow()
        {
            string day = string.Empty;
            string month = string.Empty;
            string year = string.Empty;
            string hour = string.Empty;
            string minute = string.Empty;
            string second = string.Empty;
            string ampm = string.Empty;

            DateTime usDatetime = DateTime.Now;
            day = usDatetime.Day < 10 ? "0" + usDatetime.Day.ToString() : usDatetime.Day.ToString();
            month = usDatetime.Month < 10 ? "0" + usDatetime.Month.ToString() : usDatetime.Month.ToString();
            year = usDatetime.Year.ToString();
            hour = usDatetime.Hour < 10 ? "0" + usDatetime.Hour.ToString() : usDatetime.Hour.ToString();
            minute = usDatetime.Minute < 10 ? "0" + usDatetime.Minute.ToString() : usDatetime.Minute.ToString();
            second = usDatetime.Second < 10 ? "0" + usDatetime.Second.ToString() : usDatetime.Second.ToString();

            StringBuilder str = new StringBuilder(string.Empty);
            str.Append(year);
            str.Append("-");
            str.Append(month);
            str.Append("-");
            str.Append(day);
            str.Append("-");
            str.Append(hour);
            str.Append("-");
            str.Append(minute);
            str.Append("-");
            str.Append(second);

            return str.ToString();
        }

        //Phan cach hang ngan cho so kieu VN
        //1234567890---> 123.456.789
        public string NumSeparatorThousandVn(string num)
        {
            int i = Convert.ToInt32(num);
            string str = i.ToString("##,#", CultureInfo.CreateSpecificCulture("vi-VN"));
            if (num == "0")
                return "0";
            else
                return str;
        }

        //Phan cach hang ngan cho so kieu EN USA
        //1234567890---> 123,456,789
        public string NumSeparatorThousandUs(string num)
        {
            int i = Convert.ToInt32(num);
            string str = i.ToString("##,#", CultureInfo.CreateSpecificCulture("en-US"));
            if (num == "0")
                return "0";
            else
                return str;
        }



        //valueUsd co gia tri:    #####.00  decimal in database --> 123,456.89
        public string MoneyStringTypeUs(decimal valueUsd)
        {
            return valueUsd.ToString("#,#.##", CultureInfo.CreateSpecificCulture("en-US"));
        }

        //valueVnd co gia tri:    ###### decimal in database --> 123.456.890
        public string MoneyStringTypeVn(decimal valueVnd)
        {
            return valueVnd.ToString("##,#", CultureInfo.CreateSpecificCulture("vi-VN"));
        }

        //--> $123,456.89
        public string MoneyTypeDolar(decimal valueUsd)
        {
            CultureInfo ci = new CultureInfo("en-US");
            return valueUsd.ToString("C", ci);
        }

        //--> 123.456,89đ
        public string MoneyTypeVndComma00(decimal valueVnd)
        {
            CultureInfo ci = new CultureInfo("vi-VN");
            return valueVnd.ToString("C", ci);
        }

        //--> 123.456.789 đ
        public string MoneyTypeVnd(decimal valueVnd)
        {
            return valueVnd.ToString("##,#", CultureInfo.CreateSpecificCulture("vi-VN")) + " ₫ ";
        }

        //Tao chuoi ngau nhien
        //theo ID của tiến trình và ID của tiểu trình, thời gian hệ thống... bao mat
        public string GetRandomString(int length)
        {
            byte[] randomArray = new byte[length];
            string randomString;
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomArray);
            randomString = Convert.ToBase64String(randomArray);
            return randomString;
        }

        //Lay file Fullname (ten va phan mo rong) cua mot url
        public string GetFileFullNameWithUrl(string urlFull)
        {
            string fileName = urlFull.Substring(urlFull.LastIndexOf(@"/") + 1);
            return fileName;
        }

        //Lay file  only name  
        public string GetFileOnlyName(string fileName)
        {
            return fileName.Substring(0, fileName.LastIndexOf("."));
        }

        // Lay file  only Extended  
        public string GetFileOnlyExtended(string fileName)
        {
            return fileName.Substring(fileName.LastIndexOf(".") + 1, fileName.Length);
        }

        public bool CheckStringIsSqlInjection(string strInput)
        {
            strInput = strInput.Trim();
            bool checkStr = false;
            string[] blackList = {"--",";--",";","/*","*/","@@","@",
                                           "char","nchar","varchar","nvarchar",
                                           "alter","begin","cast","create","cursor","declare","delete","drop","end","exec","execute",
                                           "fetch","insert","kill","open",
                                           "select", "sys","sysobjects","syscolumns",
                                           "table","update"};
            for (int i = 0; i < blackList.Length; i++)
            {
                if ((strInput.IndexOf(blackList[i], StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    checkStr = true;
                    break;
                }
            }
            return checkStr;
        }

        public void SendMail(string emailFrom, string emailTo, string subject, string bodyText)
        {
            string error = string.Empty;
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(emailFrom);
                message.To.Add(emailTo);
                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Body = bodyText;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.Normal;
                new SmtpClient("localhost").Send(message);
                message.Dispose();
            }
            catch (Exception ex)
            {
                error = "Loi";
            }
        }


        public string SendMail_LocalSmtp(string emailFrom, string emailTo, string emailCc1, string emailCc2, string emailBcc1, string emailBcc2, string subject, string bodyText)
        {
            string result = string.Empty;
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {
                //message
                message.From = new MailAddress(emailFrom);
                message.To.Add(new MailAddress(emailTo));

                // CC and BCC optional
                if (!string.IsNullOrEmpty(emailCc1) && emailCc1 != emailTo)
                    message.CC.Add(new MailAddress(emailCc1));
                if (!string.IsNullOrEmpty(emailCc2) && emailCc2 != emailCc1 && emailCc2 != emailTo)
                    message.CC.Add(new MailAddress(emailCc2));
                if (!string.IsNullOrEmpty(emailBcc1) && emailBcc1 != emailCc2 && emailBcc1 != emailCc1 && emailBcc1 != emailTo)
                    message.Bcc.Add(new MailAddress(emailBcc1));
                if (!string.IsNullOrEmpty(emailBcc2) && emailBcc2 != emailBcc1 && emailBcc2 != emailCc2 && emailBcc2 != emailCc1 && emailBcc2 != emailTo)
                    message.Bcc.Add(new MailAddress(emailBcc2));

                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Body = bodyText;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;

                client.Host = "localhost";
                client.Port = 25;
                client.Send(message);

                result = "OK";
            }
            catch (Exception ex) { result = ex.ToString(); }
            finally { message.Dispose(); }

            return result;
        }
        public string SendMail_OtherSmtp(string smtpHost, string smtpPort, string emailFrom, string emailTo, string emailCc1, string emailCc2, string emailBcc1, string emailBcc2, string subject, string bodyText)
        {
            string result = string.Empty;
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {
                //message
                message.From = new MailAddress(emailFrom);
                message.To.Add(new MailAddress(emailTo));

                // CC and BCC optional
                if (!string.IsNullOrEmpty(emailCc1) && emailCc1 != emailTo)
                    message.CC.Add(new MailAddress(emailCc1));
                if (!string.IsNullOrEmpty(emailCc2) && emailCc2 != emailCc1 && emailCc2 != emailTo)
                    message.CC.Add(new MailAddress(emailCc2));
                if (!string.IsNullOrEmpty(emailBcc1) && emailBcc1 != emailCc2 && emailBcc1 != emailCc1 && emailBcc1 != emailTo)
                    message.Bcc.Add(new MailAddress(emailBcc1));
                if (!string.IsNullOrEmpty(emailBcc2) && emailBcc2 != emailBcc1 && emailBcc2 != emailCc2 && emailBcc2 != emailCc1 && emailBcc2 != emailTo)
                    message.Bcc.Add(new MailAddress(emailBcc2));

                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Body = bodyText;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;

                client.Host = smtpHost;
                client.Port = Convert.ToInt32(smtpHost);

                client.Send(message);
                result = "OK";
            }
            catch (Exception ex) { result = ex.ToString(); }
            finally { message.Dispose(); }
            return result;
        }
        public string SendMail_OtherSmtpAuthentication(string smtpHost, string smtpPort, bool smtpEnableSsl, string userName, string password, string emailFrom, string emailTo, string emailCc1, string emailCc2, string emailBcc1, string emailBcc2, string subject, string bodyText)
        {
            string result = string.Empty;
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {
                //Smtp
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = smtpEnableSsl;
                client.Host = smtpHost;
                client.Port = Convert.ToInt32(smtpPort);

                //Smtp authentication
                NetworkCredential credentials = new NetworkCredential(userName, password);
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;


                //message
                message.From = new MailAddress(emailFrom);
                message.To.Add(new MailAddress(emailTo));

                // CC and BCC optional
                if (!string.IsNullOrEmpty(emailCc1) && emailCc1 != emailTo)
                    message.CC.Add(new MailAddress(emailCc1));
                if (!string.IsNullOrEmpty(emailCc2) && emailCc2 != emailCc1 && emailCc2 != emailTo)
                    message.CC.Add(new MailAddress(emailCc2));
                if (!string.IsNullOrEmpty(emailBcc1) && emailBcc1 != emailCc2 && emailBcc1 != emailCc1 && emailBcc1 != emailTo)
                    message.Bcc.Add(new MailAddress(emailBcc1));
                if (!string.IsNullOrEmpty(emailBcc2) && emailBcc2 != emailBcc1 && emailBcc2 != emailCc2 && emailBcc2 != emailCc1 && emailBcc2 != emailTo)
                    message.Bcc.Add(new MailAddress(emailBcc2));

                message.Subject = emailFrom + subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Body = bodyText;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;



                client.Send(message);
                result = "OK";
            }
            catch (Exception ex) { result = ex.ToString(); }
            finally { message.Dispose(); }
            return result;
        }
       

        public string[] GetClientInfo()
        {
            string[] info = new string[3];
            string clientIP = string.Empty;
            string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ip))
            {
                string[] ipRange = ip.Trim().Split(',');
                int le = ipRange.Length - 1;
                clientIP = ipRange[le];
            }
            else
            {
                clientIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            info[0] = clientIP;



            info[1] = HttpContext.Current.Request.UserHostName;

            info[2] = HttpContext.Current.Request.Browser.Browser;

            return info;
        }


        // Account type
        public static string TypeAdmin = "Administrator";
        public static string TypeUser = "User";
    }


}