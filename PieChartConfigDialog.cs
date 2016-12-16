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
        private Image newRowImage;

        public PieChartConfigDialog()
        {
            InitializeComponent();

            this.ColumnValue.DefaultCellStyle.Alignment= DataGridViewContentAlignment.MiddleRight;

            float dpi = this.AutoScaleDimensions.Width / 96f;
            iconSize = (int)(16f * dpi);

            newRowImage = new Bitmap(iconSize, iconSize);
            using (Graphics g = Graphics.FromImage(newRowImage))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                PointF[] arrow = new PointF[7];
                arrow[0] = new PointF(8f * dpi, 1f * dpi);
                arrow[1] = new PointF(15f * dpi, 7f * dpi);
                arrow[2] = new PointF(8f * dpi, 13f * dpi);
                arrow[3] = new PointF(8f * dpi, 9f * dpi);
                arrow[4] = new PointF(1f * dpi, 9f * dpi);
                arrow[5] = new PointF(1f * dpi, 5f * dpi);
                arrow[6] = new PointF(8f * dpi, 5f * dpi);

                g.FillPolygon(Brushes.Black, arrow);
            }

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
            if (rowIndexOfItemUnderMouseToDrop == dataGridView1.Rows.Count - 1 || rowIndexOfItemUnderMouseToDrop == -1)
                return;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
                if (rowToMove == null)
                    return;

                dataGridView1.Rows.RemoveAt(rowIndexFromMouseDown);
                dataGridView1.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);

                dataGridView1.ClearSelection();
                dataGridView1.Rows[rowIndexOfItemUnderMouseToDrop].Selected = true;
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

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            double d;
            string newCellValue;

            if (dataGridView1.Rows[e.Row.Index - 1].Cells[ColumnValue.Index].Value != null)
                newCellValue = dataGridView1.Rows[e.Row.Index - 1].Cells[ColumnValue.Index].Value.ToString();
            else
                newCellValue = string.Empty;

            if (!double.TryParse(newCellValue, out d))
                d = 1;
            else if (d <= 0)
                d = 1;
            dataGridView1.Rows[e.Row.Index - 1].Cells[ColumnValue.Index].Value = d;


            newCellValue = (string)dataGridView1.Rows[e.Row.Index - 1].Cells[ColumnName.Index].Value;
            if (newCellValue == string.Empty || newCellValue == null)
                dataGridView1.Rows[e.Row.Index - 1].Cells[ColumnName.Index].Value = $"New Slice {e.Row.Index}";


            string rowColorName = (string)dataGridView1.Rows[e.Row.Index - 1].Cells[ColumnColor.Index].Value;
            Color rowColor = Color.FromName(rowColorName);
            if (!rowColor.IsKnownColor)
                rowColor = Color.FromArgb(Convert.ToInt32(rowColorName));
            string colorTooltip = $"{rowColor.R.ToString()}, {rowColor.G.ToString()}, {rowColor.B.ToString()}" +
                ((rowColor.IsKnownColor) ? $"\n({rowColor.Name})" : string.Empty);
            dataGridView1.Rows[e.Row.Index - 1].Cells[ColumnIcon.Index].ToolTipText = colorTooltip;

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

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == ColumnValue.Index || e.ColumnIndex == ColumnName.Index)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    oldCellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                else
                    oldCellValue = string.Empty;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColumnValue.Index)
            {
                double d;
                string newCellValue;
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    newCellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                else
                    newCellValue = string.Empty;

                if (oldCellValue == string.Empty)
                    oldCellValue = "1";

                if (!double.TryParse(newCellValue, out d))
                    d = double.Parse(oldCellValue);
                else if (d <= 0)
                    d = double.Parse(oldCellValue);

                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = d;
            }
            else if (e.ColumnIndex == ColumnName.Index)
            {
                string newCellValue = (string)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (newCellValue == string.Empty || newCellValue == null)
                {
                    if (oldCellValue == string.Empty)
                        oldCellValue = $"New Slice {e.RowIndex}";
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

                string colorName;
                if (slice.Color.IsKnownColor)
                    colorName = slice.Color.Name;
                else
                    colorName = slice.Color.ToArgb().ToString();

                string colorTooltip = $"{slice.Color.R.ToString()}, {slice.Color.G.ToString()}, {slice.Color.B.ToString()}" +
                    ((slice.Color.IsKnownColor) ? $"\n({slice.Color.Name})" : string.Empty);

                dataGridView1.Rows.Add(new object[] { colorIcon, colorName, slice.Name, slice.Value, slice.Exploded });
                dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[ColumnIcon.Index].ToolTipText = colorTooltip;
            }

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

                Color color = Color.FromName((string)row.Cells[ColumnColor.Index].Value);
                if (!color.IsKnownColor)
                    color = Color.FromArgb(Convert.ToInt32((string)row.Cells[ColumnColor.Index].Value));
                string name = (string)row.Cells[ColumnName.Index].Value;
                double value = (double)row.Cells[ColumnValue.Index].Value;
                bool exploded = (bool)row.Cells[ColumnExploded.Index].Value;

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

                    if (value <= 0)
                        continue;

                    randomColor = Color.FromName(colorList[random.Next(colorList.Count)]);

                    csvSlices.Add(new Slice(name, value, randomColor, false));
                }
            }

            if (csvSlices.Count == 0)
                return;

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
                    ((slice.Color.IsKnownColor) ? $"\n({slice.Color.Name})" : string.Empty);

                dataGridView1.Rows.Add(new object[] { colorIcon, slice.Color.Name, slice.Name, slice.Value, slice.Exploded });
                dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[ColumnIcon.Index].ToolTipText = colorTooltip;
            }

            dataGridView1.CurrentCellChanged += dataGridView1_CurrentCellChanged;

            FinishTokenUpdate();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

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

                string colorName;
                if (colorDialog1.Color.IsKnownColor)
                    colorName = colorDialog1.Color.Name;
                else
                    colorName = colorDialog1.Color.ToArgb().ToString();
                dataGridView1.Rows[e.RowIndex].Cells[ColumnColor.Index].Value = colorName;

                string colorTooltip = $"{colorDialog1.Color.R.ToString()}, {colorDialog1.Color.G.ToString()}, {colorDialog1.Color.B.ToString()}" +
                    ((colorDialog1.Color.IsKnownColor) ? $"\n({colorDialog1.Color.Name})" : string.Empty);
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = colorTooltip;

                FinishTokenUpdate();
            }
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            Color randomColor = Color.FromName(colorList[random.Next(colorList.Count)]);
            e.Row.Cells[ColumnIcon.Index].Value = new Bitmap(iconSize, iconSize);
            using (Graphics g = Graphics.FromImage((Bitmap)e.Row.Cells[ColumnIcon.Index].Value))
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
            e.Row.Cells[ColumnColor.Index].Value = randomColor.Name;
            //e.Row.Cells[ColumnName.Index].Value = string.Empty;
            //e.Row.Cells[ColumnValue.Index].Value = byte.MinValue;
            e.Row.Cells[ColumnExploded.Index].Value = false;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].IsNewRow && e.ColumnIndex == ColumnIcon.Index)
            {
                e.Value = newRowImage;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

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

                dataGridView1.Rows[e.RowIndex].Cells[ColumnColor.Index].Value = randomColor.Name;
                string colorTooltip = $"{randomColor.R.ToString()}, {randomColor.G.ToString()}, {randomColor.B.ToString()}" +
                    ((randomColor.IsKnownColor) ? $"\n({randomColor.Name})" : string.Empty);
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
