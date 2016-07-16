using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAnyDo
{
    public class Task
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int CategoryId { set; get; }
        public string HighPriority { set; get; }
        public int TimeId { set; get; }
        public DateTime CreationDate { set; get; }
        public List<SubTask> SubTasks { set; get; }
        public List<Note> Notes { set; get; }
    }
}