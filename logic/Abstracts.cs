using cs_games.dame;
using cs_games.chess;
using cs_games.tic_tac_toe;
using cs_games.vier_gewinnt;

using cs_games.api;

using System.Text.Json;

namespace cs_games
{
    public abstract class Game
    {
        public static JsonElement config = SetConfig();

        public static List<string> userList = GetDBUsers();

        public static List<Game> Games
        {
            get => new List<Game> { new Dame(), new Chess(), new TicTacToe(), new VierGewinnt() };
        }

        public static bool _player1 = true;
        public static bool Player1
        {
            get => _player1;
            set => _player1 = value;
        }

        public static JsonElement SetConfig()
        {
            // config
            string current = Directory.GetCurrentDirectory().ToString().Replace("\\", "/") + "/../../../../";
            if (current.Length < 50) // ew remove pls UwU; im sowwy but vs cOwOde mag vs nicht ¯\_(ツ)_/¯
                current = "D:/dev/fha-cs/";
            string config_raw = File.OpenText(current + "config.json").ReadToEnd();

            return JsonDocument.Parse(config_raw.Substring(0, config_raw.LastIndexOf('}')) + $", \"root_path\":\"{current}\"}}").RootElement;
        }

        public static List<string> GetDBUsers()
        {
            JsonElement database = config.GetProperty("database");
            API.ConnectToDB(database.GetProperty("username").ToString(), database.GetProperty("password").ToString());

            List<string> ret = new List<string>();

            JsonElement users = API.SQLQuery("SELECT * FROM spieler", "Keine Spieler vorhanden");
            for (int i = 0; i < users.GetArrayLength(); i++)
            {
                ret.Add(users[i].GetProperty("Name").ToString());
            }

            return ret;
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

        public virtual List<string> SkinNames
        {
            get => new List<string>();
        }
        public virtual int SkinIndex
        {
            set { }
        }

        public abstract int Width { get; }
        public abstract int Height { get; }

        public Type Type
        {
            get => this.GetType();
        }

        public abstract void Init();
        public abstract bool CheckIfWin(out bool winner);
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

        public int Height
        {
            get { return _field.GetLength(0); }
        }
        public int Width
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
                    throw new ArgumentException($"Es ist nicht möglich in einem {Height}x{Width} Feld auf den Index {x},{y} zuzugreifen.");
                return _field[x, y];
            }
            set
            {
                if (!IndexIsValid(x, y))
                    throw new ArgumentException($"Es ist nicht möglich in einem {Height}x{Width} Feld mit den Index {x},{y} zuzugreifen.");
                if (value != null && _field[x, y] != null)
                    throw new ArgumentException($"Das Feld mit den Index {x},{y} ist bereits durch {_field[x, y]} belegt");
                _field[x, y] = value;
            }
        }

        public bool IndexIsValid(int x, int y)
        {
            return !(x >= Height || y >= Width);
        }

        public void Swap(int x1, int y1, int x2, int y2)
        {
            GameFigure<G>? temp = _field[x1, y1];
            _field[x1, y1] = _field[x2, y2];
            _field[x2, y2] = temp;
        }

        public override String ToString()
        {
            // hline
            string hline = "+";
            for (int i = 0; i < Width; i++)
                hline += "---+";
            hline += "\n";

            string ret = hline;
            for (int i = 0; i < Height; i++)
            {
                ret += "| ";
                string div = "";
                for (int j = 0; j < Width; j++)
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
        public virtual int X
        {
            get => _x;
            set => _x = value;
        }
        public virtual int Y
        {
            get => _y;
            set => _y = value;
        }

        protected bool _player1;
        public virtual bool Player1
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
            Player1 = player1;
            X = x;
            Y = y;
            field[x, y] = this;
        }

        // abstract area
        public virtual bool CanMove()
        {
            // Player1 => unten
            return GetMoves().Count > 0;
        }
        public virtual List<int[]> GetMoves()
        {
            return new List<int[]> { };
        }

        public abstract void MoveTo(int x, int y);
        public abstract char ToChar();
    }

    public abstract class Skin<G>
    where G : Game
    {
        public abstract string Name
        {
            get;
        }

        public virtual string getIMG(bool player1, GameFigure<G> figure)
        {
            return Game.config.GetProperty("root_path").ToString() + Game.config.GetProperty("skins").ToString();
        }
    }
}