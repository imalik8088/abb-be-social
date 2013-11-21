
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
    PageMethods.AjaxPostFeedComment(feedId, AjaxPostFeedCommentSuccess);
}
function AjaxPostFeedCommentSuccess(result, userContext, methodName)
{
    alert(result);
}

function initUI()
{   
    $('.dropdown-toggle').dropdown();
}