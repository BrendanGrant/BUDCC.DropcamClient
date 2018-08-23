using BUDCC.DropcamClient.DropcamJson;
using BUDCC.DropcamClient.NestJson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace BUDCC.DropcamClient
{
    public partial class DropcamClient
    {
        NestLoginReply _nextLoginReply;

        public async Task<NestLoginReply> Login(string username, string password)
        {
            try
            {
                if(string.IsNullOrEmpty(username))
                {
                    throw new ArgumentNullException("username");
                }
                else if (string.IsNullOrEmpty(password))
                {
                    throw new ArgumentNullException("password");
                }

                Logger.Log("Login: Creating client...");
                var client = WebRequestHelper.GetClient();
                Logger.Log("Login: Setting credentials...");
                var content = new FormUrlEncodedContent(new[]
                       {
                       new KeyValuePair<string, string>("username", username),
                       new KeyValuePair<string, string>("password", password),
                   });

                GC.Collect();

                Logger.Log("Login: Awaiting posting...");
                var result = await client.PostAsync(URLs.Login, content);
                result.EnsureSuccessStatusCode();

                Logger.Log("Login: Reading response...");
                string s = await result.Content.ReadAsStringAsync();

                Logger.Log("Login: Deserializing...");
                _nextLoginReply = JsonConvert.DeserializeObject<NestLoginReply>(s);

                await GetSessionToken(_nextLoginReply.access_token);

                return _nextLoginReply;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public async Task Logout()
        {
            try
            {
                var client = WebRequestHelper.GetClient();

                var response = await client.GetAsync(URLs.Logout());
                response.EnsureSuccessStatusCode();

                WebRequestHelper.ClearCookies();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public async Task<byte[]> GetCameraImage(CameraInformation cameraInformation)
        {
            try
            {
                var url = URLs.GetImage(cameraInformation);

                Logger.Log("GetCameraImage: Creating Client...");
                var client = WebRequestHelper.GetClient();

                Logger.Log("GetCameraImage: Getting response...");
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                Logger.Log("GetCameraImage: Getting body...");
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public async Task<CameraInformation[]> GetCameras()
        {
            try
            {
                Logger.Log("GetCameras: Creating Client...");
                var client = WebRequestHelper.GetClient();

                Logger.Log("GetCameras: Awaiting string result...");
                var response = await client.GetAsync(URLs.GetVisibleCameras);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                Logger.Log("GetCameras: Deserializing");
                var item = JsonConvert.DeserializeObject<CameraInformation[]>(result);

                return item;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public async Task<string> GetSessionToken(string accessToken)
        {
            try
            {
                Logger.Log("GetSessionToken: Creating client...");
                var req = WebRequestHelper.GetClient();

                var content = new FormUrlEncodedContent(new[]
                {
                       new KeyValuePair<string, string>("access_token", accessToken)
                });

                Logger.Log("GetSessionToken: Awaiting string result...");
                var s = await req.PostAsync(URLs.GetSessionToken, content);

                Logger.Log("GetSessionToken: Deserializing");
                var responseString = await s.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<NestSessionToken[]>(responseString);

                return item[0].session_token;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        //Target URL no longer valid, more re-investigation is required for settings setting.
        /*
        public async Task<DetailedCameraInformation> GetDetailedCameraInfo(CameraInformation cameraInformation)
        {
            if (cameraInformation == null)
            {
                throw new ArgumentNullException("cameraInformation");
            }

            try
            {
                Logger.Log("GetDetailedCameraInfo: Creating client...");
                var req = WebRequestHelper.GetClient();

                Logger.Log("GetDetailedCameraInfo: Adding custom headers...");
                Logger.Log("GetDetailedCameraInfo: Awaiting string result...");
                var res = await req.GetStringAsync(string.Format(URLs.GetDetailedCameraInfo, cameraInformation.uuid));

                Logger.Log("GetDetailedCameraInfo: Deserializing");
                var item = JsonConvert.DeserializeObject<DetailedCameraInformation>(res);

                return item;
            }
            catch (Exception ex)
            {
                Logger.Log("GetDetailedCameraInfo: " + ex.ToString());
            }

            return null;
        }
        */

        //Untested in quite a while, provided for reference only.
        /*
        public async Task SetCameraSetting(Property property)
        {
            await SetCameraSetting(new SettingSubmission() { camera_uuid = property.camera_uuid, name = property.name, value = (string)property.value });
        }

        public async Task SetCameraSetting(SettingSubmission submissionValue)
        {
            Logger.Log("SetCameraSetting: Creating client...");
            var client = WebRequestHelper.GetClient();

            string value = JsonConvert.SerializeObject(submissionValue);

            Logger.Log("SetCameraSetting: Awaiting posting...");
            var result = await client.PostAsync(string.Format(URLs.SetCameraProperty, submissionValue.camera_uuid), new StringContent(value));
            result.EnsureSuccessStatusCode();

            Logger.Log("SetCameraSetting: Reading response...");
            string s = await result.Content.ReadAsStringAsync();
            Logger.Log("SetCameraSettings() reply: " + s);
        }
        */

        private static async Task<CuePoint[]> GetCuePoints(CameraInformation cameraInformation)
        {
            Logger.Log("GetCuePoints: Creating client...");
            using (var c = WebRequestHelper.GetClient())
            {
                try
                {
                    Logger.Log("GetCuePoints: Getting body...");
                    var responseBody = await c.GetStringAsync(string.Format(URLs.GetCuePoint, cameraInformation.uuid));

                    Logger.Log(responseBody);
                    Logger.Log("GetCuePoints: Deserializing...");
                    var item = JsonConvert.DeserializeObject<CuePoint[]>(responseBody);
                    return item;
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    throw;
                }
            }
        }

        public async Task<ClipInfo> RecordClip(CameraInformation camera, DateTime startTime, double lengthInSeconds)
        {
            Logger.Log("RecordClip: Creating client...");
            using (var c = WebRequestHelper.GetClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                       new KeyValuePair<string, string>("uuid", camera.uuid),
                       new KeyValuePair<string, string>("start_date", startTime.GetUnixTime().ToString()),
                       new KeyValuePair<string, string>("length", lengthInSeconds.ToString()),
                       new KeyValuePair<string, string>("is_time_lapse", "false"),
                   });

                Logger.Log("RecordClip: Posting request...");
                var videoSubmission = await c.PostAsync(URLs.RequestVideo, content);

                if (videoSubmission.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Logger.Log("ErrorCode: {0}-{1}", (int)videoSubmission.StatusCode, videoSubmission.ToString());
                    if (videoSubmission.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        Logger.Log("Conflict found, may abort.");
                        throw new OutOfMemoryException("You have exceeded the amount of recordings supported.");
                    }
                    else if (videoSubmission.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Logger.Log("File not found, lets try again.");
                        throw new KeyNotFoundException("Requested clip could not be found");
                    }
                    else
                    {
                        throw new Exception($"Unknown failure requesting clip. Code: {videoSubmission.StatusCode}");
                    }
                }
                else
                {
                    Logger.Log("RecordClip: Reading response...");
                    var responseBody = await videoSubmission.Content.ReadAsStringAsync();

                    var item = JsonConvert.DeserializeObject<ClipInfo[]>(responseBody);
                    return item.FirstOrDefault();
                }
            }
        }

        public async Task<ClipInfo[]> GetClips()
        {
            Logger.Log("GetClips: Creating client...");
            using (var c = WebRequestHelper.GetClient())
            {
                try
                {
                    Logger.Log("GetClips: Getting request...");
                    var response = await c.GetAsync(URLs.GetClips);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();

                    Logger.Log("GetClips: Deserializing...");
                    var item = JsonConvert.DeserializeObject<ClipContainer[]>(responseBody);
                    return item.FirstOrDefault().clips.ToArray();
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    throw;
                }
            }
        }

        public async Task<ClipInfo> GetClipInfo(int id)
        {
            Logger.Log("GetClipInfo: Creating client...");
            using (var c = WebRequestHelper.GetClient())
            {
                try
                {
                    Logger.Log("GetClipInfo: Making request...");
                    var response = await c.GetAsync(URLs.GetClip(id));
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();

                    Logger.Log("GetClipInfo: Deserializing...");
                    var item = JsonConvert.DeserializeObject<ClipInfo[]>(responseBody);
                    return item.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    throw;
                }
            }
        }

        public async Task DeleteClip(int id)
        {
            Logger.Log("DeleteClip: Creating client...");
            using (var c = WebRequestHelper.GetClient())
            {
                try
                {
                    Logger.Log("GetClips: Requesting delete...");
                    var response = await c.DeleteAsync(URLs.GetClip(id));
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    throw;
                }
            }
        }
    }
}
