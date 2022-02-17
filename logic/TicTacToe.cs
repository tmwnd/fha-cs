namespace cs_games.tic_tac_toe
{
    public class TicTacToe : Game
    {
        private static bool player1 = false;
        public static bool Player1
        {
            get
            {
                player1 = !player1;
                return player1;
            }
        }

        private GameField<TicTacToe> _field;
        public GameField<TicTacToe> Field
        {
            get => _field;
        }

        public override string Name { get => "TicTacToe"; }
        public override int Width { get => _field.Width; }
        public override int Height { get => _field.Height; }

        public TicTacToe()
        {
            _field = new GameField<TicTacToe>(3);
        }

        public TicTacToe(GameField<TicTacToe> field)
        {
            _field = field;
        }

        public override void Init()
        {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    new UnusedTicTacToeFigure(_field, i, j, false);
        }

        public override bool CheckIfWin(out bool winner)
        {
            if (CheckRow(out winner, _field[0, 0], _field[0, 1], _field[0, 2])) { return true; }
            if (CheckRow(out winner, _field[1, 0], _field[1, 1], _field[1, 2])) { return true; }
            if (CheckRow(out winner, _field[2, 0], _field[2, 1], _field[2, 2])) { return true; }

            if (CheckRow(out winner, _field[0, 0], _field[1, 0], _field[2, 0])) { return true; }
            if (CheckRow(out winner, _field[0, 1], _field[1, 1], _field[2, 1])) { return true; }
            if (CheckRow(out winner, _field[0, 2], _field[1, 2], _field[2, 2])) { return true; }

            if (CheckRow(out winner, _field[0, 0], _field[1, 1], _field[2, 2])) { return true; }
            if (CheckRow(out winner, _field[0, 2], _field[1, 1], _field[2, 0])) { return true; }
            return false;
        }

        private bool CheckRow(out bool winner, params GameFigure<TicTacToe>?[] figures)
        {
            winner = figures[0]?.Player1 ?? false;
            foreach (GameFigure<TicTacToe>? figure in figures)
                if ((((TicTacToeFigure?)figure)?.CanMove() ?? false) || (winner != (figure?.Player1 ?? false)))
                    return false;
            return true;
        }

        public override string ToString()
        {
            return _field.ToString();
        }
    }

    public class UnusedTicTacToeFigure : TicTacToeFigure
    {
        public override string? IMG
        {
            get => null;
        }

        public override string Name
        {
            get => "Unused";
        }

        public override bool CanMove() { return true; }

        public override char ToChar()
        {
            return ' ';
        }

        public UnusedTicTacToeFigure(GameField<TicTacToe> field, int x, int y, bool player1) : base(field, x, y, player1) { }
    }

    public class TicTacToeFigure : GameFigure<TicTacToe>
    {
        public override string? IMG
        {
            get => Game.config.GetProperty("root_path").ToString() + Game.config.GetProperty("skins").ToString() + "tictactoe/paint_" + ((Player1) ? "1" : "2") + ".png";
        }

        public override string Name
        {
            get => (Player1) ? "Kreuz" : "Kreis";
        }

        public TicTacToeFigure(GameField<TicTacToe> field, int x, int y, bool player1) : base(field, x, y, player1) { }

        public override bool CanMove() { return false; }

        private bool CanTake() { return false; } // lol
        public override List<int[]> GetMoves() { return new List<int[]> { new int[] { X, Y } }; }
        public override void MoveTo(int x, int y)
        {
            _field[x, y] = null; // clear field
            new TicTacToeFigure(Field, X, Y, TicTacToe.Player1); // error without clear

        }

        public override char ToChar()
        {
            return (_player1) ? 'x' : 'o';
        }
    }
}