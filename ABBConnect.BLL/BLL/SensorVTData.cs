using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SensorVTData
    {
        public SensorVTData()
        {
            this.creationTime = DateTime.MinValue;
        }

        private DateTime creationTime;
        public DateTime CreationTime
        {
            get
            {
                return creationTime;
            }
            set
            {
                creationTime = value;
            }
        }

        private int rawData;
        public int RawData
        {
            get
            {
                return rawData;
            }
            set
            {
                rawData = value;
            }
        }
    }
}
