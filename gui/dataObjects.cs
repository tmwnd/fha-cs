namespace cs_games.data_objects
{
    public class GameButton<G> : Button
    where G : Game
    {
        public static G? _game;
        public static GameField<G> _field = new GameField<G>();
        public static GameButton<G>[,] gameButtons = new GameButton<G>[0, 0];

        public static void Create(GameField<G> field, G game)
        {
            _field = field;
            gameButtons = new GameButton<G>[field.Height, field.Width];
            _game = game;
        }

        public static void InitAll()
        {
            for (int i = 0; i < GameButton<G>.gameButtons.GetLength(0); i++)
                for (int j = 0; j < GameButton<G>.gameButtons.GetLength(1); j++)
                {
                    GameButton<G> button = GameButton<G>.gameButtons[i, j];
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
                System.Diagnostics.Debug.WriteLine(X);
                System.Diagnostics.Debug.WriteLine(Y);
                if (_field[X, Y]?.CanMove() ?? false)
                {
                    Enabled = true;
                    Click += StartMoveClick;
                }

                if (_field[X, Y]?.IMG != null)
                {
                    BackgroundImage = Image.FromFile(_field[X, Y]?.IMG ?? Game.GetIMGPath(""));
                    BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                {
                    BackgroundImage = null;
                }
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

            GameButton<G> button = (GameButton<G>)sender;
            _field[X, Y]?.MoveTo(button.X, button.Y);

            GameButton<G>.InitAll();

            if (_game?.CheckIfWin(out bool winner) ?? false) {
                System.Diagnostics.Debug.WriteLine($"Spieler {((winner) ? "1": "2")} hat gewonnen");
                for (int i = 0; i < _field.Width; i++)
                    for (int j = 0; j < _field.Height; j++)
                        GameButton<G>.gameButtons[i, j].Enabled = false;
            }
        }
    }
}