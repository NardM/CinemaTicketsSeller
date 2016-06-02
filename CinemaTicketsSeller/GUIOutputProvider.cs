namespace CinemaTicketsSeller
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    class GUIOutputProvider
    {
        private string[] _filmText;
        private string[] _sessionText;

        private List<Control> _allControls;

        private Label[] _filmLabels;
        private Label[] _sessionLabels;
        private TextBox _filmDescriptionBox;
        private ListBox _lbForMovies;
        private ListBox _lbForSessions;

        private Button[] _buttonsLikeSeats;

        // Публиные и конструкторы

        public GUIOutputProvider(Control formToManipulate)
        {
            _allControls = new List<Control>();
            GetAllControlsFromRoot(formToManipulate);
        }

        public void PreloadForMainForm()
        {
            _filmLabels = GetAllLabelsWithTag("film");
            _sessionLabels = GetAllLabelsWithTag("session");

            _filmDescriptionBox = GetTextBoxByTag("filmDescription");
            _lbForMovies = GetListBoxByTag("moviesList");
            _lbForSessions = GetListBoxByTag("sessionsList");

            _filmText = CopySourceText(_filmLabels);
            _sessionText = CopySourceText(_sessionLabels);
        }

        public void PreloadForSeatsForm(int activeSessionId)
        {
            ButtonsFinder();
        }

        public void ManageListBoxes(Button first, Button second, Button third)
        {
            using (CinemaContext context = new CinemaContext())
            {
                _lbForMovies.Items.AddRange(context.MovieSet.ToArray());
                _lbForSessions.Items.AddRange(context.SessionSet.ToArray());
            }

            _lbForMovies.SelectedIndex = 0;
            _lbForSessions.SelectedIndex = 0;

            first.Enabled = true;
            second.Enabled = true;
            third.Enabled = true;
        }

        public void LoadInformationToLabels(bool isFilmLabels)
        {
            using (CinemaContext dbContext = new CinemaContext())
            {
                if (isFilmLabels)
                {
                    Movie informationAboutMovie =
                        (from movie in dbContext.MovieSet
                         where movie.Id == ((Movie)_lbForMovies.SelectedItem).Id
                         select movie).Single();

                    string[] sortedInformation =
                        new string[]
                        {
                        informationAboutMovie.Title, informationAboutMovie.MovieGenre.Name,
                        informationAboutMovie.Duration.ToString(), informationAboutMovie.StudioName,
                        informationAboutMovie.Country, informationAboutMovie.AgeRestrictions.ToString()
                        };

                    for (int i = 0; i < _filmLabels.Length; i++)
                    {
                        _filmLabels[i].Text = string.Format("{0} {1}", _filmText[i], sortedInformation[i]);
                    }

                    _filmDescriptionBox.Text = informationAboutMovie.Description;
                }
                else
                {
                    Session informationAboutSession =
                        (from session in dbContext.SessionSet
                         where session.Id == ((Session)_lbForSessions.SelectedItem).Id
                         select session).Single();

                    string[] sortedInformation =
                        new string[]
                        {
                            informationAboutSession.BeginTime.ToString(),
                            informationAboutSession.EndTime.ToString(),
                            informationAboutSession.TicketPrice.ToString()
                        };

                    for (int i = 0; i < _sessionLabels.Length; i++)
                    {
                        _sessionLabels[i].Text = string.Format("{0} {1:0.00}", _sessionText[i], sortedInformation[i]);
                    }
                }
            }
        }

        public void FilterSeansesByMovie()
        {
            using(CinemaContext dbContext = new CinemaContext())
            {
                int movieId = ((Movie)_lbForMovies.SelectedItem).Id;

                Session[] filteredSessions =
                    (from session in dbContext.SessionSet
                     where session.MovieId == movieId
                     select session).ToArray();

                _lbForSessions.Items.Clear();
                _lbForSessions.Items.AddRange(filteredSessions);
                _lbForSessions.SelectedIndex = 0;
            }
        }

        public void ResetFilter()
        {
            using (CinemaContext dbContext = new CinemaContext())
            {
                _lbForSessions.Items.Clear();
                _lbForSessions.Items.AddRange(dbContext.SessionSet.ToArray());
                _lbForSessions.SelectedIndex = 0;
            }
        }

        public void CheckSeats(int activeSessionId)
        {
            using (CinemaContext dbContext = new CinemaContext())
            {
                Session activeSession =
                    (from session in dbContext.SessionSet
                     where session.Id == activeSessionId
                     select session).Single();

                foreach (Ticket ticket in activeSession.Tickets)
                {
                    string seatRowCol = string.Format("{0}/{1}", ticket.Seat.Row, ticket.Seat.Column);
                    Button ticketSeat = FindButtonByText(seatRowCol);

                    if (ticket.Status == StatusType.Busy)
                    {
                        ticketSeat.BackColor = System.Drawing.Color.Red;
                        ticketSeat.Enabled = false;
                    }
                    else if (ticket.Status == StatusType.Booked)
                    {
                        ticketSeat.BackColor = System.Drawing.Color.Gray;
                        ticketSeat.Enabled = false;
                    }
                    else
                    {
                        ticketSeat.BackColor = System.Drawing.Color.Green;
                        ticketSeat.Enabled = true;
                    }

                    ticketSeat.Tag = ticket;
                }
            }
        }

        public void RunBuyer(Button pressed, Form2 currentForm)
        {
            using (CinemaContext dbContext = new CinemaContext())
            {
                Ticket current =
                    (from ticket in dbContext.TicketSet
                     where ticket.Id == ((Ticket)pressed.Tag).Id
                     select ticket).Single();

                Form3 form = new Form3();

                form.movieTitle = current.Session.Movie.Title;
                form.seatNum = string.Format("{0}/{1}", current.Seat.Row, current.Seat.Column);
                form.ticketId = current.Id;
                form.parentForm = currentForm;
                form.price = current.Session.TicketPrice.ToString();

                currentForm.Hide();
                form.Show();
            }
        }

        // Приватные методы

        private Label[] GetAllLabelsWithTag(string tag)
        {
            IEnumerable<Label> labelsHolder =
                from label in _allControls.OfType<Label>()
                where label.Tag != null
                where label.Tag.ToString() == tag
                select label;

            IComparer labelComparer = new ComparerForLabels();
            Label[] tempHolder = labelsHolder.ToArray();
            Array.Sort(tempHolder, labelComparer);
            return tempHolder;
        }

        private void GetAllControlsFromRoot(Control root)
        {
            _allControls.Add(root);

            foreach (Control control in root.Controls)
                GetAllControlsFromRoot(control); 
        }

        private TextBox GetTextBoxByTag(string tag)
        {
            TextBox textBoxHolder =
                (from textBox in _allControls.OfType<TextBox>()
                 where textBox.Tag != null
                 where textBox.Tag.ToString() == tag
                 select textBox).Single();

            return textBoxHolder;
        }

        private ListBox GetListBoxByTag(string tag)
        {
            ListBox listBoxHolder =
                (from listBox in _allControls.OfType<ListBox>()
                 where listBox.Tag != null
                 where listBox.Tag.ToString() == tag
                select listBox).Single();

            return listBoxHolder;
        }


        private string[] CopySourceText(Label[] labelBuffer)
        {
            string[] initialTextBuffer = new string[labelBuffer.Length];

            for (int i = 0; i < labelBuffer.Length; i++)
                initialTextBuffer[i] = labelBuffer[i].Text.Trim();

            return initialTextBuffer;
        }

        private void ButtonsFinder()
        {
            _buttonsLikeSeats =
                (from button in _allControls.OfType<Button>()
                 where button.Tag.ToString() != "static"
                 select button).ToArray();
        }

        private Button FindButtonByText(string text)
        {
             return
                (from button in _buttonsLikeSeats
                 where button.Text == text 
                 select button).Single();
        }
    }

    public class ComparerForLabels : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            Label first = (Label)x;
            Label second = (Label)y;

            if (first.TabIndex < second.TabIndex)
                return -1;
            else if (first.TabIndex == second.TabIndex)
                return 0;
            else
                return 1;
        }
    }
}
