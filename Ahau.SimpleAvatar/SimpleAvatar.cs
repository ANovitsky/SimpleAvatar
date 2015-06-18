using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using Anotar.NLog;

namespace Ahau.SimpleAvatar
{
    public enum AvatarType
    {
        Rectangle,
        Ellipse
    }
    
    public class Avatar: IDisposable
    {
        private readonly ConcurrentDictionary<string, Bitmap> _cache = new ConcurrentDictionary<string, Bitmap>();

        public Color Background { get; protected set; }

        public Color Fill { get; protected set; }

        public Color TextColor { get; protected set; }
        
        public Size Size { get; protected set; }

        public Font Font { get; protected set; }

        public AvatarType Type { get; protected set; }

        public bool GenerateFillColor { get; protected set; }
        

        public static Avatar NewAvatar
        {
            get
            {
                return new Avatar();
            }
        }

        public Avatar()
        {
            Background = Color.White;
            Size = new Size(90, 90);
            Type = AvatarType.Rectangle;
            Fill = Color.LightGray;
            TextColor = Color.White;
            Font = new Font(FontFamily.GenericSansSerif, 28, FontStyle.Bold);

            GenerateFillColor = true;
        }

        #region Draws
        [LogToErrorOnException]
        public Bitmap DrawToBitmap(string content)
        {
            if (String.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException("content");
            }

            if (_cache.ContainsKey(content))
            {
                LogTo.Debug("Get avatar from cache");
                return _cache[content];
            }

            var bitmap = new Bitmap(Size.Width, Size.Height);
            
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Background);
                using (var brush = new SolidBrush(GenerateFillColor ? content[0].ToColor() : Fill))
                {
                    switch (Type)
                    {
                        case AvatarType.Rectangle:
                            g.FillRectangle(brush, new Rectangle(new Point(0,0), Size));
                            break;
                        default:
                            g.FillEllipse(brush, 0, 0, Size.Width - 1, Size.Height - 1);
                            break;
                                
                    }
                }

                var sf = new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center
                };

                g.DrawString(content, Font,
                    new SolidBrush(TextColor), new RectangleF(new PointF(0,0),  Size), sf);

                
                _cache.AddOrUpdate(content, bitmap, (s, bitmap1) => bitmap);
                LogTo.Debug("Added avatar to cache");

                return bitmap;
            }
        }

        public Stream DrawToStream(string content)
        {
            var memStream = new MemoryStream();
            DrawToBitmap(content).Save(memStream, System.Drawing.Imaging.ImageFormat.Png);

            return memStream;
        }

        [LogToErrorOnException]
        public void DrawToFile(string content, string fileName)
        {
            DrawToBitmap(content).Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
        }
        #endregion

        public Avatar BackgroundColor(Color color)
        {
            Background = color;
            return this;
        }

        public Avatar FillColor(Color color)
        {
            Fill = color;
            GenerateFillColor = false;
            return this;
        }

        public Avatar ByFirstLetterColor()
        {
            GenerateFillColor = true;
            return this;
        }


        public Avatar Ellipse()
        {
            Type = AvatarType.Ellipse;
            return this;
        }

        public Avatar AvatarSize(int width, int height)
        {
            Size = new Size(width, height);
            return this;
        }

        public Avatar Rectangle()
        {
            Type = AvatarType.Rectangle;
            return this;
        }

        public Avatar WithFont(FontFamily font, FontStyle fontStyle, float fontSize, Color color)
        {
            Font = new Font(font, fontSize, fontStyle);
            TextColor = color;
            return this;
        }
       
        public Avatar WithFontSize(float fontSize)
        {
            Font = new Font(Font.FontFamily, fontSize, Font.Style);
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
                    _cache.Clear();
                }

                _disposed = true;
            }
        }
        
        ~Avatar()
        {
            Dispose (false);
        }
        #endregion
    }
}
