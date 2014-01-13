using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.ServiceModel.Web;

namespace ABBStreamService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IABBConnectStreamWCF" in both code and config file together.
    [ServiceContract]
    public interface IABBConnectStreamWCF
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "SaveImage")]
        void saveImage(Stream stream);
    }
}
