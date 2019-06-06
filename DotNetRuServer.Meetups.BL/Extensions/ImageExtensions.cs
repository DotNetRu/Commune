using DotNetRuServer.Meetups.BL.Entities;
using DotNetRuServer.Meetups.BL.Models;

namespace DotNetRuServer.Meetups.BL.Extensions
{
    public static class ImageExtensions
    {
        public static ImageVm ToVm(this ImageData imageData)
            => new ImageVm
            {
                Id = imageData.ExternalUrl,
                Width = imageData.Width,
                Height = imageData.Height,
                ImageSize = imageData.IsSmall ? ImageSize.Small : ImageSize.Full,
                MimeType = imageData.MimeType,
                Data = imageData.Data
            };

    }
}