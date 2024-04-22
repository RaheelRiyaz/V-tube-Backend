﻿using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Application.Abstractions.IServices
{
    public interface ICloudinaryService
    {
        Task<RawUploadResult?> UploadFileOnCloudinary(IFormFile file);
        Task<List<RawUploadResult>> UploadFilesOnCloudinary(IFormFileCollection files);
    }
}
