<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AllSensors.aspx.cs" Inherits="AllSensors" %>

<%@ Register Src="controls/RealTimeSensorPage.ascx" TagName="RealTimeSensorPage" TagPrefix="abbConnect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>$("#AllSensorsIcon").addClass("active"); type = "text/javascript"</script>
    <div class="row">
        <div class="col-md-6">
            <div class="feed-header">
                <div class="form-inline">
                    <div class="form-group">
                        <h3><span class="glyphicon glyphicon-link"></span>All sensors <small>Available sensors for user to follow</small></h3>
                    </div>
                </div>
            </div>
            <div id="allSensorsContainer">
                <abbConnect:RealTimeSensorPage ID="AllRealTimeSensorPage" runat="server" />
            </div>
        </div>
        <div class="col-md-6">
            <div class="feed-header">
                <div class="form-inline">
                    <div class="form-group">
                        <h3><span class="glyphicon glyphicon-flash"></span>Followed sensors <small>User followed sensors</small></h3>
                    </div>
                </div>
            </div>
            <div id="userFollowedSensorsContainer">
                <abbConnect:RealTimeSensorPage ID="UserFollowedRealTimeSensorPage" runat="server" />
            </div>
            <div id="loading_throbber_user_followed_sensors" class="loading-throbber" data-container="userFollowedSensorsContainer"></div>
        </div>
    </div>
</asp:Content>

