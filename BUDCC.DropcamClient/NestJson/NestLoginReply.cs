using System;
using System.Collections.Generic;
using System.Text;

namespace BUDCC.DropcamClient.NestJson
{
    public class Urls
    {
        public string transport_url { get; set; }
        public string czfe_url { get; set; }
        public string direct_transport_url { get; set; }
        public string rubyapi_url { get; set; }
        public string weather_url { get; set; }
        public string log_upload_url { get; set; }
        public string support_url { get; set; }
    }

    public class Limits
    {
        public int thermostats_per_structure { get; set; }
        public int structures { get; set; }
        public int thermostats { get; set; }
        public int smoke_detectors { get; set; }
        public int smoke_detectors_per_structure { get; set; }
    }

    public class Weave
    {
        public string service_config { get; set; }
        public string pairing_token { get; set; }
        public string access_token { get; set; }
    }

    public class NestLoginReply
    {
        public string user { get; set; }
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string language { get; set; }
        public string email { get; set; }
        public bool is_superuser { get; set; }
        public bool is_staff { get; set; }
        public Urls urls { get; set; }
        public Limits limits { get; set; }
        public Weave weave { get; set; }
        public string userid { get; set; }
    }
}
