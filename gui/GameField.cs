using cs_games.chess;
using cs_games.dame;
using cs_games.data_objects;
using cs_games.tic_tac_toe;
using cs_games.vier_gewinnt;

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

        private void ButtonStartGameClick(object sender, EventArgs e)
        {
            FormGames games = new FormGames(this);
            games.ShowDialog();

            if (_game == null)
                return;

            buttonNeu.Enabled = true;

            gameField.Controls.Clear();

            _game.Init();

            int size = Math.Min(gameField.Size.Width / _game.Width, (gameField.Size.Height - 25) / _game.Height);
            int dx = (gameField.Size.Width - size * _game.Width) / 2;
            int dy = (gameField.Size.Height - 25 - size * _game.Height) / 2 + 25;

            List<string> history = _game.History;
            textboxMatchHistory.Text = $"Letze Spiele ({history.Count}):";
            foreach (string line in history)
                textboxMatchHistory.Text += Environment.NewLine + line;

            // TODO remove switch
            switch (_game.Name)
            {
                case "Dame":
                    GameButton<Dame>.Create(((Dame)_game).Field, (Dame)_game, labelPlayerNow, labelPunkteSpieler1, labelPunkteSpieler2);
                    break;
                case "Chess":
                    GameButton<Chess>.Create(((Chess)_game).Field, (Chess)_game, labelPlayerNow, labelPunkteSpieler1, labelPunkteSpieler2);
                    break;
                case "TicTacToe":
                    GameButton<TicTacToe>.Create(((TicTacToe)_game).Field, (TicTacToe)_game, labelPlayerNow, labelPunkteSpieler1, labelPunkteSpieler2);
                    break;
                case "VierGewinnt":
                    GameButton<VierGewinnt>.Create(((VierGewinnt)_game).Field, (VierGewinnt)_game, labelPlayerNow, labelPunkteSpieler1, labelPunkteSpieler2);
                    break;
            }

            for (int i = 0; i < _game.Height; i++)
            {
                for (int j = 0; j < _game.Width; j++)
                {

                    Button dynamicButton;
                    switch (_game.Name)
                    {
                        case "Dame":
                            dynamicButton = new GameButton<Dame>(i, j);
                            break;
                        case "Chess":
                            dynamicButton = new GameButton<Chess>(i, j);
                            break;
                        case "TicTacToe":
                            dynamicButton = new GameButton<TicTacToe>(i, j);
                            break;
                        case "VierGewinnt":
                            dynamicButton = new GameButton<VierGewinnt>(i, j);
                            break;
                        default:
                            throw new Exception($"Spiel {_game.Name} wurde noch nicht implementert; Gerne auf https://github.com/tmwnd/fha-cs anfragen");
                    }

                    dynamicButton.Size = new Size(size - 5, size - 5);
                    dynamicButton.Location = new Point(dx + j * size, dy + i * size);

                    gameField.Controls.Add(dynamicButton);
                }
            }

            labelPlayerNow.Text = "Aktuell " + Game.userList[0];
        }

        private void NeuClick(object sender, EventArgs e)
        {
            if (_game == null)
                return;

            Game.Player1 = true;

            _game.Init();
            switch (_game.Name)
            {
                case "Dame":
                    GameButton<Dame>.InitAll();
                    break;
                case "Chess":
                    GameButton<Chess>.InitAll();
                    break;
                case "TicTacToe":
                    GameButton<TicTacToe>.InitAll();
                    break;
                case "VierGewinnt":
                    GameButton<VierGewinnt>.InitAll();
                    break;
            }

            labelPlayerNow.Text = "Aktuell: " + Game.userList[0];
            labelPunkteSpieler1.Text = "0";
            labelPunkteSpieler2.Text = "0";
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            spieler1.Text = Game.userList[0];
            spieler2.Text = Game.userList[1];
        }
    }
}