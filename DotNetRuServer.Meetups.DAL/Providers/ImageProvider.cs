using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Entities;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.DAL.Database;
using Microsoft.EntityFrameworkCore;

namespace DotNetRuServer.Meetups.DAL.Providers
{
    public class ImageProvider : IImageProvider
    {
        private readonly DotNetRuServerContext _context;

        public ImageProvider(DotNetRuServerContext context)
        {
            _context = context;
        }

        public Task<List<ImageInfo>> GetAllImageInfosAsync()
            => _context.Images
                .Select(image => new ImageInfo
                {
                    Id = image.Id,
                    ExternalUrl = image.ExternalUrl,
                    Width = image.Width,
                    Height = image.Height,
                    MimeType = image.MimeType
                })
                .OrderBy(x=>x.Id)
                .ToListAsync();

        public Task<ImageData> GetImageOrDefaultAsync(int id)
            => _context.Images.FirstOrDefaultAsync(item => item.Id == id);

        public async Task<ImageData> SaveImageAsync(ImageData image)
        {
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
            return image;
        }

    }
}