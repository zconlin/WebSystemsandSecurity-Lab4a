﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Lab_4a.Models
{
    public class _Task
    {
        public string Id { get; set; }

        public string userId { get; set; }
    }
}