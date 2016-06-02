namespace CinemaTicketsSeller
{
    using System;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        // Запускается один раз при запуске программы
        public Form1()
        {
            InitializeComponent();

            _mainSeeder = new Seeder(); // Создаем объект, который отвечает за заполнение БД.

            _mainManipulator = new GUIOutputProvider(this); // Создаем объект, который отвечает за вывод информации
            _mainManipulator.PreloadForMainForm();
        }

        // Приватные переменные формы, для того, чтобы могли иметь к ним доступ из всех методов формы
        private GUIOutputProvider _mainManipulator;
        private Seeder _mainSeeder;

        private void DropDownMenuEl_Click(object sender, EventArgs e)
        {
            _mainSeeder.SeedDB();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _mainManipulator.FilterSeansesByMovie();
        }

        private void TurnOnDBInteraction_Click(object sender, EventArgs e)
        {
            _mainManipulator.ManageListBoxes(button1, button2, button3);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _mainManipulator.LoadInformationToLabels(true);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _mainManipulator.LoadInformationToLabels(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _mainManipulator.ResetFilter();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide(); // Прячем форму

            // Инциализируем форму выбора места в кинотеатре.
            Form2 form2 = new Form2();

            form2.activeSessionId = ((Session)listBox2.SelectedItem).Id;
            form2.parentForm = this;
            form2.Show();
        }
    }
}
