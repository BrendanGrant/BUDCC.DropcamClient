using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BUDCC.DropcamClient.DropcamJson
{
    [DebuggerDisplay("Title = {title}, Type = {type}")]
    public class CameraInformation
    {
        public string download_host { get; set; }
        public string last_local_ip { get; set; }
        public string download_server_live { get; set; }
        public int timezone_utc_offset { get; set; }
        public string uuid { get; set; }
        public bool is_streaming { get; set; }
        public string title { get; set; }
        public string public_token { get; set; }
        public string description { get; set; }
        public bool is_streaming_enabled { get; set; }
        public object location { get; set; }
        public List<string> capabilities { get; set; }
        public string timezone { get; set; }
        public bool is_connected { get; set; }
        public bool is_online { get; set; }
        public bool is_public { get; set; }
        public double hours_of_recording_max { get; set; }
        public CameraType type { get; set; }
        public int id { get; set; }
        public string owner_id { get; set; }
        public string nexus_api_nest_domain_host { get; set; }
    }

    public enum CameraType
    {
        Dropcam = 5,
        DropcamPro = 6,
    }
}