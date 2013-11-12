using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SensorHistoryData
    {
        private Sensor owner { get; set; }
        private DateTime startTime { get; set; }
        private DateTime endTime { get; set; }
        private List<int> values { get; set; }
    }
}
