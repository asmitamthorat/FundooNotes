using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer.CoundForImages
{
    public class CouldForImages: ICloudForImages
    {
        private Cloudinary _cloudinary;
        public CouldForImages() 
        {
            var myAccount = new Account { ApiKey = "apiKey", ApiSecret = "apiSecret", Cloud = "cloudName" };
            Cloudinary cloudinary = new Cloudinary(myAccount);
            _cloudinary = cloudinary;
        }

        public void UploadToCloud(string Image) 
        {

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(Image)
            };
            var uploadResult = _cloudinary.Upload(uploadParams);
        }
        
    }
}
