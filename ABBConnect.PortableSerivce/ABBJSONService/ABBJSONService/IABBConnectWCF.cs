using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

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
        DAL.GetHumanInformation_Result GetHumanInformation(string id);

        [OperationContract]
        [WebInvoke]
        DAL.GetHumanInformationByUsername_Result GetHumanInformationByUsername(string username);

        [OperationContract]
        [WebInvoke]
        List<GetLatestXFeeds_Result> GetLatestXFeeds(string X);

        [OperationContract]
        [WebInvoke]
        List<GetLatestXFeedsFromId_Result> GetLatestXFeedsFromId(string X, string Id);

        [OperationContract]
        [WebInvoke]
        int PostTestFeed(string id, string text, string filepath, string prioId);

        [OperationContract]
        [WebInvoke]
        int PostFeed(string id, string text, string prioId, string filepath);


        [OperationContract]
        [WebInvoke]
        List<DAL.GetPriorityCategories_Result> GetCategories();

        [OperationContract]
        [WebInvoke]
        List<DAL.GetAllSensors_Result> GetAllSensors();

        [OperationContract]
        [WebInvoke]
        List<DAL.GetFeedComments_Result> GetFeedComments(string feedId, string randomGuid);

        [OperationContract]
        [WebInvoke]
        List<DAL.GetFeedTags_Result> GetFeedTags(string feedId);

        [OperationContract]
        [WebInvoke]
        List<DAL.GetAllHumanFeeds_Result> GetHumanFeeds();

        [OperationContract]
        [WebInvoke]
        List<DAL.GetAllHumanFeedsByFilter_Result> GetHumanFeedsByFilter(string location, string startingTime, string endingTime);

        [OperationContract]
        [WebInvoke]
        List<DAL.GetAllSensorFeeds_Result> GetSensorFeeds();

        [OperationContract]
        [WebInvoke]
        List<DAL.GetAllSensorFeedsByFilter_Result> GetSensorFeedsByFilter(string location, string startingTime, string endingTime);

        [OperationContract]
        [WebInvoke]
        List<DAL.GetUserFeeds_Result> GetUserFeeds();

        [OperationContract]
        [WebInvoke]
        List<DAL.GetUserFeedsByFilter_Result> GetUserFeedsByFilter(string location, string startingTime, string endingTime);

        [OperationContract]
        [WebInvoke]
        List<string> GetLocations();

        [OperationContract]
        [WebInvoke]
        DAL.GetSensorInformation_Result GetSensorInformation(string id);

        [OperationContract]
        [WebInvoke]
        List<DAL.GetHistoricalDataFromSensor_Result> GetHistoricalDataFromSensor(string id, string startingTime, string endingTime);

        [OperationContract]
        [WebInvoke]
        int GetLastSensorValue(string id);

        [OperationContract]
        [WebInvoke]
        bool PostComment(string feedId, string username, string text);

        [OperationContract]
        [WebInvoke]
        bool AddTag(string feedId, string username);

        [OperationContract]
        [WebInvoke]
        bool FollowSensor(string humanId, string sensorId);

        [OperationContract]
        [WebInvoke]
        List<DAL.GetLatestFeedsByFilter_Result> GetLatestFeedsByFilter(string location, string startingTime, string endingTime);

        [OperationContract]
        [WebInvoke]
        List<DAL.GetFeedsByFilter_Result> GetFeedsByFilter(string name, string location, string startingTime, string endingTime, string feedType);
        
        [OperationContract]
        [WebInvoke]
        List<DAL.GetLatestXFeeds_Result> GetXFeedsByFilter(string id, string location, string startingTime, string endingTime, string feedType, string startId, string numFeeds, string randomGuid);

        [OperationContract]
        [WebInvoke]
        int SaveFilter(string userId, string filterName, string startingTime, string endingTime, string location, string feedType);

        [OperationContract]
        [WebInvoke]
        bool AddFilterUser(string userId, string filterId);

        [OperationContract]
        [WebInvoke]
        List<DAL.GetUserSavedFilters_Result> GetSavedFilter(string userId);

        [OperationContract]
        [WebInvoke]
        List<DAL.GetUsersByName_Result> SearchUsersByName(string query);

        [OperationContract]
        [WebInvoke]
        List<DAL.GetUserSavedFiltersTagedUsers_Result> GetFilterTaggedUsers(string filterId);

        [OperationContract]
        [WebInvoke]
        List<DAL.GetUserActivity_Result> GetUserActivity(string userId);

        [OperationContract]
        [WebInvoke]
        bool UnfollowSensor(string humanUserId, string sensorUserId);

        [OperationContract]
        [WebInvoke]
        List<int> GetFollowedSensors(string humanUserId);

        [OperationContract]
        [WebInvoke]
        DAL.GetLatestXFeeds_Result GetFeedByFeedId(string feedId, string randomGuid);

        [OperationContract]
        [WebInvoke]
        List<DAL.GetLatestXFeeds_Result> GetFeedsFromLastShift(string numFeeds, string randomGuid);

        [OperationContract]
        [WebInvoke]
        List<DAL.GetUserActivity_Result> GetUserActivityFromId(string userId, string numberOfActivities, string startId);
    }

}
