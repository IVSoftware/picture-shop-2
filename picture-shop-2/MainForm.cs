
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;

namespace picture_shop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent(); 
            ImageFolder =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Images"
            );

            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            buttonSave.Click += (sender, e) =>
            {
                var fileName = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                    "TransparentImage.png");
                using (var bmp = new Bitmap(pictureBox.Width, pictureBox.Height))
                {
                    var background = pictureBox.BackgroundImage;
                    var image = pictureBox.Image;
                    pictureBox.BackgroundImage = null; 
                    pictureBox.Image = null;

                    pictureBox.DrawToBitmap(bmp, pictureBox.ClientRectangle);
                    bmp.Save(fileName, ImageFormat.Png);
                }
                Process.Start("mspaint.exe", fileName);
            };
            pictureBox.Paint += (sender, e) =>
            {
                if (pictureBox.Image is Bitmap bmp)
                {
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
                }
            };
            trackBarTolerance.ValueChanged += (sender, e) => pictureBox.Refresh();
            pictureBoxWheel.Paint += (sender, e) =>
            {
                int diameter = Math.Min(pictureBoxWheel.Width, pictureBoxWheel.Height);
                PointF center = new PointF(diameter / 2, diameter / 2);
                Bitmap bmp = new Bitmap(diameter, diameter);

                for (int x = 0; x < diameter; x++)
                {
                    for (int y = 0; y < diameter; y++)
                    {
                        double angle = Math.Atan2(y - center.Y, x - center.X) * 180.0 / Math.PI;
                        angle = (angle < 0) ? angle + 360 : angle;
                        float distance = localDistance(new PointF(x, y), center);
                        if (distance <= center.X)
                        {
                            double saturation = distance / center.X;
                            Color color = localColorFromHSV(angle, saturation, 1.0);
                            bmp.SetPixel(x, y, color);
                        }
                        else if (distance <= center.X + 10) // You can adjust the width of the outer perimeter
                        {
                            // Add shades and tints to the outer perimeter
                            double t = (distance - center.X) / 25.0; // Adjust this value for more or less shading
                            Color color = localColorFromHSV(angle, 1.0, 1.0 - t);
                            bmp.SetPixel(x, y, color);
                        }
                    }
                }

                e.Graphics.DrawImage(
                    bmp,
                    new Point(
                        (pictureBoxWheel.Width - diameter) / 2,
                        (pictureBoxWheel.Height - diameter) / 2)
                );

                #region L o c a l F x
                float localDistance(PointF point1, PointF point2) =>
                    (float)Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));

                Color localColorFromHSV(double hue, double saturation, double value)
                {
                    int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
                    double f = hue / 60 - Math.Floor(hue / 60);
                    value = value * 255;
                    int v = Convert.ToInt32(value);
                    int p = Convert.ToInt32(value * (1 - saturation));
                    int q = Convert.ToInt32(value * (1 - f * saturation));
                    int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));
                    if (hi == 0) return Color.FromArgb(255, v, t, p);
                    else if (hi == 1) return Color.FromArgb(255, q, v, p);
                    else if (hi == 2) return Color.FromArgb(255, p, v, t);
                    else if (hi == 3) return Color.FromArgb(255, p, q, v);
                    else if (hi == 4) return Color.FromArgb(255, t, p, v);
                    else return Color.FromArgb(255, v, p, q);
                }
                #endregion L o c a l F x
            };
            pictureBoxWheel.MouseClick += (sender, e) =>
            {
                using (var bmp = new Bitmap(pictureBox.Width, pictureBox.Height))
                {
                    pictureBoxWheel.DrawToBitmap(bmp, pictureBox.ClientRectangle);
                    PickedColor = bmp.GetPixel(e.X, e.Y);
                }
            }; 
            pictureBoxWheel.MouseMove += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    using (var bmp = new Bitmap(pictureBox.Width, pictureBox.Height))
                    {
                        pictureBoxWheel.DrawToBitmap(bmp, pictureBox.ClientRectangle);
                        PickedColor = bmp.GetPixel(e.X, e.Y);
                    }
                }
            }; 
            
            labelPickedColor = new Label
            {
                Location = new Point(2,2),
                Width = 25,
                Height = 25,
                AutoSize = false,
                BackColor = PickedColor,
            };
            pictureBoxWheel.Controls.Add(labelPickedColor);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Layers.Add((Bitmap)Bitmap.FromFile(Path.Combine(ImageFolder, "Image1.png")));
            labelTolerance.Visible = trackBarTolerance.Visible = true;
        }
        Label labelPickedColor;
        public Color PickedColor
        {
            get => _pickedColor;
            set
            {
                if (!Equals(_pickedColor, value))
                {
                    _pickedColor = value;
                    labelPickedColor.BackColor = PickedColor;
                    pictureBox.Refresh();
                }
            }
        }
        Color _pickedColor = Color.White;


        private int _clickCount = 0;
        string ImageFolder { get; } = Path.Combine( 
            AppDomain.CurrentDomain.BaseDirectory, 
            "Images");
        string AppDataFolder { get; } = Path.Combine( 
            Environment.GetFolderPath( 
                Environment.SpecialFolder.LocalApplicationData),
                Assembly.GetExecutingAssembly().GetName().Name,
                "Images");

        private List<Bitmap> Layers { get; } = new List<Bitmap>();
    }
}
