using PaintDotNet;
using PaintDotNet.Effects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;

namespace PieChartEffect
{
    public class PluginSupportInfo : IPluginSupportInfo
    {
        public string Author => ((AssemblyCopyrightAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
        public string Copyright => ((AssemblyDescriptionAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0]).Description;
        public string DisplayName => ((AssemblyProductAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0]).Product;
        public Version Version => base.GetType().Assembly.GetName().Version;
        public Uri WebsiteUri => new Uri("http://forums.getpaint.net/index.php?showtopic=32169");
    }

    [PluginSupportInfo(typeof(PluginSupportInfo), DisplayName = "Pie Chart")]

    internal class PieChart : Effect<PieChartConfigToken>
    {
        public const string StaticName = "Pie Chart";
        public static readonly Bitmap StaticIcon = new Bitmap(typeof(PieChart), "PieChart.png");

        public PieChart()
            : base(StaticName, StaticIcon, SubmenuNames.Render, EffectFlags.Configurable)
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

            if (pieChartSurface == null)
                pieChartSurface = new Surface(srcArgs.Surface.Size);
            else
                pieChartSurface.Clear(Color.Transparent);

            if (overlaySurface == null)
                overlaySurface = new Surface(srcArgs.Surface.Size);
            else
                overlaySurface.Clear(Color.Transparent);

            if (donutHelperSurface == null)
                donutHelperSurface = new Surface(srcArgs.Surface.Size);
            else
                donutHelperSurface.Clear(Color.Transparent);

            Rectangle selection = EnvironmentParameters.GetSelection(srcArgs.Surface.Bounds).GetBoundsInt();
            PointF center = new PointF
            {
                X = (selection.Left + selection.Right) / 2f,
                Y = (selection.Top + selection.Bottom) / 2f
            };

            bool anyExplosions = false;
            double total = 0.0;
            try
            {
                anyExplosions = slices.Any(slice => slice.Exploded);
                total = slices.Sum(slice => slice.Value);
            }
            catch
            {
            }


            float baseDiameter = Math.Min((selection.Width - 4) * scale, (selection.Height - 4) * scale);

            float regDiameter = anyExplosions ? baseDiameter - (baseDiameter / 10f) : baseDiameter;
            float expDiameter = baseDiameter;

            PointF regOffset = new PointF
            {
                X = (selection.Width - regDiameter) / 2f + selection.Left,
                Y = (selection.Height - regDiameter) / 2f + selection.Top
            };
            PointF expOffset = new PointF
            {
                X = regOffset.X - (expDiameter / 20f),
                Y = regOffset.Y - (expDiameter / 20f)
            };

            // Draw the pie chart
            float start = angle * -1;
            float sweep = 0.0f;
            float diameter;
            PointF offset = Point.Empty;
            double labelAngle;
            PointF labelOffset = Point.Empty;
            float labelRadius = (donut) ? Math.Max(regDiameter / 2f * 0.75f, (regDiameter / 2f - regDiameter / 2f * donutSize) / 2f + regDiameter / 2f * donutSize) : regDiameter / 2f * 0.75f;

            using (SolidBrush sliceBrush = new SolidBrush(Color.Black))
            using (GraphicsPath labelPath = new GraphicsPath())
            using (FontFamily labelFont = new FontFamily("Tahoma"))
            using (Pen labelPen = new Pen(Color.FromArgb(153, Color.Black), 2.5f))
            using (Pen outlinePen = new Pen(outlineColor, 1))
            using (StringFormat labelFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (Graphics pieChartGraphics = new RenderArgs(pieChartSurface).Graphics)
            using (Graphics overlayGraphics = new RenderArgs(overlaySurface).Graphics)
            {
                pieChartGraphics.SmoothingMode = SmoothingMode.AntiAlias;

                overlayGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                overlayGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                try
                {
                    foreach (Slice slice in slices)
                    {
                        sweep = (float)(slice.Value / total * 360.0f);
                        sliceBrush.Color = slice.Color;

                        if (slice.Exploded)
                        {
                            diameter = expDiameter;
                            offset = expOffset;
                        }
                        else
                        {
                            diameter = regDiameter;
                            offset = regOffset;
                        }

                        // Fill Slice
                        pieChartGraphics.FillPie(sliceBrush, offset.X, offset.Y, diameter, diameter, start, sweep);

                        // Outline Slice
                        if (outlineColor == Color.Transparent)
                            outlinePen.Color = slice.Color;

                        outlinePen.Color = Color.FromArgb(85, outlinePen.Color);
                        pieChartGraphics.DrawPie(outlinePen, offset.X - 1, offset.Y, diameter, diameter, start, sweep);
                        pieChartGraphics.DrawPie(outlinePen, offset.X, offset.Y - 1, diameter, diameter, start, sweep);
                        pieChartGraphics.DrawPie(outlinePen, offset.X + 1, offset.Y, diameter, diameter, start, sweep);
                        pieChartGraphics.DrawPie(outlinePen, offset.X, offset.Y + 1, diameter, diameter, start, sweep);
                        outlinePen.Color = Color.FromArgb(255, outlinePen.Color);

                        pieChartGraphics.DrawPie(outlinePen, offset.X, offset.Y, diameter, diameter, start, sweep);

                        // Slice Label
                        if (labels)
                        {
                            labelAngle = Math.PI * (start + sweep / 2f) / 180f;
                            labelOffset.X = center.X + (float)(labelRadius * Math.Cos(labelAngle));
                            labelOffset.Y = center.Y + (float)(labelRadius * Math.Sin(labelAngle));

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
            }

            // Donut Stuff
            if (donut)
            {
                float donutDiameter = regDiameter * donutSize;
                PointF donutOffset = new PointF
                {
                    X = center.X - donutDiameter / 2f,
                    Y = center.Y - donutDiameter / 2f
                };

                if (outlineColor != Color.Transparent && slices.Count > 0)
                {
                    using (Graphics overlayGraphics = new RenderArgs(overlaySurface).Graphics)
                    using (Pen donutPen = new Pen(outlineColor, 1))
                    {
                        overlayGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                        donutPen.Color = Color.FromArgb(85, donutPen.Color);
                        overlayGraphics.DrawEllipse(donutPen, donutOffset.X - 1, donutOffset.Y, donutDiameter, donutDiameter);
                        overlayGraphics.DrawEllipse(donutPen, donutOffset.X, donutOffset.Y - 1, donutDiameter, donutDiameter);
                        overlayGraphics.DrawEllipse(donutPen, donutOffset.X + 1, donutOffset.Y, donutDiameter, donutDiameter);
                        overlayGraphics.DrawEllipse(donutPen, donutOffset.X, donutOffset.Y + 1, donutDiameter, donutDiameter);
                        donutPen.Color = Color.FromArgb(255, donutPen.Color);
                        overlayGraphics.DrawEllipse(donutPen, donutOffset.X, donutOffset.Y, donutDiameter, donutDiameter);
                    }
                }

                using (Graphics donutGraphics = new RenderArgs(donutHelperSurface).Graphics)
                {
                    donutGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                    donutGraphics.FillEllipse(Brushes.Black, donutOffset.X, donutOffset.Y, donutDiameter, donutDiameter);
                }
            }
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

        readonly BinaryPixelOp normalOp = LayerBlendModeUtil.CreateCompositionOp(LayerBlendMode.Normal);

        void Render(Surface dst, Surface src, Rectangle rect)
        {
            if (donut)
            {
                ColorBgra piePixel, overlayPixel;
                for (int y = rect.Top; y < rect.Bottom; y++)
                {
                    if (IsCancelRequested) return;
                    for (int x = rect.Left; x < rect.Right; x++)
                    {
                        piePixel = pieChartSurface[x, y];
                        piePixel.A = Int32Util.ClampToByte(piePixel.A - donutHelperSurface[x, y].A);

                        overlayPixel = overlaySurface[x, y];

                        dst[x, y] = normalOp.Apply(piePixel, overlayPixel);
                    }
                }
            }
            else
            {
                normalOp.Apply(pieChartSurface, rect.Location, overlaySurface, rect.Location, rect.Size);
                dst.CopySurface(pieChartSurface, rect.Location, rect);
            }
        }
    }
}
