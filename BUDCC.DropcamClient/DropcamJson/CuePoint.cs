using System;
using System.Collections.Generic;
using System.Text;

namespace BUDCC.DropcamClient.DropcamJson
{
    public class CuePoint
    {
        public long playback_time { get; set; }
        public double start_time { get; set; }
        public string camera_uuid { get; set; }
        public string face_id { get; set; }
        public bool is_important { get; set; }
        public string face_category { get; set; }
        public double end_time { get; set; }
        public int importance { get; set; }
        public string face_name { get; set; }
        public bool in_progress { get; set; }
        public string id { get; set; }
        public List<int> zone_ids { get; set; }
        public List<string> types { get; set; }
    }
}
