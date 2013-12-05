using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Category
    {
        private string categoryName;
        public string CategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                categoryName = value;
            }
        }

        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        private int priority;
        public int Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
            }
        }
    }
}
