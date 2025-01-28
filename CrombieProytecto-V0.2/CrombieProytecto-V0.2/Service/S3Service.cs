using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

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
        //Sube archivo a Amazon S3
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
                BucketName = _bucketName
            };
            await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
            return key;
        }
        //Elimina archivo cargado en Amazon S3
        public async Task DeleteFileAsync(string key)
        {
            var fileTransferUtility = new TransferUtility(_s3Client);
            await fileTransferUtility.S3Client.DeleteObjectAsync(new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = key
            });
        }

        public static async Task<bool> UploadFileAsync(
            IAmazonS3 client,
            string bucketName,
            string objectName,
            string filePath)
        {
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = objectName,
                FilePath = filePath,
            };

            var response = await client.PutObjectAsync(request);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
        //Obtiene archivo cargado a Amazon S3
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
        //Obtiene todos los archivos cargados a Amazon S3
        public async Task<List<string>> ListFilesAsync()
        {
            var request = new ListObjectsV2Request
            {
                BucketName = _bucketName
            };

            var response = await _s3Client.ListObjectsV2Async(request);
            return response.S3Objects.Select(o => o.Key).ToList();
        }
        //Obtiene la URL de un archivo cargado a Amazon S3
        public async Task<string> GetFileUrlAsync(string key)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = key,
                Expires = DateTime.UtcNow.AddHours(1)
            };

            var url = _s3Client.GetPreSignedURL(request);
            return url;
        }
    }
}
