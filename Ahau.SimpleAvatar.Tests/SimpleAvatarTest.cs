using System;
using System.Drawing;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Ahau.SimpleAvatar.Tests
{
    [TestClass]
    public class SimpleAvatarTest
    {
        const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXY12";

        [TestMethod]
        public void Ellipse()
        {
            using (var a = Avatar.NewAvatar.Ellipse())
            {
                a.Type.Should().Be(AvatarType.Ellipse);
            }
        }

        [TestMethod]
        public void Rectangle()
        {
            using (var a = Avatar.NewAvatar.Rectangle())
            {
                a.Type.Should().Be(AvatarType.Rectangle);
            }
        }

        [TestMethod]
        public void WithFont()
        {
            using (var a = Avatar.NewAvatar.WithFont(FontFamily.GenericMonospace,FontStyle.Bold,25, Color.Bisque))
            {
                a.Font.FontFamily.Should().Be(FontFamily.GenericMonospace);
                a.Font.Style.Should().Be(FontStyle.Bold);
                a.Font.Size.Should().Be(25);
                a.TextColor.Should().Be(Color.Bisque);
            }
        }

        [TestMethod]
        public void FillColor()
        {
            using (var a = Avatar.NewAvatar.FillColor(Color.Bisque))
            {
                a.GenerateFillColor.Should().BeFalse();
                a.Fill.Should().Be(Color.Bisque);
            }
        }

        [TestMethod]
        public void DrawLettersToFile()
        {

            using (var a = Avatar.NewAvatar.AvatarSize(90,90).Rectangle().WithFont(FontFamily.GenericSansSerif, FontStyle.Bold, 26, Color.White))
            {
                for (int i = 0; i < Alphabet.Length - 2; i++)
                {
                    var symbol = Alphabet.Substring(i, 1);
                    var fileName = Path.GetTempFileName();

                    a.DrawToFile(symbol, fileName);

                    File.Exists(fileName).Should().BeTrue();

                    using (var b = Bitmap.FromFile(fileName))
                    {
                        b.Size.Should().Be(a.Size);     
                    }

                    File.Delete(fileName);
                }
            }
        }

        [TestMethod]
        public void DrawLettersToBitmap()
        {
            using (var a = Avatar.NewAvatar.AvatarSize(90, 90).Rectangle().WithFont(FontFamily.GenericSansSerif, FontStyle.Bold, 26, Color.White))
            {
                for (int i = 0; i < Alphabet.Length - 2; i++)
                {
                    var symbol = Alphabet.Substring(i, 1);
                    using (var bitmap = a.DrawToBitmap(symbol))
                    {
                        bitmap.Size.Should().Be(a.Size);
                    }
                }
            }
        }

        [TestMethod]
        public void DrawLettersToStream()
        {
            using (var a = Avatar.NewAvatar.AvatarSize(90, 90).Rectangle().WithFont(FontFamily.GenericSansSerif, FontStyle.Bold, 26, Color.White))
            {
                for (int i = 0; i < Alphabet.Length - 2; i++)
                {
                    var symbol = Alphabet.Substring(i, 1);
                    using (var strm = a.DrawToStream(symbol))
                    {
                        using (var b = Bitmap.FromStream(strm))
                        {
                            b.Size.Should().Be(a.Size);     
                        }

                    }
                }
            }
        }
    }
}
