using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAnyDo
{
    public class Time
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public List<Task> Tasks { set; get; }
    }
}