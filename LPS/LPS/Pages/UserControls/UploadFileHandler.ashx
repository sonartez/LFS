<%@ WebHandler Language="C#" Class="UploadFileHandler" %>

using System;
using System.Web;
using System.IO;

public class UploadFileHandler : IHttpHandler {

    string strResponse = "error";
    string path = string.Empty;
    string guidId = string.Empty;
    string fileNameWithoutExtension = string.Empty;
    string extension = string.Empty;
    string fileNameNew = string.Empty;
    string strSaveLocation = string.Empty;
    System.Text.StringBuilder temp;
    
    public void ProcessRequest (HttpContext context) 
    {
        path = context.Request.QueryString.Get("folderType");

        try
        {

            fileNameWithoutExtension = Path.GetFileNameWithoutExtension(context.Request.Files[0].FileName);
            extension = Path.GetExtension(context.Request.Files[0].FileName).ToLower();
            guidId = context.Request.QueryString.Get("guidId");

            temp = new System.Text.StringBuilder(string.Empty);
            temp.Append(fileNameWithoutExtension);
            temp.Append(guidId);
            temp.Append(extension);
            fileNameNew =temp.ToString();

            temp = new System.Text.StringBuilder(string.Empty);
            temp.Append(path);
            temp.Append(fileNameNew);
            strSaveLocation = context.Server.MapPath(temp.ToString());
            context.Request.Files[0].SaveAs(strSaveLocation);
            strResponse = "success";
        }
        catch
        {

        }

        context.Response.ContentType = "text/plain";
        context.Response.Write(strResponse);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}