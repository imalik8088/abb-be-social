using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ABBJSONService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IABBConnectWCF
    {
        [OperationContract]
        [WebInvoke]
        bool LogIn(string username, string password);

        [OperationContract]
        [WebInvoke]
        GetHumanInformation_Result GetHumanInformation(string id);

        [OperationContract]
        [WebInvoke]
        GetHumanInformationByUsername_Result GetHumanInformationByUsername(string username);

        [OperationContract]
        [WebInvoke]
        List<GetLatestXFeeds_Result> GetLatestXFeeds(string X);

        [OperationContract]
        [WebInvoke]
        List<GetLatestXFeedsFromId_Result> GetLatestXFeedsFromId(string X, string Id);

        [OperationContract]
        [WebInvoke]
        int PostFeed(string id, string text, string filepath, string prioId);

        [OperationContract]
        [WebInvoke]
        List<GetPriorityCategories_Result> GetCategories();

        [OperationContract]
        [WebInvoke]
        List<GetFeedComments_Result> GetFeedComments(string feedId);

        [OperationContract]
        [WebInvoke]
        List<GetFeedTags_Result> GetFeedTags(string feedId);

    }

}
