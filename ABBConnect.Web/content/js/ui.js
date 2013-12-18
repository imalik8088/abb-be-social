
// Not the best solution cause every HTML element has ID of parent on itself, but its reliable one.
function focusOnFeedCommentContainer(feedId) {
    $("#feed-post-comment-input-" + feedId.toString()).focus();
    showFullFeedCommentContainer(feedId);
}
function showFullFeedCommentContainer(feedId) {
    if ($("#feed-post-comment-input-" + feedId.toString()).height() != "85") {
        $("#feed-post-comment-input-" + feedId.toString()).val('');
        $("#feed-post-comment-input-" + feedId.toString()).animate({ "height": "85px", }, "fast");
        $("#feed-post-comment-additional-settings-" + feedId.toString()).slideDown("fast");
    }
}

function hideFullFeedCommentContainer(feedId) {
    $("#feed-post-comment-input-" + feedId.toString()).animate({ "height": "26px", }, "fast");
    $("#feed-post-comment-additional-settings-" + feedId.toString()).hide();
    $("#feed-post-comment-input-" + feedId.toString()).val('Write comment...');
}

function AjaxLoadMoreHumanFeeds(lastLoadedFeedId) {
    var ajaxFeedFilter = new Object();
    ajaxFeedFilter.startDate = null;
    ajaxFeedFilter.endDate = null;
    ajaxFeedFilter.location = null;
    ajaxFeedFilter.userId = $('#humanFeedsFilterUserId').val();

    if ($('#humanFeedsFilterStartDateIsChecked').val() == 'true') {
        ajaxFeedFilter.startDate = new Date($('#humanFeedsFilterStartDateValue').val());
    }
    if ($('#humanFeedsFilterEndDateIsChecked').val() == 'true') {
        ajaxFeedFilter.endDate = new Date($('#humanFeedsFilterEndDateValue').val());
    }
    if ($('#humanFeedsFilterLocationIsChecked').val() == 'true') {
        ajaxFeedFilter.location = $('#humanFeedsFilterLocation').val();
    }

    $("#loading_throbber_human_feeds").show();
    PageMethods.AjaxLoadMoreHumanFeeds(lastLoadedFeedId, ajaxFeedFilter, AjaxLoadMoreHumanFeedsSuccess);
}
function AjaxLoadMoreHumanFeedsSuccess(result, userContext, methodName) {
    $("#loading_throbber_human_feeds").hide();
    var feedsRawData = $(result.FeedsRawData).hide().fadeIn("fast");
    $('#feedsContainer').append(feedsRawData);
    initSelectize();
}
function AjaxLoadMoreRealTimeSensorFeeds(lastLoadedFeedId) {
    var ajaxFeedFilter = new Object();
    ajaxFeedFilter.startDate = null;
    ajaxFeedFilter.endDate = null;
    ajaxFeedFilter.location = null;
    ajaxFeedFilter.userId = $('#realTimeSensorFeedsFilterUserId').val();

    if ($('#realTimeSensorFeedsFilterStartDateIsChecked').val() == 'true') {
        ajaxFeedFilter.startDate = new Date($('#realTimeSensorFeedsFilterStartDateValue').val());
    }
    if ($('#realTimeSensorFeedsFilterEndDateIsChecked').val() == 'true') {
        ajaxFeedFilter.endDate = new Date($('#realTimeSensorFeedsFilterEndDateValue').val());
    }
    if ($('#realTimeSensorFeedsFilterLocationIsChecked').val() == 'true') {
        ajaxFeedFilter.location = $('#realTimeSensorFeedsFilterLocation').val();
    }

    $("#loading_throbber_real_time_sensor_feeds").show();
    PageMethods.AjaxLoadMoreRealTimeSensorFeeds(lastLoadedFeedId, ajaxFeedFilter, AjaxLoadMoreRealTimeSensorsFeedsSuccess);
}
function AjaxLoadMoreRealTimeSensorsFeedsSuccess(result, userContext, methodName) {
    $("#loading_throbber_real_time_sensor_feeds").hide();
    var feedsRawData = $(result.FeedsRawData).hide().fadeIn("fast");
    $('#real-time-sensor-feedsContainer').append(feedsRawData);
}

function AjaxPostFeedComment(feedId) {
    var feedCommentData = $("#feed-post-comment-input-" + feedId).val();
    PageMethods.AjaxPostFeedComment(feedId, feedCommentData, AjaxPostFeedCommentSuccess);

    // Clear input text
    $("#feed-post-comment-input-" + feedId).val('');

    //Remove Box
    hideFullFeedCommentContainer(feedId);
}
function AjaxPostFeedCommentSuccess(result, userContext, methodName) {
    //LoadFeedCommentsAgain
    AjaxGetAllFeedComments(result);
}

