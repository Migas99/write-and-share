using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WriteAndShareWebApi.Exceptions;
using static WriteAndShareWebApi.Enums.Uploads;

namespace WriteAndShareWebApi.Utils
{
    public static class UploadsHandler
    {
        private static readonly Dictionary<string, float> validHeaderTypesAndSizes = new Dictionary<string, float>
        {
            { ".jpeg", 1048576 },
            { ".jpg", 1048576 },
            { ".png", 1048576 }
        };

        private static readonly Dictionary<string, float> validAvatarTypesAndSizes = new Dictionary<string, float>
        {
            { ".jpeg", 524288 },
            { ".jpg", 524288 },
            { ".png", 524288 }
        };

        private static readonly Dictionary<string, float> validPostTypesAndSizes = new Dictionary<string, float>
        {
            { ".jpeg", 10485760 },
            { ".jpg", 10485760 },
            { ".png", 10485760 },
            { ".gif", 10485760 },
            { ".mp4", 52428800 },
            { ".avi", 52428800 }
        };

        private static readonly List<string> imageTypes = new List<string>
        {
            ".jpeg",
            ".jpg",
            ".png",
            ".gif"
        };

        private static readonly List<string> videoTypes = new List<string>
        {
            ".mp4",
            ".avi"
        };

        public static async Task<string> SaveUserHeader(string user, string secret, string rootPath, string currentHeader, IFormFile header)
        {
            string extension = Path.GetExtension(header.FileName);
            if (!validHeaderTypesAndSizes.ContainsKey(extension))
                throw new CustomException(400, "File type is not supported. Only .jpeg, .jpg and .png type files are acceptable.");
            if (validHeaderTypesAndSizes.GetValueOrDefault(extension) < header.Length) 
                throw new CustomException(400, "The file is too big. The max size for an header is 1MB.");

            if (!await SightEngine.ValidateImage(user, secret, header))
                throw new CustomException(400, "Invalid image for header.");

            string relativePath = Path.Combine(rootPath, GetHeaderFolderPath());
            string previousHeader = Path.Combine(relativePath, currentHeader);
            if (currentHeader != DefaultHeader && File.Exists(previousHeader)) File.Delete(previousHeader);

            string path = Path.GetFileNameWithoutExtension(header.FileName) + ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds() + extension;
            string absolutePath = Path.Combine(relativePath, path);
            FileStream fileStream = new FileStream(absolutePath, FileMode.Create, FileAccess.Write);

            try
            {
                header.CopyTo(fileStream);
                fileStream.Close();
            }
            catch (Exception e)
            {
                fileStream.Close();
                if (File.Exists(absolutePath)) File.Delete(absolutePath);
                throw e;
            }

            return path;
        }

        public static byte[] GetUserHeader(string rootPath, string headerPath)
        {
            byte[] header = null;
            string relativePath = Path.Combine(rootPath, GetHeaderFolderPath());
            string absoluteHeaderPath = Path.Combine(relativePath, headerPath);
            if (File.Exists(absoluteHeaderPath)) header = File.ReadAllBytes(absoluteHeaderPath);
            return header;
        }

        public static async Task<string> SaveUserAvatar(string user, string secret, string rootPath, string currentAvatar, IFormFile avatar)
        {
            string extension = Path.GetExtension(avatar.FileName);
            if (!validAvatarTypesAndSizes.ContainsKey(extension))
                throw new CustomException(400, "File type is not supported. Only .jpeg, .jpg and .png type files are acceptable.");
            if (validAvatarTypesAndSizes.GetValueOrDefault(extension) < avatar.Length) 
                throw new CustomException(400, "The file is too big. The max size for an avatar is 500KB.");

            if (!await SightEngine.ValidateImage(user, secret, avatar))
                throw new CustomException(400, "Invalid image for header.");

            string relativePath = Path.Combine(rootPath, GetAvatarFolderPath());
            string previousAvatar = Path.Combine(relativePath, currentAvatar);
            if (currentAvatar != DefaultAvatar && File.Exists(previousAvatar)) File.Delete(previousAvatar);

            string path = Path.GetFileNameWithoutExtension(avatar.FileName) + ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds() + extension;
            string absolutePath = Path.Combine(relativePath, path);
            FileStream fileStream = new FileStream(absolutePath, FileMode.Create, FileAccess.Write);

            try
            {
                avatar.CopyTo(fileStream);
                fileStream.Close();
            }
            catch (Exception e)
            {
                fileStream.Close();
                if (File.Exists(absolutePath)) File.Delete(absolutePath);
                throw e;
            }

            return path;
        }

