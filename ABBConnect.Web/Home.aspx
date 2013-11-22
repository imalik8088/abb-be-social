<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="_Home" %>

<%@ Register Src="controls/FeedPage.ascx" TagName="FeedPage" TagPrefix="abbConnect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="content/js/jquery-2.0.3.min.js"></script>
    <script src="content/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        //delegates for modals, so we can clear the data between the hides.
        //at the moment, it has to be like this, i couldn't make it work with removeData
        $(document).delegate('#modalNote', 'hidden.bs.modal', function (event) {
            //$(this).data('bs.modal').$element.removeData();
            $(this).html($(this).html());
        });

        $(document).delegate('#modalPicture', 'hidden.bs.modal', function (event) {
            //$(this).find('textarea').val(null).blur();
            $(this).html($(this).html());
        });

    </script>

    <div class="row">
        <div class="col-md-6">
            <div class="feed-header">
                <div class="form-inline">
                    <div class="form-group">
                        <h3><span class="glyphicon glyphicon-link"></span>User feeds <small>Feeds from human source</small></h3>
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

                        <div class="modal fade" id="modalNote" tabindex="-1" role="dialog" aria-labelledby="modalNoteLabel" aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                        <h4 class="modal-title" id="myModalLabel">Add a new note</h4>
                                    </div>
                                    <div class="modal-body">
                                        <h5>Please insert new note text:</h5>
                                        <!-- Textbox -->
                                        <div class="input-group">
                                            <span class="input-group-addon"></span>
                                            <textarea id="textareaNote" class="input" placeholder="Insert your note text here..." style="width: 100%; resize: vertical"></textarea>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal" onclick="ClearModal()">Close</button>
                                        <button type="button" class="btn btn-primary" onclick="AjaxPostNewFeed()">Post new note</button>
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
                                        <br />
                                        <h5>Please insert the description of the picture:</h5>

                                        <!-- Textbox -->
                                        <div class="input-group">
                                            <span class="input-group-addon"></span>
                                            <textarea class="input" placeholder="Insert your note text here..." style="width: 100%; resize: vertical"></textarea>
                                        </div>

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
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        <button type="button" class="btn btn-primary">Post new picture note</button>
                                    </div>
                                </div>
                                <!-- /.modal-content -->
                            </div>
                            <!-- /.modal-dialog -->
                        </div>
                        <!-- /.modal -->

                    </div>
                </div>
            </div>
            <div id="feed-container-999" class="feed-container">
                <div class="feed-inner-container feed-inner-container-danger">
                    <div class="feed-information">
                        <img class="feed-avatar" alt="" src="content/img/avatar-abb-2.png">
                        <span class="label label-danger label-feed-information-danger">Danger</span>
                    </div>
                    <div class="feed-message feed-message-danger">
                        <span class="feed-arrow feed-arrow-danger"></span>
                        <a href="#" class="feed-name feed-name-danger" data-original-title="">Name
                        </a>
                        <span class="feed-date-time">at Jul 15th, 2013 11:24
                        </span>
                        <span class="feed-body">Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.
                        </span>
                    </div>
                </div>
                <div class="feed-comments-container">
                    <div id="feed-single-comment-container-999" class="feed-single-comment-container">
                        <div class="feed-single-comment-info pull-left">
                            <img src="content/img/avatar-abb-small.png" alt="user-avatar">
                        </div>
                        <div class="feed-single-comment-data">
                            <div class="name">
                                <a href="#" data-original-title="">ABB-Something</a>
                            </div>
                            <div class="time">
                                <i class="icon-time"></i>
                                <span>19 min</span>
                            </div>
                            <div class="feed-single-comment-hr"></div>
                            <small>Et harum quidem rerum facilis est et expedita distinctio</small>
                        </div>
                    </div>
                </div>
            </div>
            <div id="feed-container-998" class="feed-container">
                <div class="feed-inner-container feed-inner-container-default">
                    <div class="feed-information">
                        <img class="feed-avatar" alt="" src="content/img/avatar-abb-2.png">
                        <span class="label label-info label-feed-information-default">Default</span>
                    </div>
                    <div class="feed-message feed-message-default">
                        <span class="feed-arrow feed-arrow-default"></span>
                        <a href="#" class="feed-name feed-name-default" data-original-title="">Name
                        </a>
                        <span class="feed-date-time">at Jul 15th, 2013 11:24
                        </span>
                        <span class="feed-body">Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.
                        </span>
                    </div>
                </div>
                <div class="feed-comments-container">
                    <div id="eed-single-comment-container-998" class="feed-single-comment-container">
                        <div class="feed-single-comment-info pull-left">
                            <img src="content/img/avatar-abb-small.png" alt="user-avatar">
                        </div>
                        <div class="feed-single-comment-data">
                            <div class="name">
                                <a href="#" data-original-title="">ABB-Something</a>
                            </div>
                            <div class="time">
                                <i class="icon-time"></i>
                                <span>19 min</span>
                            </div>
                            <div class="feed-single-comment-hr"></div>
                            <small>Et harum quidem rerum facilis est et expedita distinctio</small>
                        </div>
                    </div>
                </div>
            </div>
            <div id="feed-container-997" class="feed-container">
                <div class="feed-inner-container feed-inner-container-warning">
                    <div class="feed-information">
                        <img class="feed-avatar" alt="" src="content/img/avatar-abb-2.png">
                        <span class="label label-warning label-feed-information-warning">Warning</span>
                    </div>
                    <div class="feed-message feed-message-warning">
                        <span class="feed-arrow feed-arrow-warning"></span>
                        <a href="#" class="feed-name feed-name-warning" data-original-title="">Name
                        </a>
                        <span class="feed-date-time">at Jul 15th, 2013 11:24
                        </span>
                        <span class="feed-body">Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.
                        </span>
                    </div>
                </div>
                <div class="feed-comments-container">
                    <div id="feed-single-comment-container-997" class="feed-single-comment-container">
                        <div class="feed-single-comment-info pull-left">
                            <img src="content/img/avatar-abb-small.png" alt="user-avatar">
                        </div>
                        <div class="feed-single-comment-data">
                            <div class="name">
                                <a href="#" data-original-title="">ABB-Something</a>
                            </div>
                            <div class="time">
                                <i class="icon-time"></i>
                                <span>19 min</span>
                            </div>
                            <div class="feed-single-comment-hr"></div>
                            <small>Et harum quidem rerum facilis est et expedita distinctio</small>
                        </div>
                    </div>
                    <div id="feed-post-comment-container">
                        <div class="feed-post-comment-info pull-left">
                            <img src="content/img/avatar-abb-small.png" alt="user-avatar">
                        </div>
                        <div class="feed-post-comment-data">
                            <textarea type="text" id="feed-post-comment-input-997" class="feed-post-comment-input" onclick="focusOnFeedCommentContainer(997)">Write comment...</textarea>
                            <div id="feed-post-comment-additional-settings-997" class="dont-show">
                                <div class="feed-single-comment-hr"></div>
                                <div class="feed-post-comment-button-container">
                                    <button type="button" class="btn btn-primary btn-sm" onclick="hideFullFeedCommentContainer(997)">Cancel</button>
                                    <button type="button" class="btn btn-primary btn-sm" onclick="AjaxPostFeedComment(997)">Post</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr>
            <abbConnect:FeedPage ID="FeedPage" runat="server" />
        </div>
        <div class="col-md-6">
            <div class="feed-header">
                <div class="form-inline">
                    <div class="form-group">
                        <h3><span class="glyphicon glyphicon-flash"></span>Sensor feeds <small>Feeds from sensor source</small></h3>
                    </div>
                </div>
            </div>
            <div id="feed-container-996" class="feed-container">
                <div class="feed-inner-container feed-inner-container-danger">
                    <div class="feed-information">
                        <img class="feed-avatar" alt="" src="content/img/avatar-abb-2.png">
                        <span class="label label-danger label-feed-information-danger">Danger</span>
                    </div>
                    <div class="feed-message feed-message-danger">
                        <span class="feed-arrow feed-arrow-danger"></span>
                        <a href="#" class="feed-name feed-name-danger" data-original-title="">Name
                        </a>
                        <span class="feed-date-time">at Jul 15th, 2013 11:24
                        </span>
                        <span class="feed-body row">
                            <div id="gauge-996" class="feed-gauge pull-left feed-gauge-size">
                            </div>
                            <div class="bs-callout bs-callout-danger feed-gauge-margin">
                                <h4>No default class</h4>
                                <div class="bs-callout-hr"></div>
                                <p>Alerts don't have default classes, only base and modifier classes. A default gray alert doesn't make too much sense, so you're required to specify a type via contextual class. Choose from success, info, warning, or danger.</p>
                            </div>
                        </span>
                    </div>
                </div>
                <div class="feed-comments-container">
                    <div id="feed-single-comment-container-996" class="feed-single-comment-container">
                        <div class="feed-single-comment-info pull-left">
                            <img src="content/img/avatar-abb-small.png" alt="user-avatar">
                        </div>
                        <div class="feed-single-comment-data">
                            <div class="name">
                                <a href="#" data-original-title="">ABB-Something</a>
                            </div>
                            <div class="time">
                                <i class="icon-time"></i>
                                <span>19 min</span>
                            </div>
                            <div class="feed-single-comment-hr"></div>
                            <small>Et harum quidem rerum facilis est et expedita distinctio</small>
                        </div>
                    </div>
                </div>
            </div>
            <div id="feed-container-995" class="feed-container">
                <div class="feed-inner-container feed-inner-container-warning">
                    <div class="feed-information">
                        <img class="feed-avatar" alt="" src="content/img/avatar-abb-2.png">
                        <span class="label label-warning label-feed-information-danger">Warning</span>
                    </div>
                    <div class="feed-message feed-message-warning">
                        <span class="feed-arrow feed-arrow-warning"></span>
                        <a href="#" class="feed-name feed-name-warning" data-original-title="">Name
                        </a>
                        <span class="feed-date-time">at Jul 15th, 2013 11:24
                        </span>
                        <span class="feed-body row">
                            <div id="gauge-995" class="feed-gauge pull-left feed-gauge-size">
                            </div>
                            <div class="bs-callout bs-callout-warning feed-gauge-margin">
                                <h4>No default class</h4>
                                <div class="bs-callout-hr"></div>
                                <p>Alerts don't have default classes, only base and modifier classes. A default gray alert doesn't make too much sense, so you're required to specify a type via contextual class. Choose from success, info, warning, or danger.</p>
                            </div>
                        </span>
                    </div>
                </div>
                <div class="feed-comments-container">
                    <div id="feed-single-comment-container-995" class="feed-single-comment-container">
                        <div class="feed-single-comment-info pull-left">
                            <img src="content/img/avatar-abb-small.png" alt="user-avatar">
                        </div>
                        <div class="feed-single-comment-data">
                            <div class="name">
                                <a href="#" data-original-title="">ABB-Something</a>
                            </div>
                            <div class="time">
                                <i class="icon-time"></i>
                                <span>19 min</span>
                            </div>
                            <div class="feed-single-comment-hr"></div>
                            <small>Et harum quidem rerum facilis est et expedita distinctio</small>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        // Instances of sensor gauges, should be in ui.js !
        var g1 = new JustGage({
            id: "gauge-996",
            value: getRandomInt(800, 980),
            min: 0,
            max: 1000,
            title: "Water flow [m3/h]",
            gaugeWidthScale: 0.5
        });
        var g2 = new JustGage({
            id: "gauge-995",
            value: getRandomInt(110, 280),
            min: 0,
            max: 400,
            title: "Wind speed [km/h]"
        });
    </script>
</asp:Content>
