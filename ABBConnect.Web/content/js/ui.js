
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

function AjaxLoadMoreFeeds(lastLoadedFeedId) {
    PageMethods.AjaxLoadMoreFeeds(lastLoadedFeedId, AjaxLoadMoreFeedsSuccess);
}
function AjaxLoadMoreFeedsSuccess(result, userContext, methodName) {

    var feedsRawData = $(result.FeedsRawData).hide().fadeIn("fast");
    $('#feedsContainer').append(feedsRawData);
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
    alert(result);

    //LoadFeedsAgain and display them, cause there is maybe new +1 comments
    AjaxGetAllFeedComments(result);
}

function AjaxGetAllFeedComments(feedID) {
    PageMethods.AjaxGetAllFeedComments(feedID, OnAjaxGetAllFeedCommentsSuccess);
}
function OnAjaxGetAllFeedCommentsSuccess(result, userContext, methodName) {
    alert("loaded:" + result.FeedId);

    var commentsRawData = $(result.FeedCommentsRawData).hide().fadeIn("fast");


    $("#feed-container-" + result.FeedId).find(".feed-comments-container").html(commentsRawData);
}

function AjaxPostNewFeed() {
    var feedContentData = $("#textareaNote").val();
    var feedType = $("#selectModalNoteMessage").val();

    // fetch the selectize instance
    var selectize = $('#input-tags')[0].selectize;
    var tags = selectize.getValue();

    PageMethods.AjaxPostNewFeed(feedContentData, feedType, tags, OnAjaxPostNewFeedSuccess);
}
function OnAjaxPostNewFeedSuccess(result, userContext, methodName) {
    if (result) {
        alert("Post successful!");
        AjaxDisplayNewFeed();
    }
    else {
        alert("Post unsuccessful!");
    }
}

//called if the posting is successful. adds the feeds on top of the container
function AjaxDisplayNewFeed() {
    PageMethods.AjaxDisplayNewFeed(AjaxDisplayNewFeedSuccess);
}
function AjaxDisplayNewFeedSuccess(result, userContext, methodName) {
    var feedsRawData = $(result.FeedsRawData).hide().fadeIn("fast");
    $('#feedsContainer').prepend(feedsRawData);
}

function PopulateSelectBoxPostType() {
    PageMethods.AjaxGetPostTypes(OnPopulateSelectBoxPostType);
}
function OnPopulateSelectBoxPostType(result, userContext, methodName) {
    //there are two attributes for each result item: CategoryName and Id
    var typeArray = JSON.parse(result);
    var obj = document.getElementById('selectModalNoteMessage');

    for (var i = 0; i < typeArray.length; i++) {
        opt = document.createElement("option");
        opt.value = typeArray[i].CategoryName;
        opt.text = typeArray[i].CategoryName.split(/(?=[A-Z])/).join(' ');
        obj.appendChild(opt);
    }
}

function ClearModalBodyListener() {

    //initialization of the selection field, called once
    var $select = $('#input-tags').selectize({
        plugins: ['remove_button'],
        labelField: 'FirstName',
        valueField: 'UserName',
        searchField: ['FirstName', 'LastName'],
        delimiter: ',',
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
                '</div>';
            },
            //rendering of selected items
            item: function (item, escape) {
                return '<div>' +
                    '<span class="humanName">' + escape(item.FirstName) + '</span>' +
                    '<span class="space"> </span>' +
                    '<span class="humanSurname">' + escape(item.LastName) + '</span>' +
                '</div>';
            }
        }
    });

    var selectize = $select[0].selectize;

    //delegates for modals, so we can clear the data between the hides.
    $(document).delegate('#modalNote', 'hide.bs.modal', function (event) {

        //$(this).html($(this).html());
        $('#selectModalNoteMessage').html($('#selectModalNoteMessage').html());
        $('#textareaNote').val('').blur();
        selectize.clear();
    });

    $(document).delegate('#modalPicture', 'hidden.bs.modal', function (event) {

        $(this).html($(this).html());
    });
}


function initUI() {
    $('.dropdown-toggle').dropdown();

    //initialization of the search bar, called once
    var $select = $('#search-input-bar').selectize({
        labelField: 'FirstName',
        valueField: 'UserName',
        searchField: ['FirstName', 'LastName'],
        delimiter: ',',
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
                '</div>';
            },
            //rendering of selected items
            item: function (item, escape) {
                return '<div>' +
                    '<span class="humanName">' + escape(item.FirstName) + '</span>' +
                    '<span class="space"> </span>' +
                    '<span class="humanSurname">' + escape(item.LastName) + '</span>' +
                '</div>';
            }
        },
        onItemAdd: function (value, $item) {
            //TODO add redirect to user page
            alert(value);
        }
    });

}

function OnClickSignOut() {
    window.location.replace("SignIn.aspx");
}