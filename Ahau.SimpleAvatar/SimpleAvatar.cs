﻿using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

namespace Ahau.SimpleAvatar
{
    public class Avatar: IDisposable
    {
        protected readonly ConcurrentDictionary<string, byte[]> Cache = new ConcurrentDictionary<string, byte[]>();

        public Color BackgroundColor { get; protected set; }

        public Color FillColor { get; protected set; }

        public Color ForeColor { get; protected set; }
        
        public Size Size { get; protected set; }

        public Font Font { get; protected set; }

        public AvatarType Type { get; protected set; }

        public bool CreateColorByFirstLetters { get; protected set; }

        public bool IsFirstLetterContent { get; set; }
        

        public static Avatar NewAvatar
        {
            get
            {
                return new Avatar();
            }
        }

        public Avatar()
        {
            //defaults
            BackgroundColor = Color.White;
            ForeColor = Color.White;
            FillColor = Color.LightGray;

            Size = new Size(90, 90);
            Type = AvatarType.Ellipse;

            Font = new Font("Tahoma", 42, FontStyle.Bold);

            CreateColorByFirstLetters = true;
            IsFirstLetterContent = true;
        }

        #region Draws

        public virtual byte[] Draw(string content)
        {
            return Draw(content, s => IsFirstLetterContent ? s.Trim(' ').Substring(0, 1).ToUpper() : s.Trim(' ').ToUpper());
        }

        public virtual byte[] Draw(string name, Func<string, string> transformFunc)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            var content = transformFunc != null ? transformFunc(name) : name;

            if (Cache.ContainsKey(content))
            {
                OnLog("Get avatar from cache");
                return Cache[content];
            }


            using (var img = DrawToImage(content))
            {
                using (var ms = new MemoryStream())
                {
                    img.Save(ms, ImageFormat.Png);
                    var bytes = ms.ToArray();
                
                    Cache.AddOrUpdate(content, bytes, (s, source) => bytes);
                    OnLog("Added avatar to cache");

                    return bytes;
                }
            }
        }

        protected virtual Image DrawToImage(string content)
        {
            var bitmap = new Bitmap(Size.Width, Size.Height);

            try
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.Clear(BackgroundColor);

                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.TextRenderingHint = TextRenderingHint.AntiAlias;

                    using (var brush = new SolidBrush(CreateColorByFirstLetters ? GetColorByName(content) : FillColor))
                    {
                        switch (Type)
                        {
                            case AvatarType.Rectangle:
                                g.FillRectangle(brush, new Rectangle(new Point(0, 0), Size));
                                break;
                            default:
                                g.FillEllipse(brush, 0, 0, Size.Width - 1, Size.Height - 1);
                                break;

                        }
                    }

                    var sf = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        FormatFlags = StringFormatFlags.NoClip,
                        Alignment = StringAlignment.Center
                    };

                    g.DrawString(content, Font,
                        new SolidBrush(ForeColor), new RectangleF(new PointF(), Size), sf);

                    return bitmap;
                }
            }
            catch (Exception ex)
            {
                OnError(new ErrorEventArgs(ex));
                throw;
            }
        }

        public virtual Color GetColorByName(string name)
        {
            if (name.Length == 0)
                return Color.Black;

            return name[0].ToColor();
        }
      
        #endregion

        public Avatar Background(Color color)
        {
            BackgroundColor = color;
            return this;
        }

        public Avatar Fill(Color color)
        {
            FillColor = color;
            CreateColorByFirstLetters = false;
            return this;
        }

        public Avatar ColorByFirstLetters()
        {
            CreateColorByFirstLetters = true;
            return this;
        }
        
        public Avatar AsEllipse()
        {
            Type = AvatarType.Ellipse;
            return this;
        }
        public Avatar AsRectangle()
        {
            Type = AvatarType.Rectangle;
            return this;
        }

        public Avatar SetSize(int width, int height)
        {
            Size = new Size(width, height);
            return this;
        }
       
        public Avatar WithFont(FontFamily font, FontStyle fontStyle, float fontSize, Color foreColor)
        {
            Font = new Font(font, fontSize, fontStyle);
            ForeColor = foreColor;
            return this;
        }

        #region Dispose
        private bool _disposed = false;

        //Implement IDisposable.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                //Disposing outside
                if (disposing)
                {
                    Cache.Clear();
                }

                _disposed = true;
            }
        }
        
        ~Avatar()
        {
            Dispose (false);
        }
        #endregion

        public event EventHandler<string> Log;

        protected virtual void OnLog(string e)
        {
            var handler = Log;
            if (handler != null) handler(this, e);
        }

        public event ErrorEventHandler Error;

        protected virtual void OnError(ErrorEventArgs e)
        {
            var handler = Error;
            if (handler != null) handler(this, e);
        }
    }
}
