using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableTransformationLayer
{
    class Connection
    {
        private readonly string url = "http://83.255.84.243:85/ServiceJSON/ABBConnectWCF.svc/";

        public string Url
        {
            get { return url; }
        }

        private readonly string urlStream = "http://83.255.84.243:85/Service/ABBConnectStreamWCF.svc/";

        public string UrlStream
        {
            get { return urlStream; }
        }
    }
}
