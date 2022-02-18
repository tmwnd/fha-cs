namespace cs_games.data_objects
{
    public class GameButton<G> : Button
    where G : Game
    {
        public static G? _game;
        public static GameField<G> _field = new GameField<G>();
        public static GameButton<G>[,] gameButtons = new GameButton<G>[0, 0];
        public static Label? _labelPlayerNow;
        public static Label? _labelPointsPlayer1;
        public static Label? _labelPointsPlayer2;

        public static void Create(GameField<G> field, G game, params Label[] labels)
        {
            Game.Player1 = true;

            _field = field;
            gameButtons = new GameButton<G>[field.Height, field.Width];
            _game = game;
            _labelPlayerNow = labels[0];
            _labelPointsPlayer1 = labels[1];
            _labelPointsPlayer2 = labels[2];
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

            for (int i = 0; i < _field.Height; i++)
                for (int j = 0; j < _field.Width; j++)
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

                    foreach (int[] index in _field[X, Y]?.GetMoves() ?? new List<int[]>())
                        gameButtons[index[0], index[1]].Enabled = true;
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
            bool take = _field[X, Y]?.MoveTo(button.X, button.Y) ?? false;

            if (take)
            {
                if (Game.Player1)
                {
                    if (GameButton<G>._labelPointsPlayer1 != null)
                        GameButton<G>._labelPointsPlayer1.Text = IntToPoints(PointsToInt(GameButton<G>._labelPointsPlayer1.Text) + 1);
                }
                else
                {
                    if (GameButton<G>._labelPointsPlayer2 != null)
                        GameButton<G>._labelPointsPlayer2.Text = IntToPoints(PointsToInt(GameButton<G>._labelPointsPlayer2.Text) + 1);
                }
            }

            Game.Player1 = !Game.Player1;

            if (GameButton<G>._labelPlayerNow != null)
                GameButton<G>._labelPlayerNow.Text = "Aktuell: " + Game.userList[((Game.Player1) ? 0 : 1)];

            GameButton<G>.InitAll();

            if (_game?.CheckIfWin(out bool winner) ?? false)
            {
                for (int i = 0; i < _field.Height; i++)
                    for (int j = 0; j < _field.Width; j++)
                        GameButton<G>.gameButtons[i, j].Enabled = false;
                MessageBox.Show($" (/ ^_^)/ Spieler {((winner) ? Game.userList[0] : Game.userList[1])} hat gewonnen \\(^_^ \\)", "Gewonnen");

                // TODO write into db
            }
        }

        private int PointsToInt(string points)
        {
            return int.Parse(points.Replace(".",""));
        }

        private string IntToPoints(int i)
        {
            string points = i.ToString();
            string ret = "";
            int counter = 0;
            foreach (char c in points.Reverse())
                ret = c + (((counter++ % 3 == 0) && (counter > 1)) ? "." : "") + ret;
            return ret;
        }
    }
}