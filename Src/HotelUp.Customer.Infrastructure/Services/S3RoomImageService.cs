using Amazon.S3;
using Amazon.S3.Model;
using HotelUp.Customer.Application.ApplicationServices;
using HotelUp.Customer.Domain.ValueObjects;
using HotelUp.Customer.Infrastructure.S3;
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

    public async Task<ImageUrl> UploadImageAsync(int roomId, IFormFile image)
    {
        var objectKey = $"rooms/{roomId}/{image.FileName}";
        var presignedUrl = await GeneratePresignedUrl(objectKey);
        var putObjectRequest = new PutObjectRequest
        {
            BucketName = _options.BucketName,
            Key = objectKey,
            FilePath = presignedUrl,
            ContentType = image.ContentType,
            InputStream = image.OpenReadStream(),
            Metadata =
            {
                ["x-amz-meta-original-filename"] = image.FileName,
                ["x-amz-meta-room-id"] = roomId.ToString(),
                ["x-amz-meta-extension"] = Path.GetExtension(image.FileName)
            }
        };
        var response = await _s3Client.PutObjectAsync(putObjectRequest);
        return new ImageUrl(presignedUrl);
    }
    
    private async Task<string> GeneratePresignedUrl(string objectKey)
    {
        string urlString;
        try
        {
            var request = new GetPreSignedUrlRequest()
            {
                BucketName = _options.BucketName,
                Key = objectKey,
                Expires = _timeProvider.GetUtcNow().AddHours(1).DateTime
            };
            urlString = await _s3Client.GetPreSignedURLAsync(request);
        }
        catch (AmazonS3Exception ex)
        {
            _logger.LogError(ex, "Error generating presigned URL");
            throw;
        }

        return urlString;
    }
}