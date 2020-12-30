using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer.CoundForImages
{
    public class CouldForImages: ICloudForImages
    {
        private Cloudinary _cloudinary;
        private readonly CloudConfiguration _config;
        public CouldForImages(CloudConfiguration config) 
        {
            _config = config;
           
        }

        public string UploadToCloud(IFormFile image, string email) 
        {

            try
            {
                if (image == null)
                {

                    return "Image not Found";
                }
                var stream = image.OpenReadStream();
                var name = image.FileName;
                Account account = new Account(_config.Name
                                         , _config.ApiKey
                                         , _config.Secret);
                _cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(name, stream)
                };

                ImageUploadResult uploadResult = _cloudinary.Upload(uploadParams);

                _cloudinary.Api.UrlImgUp.BuildUrl(String.Format("{0}.{1}", uploadResult.PublicId, uploadResult.Format));
                return uploadResult.Url.ToString();

            }
            catch (Exception ex) 
            {
                return ex.Message;
            }
           
        }
    }
        
}

