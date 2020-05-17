using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTracker
{

    public class Task 
    {
        public string taskID { get; set; }
        public string taskName { get; set; }
        public string dateStarted { get; set; }
        public string ETA { get; set; }
        public string status { get; set; }
    }
}
