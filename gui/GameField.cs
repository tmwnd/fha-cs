using cs_games.data_objects;
using cs_games.dame;
using cs_games.chess;

namespace cs_games
{
    public partial class FormGame : Form
    {
        private Game? _game;
        public Game Game
        {
            set { _game = value; }
        }

        public FormGame()
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

            FormGames games = new FormGames(this);
            games.ShowDialog();

            if (_game == null)
                return;

            gameField.Controls.Clear();

            _game.Init();

            int size = Math.Min(gameField.Size.Width / _game.Width, (gameField.Size.Height - 25) / _game.Height);
            int dx = (gameField.Size.Width - size * _game.Width) / 2;
            int dy = (gameField.Size.Height - 25 - size * _game.Height) / 2 + 25;

            for (int i = 0; i < _game.Width; i++)
            {
                for (int j = 0; j < _game.Height; j++)
                {
                    Button dynamicButton = new Button();
                    dynamicButton.Size = new Size(size - 5, size - 5);
                    dynamicButton.Location = new Point(dx + j * size, dy + i * size);
                    dynamicButton.Enabled = false;

                    switch (_game.Name)
                    {
                        case "Dame":
                            new GameButton<Dame>(dynamicButton, (_game as Dame).Field[i, j]);
                            break;
                        case "Chess":
                            new GameButton<Chess>(dynamicButton, (_game as Chess).Field[i, j]);
                            break;
                    }

                    gameField.Controls.Add(dynamicButton);
                }
            }
        }
    }
}