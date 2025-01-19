using System.Net;

using Amazon.S3;
using Amazon.S3.Model;
using HotelUp.Customer.Application.ApplicationServices;
using HotelUp.Customer.Infrastructure.S3;
using HotelUp.Customer.Infrastructure.Services.Exceptions;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HotelUp.Customer.Infrastructure.Services;

public class S3RoomImageService : IRoomImageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly S3Options _options;
    private readonly ILogger<S3RoomImageService> _logger;
    private readonly TimeProvider _timeProvider;

    public S3RoomImageService(IAmazonS3 s3Client, IOptionsSnapshot<S3Options> options, 
        ILogger<S3RoomImageService> logger, TimeProvider timeProvider)
    {
        _s3Client = s3Client;
        _logger = logger;
        _timeProvider = timeProvider;
        _options = options.Value;
    }
    
    public static string GenerateKey(int roomId, IFormFile image)
    {
        return $"rooms/{roomId}/{image.FileName}";
    }

    public async Task<string> UploadImageAsync(int roomId, IFormFile image)
    {
        var key = GenerateKey(roomId, image);
        var putObjectRequest = new PutObjectRequest
        {
            BucketName = _options.BucketName,
            Key = key,
            ContentType = image.ContentType,
            InputStream = image.OpenReadStream(),
            Metadata =
            {
                ["x-amz-meta-original-filename"] = image.FileName,
                ["x-amz-meta-extension"] = Path.GetExtension(image.FileName)
            }
        };
        var response = await _s3Client.PutObjectAsync(putObjectRequest);
        if (response.HttpStatusCode != HttpStatusCode.OK)
        {
            _logger.LogError("Failed to upload an image to S3. Status code: {StatusCode}.", response.HttpStatusCode);
            throw new RoomImageUploadFailedException();
        }

        return $"https://s3.{_options.Region}.amazonaws.com/{_options.BucketName}/{key}";
    }
}