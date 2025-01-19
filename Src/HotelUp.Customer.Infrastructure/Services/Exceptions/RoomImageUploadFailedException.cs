using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Infrastructure.Services.Exceptions;

public class RoomImageUploadFailedException : BusinessRuleException
{
    public RoomImageUploadFailedException() : base("Failed to upload an image to S3.")
    {
    }
}