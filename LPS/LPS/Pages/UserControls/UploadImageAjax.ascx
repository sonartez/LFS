<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadImageAjax.ascx.cs" Inherits="LPS.Pages.UserControls.UploadImageAjax" %>


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

//----------edit------------------------------
 
var textUpload=document.getElementById('<%=txtUpload.ClientID%>').value;
if(textUpload=="")
{
    $('#<%=imageUpload.ClientID%>').attr('src', 'NoImage');
    
    $('#<%=hplUpload.ClientID%>').attr('href', 'NoImage');
    $('#<%=hplUpload.ClientID%>').attr('title','No Image' );
    
    $('#<%=hplZoom.ClientID%>').attr('href', 'NoImage');
    $('#<%=hplZoom.ClientID%>').attr('title','No Image' );
    
}
else
{
    $('#<%=imageUpload.ClientID%>').attr('src', '<%=Page.ResolveUrl(TextboxUpload)%>');
    
    $('#<%=hplUpload.ClientID%>').attr('href', '<%=Page.ResolveUrl(TextboxUpload)%>');
    $('#<%=hplUpload.ClientID%>').attr('title',get_filename(textUpload));
    
    $('#<%=hplZoom.ClientID%>').attr('href', '<%=Page.ResolveUrl(TextboxUpload)%>');
    $('#<%=hplZoom.ClientID%>').attr('title',get_filename(textUpload));
    
}			  

//Fancybox  	  
$('#<%=hplUpload.ClientID%>').attr('class', 'fancybox_image');
$('#<%=hplUpload.ClientID%>').attr('rel', 'group');
$('#<%=hplUpload.ClientID%>').fancybox({
        'titleShow'     : true,
        'overlayShow'			: false,
        'zoomSpeedIn'			: 600,
        'zoomSpeedOut'			: 500,
        'easingIn'      : 'easeOutBack',
        'easingOut'     : 'easeInBack',
        'transitionIn'	: 'elastic',
        'transitionOut'	: 'elastic'
    }); 
 
 //Fancybox  	  
$('#<%=hplZoom.ClientID%>').attr('class', 'fancybox_image');
$('#<%=hplZoom.ClientID%>').attr('rel', 'group');
$('#<%=hplZoom.ClientID%>').fancybox({
        'titleShow'     : true,
        'overlayShow'			: false,
        'zoomSpeedIn'			: 600,
        'zoomSpeedOut'			: 500,
        'easingIn'      : 'easeOutBack',
        'easingOut'     : 'easeInBack',
        'transitionIn'	: 'elastic',
        'transitionOut'	: 'elastic'
    }); 
 

		      
			 
	
