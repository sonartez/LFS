<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadFileAjax.ascx.cs" Inherits="CMS_UserControls_UploadFileAjax" %>




<script type="text/javascript"> 

function file_get_ext(filename)
{
    return typeof filename != "undefined" ? filename.substring(filename.lastIndexOf(".")+1, filename.length).toLowerCase() : false;
}
    
function file_get(filename)
{
     return typeof filename != "undefined" ?filename.substring(0,filename.lastIndexOf(".")).toLowerCase() : false;
}

function get_filename(url)
{
    return url.substring(url.lastIndexOf('/') + 1); 
}

$(document).ready(function(){



//------------upload-----------------------
var button = $('#<%=btnUploadItem.ClientID%>'), interval;
new AjaxUpload(button,{
    action: 'UserControls/UploadFileHandler.ashx?folderType=<%=UploadFolder%>&guidId=<%=guidId%>', 
	onSubmit : function(file, ext){
	
	    // change button text, when user selects file			
	    button.text('Đang tải');
    			
	    // If you want to allow uploading only 1 file at time,you can disable upload button
	    this.disable();
    			
	    // Uploding -> Uploading. -> Uploading...
	    interval = window.setInterval(function(){
		    var text = button.text();
		    if (text.length < 20){
		    button.text(text + '.');					
		    } else {
		    button.text('Đang tải');				
	    	    }
		    }, 200);
	    },
	
	onComplete: function(file, response){
	    button.text('Chọn file tải lên');
		window.clearInterval(interval);
						
		// enable upload button
		this.enable();
			
		// view
		
		$('#<%=txtUpload.ClientID%>').attr('value', '<%=UploadFolder%>'+file_get(file)+'<%=guidId%>'+'.'+file_get_ext(file));
			  
			 
		}
	});
	
//------------upload-----------------------	
	

//------------delete-----------------------		
$("#<%=btnDeleteItem.ClientID%>").click(function() { 
 	                
    var strConfirm=new String("");
    var fileName=new String("");
    var fileUrl=new String("");
 	                
    fileUrl= $("#<%=txtUpload.ClientID%>").val();
    fileName = fileUrl.substring(fileUrl.lastIndexOf('/')+1);
 	                  
    // Ask user's confirmation before delete records 
    if (fileUrl!="")
    {
        if (confirm("Bạn muốn xóa FILE khỏi server ?")) { 
            $.ajax({ 
 	            type: "POST", 
 	            url: "UserControls/DeleteFileHandler.ashx?fileUrl="+fileUrl, 
 	            data: "{'fileUrl': '" + fileUrl + "'}",   
 	                        
 	            contentType: "application/json; charset=utf-8", 
 	            dataType: "json", 
 	            success: function() { 
 	            
 	            $("#<%=txtUpload.ClientID%>").attr('value','');	
 	            
	                
 	                         },
 	            
 	            error: function () {
 	            
 	            $("#<%=txtUpload.ClientID%>").attr('value','');	
	                
	                
	                
 	            
                             }
 	            }); 
 	  
            } 
        }
 	    return false; 
    }); 	


//------------delete-----------------------	




	


});
</script>


<div style="float:left; width:100%;"> 
     
    <div style="float:left; width:100%;margin-bottom:2px;">
        <input id="txtUpload" type="text" runat="server" class="textbox-aspnet width50per" style="color:Blue;" readonly="readonly" />
    </div>
    <div style="float:left; width:100%;margin-bottom:2px;">
        <div id="btnUploadItem" runat="server" class="button" style="float:left;display:inline; height:16px; line-height:16px; width:110px; text-align:center; vertical-align:middle; cursor:hand;" >
         Chọn file tải lên
        </div>
        <div id="btnDeleteItem" runat="server" class="button" style="float:left;margin-left:10px; display:inline;  height:16px; line-height:16px; width:110px; text-align:center; vertical-align:middle; cursor:hand;" >
         Xóa file 
        </div>
    </div>
    
</div>