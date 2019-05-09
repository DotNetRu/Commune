using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Entities;

namespace DotNetRuServer.Meetups.BL.Interfaces
{
    public interface IImageProvider
    {
        Task<List<ImageInfo>> GetAllImageInfosAsync();
        Task<ImageData> GetImageOrDefault(string externalUrl);
        Task<ImageData> GetImageOrDefault(int id);
        Task<ImageData> SaveImage(ImageData image);
    }
}