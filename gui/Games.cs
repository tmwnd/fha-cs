namespace cs_games
{
    public partial class FormGames : Form
    {
        private FormGame? _game;
        public static IEnumerable<Point> GetNextGamePosition()
        {
            for (int i = 0; ; i++)
                for (int j = 0; j < 3; j++)
                    yield return new Point(15 + 75 * j, 15 + 75 * i);
        }

        public FormGames()
        {
            InitializeComponent();

            Size size = new Size(70, 70);
            int i = 0;
            foreach (Point point in GetNextGamePosition())
            {
                if (i >= Game.Games.Count)
                    break;
                Game game = Game.Games[i++];
                Button dynamicButton = new Button();
                dynamicButton.Name = game.Name;
                //dynamicButton.Text = game.Name;
                dynamicButton.Size = size;
                dynamicButton.Location = point;
                dynamicButton.BackgroundImage = Image.FromFile(Game.GetIMGPath(game.Name));
                dynamicButton.BackgroundImageLayout = ImageLayout.Stretch;
                dynamicButton.Click += new EventHandler((object sender, EventArgs e) =>
                {
                    if (_game != null)
                        _game.Game = game;
                    Close();
                });

                groupBoxGames.Controls.Add(dynamicButton);
            }
        }

        public FormGames(FormGame game) : this()
        {
            _game = game;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
