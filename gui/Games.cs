namespace cs_games
{
    public partial class Games : Form
    {
        public static IEnumerable<Point> GetNextGamePosition()
        {
            for (int i = 0; ; i++)
                for (int j = 0; j < 3; j++)
                    yield return new Point(15 + 75 * i, 15 + 75 * j);
        }

        public Games()
        {
            InitializeComponent();

            Size size = new Size(50, 50);
            int i = 0;
            foreach (Point point in GetNextGamePosition())
            {
                if (i >= IGame.games.Count)
                    break;
                Button dynamicButton = new Button();
                dynamicButton.Text = IGame.games[i++].Name;
                dynamicButton.Size = size;
                dynamicButton.Location = point;
                
                groupBoxGames.Controls.Add(dynamicButton);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
