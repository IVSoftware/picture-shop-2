# Picture Shop 2

For purposes of demo, suppose the source image `RawImage` is a JPG and therefore a little bit ratty. And we're going to preview a transparency modification using a `PictureBox`. So to swap out the pixels, the code is this:

```
void paintImageWithTransparency(Bitmap rawImage, Rectangle rectangle, Graphics graphics)
{
    Bitmap bmp = localReplaceColor(rawImage, PickedColor, trackBarTolerance.Value);
    graphics.DrawImage(bmp, rectangle);

    Bitmap localReplaceColor(Bitmap bmp, Color targetColor, int tolerance)
    {
        if (tolerance == 0) return bmp;
        var copy = new Bitmap(bmp);
        for (int x = 0; x < bmp.Width; x++)
        {
            for (int y = 0; y < copy.Height; y++)
            {
                Color pixelColor = copy.GetPixel(x, y);
                if (localIsWithinTolerance(pixelColor, targetColor, tolerance))
                {
                    copy.SetPixel(x, y, Color.Transparent);
                }
            }
        }
        bool localIsWithinTolerance(Color pixelColor, Color targetColor, int tolerance)
        {
            return Math.Abs(pixelColor.R - targetColor.R) <= tolerance &&
                    Math.Abs(pixelColor.G - targetColor.G) <= tolerance &&
                    Math.Abs(pixelColor.B - targetColor.B) <= tolerance;
        }
        return copy;
    }
}
```

This goes in the `PictureBox` paint event handler.

```
pictureBox.Paint += (sender, e) =>
{
    if (sender is Control control)
    {
        paintImageWithTransparency(RawImage, control.ClientRectangle, e.Graphics);
    }
};
```

[![screenshot][1]][1]

---

Now we want to `Save` it, but drawing on the canvas isn't the same as setting the `Image` property (which is `null` in fact). Most other ways of saving the transparent image we're seeing in the `PictureBox` end in a non-transparent background. The way we're going to get around this is, once we preview the transparency in the picture box, is to use the identical code to make an ad-hoc Bitmap and save _that_ Bitmap without interacting with the `pictureBox`.

```
buttonSave.Click += (sender, e) =>
{
    var fileName = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
        "TransparentImage.png");

    Bitmap bmpToSave = new Bitmap(pictureBox.Width, pictureBox.Height);
    using (Graphics graphics = Graphics.FromImage(bmpToSave))
    {
        paintImageWithTransparency(RawImage, pictureBox.ClientRectangle, graphics);
    }
    bmpToSave.Save(fileName, ImageFormat.Png);

    Process.Start("mspaint.exe", fileName);
};
```

Here it is rendered in Windows 11 MS Paint.

[![transparent image][2]][2]

___
