﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Spot { get; set; }
        public IEnumerable<User> UserList { get; set; }
    }
}