        public static byte[] GetUserAvatar(string rootPath, string avatarPath)
        {
            byte[] avatar = null;
            string relativePath = Path.Combine(rootPath, GetAvatarFolderPath());
            string absoluteAvatarPath = Path.Combine(relativePath, avatarPath);
            if (File.Exists(absoluteAvatarPath)) avatar = File.ReadAllBytes(absoluteAvatarPath);
            return avatar;
        }

        public static async Task<string> SavePublicationUpload(string user, string secret, string rootPath, IFormFile upload)
        {
            string extension = Path.GetExtension(upload.FileName);
            if (!validPostTypesAndSizes.ContainsKey(extension))
                throw new CustomException(400,
                    "File type is not supported. Only .jpeg, .jpg, .png, .gif, .mp4 and .avi type files are acceptable.");
            if (validPostTypesAndSizes.GetValueOrDefault(extension) < upload.Length) 
                throw new CustomException(400, "The file is too big. The max size for this file type is " 
                    + ((validPostTypesAndSizes.GetValueOrDefault(extension) / 1024)/1024) + "MB.");

            if (imageTypes.Contains(extension))
            {
                if (!await SightEngine.ValidateImage(user, secret, upload))
                    throw new CustomException(400, "You uploaded an invalido image.");
            }
            else
            {
                if (videoTypes.Contains(extension))
                {
                    if (!await SightEngine.ValidateVideo(user, secret, upload))
                        throw new CustomException(400, "You uploaded an invalid video.");
                }
                else
                {
                    throw new CustomException(400, "File type is not supported. Only .jpeg, .jpg, .png, .gif, .mp4 and .avi type files are acceptable.");
                }
            }

            string uploadPath = Path.GetFileNameWithoutExtension(upload.FileName) + ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds() + extension;

            string relativePath = Path.Combine(rootPath, GetPublicationsFolderPath());
            string absolutePath = Path.Combine(relativePath, uploadPath);
            FileStream fileStream = new FileStream(absolutePath, FileMode.Create, FileAccess.Write);

            try
            {
                upload.CopyTo(fileStream);
                fileStream.Close();
            }
            catch (Exception e)
            {
                fileStream.Close();
                if (File.Exists(absolutePath)) File.Delete(absolutePath);
                throw e;
            }

            return uploadPath;
        }

        public static byte[] GetPublicationUpload(string rootPath, string uploadPath)
        {
            byte[] upload = null;
            string relativePath = Path.Combine(rootPath, GetPublicationsFolderPath());
            string absoluteUploadPath = Path.Combine(relativePath, uploadPath);
            if (File.Exists(absoluteUploadPath)) upload = File.ReadAllBytes(absoluteUploadPath);
            return upload;
        }

        public static string GetPublicationContentType(string uploadPath)
        {
            string extension = Path.GetExtension(uploadPath);
            if (imageTypes.Contains(extension)) return "image/jpg";
            if (videoTypes.Contains(extension)) return "video/mp4";
            return null;
        }

        public static bool DeletePublicationUpload(string rootPath, string UploadPath)
        {
            string relativePath = Path.Combine(rootPath, GetPublicationsFolderPath());
            string absolutePath = Path.Combine(relativePath, UploadPath);
            if (File.Exists(absolutePath))
            {
                File.Delete(absolutePath);
                return true;
            }

            return false;
        }
    }
}

