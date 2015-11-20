<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrlUploadImageAjax.ascx.cs" Inherits="LPS.Pages.UserControls.WebUserControl1" %>



<script type="text/javascript">

    function file_get_ext(filename) {
        return typeof filename != "undefined" ? filename.substring(filename.lastIndexOf(".") + 1, filename.length).toLowerCase() : false;
    }

    function file_get(filename) {
        return typeof filename != "undefined" ? filename.substring(0, filename.lastIndexOf(".")).toLowerCase() : false;
    }

    function get_filename(url) {
        return url.substring(url.lastIndexOf('/') + 1);
    }

    $(document).ready(function () {

        //----------edit------------------------------

        var textUpload = document.getElementById('<%=txtUpload.ClientID%>').value;
        if (textUpload == "") {
            $('#<%=imageUpload.ClientID%>').attr('src', '../../Content/NoImage-Currently.gif');
        }
        else {
            $('#<%=imageUpload.ClientID%>').attr('src', '<%=Page.ResolveUrl(TextboxUpload)%>');
        }
        //-----------edit---------------------------------------
        //------------upload-----------------------
        var button = $('#<%=btnUploadItem.ClientID%>'), interval;
        new AjaxUpload(button, {
            action: 'UserControls/UploadFileHandler.ashx?folderType=<%=UploadFolder%>&guidId=<%=guidId%>',
            onSubmit: function (file, ext) {

                // change button text, when user selects file			
                button.text('Đang tải');

                // If you want to allow uploading only 1 file at time,you can disable upload button
                this.disable();

                // Uploding -> Uploading. -> Uploading...
                interval = window.setInterval(function () {
                    var text = button.text();
                    if (text.length < 20) {
                        button.text(text + '.');
                    } else {
                        button.text('Uploading');
                    }
                }, 200);
            },

            onComplete: function (file, response) {
                button.text('Select a image');
                window.clearInterval(interval);

                // enable upload button
                this.enable();

                // view
                $('#<%=imageUpload.ClientID%>').attr('src', '<%=Page.ResolveUrl(UploadFolder)%>' + file_get(file) + '<%=guidId%>' + '.' + file_get_ext(file));
            $('#<%=txtUpload.ClientID%>').attr('value', '<%=UploadFolder%>' + file_get(file) + '<%=guidId%>' + '.' + file_get_ext(file));
        }
        });

        //------------upload-----------------------	


        //------------delete-----------------------		
        $("#<%=btnDeleteItem.ClientID%>").click(function () {

            var strConfirm = new String("");
            var fileName = new String("");
            var fileUrl = new String("");

            fileUrl = $("#<%=txtUpload.ClientID%>").val();
            fileName = fileUrl.substring(fileUrl.lastIndexOf('/') + 1);

            // Ask user's confirmation before delete records 
            if (fileUrl != "") {
                if (confirm("Are your sure to delete this Image ?")) {
                    $.ajax({
                        type: "POST",
                        url: "UserControls/DeleteFileHandler.ashx?fileUrl=" + fileUrl,
                        data: "{'fileUrl': '" + fileUrl + "'}",

                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function () {
                            $('#<%=imageUpload.ClientID%>').attr('src', '../../Content/NoImage-Currently.gif');
                        $("#<%=txtUpload.ClientID%>").attr('value', '');
                    },

                    error: function () {
                        $('#<%=imageUpload.ClientID%>').attr('src', '../../Content/NoImage-Currently.gif');
                    $("#<%=txtUpload.ClientID%>").attr('value', '');

               }
                });

        }
    }
            return false;
        });


        //------------delete-----------------------	

    });
</script>


<div style="float: left; width: 100%;">
    <div style="float: left; width: 100%; margin-bottom: 2px;">
        <img alt="No image" src="~/Content/NoImage-Currently.gif" id="imageUpload" runat="server" class="img-thumbnail" style="max-width: 300px" />
    </div>
    <div style="float: left; width: 100%; margin-bottom: 2px;">
        <input id="txtUpload" type="text" runat="server" class="form-control" style="color: Blue;" readonly="readonly" alt="no-image" />
    </div>
    <div style="float: left; width: 100%; margin-bottom: 2px;">
        <div id="btnUploadItem" runat="server" class="btn btn-default">
            Select a image
        </div>
        <div id="btnDeleteItem" runat="server" class="btn btn-default">
            Delete image 
        </div>

    </div>

</div>
