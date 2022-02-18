namespace cs_games.data_objects
{
    public class GameButton<G> : Button
    where G : Game
    {
        public static G? _game;
        public static GameField<G> _field = new GameField<G>();
        public static GameButton<G>[,] gameButtons = new GameButton<G>[0, 0];
        public static Label? _labelPlayerNow;

        public static void Create(GameField<G> field, G game, Label labelPlayerNow)
        {
            Game.Player1 = true;

            _field = field;
            gameButtons = new GameButton<G>[field.Height, field.Width];
            _game = game;
            _labelPlayerNow = labelPlayerNow;
        }

        public static void InitAll()
        {
            for (int i = 0; i < GameButton<G>.gameButtons.GetLength(0); i++)
                for (int j = 0; j < GameButton<G>.gameButtons.GetLength(1); j++)
                {
                    GameButton<G> button = GameButton<G>.gameButtons[i, j];
                    System.Diagnostics.Debug.WriteLine(button.Location);
                    button.Init();
                }
        }

        private int _x, _y;
        public int X
        {
            get => _x;
            set => _x = value;
        }
        public int Y
        {
            get => _y;
            set => _y = value;
        }

        public GameButton(int x, int y)
        {
            _x = x;
            _y = y;

            Init();

            GameButton<G>.gameButtons[x, y] = this;
        }

        public void Init()
        {
            try { Click -= StartMoveClick; } catch { }
            try { Click -= DoMoveClick; } catch { }

            if (_field[X, Y] != null)
            {
                if (_field[X, Y]?.CanMove() ?? false)
                {
                    Enabled = true;
                    Click += StartMoveClick;
                }
                else
                    Enabled = false;

                if (_field[X, Y]?.IMG != null)
                {
                    BackgroundImage = Image.FromFile(_field[X, Y]?.IMG ?? Game.GetIMGPath(""));
                    BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                    BackgroundImage = null;

            }
            else
            {
                Enabled = false;
                BackgroundImage = null;
            }
        }

        public void StartMoveClick(Object? sender, EventArgs e)
        {
            if (_field[X, Y] == null)
                return;
            if (_field[X, Y]?.Player1 != Game.Player1)
                return;

            for (int i = 0; i < _field.Width; i++)
                for (int j = 0; j < _field.Height; j++)
                    GameButton<G>.gameButtons[i, j].Enabled = false;

            foreach (int[] move in _field[X, Y]?.GetMoves() ?? new List<int[]>())
            {
                GameButton<G> button = GameButton<G>.gameButtons[move[0], move[1]];
                button.Enabled = true;
                button.Click += DoMoveClick;

                if (button.X == X && button.Y == Y)
                {
                    button.Click -= StartMoveClick;
                    button.PerformClick();
                }
            }
        }

        public void DoMoveClick(Object? sender, EventArgs e)
        {
            if (sender == null)
                return;
            if (_field[X, Y]?.Player1 != Game.Player1)
                return;

            GameButton<G> button = (GameButton<G>)sender;
            _field[X, Y]?.MoveTo(button.X, button.Y);

            Game.Player1 = !Game.Player1;

            if (GameButton<G>._labelPlayerNow != null)
                GameButton<G>._labelPlayerNow.Text = "Aktuell: " + Game.userList[((Game.Player1) ? 0 : 1)];

            GameButton<G>.InitAll();

            if (_game?.CheckIfWin(out bool winner) ?? false)
            {
                for (int i = 0; i < _field.Width; i++)
                    for (int j = 0; j < _field.Height; j++)
                        GameButton<G>.gameButtons[i, j].Enabled = false;
                MessageBox.Show($" (/ ^_^)/ Spieler {((winner) ? Game.userList[0] : Game.userList[1])} hat gewonnen \\(^_^ \\)", "Gewonnen");

                // TODO write into db
            }
        }
    }
}