function AjaxGetAllFeedComments(feedID) {
    $("#feed-container-" + feedID).find(".feed-comments-data").html('');
    $("#feed-container-" + feedID).find("#loading_throbber_human_feed_comments").show();
    PageMethods.AjaxGetAllFeedComments(feedID, OnAjaxGetAllFeedCommentsSuccess);
}
function OnAjaxGetAllFeedCommentsSuccess(result, userContext, methodName) {
    $("#feed-container-" + result.FeedId).find("#loading_throbber_human_feed_comments").hide();
    var commentsRawData = $(result.FeedCommentsRawData).hide().fadeIn("fast");
    $("#feed-container-" + result.FeedId).find(".feed-comments-data").html(commentsRawData);
}

function AjaxPublishHumanFeed() {
    var feedContentData = $("#textareaNote").val();
    var feedType = $("#selectModalNoteMessage").val();

    var selectize = $('#input-tags-post-feed')[0].selectize;
    var tagfeedTaggedUsers = selectize.getValue();

    PageMethods.AjaxPublishHumanFeed(feedContentData, feedType, tagfeedTaggedUsers, OnAjaxPublishHumanFeedSuccess);
}
function OnAjaxPublishHumanFeedSuccess(result, userContext, methodName) {
    AjaxDisplayNewPublishedHumanFeed();
    //if (result == true) {
    //     //AjaxDisplayNewPublishedHumanFeed();
    //}
    //else {
    //    alert("Post unsuccessful!");
    //}
}

//called if the posting is successful. adds the feeds on top of the container
function AjaxDisplayNewPublishedHumanFeed() {
    var ajaxFeedFilter = new Object();
    ajaxFeedFilter.startDate = null;
    ajaxFeedFilter.endDate = null;
    ajaxFeedFilter.location = null;
    ajaxFeedFilter.userId = $('#realTimeSensorFeedsFilterUserId').val();

    if ($('#realTimeSensorFeedsFilterStartDateIsChecked').val() == 'true') {
        ajaxFeedFilter.startDate = new Date($('#realTimeSensorFeedsFilterStartDateValue').val());
    }
    if ($('#realTimeSensorFeedsFilterEndDateIsChecked').val() == 'true') {
        ajaxFeedFilter.endDate = new Date($('#realTimeSensorFeedsFilterEndDateValue').val());
    }
    if ($('#realTimeSensorFeedsFilterLocationIsChecked').val() == 'true') {
        ajaxFeedFilter.location = $('#realTimeSensorFeedsFilterLocation').val();
    }

    PageMethods.AjaxDisplayNewPublishedHumanFeed(ajaxFeedFilter, AjaxDisplayNewPublishedHumanFeedSuccess);
}
function AjaxDisplayNewPublishedHumanFeedSuccess(result, userContext, methodName) {
    var feedsRawData = '';
    feedsRawData = $(result.FeedsRawData).hide().fadeIn("fast");
    $('#feedsContainer').prepend(feedsRawData);
}

function PopulateSelectBoxPostType() {
    PageMethods.AjaxGetPostTypes(OnPopulateSelectBoxPostType);
}
function OnPopulateSelectBoxPostType(result, userContext, methodName) {
    //there are two attributes for each result item: CategoryName and Id
    var typeArray = JSON.parse(result);
    var obj = document.getElementById('selectModalNoteMessage');

    //TODO search pattern for splitting strings by capital letters
    //var stringList = typeArray[0].CategoryName.split(/(?=[A-Z])/);

    for (var i = 0; i < typeArray.length; i++) {
        opt = document.createElement("option");
        opt.value = typeArray[i].CategoryName;
        opt.text = typeArray[i].CategoryName;
        obj.appendChild(opt);
    }
}

function ClearModalBodyListener() {
    ////delegates for modals, so we can clear the data between the hides.
    ////at the moment, it has to be like this, i couldn't make it work with removeData
    //$(document).delegate('#modalNote', 'hide.bs.modal', function (event) {
    //    $(this).html($(this).html());
    //});

    //$(document).delegate('#modalPicture', 'hidden.bs.modal', function (event) {
    //    $(this).html($(this).html());
    //});
}
function SaveHumanFeedsFilterData(refreshData) {
    if ($('#chbHumanFeedsFilterStartDate').is(':checked') == true) {
        $('#humanFeedsFilterStartDateIsChecked').val('true')
        var startDate = $('#datepickerStart input').datepicker('getUTCDate');
        $('#humanFeedsFilterStartDateValue').val(startDate);
    }
    if ($('#chbHumanFeedsFilterEndDate').is(':checked') == true) {
        $('#humanFeedsFilterEndDateIsChecked').val('true')
        var endDate = $('#datepickerEnd input').datepicker('getUTCDate');
        $('#humanFeedsFilterEndDateValue').val(endDate);
    }

    if ($('#chbHumanFeedsFilterStartDate').is(':checked') == true ||
        $('#chbHumanFeedsFilterEndDate').is(':checked') == true) {
        $('#humanFeedsDateFilterIsActive').fadeIn();
        $('#human-feed-filter-selector-left').addClass("btn-success");
        $('#human-feed-filter-selector-left').removeClass("btn-info");
        $('#human-feed-filter-selector-right').addClass("btn-success");
        $('#human-feed-filter-selector-right').removeClass("btn-info");
    }
    else {
        $('#humanFeedsDateFilterIsActive').hide();
        $('#human-feed-filter-selector-left').addClass("btn-info");
        $('#human-feed-filter-selector-left').removeClass("btn-success");
        $('#human-feed-filter-selector-right').addClass("btn-info");
        $('#human-feed-filter-selector-right').removeClass("btn-success");
    }

    //Clear HumanFeedsData
    if (refreshData == 1) {
        $('#feedsContainer').html('');
    }
    //Load HumanFeeds with that filter
    AjaxLoadMoreHumanFeeds(-1);

}

