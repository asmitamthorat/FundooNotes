using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer.CoundForImages
{
    public interface ICloudForImages
    {
        string UploadToCloud(IFormFile image, string email);
    }
}
