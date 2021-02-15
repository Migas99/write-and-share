using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WriteAndShareWebApi.Exceptions;

namespace WriteAndShareWebApi.Utils
{
    public static class SightEngine
    {
        private const string URL = "https://api.sightengine.com";
        private const string ImageURL = "https://api.sightengine.com/1.0/check.json";
        private const string VideoURL = "https://api.sightengine.com/1.0/video/check-sync.json";
        private static readonly List<string> AllModels = new List<string>()
        {
            "nudity",
            "wad",
            "properties",
            "face",
            "face-attributes",
            "celebrities",
            "type",
            "scam",
            "text",
            "offensive"
        };
        private const string Models = "nudity, wad, offensive";

        public static async Task<bool> ValidateImage(string user, string secret, IFormFile image)
        {
            byte[] uploadToBytes = null;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                uploadToBytes = memoryStream.ToArray();
            }

           if (uploadToBytes == null) throw new CustomException(404, "Upload is empty.");
            
            HttpClient client = new HttpClient();                
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
               
            MultipartFormDataContent httpContent = new MultipartFormDataContent();
            httpContent.Add(new StringContent(user), "api_user");
            httpContent.Add(new StringContent(secret), "api_secret");
            httpContent.Add(new StringContent(Models), "models");
            httpContent.Add(new StreamContent(image.OpenReadStream()), "media", image.FileName);
            
            var response = await client.PostAsync(ImageURL, httpContent);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                ImageResponseModel responseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageResponseModel>(json);
     
                List<string> errors = new List<string>();
                if (responseModel.Drugs > 0.7) errors.Add("Drugs were detected in the image sent.");
                //if (responseModel.Weapon > 0.7) errors.Add("Weapons were detected in the image sent.");
                if (responseModel.Nudity.Raw > 0.7) errors.Add("Nudity was detected in the image sent.");
                if (responseModel.Offensive.Prob > 0.7) errors.Add("The image sent was classified as offensive.");
                if (errors.Count > 0) throw new CustomException(400, errors);
            }
            else
            {
                Console.WriteLine("Error while connecting to external API: " + response.ReasonPhrase + ".");
                return true;
            }

            return true;
        }

        public static async Task<bool> ValidateVideo(string user, string secret, IFormFile video)
        {
            byte[] uploadToBytes = null;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                video.CopyTo(memoryStream);
                uploadToBytes = memoryStream.ToArray();
            }

            if (uploadToBytes == null) throw new CustomException(404, "Upload is empty.");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            MultipartFormDataContent httpContent = new MultipartFormDataContent();
            httpContent.Add(new StringContent(user), "api_user");
            httpContent.Add(new StringContent(secret), "api_secret");
            httpContent.Add(new StringContent(Models), "models");
            httpContent.Add(new StreamContent(video.OpenReadStream()), "media", video.FileName);

            var response = await client.PostAsync(VideoURL, httpContent);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                VideoResponseModel responseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<VideoResponseModel>(json);
             
                List<string> errors = new List<string>();
                Dictionary<string, bool> checkErrors = new Dictionary<string, bool>
                {
                    { "Drugs were detected in the video sent.", false },
                    { "Weapons were detected in the video sent.", false },
                    { "Nudity was detected in the video sent.", false },
                    { "The video sent was classified as offensive.", false }
                };

                foreach (VideoResponseModel.DataResponse.FramesResponse frame in responseModel.Data.Frames)
                {
                    if (frame.Drugs > 0.7) checkErrors["Drugs were detected in the video sent."] = true;
                    //if (frame.Weapon > 0.7) checkErrors["Weapons were detected in the video sent."] = true;
                    if (frame.Nudity.Raw > 0.7) checkErrors["Nudity was detected in the video sent."] = true;
                    if (frame.Offensive.Prob > 0.7) checkErrors["The video sent was classified as offensive."] = true;
                }

                foreach(string value in checkErrors.Keys)
                {
                    if (checkErrors[value])
                    {
                        errors.Add(value);
                    }
                }

                if (errors.Count > 0) throw new CustomException(400, errors);

            }
            else
            {
                Console.WriteLine("Error while connecting to external API: " + response.ReasonPhrase + ".");
                return true;
            }

            return true;
        }

        private class TextResponseModel
        {

        }

        private class ImageResponseModel
        {
            public string Status { get; set; }
            public float Weapon { get; set; }
            public float Alcohol { get; set; }
            public float Drugs { get; set; }
            public NudityResponse Nudity { get; set; } 
            public OffensiveResponse Offensive { get; set; }

            public class NudityResponse
            {
                public float Raw { get; set; }
                public float Partial { get; set; }
                public float Safe { get; set; }
            }

            public class OffensiveResponse
            {
                public float Prob { get; set; }
            }
        }

        private class VideoResponseModel
        {
            public string Status { get; set; }
            public DataResponse Data { get; set; }

            public class DataResponse
            {
                public List<FramesResponse> Frames { get; set; }

                public class FramesResponse
                {
                    public float Weapon { get; set; }
                    public float Alcohol { get; set; }
                    public float Drugs { get; set; }
                    public NudityResponse Nudity { get; set; }
                    public OffensiveResponse Offensive { get; set; }

                    public class NudityResponse
                    {
                        public float Raw { get; set; }
                        public float Partial { get; set; }
                        public float Safe { get; set; }
                    }

                    public class OffensiveResponse
                    {
                        public float Prob { get; set; }
                    }
                }
            }
        }
    }
}
