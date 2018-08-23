using System;
using System.Collections.Generic;
using System.Text;

namespace BUDCC.DropcamClient.NestJson
{
    public class ClipContainer
    {
        public List<ClipInfo> clips { get; set; }
        public int used_quota { get; set; }
        public int total_quota { get; set; }
        public string nest_structure_id { get; set; }

    }

    public class ClipInfo
    {
        public double length_in_seconds { get; set; }
        public int camera_id { get; set; }
        public string clip_type { get; set; }
        public bool is_youtube_uploading { get; set; }
        public string public_link { get; set; }
        public bool is_played { get; set; }
        public string title { get; set; }
        public bool is_generated { get; set; }
        public string download_url { get; set; }
        public string filename { get; set; }
        public bool is_user_generated { get; set; }
        public double generated_time { get; set; }
        public string nest_structure_id { get; set; }
        public bool is_error { get; set; }
        public string embed_url { get; set; }
        public string description { get; set; }
        public double start_time { get; set; }
        public string public_url { get; set; }
        public int play_count { get; set; }
        public bool is_facebook_uploading { get; set; }
        public bool is_public { get; set; }
        public string youtube_url { get; set; }
        public string facebook_url { get; set; }
        public string youtube_upload_error { get; set; }
        public string notes { get; set; }
        public string server { get; set; }
        public string thumbnail_url { get; set; }
        public int id { get; set; }
        public string facebook_upload_error { get; set; }
    }
}
