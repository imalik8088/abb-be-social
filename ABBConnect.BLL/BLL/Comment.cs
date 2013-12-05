using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Comment
    {
        public Comment()
        {
            this.owner = new Human();
            this.content = "";
            this.timeStamp = DateTime.MinValue;
        }

        private int iD;
        public int ID
        {
            get
            {
                return iD;
            }
            set
            {
                iD = value;
            }
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

        private DateTime timeStamp;
        public DateTime TimeStamp
        {
            get
            {
                return timeStamp;
            }
            set
            {
                timeStamp = value;
            }
        }

        private string content;
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }
    }
}
