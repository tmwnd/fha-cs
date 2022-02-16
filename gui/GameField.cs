namespace fha_cs
{
    public partial class Spiel : Form
    {
        public Spiel()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ButtonStartGameClick(object sender, EventArgs e)
        {
            //GameField.Controls.Add(new Button());

            new gui.Games().ShowDialog();
        }
    }
}