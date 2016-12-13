using PaintDotNet;
using PaintDotNet.Effects;
using System;
using System.Collections.Generic;
using System.Drawing;
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
                return new Uri("http://www.getpaint.net/redirect/plugins.html");
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
            labels = newToken.Labels;


            Rectangle selection = EnvironmentParameters.GetSelection(srcArgs.Surface.Bounds).GetBoundsInt();
            int xCenter = (selection.Left + selection.Right) / 2;
            int yCenter = (selection.Top + selection.Bottom) / 2;

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


            int baseDiameter = (selection.Width <= selection.Height) ? (int)((selection.Width - 4) * scale) : (int)((selection.Height - 4) * scale);

            int regDiameter = anyExplosions ? baseDiameter - (baseDiameter / 10) : baseDiameter;
            int expDiameter = baseDiameter;

            int regXOffset = (selection.Width - regDiameter) / 2;
            int regYOffset = (selection.Height - regDiameter) / 2;
            int expXOffset = regXOffset - (expDiameter / 20);
            int expYOffset = regYOffset - (expDiameter / 20);


            Bitmap pieChartBitmap = new Bitmap(selection.Width, selection.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics pieChartGraphics = Graphics.FromImage(pieChartBitmap);
            pieChartGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Bitmap overlayBitmap = new Bitmap(selection.Width, selection.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics overlayGraphics = Graphics.FromImage(overlayBitmap);
            overlayGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
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
            angle = angle > 0 ? -angle : Math.Abs(angle);
            float start = angle;
            float sweep = 0.0f;
            SolidBrush sliceBrush = new SolidBrush(Color.Black);
            Pen outlinePen = new Pen(outlineColor, 1);
            int diameter;
            int xOffset;
            int yOffset;

            Font labelFont = new Font(new FontFamily("Arial"), 12, FontStyle.Bold);
            SolidBrush labelBrush = new SolidBrush(Color.White);
            SolidBrush labelBrush2 = new SolidBrush(Color.FromArgb(75, Color.Black));
            StringFormat labelFormat = new StringFormat();
            labelFormat.Alignment = StringAlignment.Center;
            labelFormat.LineAlignment = StringAlignment.Center;
            double labelAngle;
            float labelXOffset;
            float labelYOffset;
            float labelRadius = (regDiameter / 2) * 0.75f;

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
                        labelXOffset = xCenter - selection.Left + (float)(labelRadius * Math.Cos(labelAngle));
                        labelYOffset = yCenter - selection.Top + (float)(labelRadius * Math.Sin(labelAngle));

                        // Hack to get a text outline
                        overlayGraphics.DrawString(slice.Name + "\n" + slice.Value.ToString(), labelFont, labelBrush2, labelXOffset + 1, labelYOffset + 1, labelFormat);
                        overlayGraphics.DrawString(slice.Name + "\n" + slice.Value.ToString(), labelFont, labelBrush2, labelXOffset - 1, labelYOffset - 1, labelFormat);
                        overlayGraphics.DrawString(slice.Name + "\n" + slice.Value.ToString(), labelFont, labelBrush2, labelXOffset + 1, labelYOffset - 1, labelFormat);
                        overlayGraphics.DrawString(slice.Name + "\n" + slice.Value.ToString(), labelFont, labelBrush2, labelXOffset - 1, labelYOffset + 1, labelFormat);
                        overlayGraphics.DrawString(slice.Name + "\n" + slice.Value.ToString(), labelFont, labelBrush2, labelXOffset + 1, labelYOffset + 0, labelFormat);
                        overlayGraphics.DrawString(slice.Name + "\n" + slice.Value.ToString(), labelFont, labelBrush2, labelXOffset - 1, labelYOffset - 0, labelFormat);
                        overlayGraphics.DrawString(slice.Name + "\n" + slice.Value.ToString(), labelFont, labelBrush2, labelXOffset + 0, labelYOffset + 1, labelFormat);
                        overlayGraphics.DrawString(slice.Name + "\n" + slice.Value.ToString(), labelFont, labelBrush2, labelXOffset - 0, labelYOffset - 1, labelFormat);

                        // Actual Label text
                        overlayGraphics.DrawString(slice.Name + "\n" + slice.Value.ToString(), labelFont, labelBrush, labelXOffset, labelYOffset, labelFormat);
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
            labelBrush.Dispose();
            labelBrush2.Dispose();
            labelFont.Dispose();


            // Donut Stuff
            if (donut)
            {
                int donutDiameter = regDiameter / 3;
                int donutXOffset = (selection.Width - donutDiameter) / 2;
                int donutYOffset = (selection.Height - donutDiameter) / 2;

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

                Bitmap donutBitmap = new Bitmap(selection.Width, selection.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics donutGraphics = Graphics.FromImage(donutBitmap);
                donutGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Brush donutBrush = new SolidBrush(Color.Black))
                    donutGraphics.FillEllipse(donutBrush, donutXOffset, donutYOffset, donutDiameter, donutDiameter);
                donutHelperSurface = Surface.CopyFromBitmap(donutBitmap);
                donutBitmap.Dispose();
            }

            pieChartSurface = Surface.CopyFromBitmap(pieChartBitmap);
            pieChartBitmap.Dispose();
            overlaySurface = Surface.CopyFromBitmap(overlayBitmap);
            overlayBitmap.Dispose();
        }

        protected override void OnRender(Rectangle[] rois, int startIndex, int length)
        {
            if (length == 0) return;
            for (int i = startIndex; i < startIndex + length; ++i)
            {
                Render(DstArgs.Surface, SrcArgs.Surface, rois[i]);
            }
        }

        List<Slice> slices;
        float angle;
        Color outlineColor;
        double scale;
        bool donut;
        bool labels;

        private Surface pieChartSurface;
        private Surface overlaySurface;
        private Surface donutHelperSurface;

        private BinaryPixelOp normalOp = LayerBlendModeUtil.CreateCompositionOp(LayerBlendMode.Normal);

        void Render(Surface dst, Surface src, Rectangle rect)
        {
            Rectangle selection = EnvironmentParameters.GetSelection(src.Bounds).GetBoundsInt();

            ColorBgra piePixel, donutPixel, overlayPixel;

            for (int y = rect.Top; y < rect.Bottom; y++)
            {
                if (IsCancelRequested) return;
                for (int x = rect.Left; x < rect.Right; x++)
                {
                    int x2 = x - selection.Left;
                    int y2 = y - selection.Top;

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
