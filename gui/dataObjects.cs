namespace cs_games.data_objects
{
    public class GameButton<G> : Button
    where G : Game
    {
        // TODO dynamic size
        public static GameButton<G>[,] gameButtons = new GameButton<G>[8, 8];

        public static void InitAll()
        {
            for (int i = 0; i < GameButton<G>.gameButtons.GetLength(0); i++)
                for (int j = 0; j < GameButton<G>.gameButtons.GetLength(1); j++)
                    GameButton<G>.gameButtons[i, j].Init();
        }


        private GameFigure<G>? _figure;
        public GameFigure<G>? Figure
        {
            get { return _figure; }
            set { _figure = value; }
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

        public GameButton(GameFigure<G>? figure, int x, int y)
        {
            _x = x;
            _y = y;
            _figure = figure;

            Init();

            GameButton<G>.gameButtons[x, y] = this;

            if (figure == null)
                return;
        }

        public void Init()
        {
            if (_figure != null)
            {
                if (_figure.CanMove())
                {
                    Enabled = true;
                    Click += StartMove;
                }

                BackgroundImage = Image.FromFile(_figure.IMG ?? Game.GetIMGPath(""));
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
            if (_figure == null)
                throw new Exception("Keine Figur vorhanden");

            for (int i = 0; i < _figure.Field.Height; i++)
                for (int j = 0; j < _figure.Field.Width; j++)
                {
                    GameButton<G> button = GameButton<G>.gameButtons[i, j];
                    button.Enabled = false;
                    button.Click -= StartMove;
                }

            foreach (int[] move in _figure.GetMoves())
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

            GameFigure<G>? moveTo = button.Figure;
            GameFigure<G>? moveFrom = Figure;

            if (moveFrom != null)
                foreach (int[] move in moveFrom.GetMoves())
                {
                    GameButton<G>.gameButtons[move[0], move[1]].Click -= DoMove;
                }

            button.Figure = moveFrom;
            Figure = moveTo;

            moveFrom?.MoveTo(button.X, button.Y);

            GameButton<G>.InitAll();
        }
    }
}