//-----------edit---------------------------------------



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
		    button.text('Uploading');				
	    	    }
		    }, 200);
	    },
	
	onComplete: function(file, response){
	    button.text('Chọn file tải lên');
		window.clearInterval(interval);
						
		// enable upload button
		this.enable();
			
		// view
		$('#<%=imageUpload.ClientID%>').attr('src', '<%=Page.ResolveUrl(UploadFolder)%>'+file_get(file)+'<%=guidId%>'+'.'+file_get_ext(file));
		$('#<%=txtUpload.ClientID%>').attr('value', '<%=UploadFolder%>'+file_get(file)+'<%=guidId%>'+'.'+file_get_ext(file));
		
		$('#<%=hplUpload.ClientID%>').attr('href', '<%=Page.ResolveUrl(UploadFolder)%>'+file_get(file)+'<%=guidId%>'+'.'+file_get_ext(file));
		$('#<%=hplUpload.ClientID%>').attr('title', file_get(file)+'<%=guidId%>'+'.'+file_get_ext(file));
		$('#<%=hplZoom.ClientID%>').attr('href', '<%=Page.ResolveUrl(UploadFolder)%>'+file_get(file)+'<%=guidId%>'+'.'+file_get_ext(file));
		$('#<%=hplZoom.ClientID%>').attr('title', file_get(file)+'<%=guidId%>'+'.'+file_get_ext(file));
		
		
		
		//Fancybox
		$('#<%=hplUpload.ClientID%>').attr('class', 'fancybox_image');
		$('#<%=hplUpload.ClientID%>').attr('rel', 'group');
		$('#<%=hplUpload.ClientID%>').fancybox({
		        'titleShow'     : true,
		        'overlayShow'			: false,
		        'zoomSpeedIn'			: 600,
		        'zoomSpeedOut'			: 500,
		        'easingIn'      : 'easeOutBack',
		        'easingOut'     : 'easeInBack',
		        'transitionIn'	: 'elastic',
		        'transitionOut'	: 'elastic'
	        });
	        
	    //Fancybox
		$('#<%=hplZoom.ClientID%>').attr('class', 'fancybox_image');
		$('#<%=hplZoom.ClientID%>').attr('rel', 'group');
		$('#<%=hplZoom.ClientID%>').fancybox({
		        'titleShow'     : true,
		        'overlayShow'			: false,
		        'zoomSpeedIn'			: 600,
		        'zoomSpeedOut'			: 500,
		        'easingIn'      : 'easeOutBack',
		        'easingOut'     : 'easeInBack',
		        'transitionIn'	: 'elastic',
		        'transitionOut'	: 'elastic'
	        });    
		
		
		
		
					 
			  
			 
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
        if (confirm("Are your sure to delete this Image ?")) { 
            $.ajax({ 
 	            type: "POST", 
 	            url: "UserControls/DeleteFileHandler.ashx?fileUrl="+fileUrl, 
 	            data: "{'fileUrl': '" + fileUrl + "'}",   
 	                        
 	            contentType: "application/json; charset=utf-8", 
 	            dataType: "json", 
 	            success: function() { 
 	            $('#<%=imageUpload.ClientID%>').attr('src','NoImage');
 	            $("#<%=txtUpload.ClientID%>").attr('value','');	
 	            
 	            $('#<%=hplUpload.ClientID%>').attr('href','NoImage');
 	            $('#<%=hplUpload.ClientID%>').attr('title','NoImage');
 	            $('#<%=hplZoom.ClientID%>').attr('href','NoImage');
 	            $('#<%=hplZoom.ClientID%>').attr('title','NoImage');
 	            
 	            //Fancybox
 	            $('#<%=hplUpload.ClientID%>').attr('class', 'fancybox_image');
 	            $('#<%=hplUpload.ClientID%>').attr('rel', 'group'); 
		        $('#<%=hplUpload.ClientID%>').fancybox({
		                'titleShow'     : true,
		                'overlayShow'			: false,
		                'zoomSpeedIn'			: 600,
		                'zoomSpeedOut'			: 500,
		                'easingIn'      : 'easeOutBack',
		                'easingOut'     : 'easeInBack',
		                'transitionIn'	: 'elastic',
		                'transitionOut'	: 'elastic'
	                });
                //Fancybox
 	            $('#<%=hplZoom.ClientID%>').attr('class', 'fancybox_image');
 	            $('#<%=hplZoom.ClientID%>').attr('rel', 'group'); 
		        $('#<%=hplZoom.ClientID%>').fancybox({
		                'titleShow'     : true,
		                'overlayShow'			: false,
		                'zoomSpeedIn'			: 600,
		                'zoomSpeedOut'			: 500,
		                'easingIn'      : 'easeOutBack',
		                'easingOut'     : 'easeInBack',
		                'transitionIn'	: 'elastic',
		                'transitionOut'	: 'elastic'
	                });	                
	                
	                
	                
	                
 	                         },
 	            
 	            error: function () {
 	            $('#<%=imageUpload.ClientID%>').attr('src','NoImage');
 	            $("#<%=txtUpload.ClientID%>").attr('value','');	
 	            
 	            $('#<%=hplUpload.ClientID%>').attr('href','NoImage');
 	            $('#<%=hplUpload.ClientID%>').attr('title','NoImage');
 	            $('#<%=hplZoom.ClientID%>').attr('href','NoImage');
 	            $('#<%=hplZoom.ClientID%>').attr('title','NoImage');
 	            
 	            
 	            //Fancybox
 	            $('#<%=hplUpload.ClientID%>').attr('class', 'fancybox_image');
 	            $('#<%=hplUpload.ClientID%>').attr('rel', 'group'); 
		        $('#<%=hplUpload.ClientID%>').fancybox({
		                'titleShow'     : true,
		                'overlayShow'			: false,
		                'zoomSpeedIn'			: 600,
		                'zoomSpeedOut'			: 500,
		                'easingIn'      : 'easeOutBack',
		                'easingOut'     : 'easeInBack',
		                'transitionIn'	: 'elastic',
		                'transitionOut'	: 'elastic'
	                });
	                
	             //Fancybox
 	            $('#<%=hplZoom.ClientID%>').attr('class', 'fancybox_image');
 	            $('#<%=hplZoom.ClientID%>').attr('rel', 'group'); 
		        $('#<%=hplZoom.ClientID%>').fancybox({
		                'titleShow'     : true,
		                'overlayShow'			: false,
		                'zoomSpeedIn'			: 600,
		                'zoomSpeedOut'			: 500,
		                'easingIn'      : 'easeOutBack',
		                'easingOut'     : 'easeInBack',
		                'transitionIn'	: 'elastic',
		                'transitionOut'	: 'elastic'
	                });   
	                
	                
	                
	                
 	            
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
       <a id="hplUpload" runat="server" style="float:left; padding-right:10px;">
            <img alt="Không có ảnh" src="" id="imageUpload" runat="server" style="border:1px #CCCCCC solid; width:150px; height:80px; color:Blue;" />
       </a>
        
       <a id="hplZoom" runat="server" style="float:left; height:80px; line-height:80px; display:inline-block;">
       + Zoom
       </a> 
    </div>
    <div style="float:left; width:100%;margin-bottom:2px;">
        <input id="txtUpload" type="text" runat="server" class="form-control" style="color:Blue;" readonly="readonly" alt="no-image" />
    </div>
    <div style="float:left; width:100%;margin-bottom:2px;">
        <div id="btnUploadItem" runat="server" class="button" style="float:left;display:inline; height:16px; line-height:16px; width:110px; text-align:center; vertical-align:middle; cursor:hand;" >
         Select a image
        </div>
        <div id="btnDeleteItem" runat="server" class="button" style="float:left;margin-left:10px; display:inline;  height:16px; line-height:16px; width:110px; text-align:center; vertical-align:middle; cursor:hand;" >
         Delete image 
        </div>
        
    </div>
    
</div>