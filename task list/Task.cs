using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace task_list
{
    internal class Task
    {
        protected string mTaskName;
        protected bool mMarked;

        //constructor
        public Task(string taskName)
        {
            this.mTaskName = taskName;
            this.mMarked = false;
        }
        public Task(string taskName, bool marked)
        {
            this.mTaskName = taskName;
            this.mMarked = marked;
        }


        //properties
        public string TaskName
        {
            get { return mTaskName; }
            set { mTaskName = value; }
        }
        public bool Marked
        {
            get { return mMarked; }
            set { mMarked = value; }
        }

        //methods
        public override string ToString()
        {
            return String.Format($"{mTaskName}");
        }
    }
}
