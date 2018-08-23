using BUDCC.DropcamClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var foo = DoLoginAndTest();
            Console.ReadLine();
        }

        private static async Task DoLoginAndTest()
        {
            try
            {
                var client = new DropcamClient();
                var loginResults = await client.Login("YOUR_NEST_ACCOUNT_EMAIL_ADDRESS", "PASSWORD");

                var cameras = await client.GetCameras();
                foreach (var camera in cameras)
                {
                    var imageBytes = await client.GetCameraImage(camera);
                    File.WriteAllBytes(camera.uuid + ".jpg", imageBytes);

                    if (camera.hours_of_recording_max > 0)
                    {
                        var clip = await client.RecordClip(camera, DateTime.Now.AddHours(-1), 60);

                        do
                        {
                            System.Threading.Thread.Sleep(5000);

                            Console.WriteLine("Checking on clip request...");

                            clip = await client.GetClipInfo(clip.id);
                        } while (clip.is_error == false && clip.is_generated == false);

                        if (clip.is_generated)
                        {
                            Console.WriteLine("Clip successfully generated");
                        }
                        else if (clip.is_error)
                        {
                            Console.WriteLine("Error generating clip");
                        }

                        await client.DeleteClip(clip.id);
                    }
                }

                var clips = await client.GetClips();
                foreach (var clip in clips)
                {
                    Console.WriteLine($"Clip: \"{clip.title}\" from {UnixTime.DateTimeFromUnixTimestampSeconds((long)clip.start_time)} length: {TimeSpan.FromSeconds(clip.length_in_seconds)}");
                }
            }
            catch(Exception ex)
            {
                ex = ex;
            }
        }
    }
}
