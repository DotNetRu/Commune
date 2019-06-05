using System.IO;

namespace DotNetRuServer.Meetups.BL.Models
{
    public class ImageVm
    {
        public string Id { get; set; }
        public ImageSize ImageSize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string MimeType { get; set; }
        public byte[] Data { get; set; }
    }

    public class UploadImageInfo
    {
        public ImageSize ImageSize { get; set; }
        public string MimeType { get; set; }
    }
}