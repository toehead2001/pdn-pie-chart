using PaintDotNet;
using PaintDotNet.Effects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace PieChartEffect
{
    public class PluginSupportInfo : IPluginSupportInfo
    {
        public string Author
        {
            get
            {
                return ((AssemblyCopyrightAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
            }
        }
        public string Copyright
        {
            get
            {
                return ((AssemblyDescriptionAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0]).Description;
            }
        }

        public string DisplayName
        {
            get
            {
                return ((AssemblyProductAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0]).Product;
            }
        }

        public Version Version
        {
            get
            {
                return base.GetType().Assembly.GetName().Version;
            }
        }

        public Uri WebsiteUri
        {
            get
            {
                return new Uri("http://forums.getpaint.net/index.php?showtopic=32169");
            }
        }
    }

    [PluginSupportInfo(typeof(PluginSupportInfo), DisplayName = "Pie Chart")]

    internal class PieChart : Effect<PieChartConfigToken>
    {
        public static string StaticName
        {
            get
            {
                return "Pie Chart";
            }
        }

        public static Bitmap StaticIcon
        {
            get
            {
                return new Bitmap(typeof(PieChart), "PieChart.png");
            }
        }

        public static string SubmenuName
        {
            get
            {
                return SubmenuNames.Render;
            }
        }

        public PieChart()
            : base(StaticName, StaticIcon, SubmenuName, EffectFlags.Configurable)
        {
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            return new PieChartConfigDialog();
        }

        protected override void OnSetRenderInfo(PieChartConfigToken newToken, RenderArgs dstArgs, RenderArgs srcArgs)
        {
            slices = newToken.Slices;
            angle = (float)newToken.Angle;
            outlineColor = newToken.OutlineColor;
            scale = newToken.Scale;
            donut = newToken.Donut;
            donutSize = newToken.DonutSize;
            labels = newToken.Labels;


            Rectangle selection = EnvironmentParameters.GetSelection(srcArgs.Surface.Bounds).GetBoundsInt();
            float xCenter = (selection.Left + selection.Right) / 2f;
            float yCenter = (selection.Top + selection.Bottom) / 2f;

            bool anyExplosions = false;
            try
            {
                foreach (Slice slice in slices)
                {
                    if (slice.Exploded)
                    {
                        anyExplosions = true;
                        break;
                    }
                }
            }
            catch
            {
            }


            float baseDiameter = Math.Min((selection.Width - 4) * scale, (selection.Height - 4) * scale);

            float regDiameter = anyExplosions ? baseDiameter - (baseDiameter / 10f) : baseDiameter;
            float expDiameter = baseDiameter;

            float regXOffset = (selection.Width - regDiameter) / 2f;
            float regYOffset = (selection.Height - regDiameter) / 2f;
            float expXOffset = regXOffset - (expDiameter / 20f);
            float expYOffset = regYOffset - (expDiameter / 20f);


            Bitmap pieChartBitmap = new Bitmap(selection.Width, selection.Height);
            Graphics pieChartGraphics = Graphics.FromImage(pieChartBitmap);
            pieChartGraphics.SmoothingMode = SmoothingMode.AntiAlias;

            Bitmap overlayBitmap = new Bitmap(selection.Width, selection.Height);
            Graphics overlayGraphics = Graphics.FromImage(overlayBitmap);
            overlayGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            overlayGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;


            // Sum the slice values to get the total
            double total = 0.0;
            try
            {
                foreach (Slice slice in slices)
                    total += slice.Value;
            }
            catch
            {
            }

            // Draw the pie chart
            float start = angle * -1;
            float sweep = 0.0f;
            SolidBrush sliceBrush = new SolidBrush(Color.Black);
            Pen outlinePen = new Pen(outlineColor, 1);
            float diameter;
            float xOffset;
            float yOffset;

            GraphicsPath labelPath = new GraphicsPath();
            FontFamily labelFont = new FontFamily("Tahoma");
            Pen labelPen = new Pen(Color.FromArgb(153, Color.Black), 2.5f);
            StringFormat labelFormat = new StringFormat();
            labelFormat.Alignment = StringAlignment.Center;
            labelFormat.LineAlignment = StringAlignment.Center;
            double labelAngle;
            PointF labelOffset = new PointF();
            float labelRadius = regDiameter / 2f * 0.75f;
            if (donut)
                labelRadius = Math.Max(regDiameter / 2f * 0.75f, (regDiameter / 2f - regDiameter / 2f * donutSize) / 2f + regDiameter / 2f * donutSize);

            try
            {
                foreach (Slice slice in slices)
                {
                    sweep = (float)(slice.Value / total * 360.0f);
                    sliceBrush.Color = slice.Color;

                    if (slice.Exploded)
                    {
                        diameter = expDiameter;
                        xOffset = expXOffset;
                        yOffset = expYOffset;
                    }
                    else
                    {
                        diameter = regDiameter;
                        xOffset = regXOffset;
                        yOffset = regYOffset;
                    }

                    // Fill Slice
                    pieChartGraphics.FillPie(sliceBrush, xOffset, yOffset, diameter, diameter, start, sweep);

                    // Outline Slice
                    if (outlineColor == Color.Transparent)
                        outlinePen.Color = slice.Color;

                    outlinePen.Color = Color.FromArgb(85, outlinePen.Color);
                    pieChartGraphics.DrawPie(outlinePen, xOffset - 1, yOffset, diameter, diameter, start, sweep);
                    pieChartGraphics.DrawPie(outlinePen, xOffset, yOffset - 1, diameter, diameter, start, sweep);
                    pieChartGraphics.DrawPie(outlinePen, xOffset + 1, yOffset, diameter, diameter, start, sweep);
                    pieChartGraphics.DrawPie(outlinePen, xOffset, yOffset + 1, diameter, diameter, start, sweep);
                    outlinePen.Color = Color.FromArgb(255, outlinePen.Color);

                    pieChartGraphics.DrawPie(outlinePen, xOffset, yOffset, diameter, diameter, start, sweep);

                    // Slice Label
                    if (labels)
                    {
                        labelAngle = Math.PI * (start + sweep / 2f) / 180f;
                        labelOffset.X = xCenter - selection.Left + (float)(labelRadius * Math.Cos(labelAngle));
                        labelOffset.Y = yCenter - selection.Top + (float)(labelRadius * Math.Sin(labelAngle));

                        labelPath.Reset();
                        labelPath.AddString(slice.Name + "\n" + slice.Value, labelFont, (int)FontStyle.Bold, 14, labelOffset, labelFormat);
                        overlayGraphics.DrawPath(labelPen, labelPath);
                        overlayGraphics.FillPath(Brushes.White, labelPath);
                    }

                    start += sweep;
                }
            }
            catch
            {
            }


            // Clean up resources
            outlinePen.Dispose();
            sliceBrush.Dispose();
            labelPen.Dispose();
            labelPath.Dispose();
            labelFormat.Dispose();
            labelFont.Dispose();


            // Donut Stuff
            if (donut)
            {
                float donutDiameter = regDiameter * donutSize;
                float donutXOffset = (selection.Width - donutDiameter) / 2f;
                float donutYOffset = (selection.Height - donutDiameter) / 2f;

                if (outlineColor != Color.Transparent && slices.Count > 0)
                {
                    using (Pen donutPen = new Pen(outlineColor, 1))
                    {
                        donutPen.Color = Color.FromArgb(85, donutPen.Color);
                        overlayGraphics.DrawEllipse(donutPen, donutXOffset - 1, donutYOffset, donutDiameter, donutDiameter);
                        overlayGraphics.DrawEllipse(donutPen, donutXOffset, donutYOffset - 1, donutDiameter, donutDiameter);
                        overlayGraphics.DrawEllipse(donutPen, donutXOffset + 1, donutYOffset, donutDiameter, donutDiameter);
                        overlayGraphics.DrawEllipse(donutPen, donutXOffset, donutYOffset + 1, donutDiameter, donutDiameter);
                        donutPen.Color = Color.FromArgb(255, donutPen.Color);
                        overlayGraphics.DrawEllipse(donutPen, donutXOffset, donutYOffset, donutDiameter, donutDiameter);
                    }
                }

                Bitmap donutBitmap = new Bitmap(selection.Width, selection.Height);
                using (Graphics donutGraphics = Graphics.FromImage(donutBitmap))
                {
                    donutGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                    donutGraphics.FillEllipse(Brushes.Black, donutXOffset, donutYOffset, donutDiameter, donutDiameter);
                }
                donutHelperSurface = Surface.CopyFromBitmap(donutBitmap);
                donutBitmap.Dispose();
            }

            pieChartSurface = Surface.CopyFromBitmap(pieChartBitmap);
            pieChartGraphics.Dispose();
            pieChartBitmap.Dispose();
            overlaySurface = Surface.CopyFromBitmap(overlayBitmap);
            overlayGraphics.Dispose();
            overlayBitmap.Dispose();
        }

        protected override void OnRender(Rectangle[] renderRects, int startIndex, int length)
        {
            if (length == 0) return;
            for (int i = startIndex; i < startIndex + length; ++i)
            {
                Render(DstArgs.Surface, SrcArgs.Surface, renderRects[i]);
            }
        }

        List<Slice> slices;
        float angle;
        Color outlineColor;
        float scale;
        bool donut;
        float donutSize;
        bool labels;

        Surface pieChartSurface;
        Surface overlaySurface;
        Surface donutHelperSurface;

        BinaryPixelOp normalOp = LayerBlendModeUtil.CreateCompositionOp(LayerBlendMode.Normal);

        void Render(Surface dst, Surface src, Rectangle rect)
        {
            Rectangle selection = EnvironmentParameters.GetSelection(src.Bounds).GetBoundsInt();

            ColorBgra piePixel, donutPixel, overlayPixel;

            for (int y = rect.Top; y < rect.Bottom; y++)
            {
                if (IsCancelRequested) return;
                int y2 = y - selection.Top;
                for (int x = rect.Left; x < rect.Right; x++)
                {
                    int x2 = x - selection.Left;

                    piePixel = pieChartSurface.GetBilinearSample(x2, y2);
                    if (donut)
                    {
                        donutPixel = donutHelperSurface.GetBilinearSample(x2, y2);
                        piePixel.A = Int32Util.ClampToByte(piePixel.A - donutPixel.A);
                    }

                    overlayPixel = overlaySurface.GetBilinearSample(x2, y2);

                    dst[x, y] = normalOp.Apply(piePixel, overlayPixel);
                }
            }
        }
    }
}
