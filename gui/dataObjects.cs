namespace cs_games.data_objects
{
    public class GameButton<G> : Button
    where G : Game
    {
        // TODO dynamic size
        public static GameField<G> _field = new GameField<G>();
        public static GameButton<G>[,] gameButtons = new GameButton<G>[0, 0];

        public static void Create(GameField<G> field)
        {
            _field = field;
            gameButtons = new GameButton<G>[field.Height, field.Width];
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
            try { Click -= StartMove; } catch { }
            try { Click -= DoMove; } catch { }

            if (_field[X, Y] != null)
            {
                if (_field[X, Y]?.CanMove() ?? false)
                {
                    Enabled = true;
                    Click += StartMove;
                }

                BackgroundImage = Image.FromFile(_field[X, Y]?.IMG ?? Game.GetIMGPath(""));
                BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                Enabled = false;
                BackgroundImage = null;
            }
        }

        public void StartMove(Object? sender, EventArgs e)
        {
            if (_field[X, Y] == null)
                return;

            for (int i = 0; i < _field.Height; i++)
                for (int j = 0; j < _field.Width; j++)
                    GameButton<G>.gameButtons[i, j].Enabled = false;

            foreach (int[] move in _field[X, Y]?.GetMoves() ?? new List<int[]>())
            {
                GameButton<G> button = GameButton<G>.gameButtons[move[0], move[1]];
                button.Enabled = true;
                button.Click += DoMove;
            }
        }

        public void DoMove(Object? sender, EventArgs e)
        {
            if (sender == null)
                return;

            GameButton<G> button = (GameButton<G>)sender;
            _field[X, Y]?.MoveTo(button.X, button.Y);

            GameButton<G>.InitAll();
        }
    }
}