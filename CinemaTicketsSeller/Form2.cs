using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaTicketsSeller
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            _seatSelectForm = new GUIOutputProvider(this);
        }

        public int activeSessionId;
        public Form1 parentForm;

        private GUIOutputProvider _seatSelectForm;

        private void Form2_Load(object sender, EventArgs e)
        {
            _seatSelectForm.PreloadForSeatsForm(activeSessionId);
        }

        private void Form2_Activated(object sender, EventArgs e)
        {
            _seatSelectForm.CheckSeats(activeSessionId);
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            _seatSelectForm.PreloadForSeatsForm(activeSessionId);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            _seatSelectForm.RunBuyer((Button)sender, this);
        }
    }
}
