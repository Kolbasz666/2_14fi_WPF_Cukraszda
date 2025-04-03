﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_14fi_WPF_Cukraszda
{
    public class Cake
    {
        public Cake() { }
        public Cake(string name, int price, string picture)
        {
            this.name = name;
            this.price = price;
            this.picture = picture;
        }
        public string id { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public string description { get; set; }
        public string allergenes { get; set; }
        public int price { get; set; }
        public int stock { get; set; }
        public string message { get; set; }
    }
}
