
// Not the best solution cause every HTML element has ID of parent on itself, but its reliable one.
function focusOnFeedCommentContainer(feedId)
{
    $("#feed-post-comment-input-" + feedId.toString()).focus();
    showFullFeedCommentContainer(feedId);
}
function showFullFeedCommentContainer(feedId)
{
    if ($("#feed-post-comment-input-" + feedId.toString()).height() != "85")
    {
        $("#feed-post-comment-input-" + feedId.toString()).val('');
        $("#feed-post-comment-input-" + feedId.toString()).animate({ "height": "85px", }, "fast");
        $("#feed-post-comment-additional-settings-" + feedId.toString()).slideDown("fast");
    }
}

function hideFullFeedCommentContainer(feedId)
{   
    $("#feed-post-comment-input-" + feedId.toString()).animate({ "height": "26px", }, "fast");
    $("#feed-post-comment-additional-settings-" + feedId.toString()).hide();
    $("#feed-post-comment-input-" + feedId.toString()).val('Write comment...');
}

function AjaxPostFeedComment(feedId)
{
    var feedCommentData = $("#feed-post-comment-input-" + feedId).val();
    PageMethods.AjaxPostFeedComment(feedId, feedCommentData, AjaxPostFeedCommentSuccess);

    // Clear input text
    $("#feed-post-comment-input-" + feedId).val('');  

    //Remove Box
    hideFullFeedCommentContainer(feedId);
}
function AjaxPostFeedCommentSuccess(result, userContext, methodName)
{
    alert(result);

    //LoadFeedsAgain and display them, cause there is maybe new +1 comments
    AjaxGetAllFeedComments(result);
}

function AjaxGetAllFeedComments(feedID)
{
    PageMethods.AjaxGetAllFeedComments(feedID, OnAjaxGetAllFeedCommentsSuccess);
}
function OnAjaxGetAllFeedCommentsSuccess(result, userContext, methodName)
{
    alert("loaded:" + result.FeedId);
    //$("#feed-container-" + result.FeedId + ".feed-comments-container").html(result.CommentsRawData);
}

function AjaxPostNewFeed() {
    var feedContentData = $("#textareaNote").val();

    PageMethods.AjaxPostNewFeed(feedContentData, OnAjaxPostNewFeedSuccess);
}
function OnAjaxPostNewFeedSuccess(result, userContext, methodName) {
    alert("Post successful:" + result);
    $("#modalNote").modal('hide');
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
}


function initUI()
{   
    $('.dropdown-toggle').dropdown();
}