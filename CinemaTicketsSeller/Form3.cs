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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public int ticketId;
        public string seatNum;
        public string movieTitle;
        public string price;
        public Form2 parentForm;

        private void Form3_Load(object sender, EventArgs e)
        {
            label1.Text = string.Format("Фильм: {0}", movieTitle);
            label2.Text = string.Format("Место: {0}", seatNum);
            label3.Text = string.Format("Цена: {0}", price);
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetTicketStatus(StatusType.Busy);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetTicketStatus(StatusType.Booked);
            Close();
        }

        private void SetTicketStatus(StatusType statusType)
        {
            using (CinemaContext dbContext = new CinemaContext())
            {
                Ticket updatedTicket =
                    (from ticket in dbContext.TicketSet
                     where ticket.Id == ticketId
                     select ticket).Single();

                updatedTicket.Status = statusType;

                dbContext.SaveChanges();
            }
        }
    }
}
