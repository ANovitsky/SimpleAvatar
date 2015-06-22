using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahau.SimpleAvatar.Tests
{
    class SimpleAvatarExample
    {
        private readonly Lazy<Avatar> _avatars;

        public SimpleAvatarExample()
        {
            _avatars = new Lazy<Avatar>(() => Avatar.NewAvatar.AsRectangle().Fill(Color.CornflowerBlue));
        }

        private Avatar Avatars
        {
            get { return _avatars.Value; }
        }

        public Image GetUserAvatarByName(string name)
        {
            return Avatars.Draw(name).ToImage();
        }

        public Stream GetUserAvatarByNameWithDot(string name)
        {
            return Avatars.Draw(name, s => s + ".").ToStream();
        }
        
        public byte[] GetUserAvatarBlob(string name)
        {
            return Avatars.Draw(name);
        }
    }
}
