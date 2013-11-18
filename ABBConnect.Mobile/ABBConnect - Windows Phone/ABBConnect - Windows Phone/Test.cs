using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABBConnect___Windows_Phone
{
    class Test
    {
        int ID, priority;

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public int ID1
        {
            get { return ID; }
            set { ID = value; }
        }
        DateTime timestamp;

        public DateTime Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }
        string location, content, category;

        public string Category
        {
            get { return category; }
            set { category = value; }
        }


        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        List<String> comments, tags;

        public List<String> Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        public List<String> Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        string author;

        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public Test()
        {
            comments = new List<string>();
            tags = new List<string>();
        }

    }
}
