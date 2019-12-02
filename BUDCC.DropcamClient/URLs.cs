using BUDCC.DropcamClient.DropcamJson;
using System;

namespace BUDCC.DropcamClient
{
    public class URLs
    {
        public static string Login = "https://home.nest.com/session";
        public static string Logout() => $"https://home.nest.com/dropcam/api/logout?_={UnixTime.GetCurrentUnixTimestampSeconds()}";
        public static string GetVisibleCameras = "https://home.nest.com/dropcam/api/cameras";
        public static string GetSessionToken = "https://home.nest.com/dropcam/api/login";
        public static string CamerasList = "https://home.nest.com/dropcam/api/cameras";

        public static string RequestVideo = "https://home.nest.com/dropcam/api/clips/request";
        public static string GetClips => $"https://home.nest.com/dropcam/api/visible_clips?_=UnixTime.GetCurrentUnixTimestampSeconds()";
        public static string GetClip(int clipId) => $"https://home.nest.com/dropcam/api/clips/{clipId}";

        public static string GetDetailedCameraInfo = "https://www.dropcam.com/app/cameras/{0}";
        public static string SetCameraProperty = "https://www.dropcam.com/app/cameras/properties/{0}";
        public static string GetCuePoint = "https://nexusapi-us1.camera.home.nest.com/cuepoint/{0}/2?";
        public static string GetCuePointEx = "https://nexusapi-us1.camera.home.nest.com/cuepoint/{0}/2?start_time={1}&_={2}";

        public static string GetImage(CameraInformation camera)
        {
            if(camera == null)
            {
                throw new ArgumentNullException("camera");
            }
            return string.Format("https://{0}/get_image?uuid={1}&cachebuster={2}&width=1280", camera.nexus_api_nest_domain_host, camera.uuid, UnixTime.GetCurrentUnixTimestampSeconds());
        }
    }
}
