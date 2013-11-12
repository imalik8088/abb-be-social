using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Comment
    {
        private int ID { get; set; }
        private User owner { get; set; }
        private DateTime timeStamp { get; set; }
        private string content { get; set; }
    }
}