function initSelectize(elementPrefixName, elementId, isLocked) {
    var $select = $('#' + elementPrefixName + "-" + elementId).find('.feed-input-tags').selectize({
        delimiter: ',',
        persist: false,
        createOnBlur: true,
        disabled: true,
        create: function (input) {
            return {
                value: input,
                text: input
            }
        }
    });
    if (isLocked == true) $select[0].selectize.lock();
}

function AjaxPopulateSelectBoxPostFeedType() {
    PageMethods.AjaxGetPostFeedTypes(OnAjaxPopulateSelectBoxPostFeedType);
}
function OnAjaxPopulateSelectBoxPostFeedType(result, userContext, methodName) {
    //there are two attributes for each result item: CategoryName and Id
    var typeArray = result;

    //the two selectboxes
    var selectModalNoteMessage = document.getElementById('selectModalNoteMessage');
    var selectModalPictureMessage = document.getElementById('selectModalPictureMessage');

    for (var i = 0; i < typeArray.length; i++) {
        opt = document.createElement("option");
        opt.value = typeArray[i].CategoryName;
        opt.text = typeArray[i].CategoryName.split(/(?=[A-Z])/).join(' ');
        selectModalNoteMessage.appendChild(opt);
        selectModalPictureMessage.appendChild(opt);
    }
}
function AjaxGetAvailableUsersToTag() {
    PageMethods.AjaxGetAvailableUsersToTag(OnAjaxGetAvailableUsersToTagSuccess);
}

function OnAjaxGetAvailableUsersToTagSuccess(result, userContext, methodName) {

    //the result is an array of User objects
    var availableUsers = result;

    //save the options for both inputs
    var options = {
        delimiter: ',',
        persist: false,
        createOnBlur: true,
        disabled: true,
        maxItems: null,
        valueField: 'ID',
        labelField: 'UserName',
        searchField: 'UserName',
        options: availableUsers,
        create: false
    };

    var $selectNote = $('#input-tags-post-feed').selectize(options);
    var $selectPicture = $('#input-tags-post-picture').selectize(options);

    var selectizeNote = $selectNote[0].selectize;
    var selectizePicture = $selectPicture[0].selectize;

    //delegates for modals, so we can clear the data between the hides.
    //at the moment, it has to be like this, i couldn't make it work with removeData
    $(document).delegate('#modalNote', 'hide.bs.modal', function (event) {

        $('#selectModalNoteMessage').html($('#selectModalNoteMessage').html());
        $('#textareaNote').val('').blur();
        selectizeNote.clear();

    });

    $(document).delegate('#modalPicture', 'hide.bs.modal', function (event) {

        $('#selectModalPictureMessage').html($('#selectModalPictureMessage').html());
        $('#textAreaPicture').val('').blur();
        selectizePicture.clear();
    });
}

function initUI() {
    $('.dropdown-toggle').dropdown();
    $('#datepickerStart input').datepicker({
        format: "dd.mm.yyyy",
        orientation: "top right",
        todayHighlight: true
    });
    $('#datepickerEnd input').datepicker({
        format: "dd.mm.yyyy",
        orientation: "top right",
        todayHighlight: true
    });

    $('input[type="checkbox"].bs').checkbox({
        buttonStyle: 'btn-danger',
        buttonStyleChecked: 'btn-success',
        checkedClass: 'glyphicon glyphicon glyphicon-check',
        uncheckedClass: 'glyphicon glyphicon glyphicon-unchecked'
    });


    //Checkbox changes value
    $('input[type="checkbox"].bs').change(function () {
        if ($(this).is(":checked")) {
            var datepickerValue = $("#" + $(this).data('datepicker') + " input").val();
            if (datepickerValue == '') {
                $(this).attr("checked", false);
            }

        }
    });

}

function OnClickSignOut() {
    window.location.replace("SignIn.aspx");
}