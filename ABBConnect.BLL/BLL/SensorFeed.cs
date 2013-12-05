using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SensorFeed: Feed
    {
        public SensorFeed()
        {
            owner = new Sensor();
        }

        private Sensor owner;
        public Sensor Owner
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
    }
}
