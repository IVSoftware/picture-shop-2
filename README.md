# Picture Shop 2

I've been having too much fun with `PictureBox` lately. [How to save multiple overlapping PictureBoxes?](https://stackoverflow.com/a/77502322/5438626) 

For your question, the strategy would be similar: Draw the image into 
`PictureBox` then similar to the code in that answer you would replace pixels that fall within a certain tolerance with a transparent pixel. 

```
pictureBox.Paint += (sender, e) =>
{
    Bitmap bmp;
    for (int i = 0; i < Layers.Count; i++)
    {
        switch (i)
        {
            case 0:
                bmp = Layers[i];
                bmp = localReplaceColor(Layers[i], PickedColor, trackBarTolerance.Value);
                break;
            default:
                return;
        }
        e.Graphics.DrawImage(bmp, pictureBox.ClientRectangle);
    }
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
};
```

To save it, draw the `PictureBox` to a bitmap and save it something like this:

```
buttonSave.Click += (sender, e) =>
{
    var fileName = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
        "TransparentImage.png");
    using (var bmp = new Bitmap(pictureBox.Width, pictureBox.Height))
    {
        pictureBox.DrawToBitmap(bmp, pictureBox.ClientRectangle);
        bmp.Save(fileName, ImageFormat.Png);
    }
    Process.Start("mspaint.exe", fileName);
};
```