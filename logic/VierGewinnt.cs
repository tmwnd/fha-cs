namespace cs_games.vier_gewinnt
{
    public class VierGewinnt : Game
    {
        private GameField<VierGewinnt> _field;
        public GameField<VierGewinnt> Field
        {
            get => _field;
        }

        public override string Name { get => "VierGewinnt"; }
        public override int Width { get => _field.Width; }
        public override int Height { get => _field.Height; }

        public VierGewinnt()
        {
            _field = new GameField<VierGewinnt>(8, 6);
        }

        public VierGewinnt(GameField<VierGewinnt> field)
        {
            _field = field;
        }

        public override void Init()
        {
            int i = Height - 1;
            for (int j = 0; j < Width; j++)
                new UnusedVierGewinntFigure(_field, i, j, false);
        }

        public override bool CheckIfWin(out bool winner)
        {
            winner = false;
            for (int i = 0; i < _field.Width - 3; i++)
                for (int j = 0; j < _field.Height; j++)
                    if (CheckRow(out winner, _field[i, j], _field[i + 1, j], _field[i + 2, j], _field[i + 3, j])) { return true; }

            for (int i = 0; i < _field.Width; i++)
                for (int j = 0; j < _field.Height - 3; j++)
                    if (CheckRow(out winner, _field[i, j], _field[i, j + 1], _field[i, j + 2], _field[i, j + 3])) { return true; }

            for (int i = 0; i < Math.Min(_field.Width, _field.Height) - 3; i++)
                if (CheckRow(out winner, _field[i, i], _field[i + 1, i + 1], _field[i + 2, i + 2], _field[i + 3, i + 3])) { return true; }
            for (int i = 0; i < Math.Min(_field.Width, _field.Height) - 3; i++)
                if (CheckRow(out winner, _field[i, i + 3], _field[i + 1, i + 2], _field[i + 2, i + 1], _field[i + 3, i])) { return true; }

            return false;
        }
        private bool CheckRow(out bool winner, params GameFigure<VierGewinnt>?[] figures)
        {
            winner = figures[0]?.Player1 ?? false;
            foreach (GameFigure<VierGewinnt>? figure in figures)
                if ((((VierGewinntFigure?)figure)?.CanMove() ?? false) || (winner != (figure?.Player1 ?? false)))
                    return false;
            return true;
        }

        public override string ToString()
        {
            return _field.ToString();
        }
    }

    public class VierGewinntFigure : GameFigure<VierGewinnt>
    {
        public override string? IMG
        {
            get => base.IMG_DIR + "/viergewinnt/viergewinnt_" + ((Player1) ? "1" : "2") + ".png";
        }

        public override string Name
        {
            get => (Player1) ? "Kreuz" : "Kreis";
        }

        public override bool Player1
        {
            get => base.Player1;
        }

        public VierGewinntFigure(GameField<VierGewinnt> field, int x, int y, bool player1) : base(field, x, y, player1) { }

        public override bool CanMove() { return false; }

        private bool CanTake() { return false; } // lol
        public override List<int[]> GetMoves() { return (X > 0) ? new List<int[]> { new int[] { X - 1, Y } } : new List<int[]>(); }
        public override bool MoveTo(int x, int y)
        {
            _field[x, y] = null; // clear field
            new VierGewinntFigure(Field, X, Y, Game.Player1); // error without clear

            foreach (int[] move in _field[X, Y]?.GetMoves() ?? new List<int[]>())
                new UnusedVierGewinntFigure(_field, move[0], move[1], Player1);

            return true;
        }

        public override char ToChar()
        {
            return (_player1) ? 'x' : 'o';
        }
    }

    public class UnusedVierGewinntFigure : VierGewinntFigure
    {
        public override string? IMG
        {
            get => null;
        }

        public override bool Player1
        {
            get => Game.Player1;
        }

        public override bool CanMove() { return true; }
        public override List<int[]> GetMoves() { return new List<int[]> { new int[] { X, Y } }; }

        public override char ToChar()
        {
            return ' ';
        }

        public UnusedVierGewinntFigure(GameField<VierGewinnt> field, int x, int y, bool player1) : base(field, x, y, player1) { }
    }
}