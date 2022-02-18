namespace cs_games.dame
{
    public class Dame : Game
    {
        private GameField<Dame> _field;
        public GameField<Dame> Field
        {
            get => _field;
        }

        public override string Name { get => "Dame"; }

        public static List<Skin<Dame>> skins = new List<Skin<Dame>> { new DameCSBlockkursSkin(), new DameChineseSkin(), new DameChineseColoredSkin() };
        public static int skinIndex = 0;
        public override List<string> SkinNames
        {
            get
            {
                List<string> ret = new List<string>();
                foreach (Skin<Dame> skin in skins)
                    ret.Add(skin.Name);
                return ret;
            }
        }
        public override int SkinIndex
        {
            set => Dame.skinIndex = value;
        }

        public override int Width { get => _field.Width; }
        public override int Height { get => _field.Height; }

        public Dame() { _field = new GameField<Dame>(); }

        public Dame(GameField<Dame> field)
        {
            _field = field;
        }

        public override void Init()
        {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    _field[i, j] = null;
            foreach (int i in new int[] { 0, 1, 2, 5, 6, 7 })
                for (int j = 0; j < 8; j += 2)
                    new DameFigure(_field, i, j + ((i % 2 == 1) ? 1 : 0), i > 2);
        }

        public override bool CheckIfWin(out bool winner)
        {
            winner = false;
            return false;
        }

        public override string ToString()
        {
            return _field.ToString();
        }
    }

    public class DameFigure : GameFigure<Dame>
    {
        public override string IMG
        {
            get => Dame.skins[Dame.skinIndex].getIMG(_player1, this);
        }

        public override string Name
        {
            get => "Pawn";
        }

        public DameFigure(GameField<Dame> field, int x, int y, bool player1) : base(field, x, y, player1) { }

        public override bool CanMove()
        {
            if (Game.Player1 != Player1) // not your turn
                return false;

            bool canTake = CanTake();
            for (int i = 0; i < Field.Height && !canTake; i++)
                for (int j = 0; j < Field.Width && !canTake; j++)
                    canTake = canTake || ((((DameFigure?)Field[i, j])?.CanTake() ?? false) && Field[i, j]?.Player1 == Player1);

            if (canTake) // some stone can take
                return canTake == CanTake();

            if (Player1)
            {
                // unten
                if (X > 0 && ((Y < Field.Width - 1 && Field[X - 1, Y + 1] == null) || (Y > 0 && Field[X - 1, Y - 1] == null)))
                    return true;
            }
            else
            {
                // oben
                if (X < Field.Height - 1 && ((Y < Field.Width - 1 && Field[X + 1, Y + 1] == null) || (Y > 0 && Field[X + 1, Y - 1] == null)))
                    return true;
            }
            return false;
        }

        public virtual bool CanTake()
        {
            if (Player1)
            {
                // unten
                if (X > 1)
                {
                    if (Y < Field.Width - 2 && Field[X - 1, Y + 1] != null && Field[X - 1, Y + 1]?.Player1 != Player1 && Field[X - 2, Y + 2] == null)
                        return true;
                    if (Y > 1 && Field[X - 1, Y - 1] != null && Field[X - 1, Y - 1]?.Player1 != Player1 && Field[X - 2, Y - 2] == null)
                        return true;
                }
            }
            else
            {
                // oben
                if (X < _field.Height - 2)
                {
                    if (Y < Field.Width - 2 && Field[X + 1, Y + 1] != null && Field[X + 1, Y + 1]?.Player1 != Player1 && Field[X + 2, Y + 2] == null)
                        return true;
                    if (Y > 1 && Field[X + 1, Y - 1] != null && Field[X + 1, Y - 1]?.Player1 != Player1 && Field[X + 2, Y - 2] == null)
                        return true;
                }
            }
            return false;
        }

        public override List<int[]> GetMoves()
        {
            List<int[]> ret = new List<int[]>();

            bool canTake = false;
            for (int i = 0; i < Field.Height && !canTake; i++)
                for (int j = 0; j < Field.Width && !canTake; j++)
                    canTake = canTake || ((((DameFigure?)Field[i, j])?.CanTake() ?? false) && Field[i, j]?.Player1 == Player1);
            if (Player1)
            {
                // friendly move
                if (!canTake && X > 0)
                {
                    if (Y < Field.Width - 1 && Field[X - 1, Y + 1] == null)
                        ret.Add(new int[] { X - 1, Y + 1 });
                    if (Y > 0 && Field[X - 1, Y - 1] == null)
                        ret.Add(new int[] { X - 1, Y - 1 });
                }
                // take
                if (X > 1)
                {
                    if (Y < Field.Width - 2 && Field[X - 1, Y + 1] != null && Field[X - 1, Y + 1]?.Player1 != Player1 && Field[X - 2, Y + 2] == null)
                        ret.Add(new int[] { X - 2, Y + 2 });
                    if (Y > 1 && Field[X - 1, Y - 1] != null && Field[X - 1, Y - 1]?.Player1 != Player1 && Field[X - 2, Y - 2] == null)
                        ret.Add(new int[] { X - 2, Y - 2 });
                }
            }
            else
            {
                // friendly move
                if (!canTake && X < _field.Height - 1)
                {
                    if (Y < Field.Width - 1 && Field[X + 1, Y + 1] == null)
                        ret.Add(new int[] { X + 1, Y + 1 });
                    if (Y > 0 && Field[X + 1, Y - 1] == null)
                        ret.Add(new int[] { X + 1, Y - 1 });
                }
                // take
                if (X < _field.Height - 2)
                {
                    if (Y < Field.Width - 2 && Field[X + 1, Y + 1] != null && Field[X + 1, Y + 1]?.Player1 != Player1 && Field[X + 2, Y + 2] == null)
                        ret.Add(new int[] { X + 2, Y + 2 });
                    if (Y > 1 && Field[X + 1, Y - 1] != null && Field[X + 1, Y - 1]?.Player1 != Player1 && Field[X + 2, Y - 2] == null)
                        ret.Add(new int[] { X + 2, Y - 2 });
                }
            }
            return ret;
        }

        public override bool MoveTo(int x, int y)
        {
            bool take = false;
            if (Math.Abs(X - x) == 2 && Math.Abs(Y - y) == 2)
            {
                _field[(X + x) / 2, (Y + y) / 2] = null;
                take = true;
            }

            _field.Swap(X, Y, x, y);
            X = x;
            Y = y;

            if (take && CanTake())
                Game.Player1 = !Game.Player1;

            if ((Player1 && X == _field.Height) || (!Player1 && X == 0))
                _field[X, Y] = new BetterDameFigure(this);
            return take;
        }

        public override char ToChar()
        {
            return (_player1) ? 'x' : 'o';
        }
    }

    public class BetterDameFigure : DameFigure
    {
        public BetterDameFigure(GameField<Dame> field, int x, int y, bool player1) : base(field, x, y, player1) { }

        // copy ctor
        public BetterDameFigure(DameFigure old) : this(old.Field, old.X, old.Y, old.Player1) { }

        public override string IMG
        {
            // TODO new skin
            get => Dame.skins[Dame.skinIndex].getIMG(_player1, this);
        }

        public override bool CanMove()
        {
            if (Game.Player1 != Player1) // not your turn
                return false;

            bool canTake = CanTake();
            for (int i = 0; i < Field.Height && !canTake; i++)
                for (int j = 0; j < Field.Width && !canTake; j++)
                    canTake = canTake || ((((DameFigure?)Field[i, j])?.CanTake() ?? false) && Field[i, j]?.Player1 == Player1);

            if (canTake) // some stone can take
                return canTake == CanTake();

            // unten
            if (X > 0 && ((Y < Field.Width - 1 && Field[X - 1, Y + 1] == null) || (Y > 0 && Field[X - 1, Y - 1] == null)))
                return true;
            // oben
            if (X < Field.Height - 1 && ((Y < Field.Width - 1 && Field[X + 1, Y + 1] == null) || (Y > 0 && Field[X + 1, Y - 1] == null)))
                return true;
            return false;
        }

        public override bool CanTake()
        {
            // unten
            if (X > 1)
            {
                if (Y < Field.Width - 2 && Field[X - 1, Y + 1] != null && Field[X - 1, Y + 1]?.Player1 != Player1 && Field[X - 2, Y + 2] == null)
                    return true;
                if (Y > 1 && Field[X - 1, Y - 1] != null && Field[X - 1, Y - 1]?.Player1 != Player1 && Field[X - 2, Y - 2] == null)
                    return true;
            }
            // oben
            if (X < _field.Height - 2)
            {
                if (Y < Field.Width - 2 && Field[X + 1, Y + 1] != null && Field[X + 1, Y + 1]?.Player1 != Player1 && Field[X + 2, Y + 2] == null)
                    return true;
                if (Y > 1 && Field[X + 1, Y - 1] != null && Field[X + 1, Y - 1]?.Player1 != Player1 && Field[X + 2, Y - 2] == null)
                    return true;
            }
            return false;
        }

        public override List<int[]> GetMoves()
        {
            List<int[]> ret = new List<int[]>();

            bool canTake = false;
            for (int i = 0; i < Field.Height && !canTake; i++)
                for (int j = 0; j < Field.Width && !canTake; j++)
                    canTake = canTake || ((((DameFigure?)Field[i, j])?.CanTake() ?? false) && Field[i, j]?.Player1 == Player1);
            // friendly move
            if (!canTake && X > 0)
            {
                if (Y < Field.Width - 1 && Field[X - 1, Y + 1] == null)
                    ret.Add(new int[] { X - 1, Y + 1 });
                if (Y > 0 && Field[X - 1, Y - 1] == null)
                    ret.Add(new int[] { X - 1, Y - 1 });
            }
            // take
            if (X > 1)
            {
                if (Y < Field.Width - 2 && Field[X - 1, Y + 1] != null && Field[X - 1, Y + 1]?.Player1 != Player1 && Field[X - 2, Y + 2] == null)
                    ret.Add(new int[] { X - 2, Y + 2 });
                if (Y > 1 && Field[X - 1, Y - 1] != null && Field[X - 1, Y - 1]?.Player1 != Player1 && Field[X - 2, Y - 2] == null)
                    ret.Add(new int[] { X - 2, Y - 2 });
            }
            // friendly move
            if (!canTake && X < _field.Height - 1)
            {
                if (Y < Field.Width - 1 && Field[X + 1, Y + 1] == null)
                    ret.Add(new int[] { X + 1, Y + 1 });
                if (Y > 0 && Field[X + 1, Y - 1] == null)
                    ret.Add(new int[] { X + 1, Y - 1 });
            }
            // take
            if (X < _field.Height - 2)
            {
                if (Y < Field.Width - 2 && Field[X + 1, Y + 1] != null && Field[X + 1, Y + 1]?.Player1 != Player1 && Field[X + 2, Y + 2] == null)
                    ret.Add(new int[] { X + 2, Y + 2 });
                if (Y > 1 && Field[X + 1, Y - 1] != null && Field[X + 1, Y - 1]?.Player1 != Player1 && Field[X + 2, Y - 2] == null)
                    ret.Add(new int[] { X + 2, Y - 2 });
            }
            return ret;
        }
    }

    public class DameChineseSkin : Skin<Dame>
    {
        public override string Name { get => "DameChineseSkin"; }

        public override string getIMG(bool player1, GameFigure<Dame> figure)
        {
            return base.getIMG(player1, figure) + "dame/chinese_" + ((player1) ? "1" : "2") + ".png";
        }
    }

    public class DameChineseColoredSkin : Skin<Dame>
    {
        public override string Name { get => "DameChineseColoredSkin"; }

        public override string getIMG(bool player1, GameFigure<Dame> figure)
        {
            return base.getIMG(player1, figure) + "dame/chinese_colored_" + ((player1) ? "1" : "2") + ".png";
        }
    }

    public class DameCSBlockkursSkin : Skin<Dame>
    {
        public override string Name { get => "DameCSBlockkursSkin"; }

        public override string getIMG(bool player1, GameFigure<Dame> figure)
        {
            return base.getIMG(player1, figure) + "dame/cs_blockkurs_" + ((player1) ? "1" : "2") + ".png";
        }
    }
}