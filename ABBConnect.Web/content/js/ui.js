
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
    //$("#feed-container-" + result.FeedId + ".feed-comments-container").html(result.CommentsRawData);
}

function AjaxPostNewFeed() {
    var feedContentData = $("#textareaNote").val();
    var feedType = $("#selectModalNoteMessage").val();

    PageMethods.AjaxPostNewFeed(feedContentData, feedType, OnAjaxPostNewFeedSuccess);
}
function OnAjaxPostNewFeedSuccess(result, userContext, methodName) {
    if (result == true)
        alert("Post successful!");
    else
        alert("Post unsuccessful!");
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
}


function initUI() {
    $('.dropdown-toggle').dropdown();
}

function OnClickSignOut() {
    window.location.replace("SignIn.aspx");
}