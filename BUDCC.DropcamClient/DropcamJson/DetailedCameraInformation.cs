using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BUDCC.DropcamClient.DropcamJson
{
    [DebuggerDisplay("username = {username}, id = {id}")]
    public sealed class AcceptedSubscription
    {
        public string username { get; set; }
        public int id { get; set; }
    }

    [DebuggerDisplay("email = {email}, id = {id}, is_subscribed = {is_subscribed}")]
    public sealed class PersonalSubscription
    {
        public string uuid { get; set; }
        public bool is_declined { get; set; }
        public bool is_subscribed { get; set; }
        public string camera_uuid { get; set; }
        public bool is_blocked { get; set; }
        public string id { get; set; }
        public bool is_pending { get; set; }
        public string email { get; set; }
    }

    public sealed class Schedule
    {
        public int? overridden_at { get; set; } //TODO: Examine of this being nullable is important
        public bool enabled { get; set; }
        public bool? override_state { get; set; } //TODO: Turned into Nullable
        public List<object> periods { get; set; }
        public string key { get; set; }
        public bool geofencing_enabled { get; set; }
    }

    [DebuggerDisplay("{name} = {value}")]
    public sealed class Property
    {
        public string camera_uuid { get; set; }
        public string name { get; set; }
        public object value { get; set; }
    }

    public sealed class Setting
    {
        public string security_mode { get; set; }
        public object password { get; set; }
        public int id { get; set; }
        public int camera_id { get; set; }
        public string ssid { get; set; }
    }

    public sealed class Notification
    {
        public object last_sent_at { get; set; }
        public int target_id { get; set; }
        public int id { get; set; }
        public int camera_id { get; set; }
    }

    public sealed class DetailedCameraInformation
    {
        public string download_server_live { get; set; }
        public bool is_audio_enabled { get; set; }
        public string nexus_get_image_url { get; set; }
        public string owner_email { get; set; }
        public bool is_status_led_enabled { get; set; }
        public List<AcceptedSubscription> accepted_subscriptions { get; set; }
        public bool is_video_flipped { get; set; }
        public List<PersonalSubscription> personal_subscriptions { get; set; }
        public string timezone { get; set; }
        public bool is_motion_detection_enabled { get; set; }
        public int id { get; set; }
        public string description { get; set; }
        public bool are_notifications_enabled { get; set; }
        public string uuid { get; set; }
        public string title { get; set; }
        public string public_token { get; set; }
        public List<Schedule> schedules { get; set; }
        public List<string> capabilities { get; set; }
        public List<Property> properties { get; set; }
        public bool is_trial_mode { get; set; }
        public List<object> pending_subscriptions { get; set; }
        public object location { get; set; }
        public string setup_wireless_url { get; set; }
        public string serial_number { get; set; }
        public int type { get; set; }
        public Setting setting { get; set; }
        public string owner_id { get; set; }
        public string last_local_ip { get; set; }
        public bool is_streaming_enabled { get; set; }
        public bool is_streaming { get; set; }
        public int timezone_utc_offset { get; set; }
        public string irled_state { get; set; }
        public List<Notification> notifications { get; set; }
        public bool is_online { get; set; }
        public bool is_public { get; set; }
        public bool is_sound_notify_enabled { get; set; }
        public string download_host { get; set; }
        public string recording_add_on_title { get; set; }
        public string dptz_state { get; set; }
        public double audio_input_gain_level { get; set; }
        public bool is_motion_notify_enabled { get; set; }
        public bool is_hd_video_enabled { get; set; }
        public bool is_sound_detection_enabled { get; set; }
        public List<List<string>> ptz_quadrants { get; set; }
        public double hours_of_recording_max { get; set; }
        public bool is_connected { get; set; }
        public List<List<string>> timezone_options { get; set; }
        public bool is_offline_notify_enabled { get; set; }
    }
}
