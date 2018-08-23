using System;
using System.Collections.Generic;
using System.Text;

namespace BUDCC.DropcamClient.DropcamJson
{
    public class ServerReply<T>
    {
        public int status { get; set; }
        public List<T> items { get; set; }
        public string status_description { get; set; }
        public string status_detail { get; set; }
    }
}
