using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.Abstractions.IServices;
using V_Tube.Application.CloudinaryInstance;

namespace V_Tube.Infrastructure.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly CloudinaryInstance cloudinary;
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinaryInstance> cloudinary)
        {
            this.cloudinary = cloudinary.Value;

            var account = new Account(
                  this.cloudinary.CloudName,
                  this.cloudinary.ApiKey,
                  this.cloudinary.ApiSecret
              );

            _cloudinary = new Cloudinary(account);
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

    }
}
