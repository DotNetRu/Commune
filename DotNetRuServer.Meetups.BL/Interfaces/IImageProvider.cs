using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Entities;

namespace DotNetRuServer.Meetups.BL.Interfaces
{
    public interface IImageProvider
    {
        Task<List<ImageInfo>> GetAllImageInfosAsync();
        Task<ImageData> GetImageOrDefaultAsync(int id);
        Task<ImageData> SaveImageAsync(ImageData image);
    }
}