# Simple Avatar
The simple .NET library for generate colored avatar by username.

## Usage

Rectangular avatar:

    using (var a = Avatar.NewAvatar.Rectangle())
    {
      var result = a.DrawToBitmap("B");
    }
    
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/B.png)
  
Сircle avatar with custom font size:

    using (var a = Avatar.NewAvatar.Ellipse().WithFontSize(36))
    {
      var result = a.DrawToBitmap("A");
    }

![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/Custom/D.png)

With static blue color:

    using (var a = Avatar.NewAvatar.FillColor(Color.CornflowerBlue))
    {
      var strm = a.DrawToStream("A");
    }

![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/Custom/M.png)

## NuGet
[Nuget package](https://www.nuget.org/packages/Ahau.SimpleAvatar)


## Default 
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/A.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/B.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/C.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/D.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/E.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/F.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/G.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/H.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/I.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/J.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/K.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/L.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/M.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/N.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/O.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/P.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/Q.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/R.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/S.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/T.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/U.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/V.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/W.png)
![](https://dl.dropboxusercontent.com/u/15543358/SimpleAvatar/Y.png)

## Version
0.1.x
