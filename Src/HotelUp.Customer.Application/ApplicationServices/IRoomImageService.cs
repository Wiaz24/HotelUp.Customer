using HotelUp.Customer.Domain.ValueObjects;

using Microsoft.AspNetCore.Http;

namespace HotelUp.Customer.Application.ApplicationServices;

public interface IRoomImageService
{
    Task<string> UploadImageAsync(int roomId, IFormFile image);
}