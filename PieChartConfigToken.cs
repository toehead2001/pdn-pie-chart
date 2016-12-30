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
        float scale = 1;
        bool donut = false;
        float donutSize = 0.333f;
        bool labels = false;

        public PieChartConfigToken() : base()
        {
            this.Slices = slices;
            this.Angle = angle;
            this.OutlineColor = outlineColor;
            this.Scale = scale;
            this.Donut = donut;
            this.DonutSize = donutSize;
            this.Labels = labels;
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
        public float Scale
        {
            get;
            set;
        }
        public bool Donut
        {
            get;
            set;
        }
        public float DonutSize
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
