using PaintDotNet.Effects;
using System.Collections.Generic;
using System.Drawing;

namespace PieChartEffect
{
    class PieChartConfigToken : EffectConfigToken
    {
        private List<Slice> slices = new List<Slice>();
        double angle = 0;
        Color outlineColor = Color.Black;
        double scale = 1;
        bool donut = false;
        bool labels = false;

        public PieChartConfigToken() : base()
        {
            this.Slices = slices;
            this.Angle = angle;
            this.OutlineColor = outlineColor;
            this.Scale = scale;
            this.Donut = donut;
            this.Labels = labels;
        }

        private PieChartConfigToken(PieChartConfigToken copyMe)
        {
            this.Slices = copyMe.Slices;
            this.Angle = copyMe.Angle;
            this.OutlineColor = copyMe.OutlineColor;
            this.Scale = copyMe.Scale;
            this.Donut = copyMe.Donut;
            this.Labels = copyMe.Labels;
        }
        
        public override object Clone()
        {
            return new PieChartConfigToken(this);
        }

        public List<Slice> Slices
        {
            get;
            set;
        }
        public double Angle
        {
            get;
            set;
        }
        public Color OutlineColor
        {
            get;
            set;
        }
        public double Scale
        {
            get;
            set;
        }
        public bool Donut
        {
            get;
            set;
        }
        public bool Labels
        {
            get;
            set;
        }
    }
}
