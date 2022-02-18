namespace cs_games
{
    public partial class FormGames : Form
    {
        private FormGame? _game;
        public static IEnumerable<Point> GetNextGamePosition()
        {
            for (int i = 0; ; i++)
                for (int j = 0; j < 3; j++)
                    yield return new Point(15 + 75 * j, 15 + 100 * i);
        }

        public FormGames()
        {
            InitializeComponent();

            Size button_size = new Size(70, 70);
            Size combobox_size = new Size(70, 25);
            Point offset = new Point(0, 75);
            int i = 0;
            foreach (Point point in GetNextGamePosition())
            {
                if (i >= Game.Games.Count)
                    break;
                Game game = Game.Games[i++];
                Button dynamicButton = new Button();
                dynamicButton.Name = game.Name;
                //dynamicButton.Text = game.Name;
                dynamicButton.Size = button_size;
                dynamicButton.Location = point;
                dynamicButton.BackgroundImage = Image.FromFile(Game.GetIMGPath(game.Name));
                dynamicButton.BackgroundImageLayout = ImageLayout.Stretch;
                dynamicButton.Click += new EventHandler((object? sender, EventArgs e) =>
                {
                    if (_game != null)
                        _game.Game = game;
                    Close();
                });

                groupBoxGames.Controls.Add(dynamicButton);

                ComboBox dynamicComboBox = new ComboBox();
                
                dynamicComboBox.Size = combobox_size;
                dynamicComboBox.Location = new Point(point.X + offset.X, point.Y + offset.Y);
                dynamicComboBox.Enabled = false;
                dynamicComboBox.DropDownWidth = 200;

                dynamicComboBox.SelectedIndexChanged += (object? sender, EventArgs e) => { game.SkinIndex = ((ComboBox?)sender)?.SelectedIndex ?? 0; };

                foreach (string skin in game.SkinNames)
                {
                    dynamicComboBox.Items.Add(skin);
                    dynamicComboBox.Enabled = true;
                    dynamicComboBox.SelectedIndex = 0;
                }

                groupBoxGames.Controls.Add(dynamicComboBox);
            }
        }

        public FormGames(FormGame game) : this()
        {
            _game = game;
        }
    }
}
