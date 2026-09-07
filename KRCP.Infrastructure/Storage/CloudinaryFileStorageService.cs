using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using KRCP.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Storage
{
    public class CloudinaryFileStorageService: IFileStorageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryFileStorageService(IConfiguration configuration)
        {
            var account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]);

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadAsync(Stream fileStream, string fileName)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, fileStream),
                Folder = "KRCP/Ot-Evidencias"
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            if(result.Error != null)
            {
                throw new InvalidOperationException($"Error al subir el archivo a Cloudinary:{result.Error.Message}");
            }

            return result.SecureUrl.ToString();
        }
    }
}
