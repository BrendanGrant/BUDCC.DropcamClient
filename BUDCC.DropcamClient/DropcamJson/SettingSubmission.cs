using System;
using System.Collections.Generic;
using System.Text;

namespace BUDCC.DropcamClient.DropcamJson
{
    public sealed class SettingSubmission
    {
        public string camera_uuid { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }

    public enum LEDState
    {
        Auto,
        On,
        Off
    }
}
