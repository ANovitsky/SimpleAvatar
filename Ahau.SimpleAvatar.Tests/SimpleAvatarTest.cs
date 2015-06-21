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
            using (var a = Avatar.NewAvatar.AsEllipse())
            {
                a.Type.Should().Be(AvatarType.Ellipse);
            }
        }

        [TestMethod]
        public void Rectangle()
        {
            using (var a = Avatar.NewAvatar.AsRectangle())
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
                a.ForeColor.Should().Be(Color.Bisque);
            }
        }

        [TestMethod]
        public void FillColor()
        {
            using (var a = Avatar.NewAvatar.Fill(Color.Bisque))
            {
                a.CreateColorByFirstLetters.Should().BeFalse();
                a.FillColor.Should().Be(Color.Bisque);
            }
        }

        [TestMethod]
        public void DrawLettersToFile()
        {

            using (var a = Avatar.NewAvatar.SetSize(90,90).AsRectangle().WithFont(FontFamily.GenericSansSerif, FontStyle.Bold, 26, Color.White))
            {
                for (int i = 0; i < Alphabet.Length - 4; i++)
                {
                    var symbol = Alphabet.Substring(i, 2);
                    var fileName = String.Format("D:\\" + symbol + ".png" );//Path.GetTempFileName();

                    a.Draw(symbol).SaveTo(fileName);

                    File.Exists(fileName).Should().BeTrue();

                    using (var b = Image.FromFile(fileName))
                    {
                        b.Size.Should().Be(a.Size);     
                    }

                    //File.Delete(fileName);
                }
            }
        }

        [TestMethod]
        public void DrawLettersToBitmap()
        {
            using (var a = Avatar.NewAvatar.SetSize(90, 90).AsRectangle().WithFont(FontFamily.GenericSansSerif, FontStyle.Bold, 26, Color.White))
            {
                for (int i = 0; i < Alphabet.Length - 2; i++)
                {
                    var symbol = Alphabet.Substring(i, 1);
                    using (var bitmap = a.Draw(symbol).ToImage())
                    {
                        bitmap.Size.Should().Be(a.Size);
                    }
                }
            }
        }

        [TestMethod]
        public void DrawLettersToStream()
        {
            using (var a = Avatar.NewAvatar.SetSize(90, 90).AsRectangle().WithFont(FontFamily.GenericSansSerif, FontStyle.Bold, 26, Color.White))
            {
                for (int i = 0; i < Alphabet.Length - 2; i++)
                {
                    var symbol = Alphabet.Substring(i, 1);
                    using (var strm = a.Draw(symbol).ToStream())
                    {
                        using (var b = Image.FromStream(strm))
                        {
                            b.Size.Should().Be(a.Size);     
                        }

                    }
                }
            }
        }
    }
}
