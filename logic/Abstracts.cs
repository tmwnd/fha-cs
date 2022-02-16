namespace CS_Games
{
    public interface IGame
    {
        public void Init();
        public String ToString();
    }

    public class GameField<Game>
    where Game : IGame
    {
        protected GameFigure<Game>?[,] _field;
        public GameFigure<Game>?[,] Field
        {
            get => _field;
            set => _field = value;
        }

        // default ctor for already implemented games
        public GameField()
        {
            _field = new GameFigure<Game>?[8, 8];
        }

        // ctor for square fields
        public GameField(int x)
        {
            _field = new GameFigure<Game>?[x, x];
        }

        // ctor for rectangular fields
        public GameField(int x, int y)
        {
            _field = new GameFigure<Game>?[x, x];
        }

        public GameFigure<Game>? this[int x, int y]
        {
            get
            {
                if (!IndexIsValid(x, y))
                    throw new ArgumentException($"Es ist nicht möglich in einem {_field.GetLength(0)}x{_field.GetLength(1)} Feld auf den Index {x},{y} zuzugreifen.");
                return _field[x, y];
            }
            set
            {
                if (!IndexIsValid(x, y))
                    throw new ArgumentException($"Es ist nicht möglich in einem {_field.GetLength(0)}x{_field.GetLength(1)} Feld mit den Index {x},{y} zuzugreifen.");
                if (_field[x, y] != null)
                    throw new ArgumentException($"Das Feld mit den Index {x},{y} ist bereits durch {_field[x, y]} belegt");
                _field[x, y] = value;
            }
        }

        public bool IndexIsValid(int x, int y)
        {
            return !(x >= _field.GetLength(0) || y >= _field.GetLength(1));
        }

        public override String ToString()
        {
            // hline
            string hline = "+";
            for (int i = 0; i < _field.GetLength(0); i++)
                hline += "---+";
            hline += "\n";

            string ret = hline;
            for (int i = 0; i < _field.GetLength(0); i++)
            {
                ret += "| ";
                string div = "";
                for (int j = 0; j < _field.GetLength(1); j++)
                {
                    ret += div + (this[i, j]?.ToChar().ToString() ?? " ");
                    div = " | ";
                }
                ret += " |\n" + hline;
            }
            return ret;
        }
    }

    public abstract class GameFigure<Game>
    where Game : IGame
    {
        protected bool _player1;
        public bool Player1 { get; set; }

        protected GameField<Game> _field;
        public GameField<Game> Field
        {
            get
            {
                if (_field == null)
                    throw new Exception("Figur gehört zu keinem Spielfeld");
                return _field;
            }
            set => _field = value;
        }

        public GameFigure(GameField<Game> field, int x, int y, bool player1)
        {
            _field = field;
            _player1 = player1;
            field[x, y] = this;
        }

        // abstract area
        public abstract void MoveTo();
        public abstract char ToChar();
    }
}