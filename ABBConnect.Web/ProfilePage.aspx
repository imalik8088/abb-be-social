<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProfilePage.aspx.cs" Inherits="_UserPage" %>

<%@ Register Src="controls/FeedPage.ascx" TagName="FeedPage" TagPrefix="abbConnect" %>
<%@ Register Src="controls/NewFeedPagelet.ascx" TagName="NewFeedPagelet" TagPrefix="abbConnect" %>
<%@ Register Src="controls/Commentlet.ascx" TagName="Commentlet" TagPrefix="abbConnect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">        
        <div class="col-md-6">
            <div class="feed-header">
                <div class="form-inline">
                    <div class="form-group">                
                        <h3><span class="glyphicon glyphicon-link"></span>Profile info</h3>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="feed-header">
                <div class="form-inline">
                    <div class="form-group">                
                        <h3><span class="glyphicon glyphicon-link"></span>User feeds</h3>
                    </div>
                    <div class="form-group button-group">
                        <div class="btn-group">
                            <button type="button" class="btn btn-warning">Feed Selection</button>
                            <button type="button" class="btn btn-warning dropdown-toggle" data-toggle="dropdown">
                                <span class="caret"></span>
                                <span class="sr-only">Toggle Dropdown</span>
                            </button>
                            <ul class="dropdown-menu" role="menu">
                                <li><a href="#">Critical</a></li>
                                <li><a href="#">Warning</a></li>
                                <li class="divider"></li>
                                <li><a href="#">All</a></li>
                            </ul>
                        </div>
                        <div class="btn-group">
                            <button type="button" class="btn btn-danger">Publish</button>
                            <button type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown">
                                <span class="caret"></span>
                                <span class="sr-only">Toggle Dropdown</span>
                            </button>
                            <ul class="dropdown-menu" role="menu">
                                <li><a href="#" data-toggle="modal" data-target="#modalNote">Note</a></li>
                                <li><a href="#" data-toggle="modal" data-target="#modalPicture">Picture</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div id="feedsContainer">
                <abbConnect:FeedPage ID="FeedPage" runat="server" />
            </div>
        </div>
    </div>

    <!-- Modals -->
    <div id="modals">
        <div class="modal fade" id="modalNote" tabindex="-1" role="dialog" aria-labelledby="modalNoteLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel">Add a new note</h4>
                    </div>
                    <div class="modal-body">
                        <h5>Please select the note type:</h5>
                        <!--SelectBox for post body-->
                        <select class="form-control" id="selectModalNoteMessage">
                        </select>
                        <hr>
                        <h5>Please insert new note text:</h5>
                        <!-- Textbox -->
                        <textarea id="textareaNote" class="input col-md-12" placeholder="Insert your note text here..." rows="5" ></textarea>
                        <br />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="AjaxPostNewFeed()">Post new note</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
        <div class="modal fade" id="modalPicture" tabindex="-1" role="dialog" aria-labelledby="modalPictureLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="H1">Add a new picture note</h4>
                    </div>
                    <div class="modal-body">
                        <!-- File upload-->
                        <input id="filePicture" type="file" style="display: none" />
                        <div class="input-append">
                            <input id="inputPicturePath" class="input-large" type="text" style="width: 85%;" />
                            <a class="btn" onclick="$('input[id=filePicture]').click();">Browse</a>
                        </div>

                        <script type="text/javascript">
                            $('input[id=filePicture]').change(function () {
                                $('#inputPicturePath').val($(this).val());
                            });
                        </script>

                        <br />
                        <h5>Please insert the description of the picture:</h5>

                        <!-- Textbox -->
                        <div class="input-group">
                            <span class="input-group-addon"></span>
                            <textarea class="input" placeholder="Insert your note text here..." style="width: 100%; resize: vertical"></textarea>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal" onclick="ClearModal()">Close</button>
                        <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="ClearModal()">Post new picture note</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
    </div>
   
</asp:Content>