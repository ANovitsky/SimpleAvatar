using System.Drawing;
using System.IO;

namespace Ahau.SimpleAvatar
{
    public static class AvatarImageExtender
    {
        public static Image ToImage(this byte[] source)
        {
            using (var strm = new MemoryStream(source))
            {
                return Image.FromStream(strm);
            }
        }

        public static Stream ToStream(this byte[] source)
        {
            return new MemoryStream(source);
        }

        public static void SaveTo(this byte[] source, string fileName)
        {
            using (var img = source.ToImage())
            {
                img.Save(fileName);
            }
        }
    }
}