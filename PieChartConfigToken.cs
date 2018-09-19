using PaintDotNet.Effects;
using System.Collections.Generic;
using System.Drawing;

namespace PieChartEffect
{
    internal class PieChartConfigToken : EffectConfigToken
    {
        public PieChartConfigToken()
        {
            this.Slices = new List<Slice>();
            this.Angle = 0;
            this.OutlineColor = Color.Black;
            this.Scale = 1;
            this.Donut = false;
            this.DonutSize = 0.333f;
            this.Labels = false;
        }

        private PieChartConfigToken(PieChartConfigToken copyMe)
        {
            this.Slices = copyMe.Slices;
            this.Angle = copyMe.Angle;
            this.OutlineColor = copyMe.OutlineColor;
            this.Scale = copyMe.Scale;
            this.Donut = copyMe.Donut;
            this.DonutSize = copyMe.DonutSize;
            this.Labels = copyMe.Labels;
        }

        public override object Clone()
        {
            return new PieChartConfigToken(this);
        }

        public List<Slice> Slices;
        public double Angle;
        public Color OutlineColor;
        public float Scale;
        public bool Donut;
        public float DonutSize;
        public bool Labels;
    }
}
