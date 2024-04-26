using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediaInfo.DotNetWrapper;
using MediaInfo.DotNetWrapper.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.Abstractions.IServices;
using V_Tube.Application.CloudinaryInstance;
using Xabe.FFmpeg;

namespace V_Tube.Infrastructure.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly CloudinaryInstance cloudinary;
        private readonly Cloudinary _cloudinary;
        private readonly IStorageService storageService;

        public CloudinaryService
            (
            IOptions<CloudinaryInstance> cloudinary,
            IStorageService storageService
            )
        {
            this.cloudinary = cloudinary.Value;

            var account = new Account(
                  this.cloudinary.CloudName,
                  this.cloudinary.ApiKey,
                  this.cloudinary.ApiSecret
              );

            _cloudinary = new Cloudinary(account);
            this.storageService = storageService;
        }
        public async Task<RawUploadResult?> UploadFileOnCloudinary(IFormFile file)
        {
            try
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new RawUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    // Handle upload result
                    if (uploadResult.Error is not null)
                        return null;

                    // File uploaded successfully
                    return uploadResult;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<RawUploadResult>> UploadFilesOnCloudinary(IFormFileCollection files)
        {
            var filePaths = new List<RawUploadResult>();

            foreach (var file in files)
            {
                var res = await UploadFileOnCloudinary(file);

                if (res is not null)
                    filePaths.Add(res);
            }

            return filePaths;
        }


        public async Task<(RawUploadResult?, TimeSpan?)> UploadVideoFileOnCloudinary(IFormFile file)
        {
            try
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new RawUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    // Handle upload result
                    if (uploadResult.Error is not null)
                        return (null, null);

                    var filePath = await storageService.SaveLocalFile(file);
                    var duration = GetVideoDuration(filePath);
                    //var deletedRes = storageService.DeleteLocalFile(filePath);

                    // File uploaded successfully
                    return (uploadResult, null);
                }
            }
            catch
            {
                return (null, null);
            }
        }

        private string GetVideoDuration(string filePath)
        {
            // Use FFmpeg to get the duration of the video file
            string arguments = $"-i \"{filePath}\" -f null -";
            string output = RunFFmpeg(arguments);

            // Extract duration from FFmpeg output
            string duration = ParseDuration(output);

            return duration;
        }

        static string RunFFmpeg(string arguments)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "ffmpeg";
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.Start();

                string output = process.StandardError.ReadToEnd();

                process.WaitForExit();

                return output;
            }
        }

        static string ParseDuration(string output)
        {
            // Example FFmpeg output line: Duration: 00:10:34.45
            int start = output.IndexOf("Duration: ") + "Duration: ".Length;
            int end = output.IndexOf(",", start);
            string duration = output.Substring(start, end - start).Trim();

            return duration;
        }

    }
}
