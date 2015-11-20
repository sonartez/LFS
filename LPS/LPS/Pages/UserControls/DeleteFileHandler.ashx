<%@ WebHandler Language="C#" Class="DeleteFileHandler" %>

using System;
using System.Web;
using System.IO;

public class DeleteFileHandler : IHttpHandler {

    string strResponse = "error";
    
    public void ProcessRequest (HttpContext context) 
    {
        string fileUrl = context.Request.QueryString.Get("fileUrl");
        string fileToDelete = context.Server.MapPath(fileUrl);
        try
        {
            File.Delete(fileToDelete);
            strResponse = "success";
        }
        catch (Exception ex) { throw ex; }

        context.Response.ContentType = "text/plain";
        context.Response.Write(strResponse);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}