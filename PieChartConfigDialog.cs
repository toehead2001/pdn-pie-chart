using PaintDotNet.Effects;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PieChartEffect
{
    internal partial class PieChartConfigDialog : EffectConfigDialog<PieChart, PieChartConfigToken>
    {
        public PieChartConfigDialog()
        {
            InitializeComponent();
        }

        public void helpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PieChartConfigDialog));
            MessageBox.Show(resources.GetString("Help"), "Pie Chart - Help");
        }

        private Color getRandomColor()
        {
            Random randomGen = new Random();
            KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColor randomColorName = names[randomGen.Next(names.Length)];
            Color randomColor = Color.FromKnownColor(randomColorName);

            return randomColor;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Make sure there's a category name
            if (string.IsNullOrEmpty(tbCategoryName.Text))
            {
                tbCategoryName.Focus();
                return;
            }

            double d;
            if (!double.TryParse(tbCategoryValue.Text, out d))
            {
                MessageBox.Show(this, "Slice values must be numerical", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbCategoryValue.Focus();
                return;
            }

            // Ensure that the values are > 0.0
            if (d <= 0.0)
            {
                MessageBox.Show(this, "Slice values must be greater 0.0", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbCategoryValue.Focus();
                return;
            }

            // Create the item and add it to the list box
            Slice slice = new Slice(tbCategoryName.Text, Convert.ToDouble(tbCategoryValue.Text), pnlColor.BackColor, checkBoxExploded.Checked);
            lbCategories.Items.Add(slice);

            // Clear the fields for the next item
            tbCategoryName.Text = string.Empty;
            tbCategoryValue.Text = string.Empty;
            pnlColor.BackColor = getRandomColor();
            checkBoxExploded.Checked = false;

            // Focus back on the name box
            tbCategoryName.Focus();

            // Update
            FinishTokenUpdate();
        }

        private void btnRemoveCategory_Click(object sender, EventArgs e)
        {
            if ((lbCategories.SelectedIndex != -1) && (lbCategories.Items.Count == 1))
            {
                lbCategories.Items.RemoveAt(lbCategories.SelectedIndex);

                FinishTokenUpdate();
            }
            else if ((lbCategories.SelectedIndex != -1) && (lbCategories.SelectedIndex < lbCategories.Items.Count - 1))
            {
                lbCategories.SelectedIndex += 1;
                lbCategories.Items.RemoveAt(lbCategories.SelectedIndex - 1);

                FinishTokenUpdate();
            }
            else if ((lbCategories.SelectedIndex != -1) && (lbCategories.SelectedIndex == lbCategories.Items.Count - 1))
            {
                lbCategories.SelectedIndex -= 1;
                lbCategories.Items.RemoveAt(lbCategories.SelectedIndex + 1);

                FinishTokenUpdate();
            }
        }

        private void btnMoveUpCategory_Click(object sender, EventArgs e)
        {
            if (lbCategories.SelectedIndex > 0)
            {
                lbCategories.Items.Insert(lbCategories.SelectedIndex - 1, lbCategories.SelectedItem);
                lbCategories.SelectedIndex -= 2;
                lbCategories.Items.RemoveAt(lbCategories.SelectedIndex + 2);

                FinishTokenUpdate();
            }
        }

        private void btnMoveDownCategory_Click(object sender, EventArgs e)
        {
            if ((lbCategories.SelectedIndex != -1) && (lbCategories.SelectedIndex < lbCategories.Items.Count - 1))
            {
                lbCategories.Items.Insert(lbCategories.SelectedIndex + 2, lbCategories.SelectedItem);
                lbCategories.SelectedIndex += 2;
                lbCategories.Items.RemoveAt(lbCategories.SelectedIndex - 2);

                FinishTokenUpdate();
            }
        }

        private void lbCategories_DrawItem(object sender, DrawItemEventArgs e)
        {
            bool selected = lbCategories.SelectedIndex == e.Index;

            if (-1 == e.Index)
            {
                // Fill with background color and return
                using (SolidBrush temp = new SolidBrush(e.BackColor))
                {
                    e.Graphics.FillRectangle(temp, e.Bounds);
                }
                return;
            }

            // Get the item
            Slice slice = (Slice)lbCategories.Items[e.Index];
            
            // Start by filling the background
            if (selected)
            {
                using (SolidBrush hiLighB = new SolidBrush(SystemColors.Highlight))
                {
                    e.Graphics.FillRectangle(hiLighB, e.Bounds);
                }
            }
            else
            {
                using (SolidBrush backB = new SolidBrush(e.BackColor))
                {
                    e.Graphics.FillRectangle(backB, e.Bounds);
                }
            }

            // Draw a small box for the item's color
            Rectangle box = e.Bounds;
            box.Height -= 4;
            box.Width = box.Height;
            box.X += 2;
            box.Y += 2;
            SolidBrush boxBrush = new SolidBrush(slice.Color);
            e.Graphics.FillRectangle(boxBrush, box);

            // Draw the item's text
            SolidBrush textBrush = new SolidBrush(e.ForeColor);
            string itemText = slice.Name + " - " + slice.Value + (slice.Exploded ? " (Exploded)" : "");
            e.Graphics.DrawString(itemText, lbCategories.Font, textBrush, box.Right + 2, e.Bounds.Y);
        }

        private void lbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbCategories.Refresh();
        }

        private void pnlColor_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == colorDialog1.ShowDialog())
            {
                pnlColor.BackColor = colorDialog1.Color;
            }
        }

        private void angleSelector1_AngleChanged()
        {
            txtAngle1.Value = (decimal)angleSelector1.Angle;
        }

        private void txtAngle1_ValueChanged(object sender, EventArgs e)
        {
            if (txtAngle1.Value == txtAngle1.Maximum)
                txtAngle1.Value = txtAngle1.Minimum + 1;

            if (txtAngle1.Value == txtAngle1.Minimum)
                txtAngle1.Value = txtAngle1.Maximum - 1;

            angleSelector1.Angle = (double)txtAngle1.Value;

            //System.Threading.Thread.Sleep(50); // Work around for race condition with the slice list
            FinishTokenUpdate();
        }

        private void radioNone_CheckedChanged(object sender, EventArgs e)
        {
            FinishTokenUpdate();
        }

        private void radioBlack_CheckedChanged(object sender, EventArgs e)
        {
            FinishTokenUpdate();
        }

        private void radioGray_CheckedChanged(object sender, EventArgs e)
        {
            FinishTokenUpdate();
        }

        private void radioWhite_CheckedChanged(object sender, EventArgs e)
        {
            FinishTokenUpdate();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            FinishTokenUpdate();
        }

        private void checkBoxDonut_CheckedChanged(object sender, EventArgs e)
        {
            FinishTokenUpdate();
        }

        private void checkBoxLabels_CheckedChanged(object sender, EventArgs e)
        {
            FinishTokenUpdate();
        }

        #region EffectConfigDialog stuff

        protected override PieChartConfigToken CreateInitialToken()
        {
            return new PieChartConfigToken();
        }

        protected override void InitDialogFromToken(PieChartConfigToken fromToken)
        {
            // Start by clearing the data on the form
            tbCategoryName.Name = string.Empty;
            tbCategoryValue.Name = string.Empty;
            pnlColor.BackColor = getRandomColor();
            checkBoxExploded.Checked = false;
            lbCategories.Items.Clear();

            int i = 1;
            foreach (Slice tSlice in fromToken.Slices)
            {
                Slice slice = new Slice(tSlice.Name, tSlice.Value, tSlice.Color, tSlice.Exploded);
                lbCategories.Items.Add(slice);
                i++;
            }

            txtAngle1.Value = (decimal)fromToken.Angle;

            if (fromToken.OutlineColor == Color.Transparent)
                radioNone.Checked = true;
            else if (fromToken.OutlineColor == Color.White)
                radioWhite.Checked = true;
            else if (fromToken.OutlineColor == Color.FromArgb(68, 68, 68))
                radioGray.Checked = true;
            else
                radioBlack.Checked = true;

            numericUpDownScale.Value = (decimal)fromToken.Scale;

            checkBoxDonut.Checked = fromToken.Donut;

            checkBoxLabels.Checked = fromToken.Labels;
        }

        protected override void LoadIntoTokenFromDialog(PieChartConfigToken toToken)
        {
            toToken.Angle = (double)txtAngle1.Value;

            if (radioNone.Checked)
                toToken.OutlineColor = Color.Transparent;
            else if (radioWhite.Checked)
                toToken.OutlineColor = Color.White;
            else if (radioGray.Checked)
                toToken.OutlineColor = Color.FromArgb(68, 68, 68);
            else
                toToken.OutlineColor = Color.Black;

            toToken.Scale = (double)numericUpDownScale.Value;

            toToken.Donut = checkBoxDonut.Checked;

            toToken.Labels = checkBoxLabels.Checked;

            toToken.Slices.Clear();
            for (int i = 0; i < lbCategories.Items.Count; i++)
            {
                Slice slice = (Slice)lbCategories.Items[i];

                toToken.Slices.Add(new Slice(slice.Name, slice.Value, slice.Color, slice.Exploded));
            }
        }

        #endregion

    }

    internal class Slice
    {
        public Color Color;
        public string Name;
        public double Value;
        public bool Exploded;

        public Slice(string name, double value, Color color, bool exploded)
        {
            Name = name;
            Value = value;
            Color = color;
            Exploded = exploded;
        }
    }
}
