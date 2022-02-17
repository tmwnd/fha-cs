using cs_games.dame;
using cs_games.chess;
using cs_games.tic_tac_toe;
using cs_games.vier_gewinnt;

using System.Text.Json;

namespace cs_games
{
    public abstract class Game
    {
        public static JsonElement config = JsonDocument.Parse(File.OpenText("D:/dev/fha-cs/config.json").ReadToEnd()).RootElement;
        public static List<Game> Games
        {
            get => new List<Game> { new Dame(), new Chess(), new TicTacToe(), new VierGewinnt() };
        }

        public static string GetIMGPath(string game)
        {
            try
            {
                string ret = config.GetProperty("root_path").ToString() + config.GetProperty("games").GetProperty("folder").ToString() + game.ToLower() + ".png";
                if (File.Exists(ret))
                    return ret;
                throw new Exception($"Spiel {game} wurde nicht gefunden");
            }
            catch
            {
                return config.GetProperty("root_path").ToString() + config.GetProperty("games").GetProperty("folder").ToString() + config.GetProperty("games").GetProperty("default").ToString();
            }
        }

        public abstract string Name { get; }

        public abstract int Width { get; }
        public abstract int Height { get; }

        public Type Type
        {
            get => this.GetType();
        }

        public abstract void Init();
        public override abstract string ToString();
    }

    public class GameField<G>
    where G : Game
    {
        protected GameFigure<G>?[,] _field;
        public GameFigure<G>?[,] Field
        {
            get => _field;
            set => _field = value;
        }

        public int Width
        {
            get { return _field.GetLength(0); }
        }
        public int Height
        {
            get { return _field.GetLength(1); }
        }

        // default ctor for already implemented games
        public GameField() : this(8) { }

        // ctor for square fields
        public GameField(int x)
        {
            _field = new GameFigure<G>?[x, x];
        }

        // ctor for rectangular fields
        public GameField(int x, int y)
        {
            _field = new GameFigure<G>?[x, y];
        }

        public GameFigure<G>? this[int x, int y]
        {
            get
            {
                if (!IndexIsValid(x, y))
                    throw new ArgumentException($"Es ist nicht möglich in einem {Width}x{Height} Feld auf den Index {x},{y} zuzugreifen.");
                return _field[x, y];
            }
            set
            {
                if (!IndexIsValid(x, y))
                    throw new ArgumentException($"Es ist nicht möglich in einem {Width}x{Height} Feld mit den Index {x},{y} zuzugreifen.");
                if (value != null && _field[x, y] != null)
                    throw new ArgumentException($"Das Feld mit den Index {x},{y} ist bereits durch {_field[x, y]} belegt");
                _field[x, y] = value;
            }
        }

        public bool IndexIsValid(int x, int y)
        {
            return !(x >= Width || y >= Height);
        }

        public override String ToString()
        {
            // hline
            string hline = "+";
            for (int i = 0; i < Width; i++)
                hline += "---+";
            hline += "\n";

            string ret = hline;
            for (int i = 0; i < Width; i++)
            {
                ret += "| ";
                string div = "";
                for (int j = 0; j < Height; j++)
                {
                    ret += div + (this[i, j]?.ToChar().ToString() ?? " ");
                    div = " | ";
                }
                ret += " |\n" + hline;
            }
            return ret;
        }
    }

    public abstract class GameFigure<G>
    where G : Game
    {
        protected int _x, _y;
        public int X { get; set; }
        public int Y { get; set; }

        protected bool _player1;
        public bool Player1
        {
            get => _player1;
            set => _player1 = value;
        }

        protected GameField<G> _field;
        public GameField<G> Field
        {
            get
            {
                if (_field == null)
                    throw new Exception("Figur gehört zu keinem Spielfeld");
                return _field;
            }
            set => _field = value;
        }

        public virtual string? IMG { get; }

        public virtual string? Name { get; }

        public GameFigure(GameField<G> field, int x, int y, bool player1)
        {
            _field = field;
            _player1 = player1;
            field[x, y] = this;
        }

        // abstract area
        public virtual bool CanMove()
        {
            return false;
        }
        public abstract void MoveTo(int x, int y);
        public abstract char ToChar();
    }

    public abstract class Skin<G>
    where G : Game
    {
        public virtual string getIMG(bool player1, GameFigure<G> figure)
        {
            return Game.config.GetProperty("root_path").ToString() + Game.config.GetProperty("skins").ToString();
        }
    }
}