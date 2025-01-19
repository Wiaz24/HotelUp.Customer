using HotelUp.Customer.Domain.ValueObjects;

using Microsoft.AspNetCore.Http;

namespace HotelUp.Customer.Application.ApplicationServices;

public interface IRoomImageService
{
    Task<ImageUrl> UploadImageAsync(int roomId, IFormFile image);
}