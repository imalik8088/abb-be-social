using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PortableTransformationLayer
{
    class Connection
    {
        private readonly string url;

        public string Url
        {
            get { return url; }
        }

        private readonly string urlStream;

        public string UrlStream
        {
            get { return urlStream; }
        }

        public Connection()
        {
            url = Properties.Resources.url;
            urlStream = Properties.Resources.urlStream;
        }
    }
}
