using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cross_game
{
    public partial class Form1 : Form
    {
        private Button[] buttons;
        private Button curBtn;
        private int Srow, Scol;
        private int Erow, Ecol;

        public Form1()
        {
            InitializeComponent();
            InitializeGroupedButtons();
            RestartBtn.Click += OnClickRestartBtn;
            BaseTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

        }
        private void InitializeGroupedButtons()
        {
            buttons = new Button[]
            {
                button1,button2,button3,button4,button5,button6,button7,button8,button9,button10,
                button11,button12,button13,button14,button15,button16,button17,button18,button19,button20,
                button21,button22,button23,button24,button25,button26,button27,button28,button29,button30,
                button31,button32
            };

            for (int i = 0; i < 32; i++)
            {
                buttons[i].MouseDown += MouseDownEvent;
                buttons[i].MouseUp += MouseUpEvent;

            }

        }
        private void MouseDownEvent(Object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(Panel))
            {
                return;
            }
            curBtn = sender as Button;

            Point p1 = curBtn.PointToScreen(new Point(e.X, e.Y));
            p1 = BaseTableLayoutPanel.PointToClient(p1);

            int iBoxWidht = BaseTableLayoutPanel.Width / 7;
            int iBoxHeight = BaseTableLayoutPanel.Height / 7;

            Srow = p1.Y / (iBoxHeight);
            Scol = p1.X / iBoxWidht;

            
        }

        private void MouseUpEvent(Object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(Panel))
            {
                return;
            }

            if (curBtn!=null)
            {
                Point p = curBtn.PointToScreen(new Point(e.X, e.Y));
                p = BaseTableLayoutPanel.PointToClient(p);

                int iBoxWidht = BaseTableLayoutPanel.Width / 7;
                int iBoxHeight = BaseTableLayoutPanel.Height / 7;

                Erow = p.Y / (iBoxHeight);
                Ecol = p.X / iBoxWidht;

                if (ContainsBtn(BaseTableLayoutPanel.GetControlFromPosition(Ecol, Erow)))
                {
                    return;
                }



                int diffRow =Erow-Srow ;
                int diffCol =Ecol-Scol ;

                //up
                if (diffRow == -2 && diffCol == 0)
                {
                    BaseTableLayoutPanel.Controls.Remove(BaseTableLayoutPanel.GetControlFromPosition(Scol, Srow-1));
                    textBox1.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) - 1);
                }
                //down
                else if (diffRow == 2 && diffCol == 0)
                {
                    BaseTableLayoutPanel.Controls.Remove(BaseTableLayoutPanel.GetControlFromPosition(Ecol, Erow - 1));
                    textBox1.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) - 1);
                }
                //right
                else if (diffRow == 0 && diffCol == 2)
                {
                    BaseTableLayoutPanel.Controls.Remove(BaseTableLayoutPanel.GetControlFromPosition(Ecol-1, Erow));
                    textBox1.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) - 1);
                }
                //left
                else if (diffRow == 0 && diffCol == -2)
                {
                    BaseTableLayoutPanel.Controls.Remove(BaseTableLayoutPanel.GetControlFromPosition(Scol-1, Erow));
                    textBox1.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) - 1);
                }
                else
                {
                    MessageBox.Show("Game Over\nPoints: "+textBox1.Text);
                    OnClickRestartBtn(null,null);
                    return;
                }
                BaseTableLayoutPanel.Controls.Add(curBtn, Ecol, Erow);



                //Control c = BaseTableLayoutPanel.GetControlFromPosition(0, 3);
                //BaseTableLayoutPanel.Controls.Remove(BaseTableLayoutPanel.GetControlFromPosition(3, 0));

                //foreach (Control i in c.Controls)
                //{

                //     BaseTableLayoutPanel.Controls.Remove(i);

                //}


            }
            curBtn = null;
        }
        private bool ContainsBtn(Control panel)
        {
            if (panel == null) return false;
            foreach (Control c in panel.Controls)
            {
                if (c is Button)
                {
                    return true;
                }


            }
            return false;
        }
        //private bool ContainsPanel(Point cursorPoints, Panel ContainerPanel)
        //{


        //    if ((cursorPoints.X > ContainerPanel.Location.X && cursorPoints.X < (ContainerPanel.Location.X + ContainerPanel.Width)) && (cursorPoints.Y > ContainerPanel.Location.Y && cursorPoints.Y < (ContainerPanel.Location.Y + ContainerPanel1.Height)))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        private void OnClickRestartBtn(object sender, EventArgs e)
        {

            //Point p = this.PointToClient(new Point(67, 60));

            this.Close();

            System.Diagnostics.Process.Start(Application.ExecutablePath);

        }


    }
}
