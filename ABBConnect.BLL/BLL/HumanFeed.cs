using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class HumanFeed: Feed
    {
        public HumanFeed()
        {
            owner = new Human();
            mediaFilePath = "";
        }

        private Human owner;
        public Human Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        private string mediaFilePath;
        public string MediaFilePath
        {
            get
            {
                return mediaFilePath;
            }
            set
            {
                mediaFilePath = value;
            }
        }
    }
}
