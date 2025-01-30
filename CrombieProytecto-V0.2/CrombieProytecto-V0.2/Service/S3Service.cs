using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CrombieProytecto_V0._2.Service
{
    public class S3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3Service(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _bucketName = configuration["AWS:BucketName"];
        }

        // Sube un archivo a Amazon S3 y devuelve la clave (key)
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var key = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var fileTransferUtility = new TransferUtility(_s3Client);

            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var fileTransferUtilityRequest = new TransferUtilityUploadRequest
            {
                InputStream = memoryStream,
                Key = key,
                BucketName = _bucketName,
                CannedACL = S3CannedACL.PublicRead // Hacer el archivo público
            };

            await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
            return key;
        }

        // Elimina un archivo de Amazon S3 por su clave (key)
        public async Task DeleteFileAsync(string key)
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = key
            };

            await _s3Client.DeleteObjectAsync(deleteRequest);
        }

        // Obtiene la URL de un archivo por su clave (key)
        public string GetFileUrl(string key)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = key,
                Expires = DateTime.UtcNow.AddHours(1) // URL válida por 1 hora
            };

            return _s3Client.GetPreSignedURL(request);
        }

        // Obtiene todas las URLs de los archivos en el bucket
        public async Task<List<string>> GetAllFileUrlsAsync()
        {
            var request = new ListObjectsV2Request
            {
                BucketName = _bucketName
            };

            var response = await _s3Client.ListObjectsV2Async(request);
            var fileUrls = response.S3Objects.Select(o => GetFileUrl(o.Key)).ToList();

            return fileUrls;
        }

        // Obtiene un archivo por su clave (key) y lo devuelve como un Stream
        public async Task<Stream> GetFileAsync(string key)
        {
            var request = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = key
            };

            using var response = await _s3Client.GetObjectAsync(request);
            var memoryStream = new MemoryStream();
            await response.ResponseStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            return memoryStream;
        }

        // Obtiene todas las claves (keys) de los archivos en el bucket
        public async Task<List<string>> ListFilesAsync()
        {
            var request = new ListObjectsV2Request
            {
                BucketName = _bucketName
            };

            var response = await _s3Client.ListObjectsV2Async(request);
            return response.S3Objects.Select(o => o.Key).ToList();
        }
    }
}