using PaintDotNet.Effects;
using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualBasic.FileIO;
using System.Windows.Forms;

namespace PieChartEffect
{
    internal partial class PieChartConfigDialog : EffectConfigDialog<PieChart, PieChartConfigToken>
    {
        private Random random = new Random();
        private List<string> colorList = new List<string>();
        private int iconSize;
        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;
        private string oldCellValue;
        private readonly Image plusImage = new Bitmap(typeof(PieChartConfigDialog), "Plus.png");

        public PieChartConfigDialog()
        {
            InitializeComponent();

            float dpi = this.AutoScaleDimensions.Width / 96f;
            iconSize = (int)(16f * dpi);

            foreach (var prop in typeof(Color).GetProperties())
            {
                if (prop.PropertyType.Name == "Color" && prop.Name != "Transparent")
                    colorList.Add(prop.Name);
            }
        }

        public void helpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PieChartConfigDialog));
            MessageBox.Show(resources.GetString("Help"), "Pie Chart - Help");
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

        #region DataGridView functions
        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    // Proceed with the drag and drop, passing in the list item.
                    DragDropEffects dropEffect = dataGridView1.DoDragDrop(
                          dataGridView1.Rows[rowIndexFromMouseDown],
                          DragDropEffects.Move);
                }
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown = dataGridView1.HitTest(e.X, e.Y).RowIndex;

            if (rowIndexFromMouseDown != -1 && !dataGridView1.Rows[rowIndexFromMouseDown].IsNewRow)
            {
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(
                          new Point(
                            e.X - (dragSize.Width / 2),
                            e.Y - (dragSize.Height / 2)),
                      dragSize);
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            rowIndexOfItemUnderMouseToDrop = dataGridView1.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
                dataGridView1.Rows.RemoveAt(rowIndexFromMouseDown);
                dataGridView1.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);

            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //FinishTokenUpdate();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            FinishTokenUpdate();
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            FinishTokenUpdate();
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            FinishTokenUpdate();
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == ColumnExploded.Index && e.RowIndex != -1)
            {
                dataGridView1.EndEdit();
                FinishTokenUpdate();
            }
        }

        private void dataGridView1_NumberSort(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Index == ColumnValue.Index)
            {
                e.SortResult = double.Parse(e.CellValue1.ToString()).CompareTo(double.Parse(e.CellValue2.ToString()));
                e.Handled = true;//pass by the default sorting
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == ColumnValue.Index || e.ColumnIndex == ColumnName.Index)
            {
                oldCellValue = (string)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColumnValue.Index)
            {
                double d;
                string newCellValue = (string)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (newCellValue == string.Empty || !double.TryParse(newCellValue, out d))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = oldCellValue;
                }
            }
            else if (e.ColumnIndex == ColumnName.Index)
            {
                string newCellValue = (string)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (newCellValue == string.Empty)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = oldCellValue;
                }
            }
        }

        private void pnlColor_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle swatchRect = e.ClipRectangle;
            swatchRect.Width--;
            swatchRect.Height--;

            g.DrawRectangle(Pens.Black, swatchRect);
            swatchRect.Width -= 2;
            swatchRect.Height -= 2;
            swatchRect.Offset(1, 1);
            g.DrawRectangle(Pens.White, swatchRect);
        }
        #endregion

        #region EffectConfigDialog stuff
        protected override PieChartConfigToken CreateInitialToken()
        {
            return new PieChartConfigToken();
        }

        protected override void InitDialogFromToken(PieChartConfigToken effectTokenCopy)
        {
            #region DataGridView
            dataGridView1.RowsAdded -= dataGridView1_RowsAdded;
            dataGridView1.RowsRemoved -= dataGridView1_RowsRemoved;
            dataGridView1.CurrentCellChanged -= dataGridView1_CurrentCellChanged;

            // note: don't change to foreach
            int rowCount = dataGridView1.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                if (!dataGridView1.Rows[0].IsNewRow)
                    dataGridView1.Rows.RemoveAt(0);
            }

            foreach (Slice slice in effectTokenCopy.Slices)
            {
                Bitmap colorIcon = new Bitmap(iconSize, iconSize);
                using (Graphics g = Graphics.FromImage(colorIcon))
                using (SolidBrush color = new SolidBrush(slice.Color))
                {
                    Rectangle rect = new Rectangle((int)g.VisibleClipBounds.X, (int)g.VisibleClipBounds.Y, (int)g.VisibleClipBounds.Width, (int)g.VisibleClipBounds.Height);
                    g.FillRectangle(color, g.ClipBounds);
                    rect.Width--;
                    rect.Height--;
                    g.DrawRectangle(Pens.Black, rect);
                    rect.Width -= 2;
                    rect.Height -= 2;
                    rect.Offset(1, 1);
                    g.DrawRectangle(Pens.White, rect);
                }

                string colorTooltip = $"{slice.Color.R.ToString()}, {slice.Color.G.ToString()}, {slice.Color.B.ToString()}" +
                    ((slice.Color.IsNamedColor) ? $"\n({slice.Color.ToKnownColor().ToString()})" : string.Empty);

                dataGridView1.Rows.Add(new object[] { colorIcon, slice.Color.ToArgb().ToString(), slice.Name, slice.Value.ToString(), slice.Exploded });
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].ToolTipText = colorTooltip;
            }

            dataGridView1.RowsAdded += dataGridView1_RowsAdded;
            dataGridView1.RowsRemoved += dataGridView1_RowsRemoved;
            dataGridView1.CurrentCellChanged += dataGridView1_CurrentCellChanged;
            #endregion

            txtAngle1.Value = (decimal)effectTokenCopy.Angle;

            if (effectTokenCopy.OutlineColor == Color.Transparent)
                radioNone.Checked = true;
            else if (effectTokenCopy.OutlineColor == Color.White)
                radioWhite.Checked = true;
            else if (effectTokenCopy.OutlineColor == Color.FromArgb(68, 68, 68))
                radioGray.Checked = true;
            else
                radioBlack.Checked = true;

            numericUpDownScale.Value = (decimal)effectTokenCopy.Scale;

            checkBoxDonut.Checked = effectTokenCopy.Donut;

            checkBoxLabels.Checked = effectTokenCopy.Labels;
        }

        protected override void LoadIntoTokenFromDialog(PieChartConfigToken writeValuesHere)
        {
            writeValuesHere.Angle = (double)txtAngle1.Value;

            if (radioNone.Checked)
                writeValuesHere.OutlineColor = Color.Transparent;
            else if (radioWhite.Checked)
                writeValuesHere.OutlineColor = Color.White;
            else if (radioGray.Checked)
                writeValuesHere.OutlineColor = Color.FromArgb(68, 68, 68);
            else
                writeValuesHere.OutlineColor = Color.Black;

            writeValuesHere.Scale = (double)numericUpDownScale.Value;

            writeValuesHere.Donut = checkBoxDonut.Checked;

            writeValuesHere.Labels = checkBoxLabels.Checked;

            writeValuesHere.Slices.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow)
                    continue;

                Color color = Color.FromArgb(Convert.ToInt32((string)row.Cells[1].Value));
                string name = (string)row.Cells[2].Value;
                double value = Convert.ToDouble((string)row.Cells[3].Value);
                bool exploded = (bool)row.Cells[4].Value;

                writeValuesHere.Slices.Add(new Slice(name, value, color, exploded));
            }
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != openFileDialog1.ShowDialog())
                return;

            List<Slice> csvSlices = new List<Slice>();

            using (TextFieldParser parser = new TextFieldParser(openFileDialog1.FileName))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                string[] fields;
                string name;
                double value;
                Color randomColor;
                while (!parser.EndOfData)
                {
                    //Process row
                    try {
                        fields = parser.ReadFields(); }
                    catch { return; }

                    if (fields.Length < 2)
                        continue;

                    if (double.TryParse(fields[0], out value))
                        name = fields[1];
                    else if (double.TryParse(fields[1], out value))
                        name = fields[0];
                    else
                        continue;

                    randomColor = Color.FromName(colorList[random.Next(colorList.Count)]);

                    csvSlices.Add(new Slice(name, value, randomColor, false));
                }
            }

            if (csvSlices.Count == 0)
                return;

            dataGridView1.RowsAdded -= dataGridView1_RowsAdded;
            dataGridView1.RowsRemoved -= dataGridView1_RowsRemoved;
            dataGridView1.CurrentCellChanged -= dataGridView1_CurrentCellChanged;

            foreach (Slice slice in csvSlices)
            {
                Bitmap colorIcon = new Bitmap(iconSize, iconSize);
                using (Graphics g = Graphics.FromImage(colorIcon))
                using (SolidBrush color = new SolidBrush(slice.Color))
                {
                    Rectangle rect = new Rectangle((int)g.VisibleClipBounds.X, (int)g.VisibleClipBounds.Y, (int)g.VisibleClipBounds.Width, (int)g.VisibleClipBounds.Height);
                    g.FillRectangle(color, g.ClipBounds);
                    rect.Width--;
                    rect.Height--;
                    g.DrawRectangle(Pens.Black, rect);
                    rect.Width -= 2;
                    rect.Height -= 2;
                    rect.Offset(1, 1);
                    g.DrawRectangle(Pens.White, rect);
                }

                string colorTooltip = $"{slice.Color.R.ToString()}, {slice.Color.G.ToString()}, {slice.Color.B.ToString()}" +
                    ((slice.Color.IsNamedColor) ? $"\n({slice.Color.ToKnownColor().ToString()})" : string.Empty);

                dataGridView1.Rows.Add(new object[] { colorIcon, slice.Color.ToArgb().ToString(), slice.Name, slice.Value.ToString(), slice.Exploded });
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].ToolTipText = colorTooltip;
            }

            dataGridView1.RowsAdded += dataGridView1_RowsAdded;
            dataGridView1.RowsRemoved += dataGridView1_RowsRemoved;
            dataGridView1.CurrentCellChanged += dataGridView1_CurrentCellChanged;

            FinishTokenUpdate();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColumnIcon.Index && !dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                if (DialogResult.OK != colorDialog1.ShowDialog())
                    return;

                using (Graphics g = Graphics.FromImage((Bitmap)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                using (SolidBrush color = new SolidBrush(colorDialog1.Color))
                {
                    Rectangle rect = new Rectangle((int)g.VisibleClipBounds.X, (int)g.VisibleClipBounds.Y, (int)g.VisibleClipBounds.Width, (int)g.VisibleClipBounds.Height);
                    g.FillRectangle(color, g.ClipBounds);
                    rect.Width--;
                    rect.Height--;
                    g.DrawRectangle(Pens.Black, rect);
                    rect.Width -= 2;
                    rect.Height -= 2;
                    rect.Offset(1, 1);
                    g.DrawRectangle(Pens.White, rect);
                }

                dataGridView1.Rows[e.RowIndex].Cells[1].Value = colorDialog1.Color.ToArgb().ToString();
                string colorTooltip = $"{colorDialog1.Color.R.ToString()}, {colorDialog1.Color.G.ToString()}, {colorDialog1.Color.B.ToString()}" +
                    ((colorDialog1.Color.IsNamedColor) ? $"\n({colorDialog1.Color.ToKnownColor().ToString()})" : string.Empty);
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = colorTooltip;

                FinishTokenUpdate();
            }
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            Color randomColor = Color.FromName(colorList[random.Next(colorList.Count)]);
            e.Row.Cells[0].Value = new Bitmap(iconSize, iconSize);
            using (Graphics g = Graphics.FromImage((Bitmap)e.Row.Cells[0].Value))
            using (SolidBrush color = new SolidBrush(randomColor))
            {
                Rectangle rect = new Rectangle((int)g.VisibleClipBounds.X, (int)g.VisibleClipBounds.Y, (int)g.VisibleClipBounds.Width, (int)g.VisibleClipBounds.Height);
                g.FillRectangle(color, g.ClipBounds);
                rect.Width--;
                rect.Height--;
                g.DrawRectangle(Pens.Black, rect);
                rect.Width -= 2;
                rect.Height -= 2;
                rect.Offset(1, 1);
                g.DrawRectangle(Pens.White, rect);
            }
            e.Row.Cells[1].Value = randomColor.ToArgb().ToString();
            //e.Row.Cells[2].Value = string.Empty;
            //e.Row.Cells[3].Value = byte.MinValue.ToString();
            e.Row.Cells[4].Value = false;

            string colorTooltip = $"{randomColor.R.ToString()}, {randomColor.G.ToString()}, {randomColor.B.ToString()}" +
                    ((randomColor.IsNamedColor) ? $"\n({randomColor.ToKnownColor().ToString()})" : string.Empty);
            e.Row.Cells[0].ToolTipText = colorTooltip;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].IsNewRow && e.ColumnIndex == ColumnIcon.Index)
            {
                e.Value = plusImage;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColumnIcon.Index && !dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                Color randomColor = Color.FromName(colorList[random.Next(colorList.Count)]);
                using (Graphics g = Graphics.FromImage((Bitmap)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                using (SolidBrush color = new SolidBrush(randomColor))
                {
                    Rectangle rect = new Rectangle((int)g.VisibleClipBounds.X, (int)g.VisibleClipBounds.Y, (int)g.VisibleClipBounds.Width, (int)g.VisibleClipBounds.Height);
                    g.FillRectangle(color, g.ClipBounds);
                    rect.Width--;
                    rect.Height--;
                    g.DrawRectangle(Pens.Black, rect);
                    rect.Width -= 2;
                    rect.Height -= 2;
                    rect.Offset(1, 1);
                    g.DrawRectangle(Pens.White, rect);
                }

                dataGridView1.Rows[e.RowIndex].Cells[1].Value = randomColor.ToArgb().ToString();
                string colorTooltip = $"{randomColor.R.ToString()}, {randomColor.G.ToString()}, {randomColor.B.ToString()}" +
                    ((randomColor.IsNamedColor) ? $"\n({randomColor.ToKnownColor().ToString()})" : string.Empty);
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = colorTooltip;

                FinishTokenUpdate();
            }
        }
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
