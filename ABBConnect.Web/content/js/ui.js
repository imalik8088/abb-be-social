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

    //TODO add filter check for human post types

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

    //check if it's empty
    if (feedCommentData) {
        PageMethods.AjaxPostFeedComment(feedId, feedCommentData, AjaxPostFeedCommentSuccess);
    }
    else
        alert('Error: no comment inserted!');

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

    //check if empty
    if (feedContentData) {
        PageMethods.AjaxPublishHumanFeed(feedContentData, feedType, tagfeedTaggedUsers, OnAjaxPublishHumanFeedSuccess);
    } else
        alert('Error: no message inserted!');
}

function AjaxPublishHumanPictureFeed() {
    var feedContentData = $("#textAreaPicture").val();
    var feedType = $("#selectModalPictureMessage").val();

    var selectize = $('#input-tags-post-picture')[0].selectize;
    var tagfeedTaggedUsers = selectize.getValue();

    var base64picture = $('#modalImgFile').attr('src');

    if (feedContentData && base64picture) {
        PageMethods.AjaxPublishHumanPictureFeed(feedContentData, feedType, tagfeedTaggedUsers, base64picture, OnAjaxPublishHumanFeedSuccess);
    } else
        alert('Error: no message or picture inserted!');
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

    //display active if the date filter is active
    if ($('#chbHumanFeedsFilterStartDate').is(':checked') == true ||
    $('#chbHumanFeedsFilterEndDate').is(':checked') == true)
        $('#humanFeedsDateFilterIsActive').fadeIn();
    else
        $('#humanFeedsDateFilterIsActive').hide();

    //check if any checkbox is selected. if so, then highlight the selector buttons
    if ($('#chbHumanFeedsFilterStartDate').is(':checked') == true ||
        $('#chbHumanFeedsFilterEndDate').is(':checked') == true ||
        $('#chbWorkPost').is(':checked') == true ||
        $('#chbStickyNote').is(':checked') == true ||
        $('#chbVacationPost').is(':checked') == true) {

        $('#human-feed-filter-selector-left').addClass("btn-success");
        $('#human-feed-filter-selector-left').removeClass("btn-info");
        $('#human-feed-filter-selector-right').addClass("btn-success");
        $('#human-feed-filter-selector-right').removeClass("btn-info");
    }
    else {
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
    var identifier = '#' + elementPrefixName + "-" + elementId;

    //if the element doesn't have any tags, delete the additional body, so it doesn't clutter
    if ($(identifier).find('.feed-input-tags')[0].value == "") {
        var elem = $(identifier).find('.feed-body-addition')[0];
        elem.parentNode.removeChild(elem);
    }
    else {
        var $select = $(identifier).find('.feed-input-tags').selectize({
            delimiter: ',',
            persist: false,
            disabled: true,
            render: {
                //rendering of displayed tags
                item: function (item, escape) {

                    var name = item.value.split('||')[0];
                    var ID = item.value.split('||')[1];

                    return '<div class="tag-div">' +
                                '<a class="tag-user-link" href="userProfile.aspx?userId=' + ID + '">' +
                                    '<span>#' + escape(name) +
                                    '</span>' +
                                '</a>' +
                         '</div>';
                }
            }
        });

        if (isLocked == true) $select[0].selectize.lock();
    }
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

    AddCategoryOptionsToModal(selectModalNoteMessage, typeArray);
    AddCategoryOptionsToModal(selectModalPictureMessage, typeArray);
}

function AddCategoryOptionsToModal(modal, typeArray) {

    for (var i = 0; i < typeArray.length; i++) {
        var opt = document.createElement("option");
        opt.value = typeArray[i].CategoryName;
        opt.text = typeArray[i].CategoryName.split(/(?=[A-Z])/).join(' ');

        modal.appendChild(opt);
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
        searchField: ['FirstName', 'LastName', 'UserName'],
        options: availableUsers,
        create: false,
        render: {
            //rendering of the options in the dropdown menu
            option: function (item, escape) {
                return '<div>' +
                    '<span class="humanName">' + escape(item.FirstName) + '</span>' +
                    '<span class="space"> </span>' +
                    '<span class="humanSurname">' + escape(item.LastName) + '</span>' +
                    '<span class="space"> </span>' +
                    '<span class="humanUserName">  (' + item.UserName + ')</span>' +
                '</div>';
            },
            //rendering of selected items
            item: function (item, escape) {
                return '<div>' +
                    '<span class="humanName">' + escape(item.FirstName) + '</span>' +
                    '<span class="space"> </span>' +
                    '<span class="humanSurname">' + escape(item.LastName) + '</span>' +
                    '<span class="space"> </span>' +
                    '<span class="humanUserName">  (' + item.UserName + ')</span>' +
                '</div>';
            }
        }
    };

    var $selectNote = $('#input-tags-post-feed').selectize(options);
    var $selectPicture = $('#input-tags-post-picture').selectize(options);

    var selectizeNote = $selectNote[0].selectize;
    var selectizePicture = $selectPicture[0].selectize;

    //delegates for modals, so we can clear the data between the hides.
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

    selectizeSearchBar();

    //stop propagation on dropdown menu for the filters, so we can select multiple checkboxes
    //this enables the data-stop-propagation html attribute, so we can use it anywhere clickable
    $(function () {
        $("ul.dropdown-menu").on("click", "[data-stop-propagation]", function (e) {
            e.stopPropagation();
        });
    });

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

    //Date selector checkboxes
    $('input[type="checkbox"].bs').checkbox({
        buttonStyle: 'btn-danger',
        buttonStyleChecked: 'btn-success',
        checkedClass: 'glyphicon glyphicon glyphicon-check',
        uncheckedClass: 'glyphicon glyphicon glyphicon-unchecked'
    });

    //Message type checkboxes - work, vacation, sticky
    $('input[type="checkbox"].messagetype').checkbox({
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

    setRefreshListeners();
}

function selectizeSearchBar() {
    //initialization of the search bar, called once
    var $select = $('#search-input-bar').selectize({
        valueField: 'ID',
        searchField: ['FirstName', 'LastName', 'UserName'],
        delimiter: ',',
        maxItems: 1,
        create: false,
        options: [],
        load: function (query, callback) {
            if (!query.length) return callback();

            AjaxGetQueriedUsers(query);

            function AjaxGetQueriedUsers(query) {
                PageMethods.AjaxGetQueriedUsers(query, AjaxGetQueriedUsersSuccess);
            }

            function AjaxGetQueriedUsersSuccess(result, userContext, methodName) {
                //the result is an array of User objects
                var users = JSON.parse(result);
                callback(users);
            }
        },
        render: {
            //rendering of the options in the dropdown menu
            option: function (item, escape) {
                return '<div>' +
                    '<span class="humanName">' + escape(item.FirstName) + '</span>' +
                    '<span class="space"> </span>' +
                    '<span class="humanSurname">' + escape(item.LastName) + '</span>' +
                    '<span class="space"> </span>' +
                    '<span class="humanUserName">  (' + item.UserName + ')</span>' +
                '</div>';
            },
            //rendering of selected items
            item: function (item, escape) {
                return '<div class="searched-user">' +
                    '<span class="humanName">' + escape(item.FirstName) + '</span>' +
                    '<span class="space"> </span>' +
                    '<span class="humanSurname">' + escape(item.LastName) + '</span>' +
                    '<span class="space"> </span>' +
                    '<span class="humanUserName">  (' + item.UserName + ')</span>' +
                '</div>';
            }
        },
        onItemAdd: function (value, $item) {
            //TODO freeze the screen until next load
            //redirects to user page, determined by id
            window.location = "userProfile.aspx?userId=" + value;
        }
    });
}

function setRefreshListeners() {
    //3s wait until the filter is applied when checking boxes
    //this will be used to invoke a delay, so nothing bugs out when clicking rapidly
    var timeout = 3000;
    var awaitingRefresh = false;

    //whenever a checkbox is checked or unchecked, refresh the data
    $('#chbWorkPost').change(function () {
        reserveRefreshOfFilteredData();
    });

    $('#chbStickyNote').change(function () {
        reserveRefreshOfFilteredData();
    });

    $('#chbVacationPost').change(function () {
        reserveRefreshOfFilteredData();
    });

    function reserveRefreshOfFilteredData() {
        if (!awaitingRefresh) {
            awaitingRefresh = true;
            setTimeout(delayedSaveHumanFeedsFilterData, timeout);
        }
    }

    function delayedSaveHumanFeedsFilterData() {
        SaveHumanFeedsFilterData(1);
        awaitingRefresh = false;
    }
}

function OnClickSignOut() {
    window.location.replace("SignIn.aspx");
}