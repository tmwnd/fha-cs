namespace cs_games.chess
{
    public class Chess : Game
    {
        private King? king1;
        private King? king2;

        private GameField<Chess> _field;
        public GameField<Chess> Field
        {
            get => _field;
        }

        public override string Name { get => "Chess"; }
        public override int Width { get => _field.Width; }
        public override int Height { get => _field.Height; }

        public Chess()
        {
            _field = new GameField<Chess>();
        }

        public Chess(GameField<Chess> field)
        {
            _field = field;
        }

        public override void Init()
        {
            for (int i = 0; i < 8; i++)
            {
                new Pawn(Field, 1, i, true);
                new Pawn(Field, 6, i, false);
            }
            new Rook(Field, 0, 0, true);
            new Rook(Field, 7, 0, false);
            new Rook(Field, 0, 7, true);
            new Rook(Field, 7, 7, false);
            new Knight(Field, 0, 6, true);
            new Knight(Field, 0, 1, true);
            new Knight(Field, 7, 6, false);
            new Knight(Field, 7, 1, false);
            new Bishop(Field, 0, 2, true);
            new Bishop(Field, 0, 5, true);
            new Bishop(Field, 7, 2, false);
            new Bishop(Field, 7, 5, false);
            new Queen(Field, 0, 3, true);
            new Queen(Field, 7, 3, false);
            king1 = new King(Field, 0, 4, true);
            king2 = new King(Field, 7, 4, false);
        }

        public override bool CheckIfWin(out bool winner)
        {
            Pawn.LastField = Field;
            if (king1?.GetMoves().Count == 0 && king1.IsAllowed(king1.X, king1.Y))
            {
                winner = false;
                return true;
            }
            if (king2?.GetMoves().Count == 0 && king2.IsAllowed(king2.X, king2.Y))
            {
                winner = true;
                return true;
            }
            winner = false;
            return false;
        }

        public override string ToString()
        {
            return Field.ToString();
        }
    }

    internal class Rook : GameFigure<Chess>
    {
        private bool _hasMoved;
        public bool HasMoved { get; set; }
        public Rook(GameField<Chess> field, int x, int y, bool player1) : base(field, x, y, player1) { HasMoved = false; }

        public override void MoveTo(int y, int x)
        {
            Field[X, Y] = null;
            X = x;
            Y = y;
            Field[x, y] = null;
            Field[x, y] = this;
            HasMoved = true;
        }

        public override List<int[]> GetMoves()
        {
            List<int[]> result = new();
            for (int i = X; i < 8; i++)
            {
                if (i == X) continue;
                if (!(Field[i, Y]?.Player1 == Player1))
                    result.Add(new int[] { i, Y });
                if (Field[i, Y] != null)
                    break;
            }
            for (int i = X; i >= 0; i--)
            {
                if (i == X) continue;
                if (!(Field[i, Y]?.Player1 == Player1))
                    result.Add(new int[] { i, Y });
                if (Field[i, Y] != null)
                    break;
            }
            for (int i = Y; i < 8; i++)
            {
                if (i == Y) continue;
                if (!(Field[X, i]?.Player1 == Player1))
                    result.Add(new int[] { X, i });
                if (Field[X, i] != null)
                    break;
            }
            for (int i = Y; i >= 0; i--)
            {
                if (i == Y) continue;
                if (!(Field[X, i]?.Player1 == Player1))
                    result.Add(new int[] { X, i });
                if (Field[X, i] != null)
                    break;
            }
            return result;
        }

        public override char ToChar()
        {
            return Player1 ? 'T' : 't';
        }
    }

    internal class Knight : GameFigure<Chess>
    {
        public Knight(GameField<Chess> field, int x, int y, bool player1) : base(field, x, y, player1) { }

        public override void MoveTo(int x, int y)
        {
            Field[X, Y] = null;
            X = x;
            Y = y;
            Field[x, y] = null;
            Field[x, y] = this;
        }

        public override List<int[]> GetMoves()
        {
            List<int[]> result = new();
            for (int i = -2; i < 3; i++)
                for (int j = -2; j < 3; j++)
                    if (!(Math.Abs(i) == Math.Abs(j)) && i != 0 && j != 0 && 0 <= X + i && 0 <= Y + j && 8 > X + i && 8 > Y + j)
                        if (!(Field[X + i, Y + j]?.Player1 == Player1)) result.Add(new int[] { X + i, Y + j });
            return result;
        }

        public override char ToChar()
        {
            return Player1 ? 'S' : 's';
        }
    }

    internal class Bishop : GameFigure<Chess>
    {
        public Bishop(GameField<Chess> field, int x, int y, bool player1) : base(field, x, y, player1) { }

        public override void MoveTo(int x, int y)
        {
            Field[X, Y] = null;
            X = x;
            Y = y;
            Field[x, y] = null;
            Field[x, y] = this;
        }

        public override List<int[]> GetMoves()
        {
            List<int[]> result = new();
            for (int i = X, j = Y; i < 8 && j < 8; i++, j++)
            {
                if (i == X && j == Y) continue;
                if (!(Field[i, j]?.Player1 == Player1))
                    result.Add(new int[] { i, j });
                if (Field[i, j] != null)
                    break;
            }
            for (int i = X, j = Y; i < 8 && j >= 0; i++, j--)
            {
                if (i == X && j == Y) continue;
                if (!(Field[i, j]?.Player1 == Player1))
                    result.Add(new int[] { i, j });
                if (Field[i, j] != null)
                    break;
            }
            for (int i = X, j = Y; i >= 0 && j < 8; i--, j++)
            {
                if (i == X && j == Y) continue;
                if (!(Field[i, j]?.Player1 == Player1))
                    result.Add(new int[] { i, j });
                if (Field[i, j] != null)
                    break;
            }
            for (int i = X, j = Y; i >= 0 && j >= 0; i--, j--)
            {
                if (i == X && j == Y) continue;
                if (!(Field[i, j]?.Player1 == Player1))
                    result.Add(new int[] { i, j });
                if (Field[i, j] != null)
                    break;
            }
            return result;
        }

        public override char ToChar()
        {
            return Player1 ? 'L' : 'l';
        }
    }

    internal class Queen : GameFigure<Chess>
    {
        public Queen(GameField<Chess> field, int x, int y, bool player1) : base(field, x, y, player1) { }

        public override void MoveTo(int x, int y)
        {
            Field[X, Y] = null;
            X = x;
            Y = y;
            Field[x, y] = null;
            Field[x, y] = this;
        }

        public override List<int[]> GetMoves()
        {
            List<int[]> result = new();
            #region inLine
            for (int i = X; i < 8; i++)
            {
                if (i == X) continue;
                if (!(Field[i, Y]?.Player1 == Player1))
                    result.Add(new int[] { i, Y });
                if (Field[i, Y] != null)
                    break;
            }
            for (int i = X; i >= 0; i--)
            {
                if (i == X) continue;
                if (!(Field[i, Y]?.Player1 == Player1))
                    result.Add(new int[] { i, Y });
                if (Field[Y, i] != null)
                    break;
            }
            for (int i = Y; i < 8; i++)
            {
                if (i == Y) continue;
                if (!(Field[X, i]?.Player1 == Player1))
                    result.Add(new int[] { X, i });
                if (Field[X, i] != null)
                    break;
            }
            for (int i = Y; i >= 0; i--)
            {
                if (i == Y) continue;
                if (!(Field[X, i]?.Player1 == Player1))
                    result.Add(new int[] { X, i });
                if (Field[X, i] != null)
                    break;
            }
            #endregion
            #region diagonal
            for (int i = X, j = Y; i < 8 && j < 8; i++, j++)
            {
                if (i == X && j == Y) continue;
                if (!(Field[i, j]?.Player1 == Player1))
                    result.Add(new int[] { i, j });
                if (Field[i, j] != null)
                    break;
            }
            for (int i = X, j = Y; i < 8 && j >= 0; i++, j--)
            {
                if (i == X && j == Y) continue;
                if (!(Field[i, j]?.Player1 == Player1))
                    result.Add(new int[] { i, j });
                if (Field[i, j] != null)
                    break;
            }
            for (int i = X, j = Y; i >= 0 && j < 8; i--, j++)
            {
                if (i == X && j == Y) continue;
                if (!(Field[i, j]?.Player1 == Player1))
                    result.Add(new int[] { i, j });
                if (Field[i, j] != null)
                    break;
            }
            for (int i = X, j = Y; i >= 0 && j >= 0; i--, j--)
            {
                if (i == X && j == Y) continue;
                if (!(Field[i, j]?.Player1 == Player1))
                    result.Add(new int[] { i, j });
                if (Field[i, j] != null)
                    break;
            }
            #endregion
            return result;
        }

        public override char ToChar()
        {
            return Player1 ? 'D' : 'd';
        }
    }

    internal class Pawn : GameFigure<Chess>
    {
        private static GameField<Chess>? _lastField;
        public static GameField<Chess>? LastField { get; set; }
        public Pawn(GameField<Chess> field, int x, int y, bool player1) : base(field, x, y, player1) { }

        public override void MoveTo(int x, int y)
        {
            Field[X, Y] = null;
            X = x;
            Y = y;
            Field[x, y] = null;
            Field[x, y] = this;
            if (Y == 7 || Y == 0)
            {
                Field[Y, X] = null;
                Field[Y, X] = new Queen(Field, X, Y, Player1);
            }
        }

        public override char ToChar()
        {
            return Player1 ? 'B' : 'b';
        }

        public override List<int[]> GetMoves()
        {
            List<int[]> list = new();
            try
            {
                if (Field[X + (Player1 ? 1 : -1), Y] == null)
                    list.Add(new int[] { X + (Player1 ? 1 : -1), Y });
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                if (!Field[X + (Player1 ? 1 : -1), Y + 1]?.Player1 == Player1)
                    list.Add(new int[] { X + (Player1 ? 1 : -1), Y + 1 });
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                if (!Field[X + (Player1 ? 1 : -1), Y - 1]?.Player1 == Player1)
                    list.Add(new int[] { X + (Player1 ? 1 : -1), Y - 1 });
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                if (X == (Player1 ? 1 : 6) && Field[X + (Player1 ? 1 : -1), Y] == null && Field[X + (Player1 ? 2 : -2), Y] == null)
                    list.Add(new int[] { X + (Player1 ? 2 : -2), Y });
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                if ((Field[X, Y + 1] is Pawn) && (Field[X, Y + 1]?.Player1 != Player1 && (LastField?[X - (Player1 ? 2 : -2), Y + 1] == Field[X + 1, Y])))
                    list.Add(new int[] { X + (Player1 ? 1 : -1), Y + 1 });
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                if ((Field[X - 1, Y] is Pawn) && (!Field[X - 1, Y]?.Player1 == Player1) && (LastField?[X - 1, Y - (Player1 ? 2 : -2)] == Field[X - 1, Y]))
                    list.Add(new int[] { X + (Player1 ? 1 : -1), Y - 1 });
            }
            catch (IndexOutOfRangeException) { }
            return list;
        }
    }

    internal class King : GameFigure<Chess>
    {
        private bool _hasMoved;
        public bool HasMoved { get; set; }

        public King(GameField<Chess> field, int x, int y, bool player1) : base(field, x, y, player1) { HasMoved = false; }

        public override void MoveTo(int x, int y)
        {
            Field[X, Y] = null;
            X = x;
            Y = y;
            Field[x, y] = null;
            Field[x, y] = this;
            HasMoved = true;
        }

        public override List<int[]> GetMoves()
        {
            List<int[]> ret = new();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (X + i < 8 && X + i >= 0 && Y + j < 8 && Y + j >= 0)
                        if (!(Field[X + i, Y + j]?.Player1 == Player1))
                        {
                            if (IsAllowed(X + i, Y + j))
                                ret.Add(new int[] { X + i, Y + j });
                        }

                }
            }
            if (!HasMoved && IsAllowed(X, 4))
            {
                if (Field[X, 0] is Rook rook0)
                {
                    if (Field[X, 1] == null && Field[X, 2] == null && IsAllowed(X, 2) && Field[X, 3] == null && IsAllowed(X, 3) && !rook0.HasMoved)
                        ret.Add(new int[] { X, 2 });
                }
                if (Field[X, 7] is Rook rook7)
                {
                    if (Field[X, 6] == null && IsAllowed(X, 6) && Field[X, 5] == null && IsAllowed(X, 5) && !rook7.HasMoved)
                        ret.Add(new int[] { X, 6 });
                }
            }
            return ret;
        }

        public bool IsAllowed(int x, int y)
        {
            #region Rook and Queen
            for (int k = x; k < 8; k++)
                if (Field[k, y] != null)
                {
                    if ((Field[k, y] is Rook || Field[k, y] is Queen) && Field[k, y]?.Player1 != Player1)
                        return false;
                    else break;
                }
            for (int k = x; k >= 0; k--)
                if (Field[k, y] != null)
                {
                    if ((Field[k, y] is Rook || Field[y, k] is Queen) && Field[y, k]?.Player1 != Player1)
                        return false;
                    else break;
                }
            for (int k = y; k < 8; k++)
                if (Field[k, x] != null)
                {
                    if ((Field[x, k] is Rook || Field[x, k] is Queen) && Field[x, k]?.Player1 != Player1)
                        return false;
                    else break;
                }
            for (int k = y; k >= 0; k--)
                if (Field[x, k] != null)
                {
                    if ((Field[x, k] is Rook || Field[x, k] is Queen) && Field[x, k]?.Player1 != Player1)
                        return false;
                    else break;
                }
            #endregion
            #region Bishop and Queen
            for (int k = x, l = y; k < 8 && l < 8; k++, l++)
                if (Field[k, l] != null)
                    if ((Field[k, l] is Queen || Field[k, l] is Bishop) && Field[k, l]?.Player1 != Player1)
                        return false;
                    else break;
            for (int k = x, l = y; k >= 0 && l < 8; k--, l++)
                if (Field[k, l] != null)
                    if ((Field[k, l] is Queen || Field[k, l] is Bishop) && Field[k, l]?.Player1 != Player1)
                        return false;
                    else break;
            for (int k = x, l = y; k < 8 && l >= 0; k++, l--)
                if (Field[k, l] != null)
                    if ((Field[k, l] is Queen || Field[k, l] is Bishop) && Field[k, l]?.Player1 != Player1)
                        return false;
                    else
                        break;
            for (int k = x, l = y; k >= 0 && l >= 0; k--, l--)
                if (Field[k, l] != null)
                    if ((Field[k, l] is Queen || Field[k, l] is Bishop) && Field[k, l]?.Player1 != Player1)
                        return false;
                    else
                        break;
            #endregion
            #region Knight
            for (int k = -2; k < 3; k++)
                for (int l = -2; l < 3; l++)
                {
                    if (Math.Abs(k) == Math.Abs(l) || k == 0 || l == 0)
                        break;
                    try
                    {
                        if (Field[k, l] is Knight)
                        {
                            return false;
                        }
                    }
                    catch (IndexOutOfRangeException) { }
                }
            #endregion
            #region Pawn
            if (Player1)
                try
                {
                    if ((Field[x - (Player1 ? 1 : -1), y + 1] is Pawn && Field[x - (Player1 ? 1 : -1), y + 1]?.Player1 != Player1))
                        return false;
                    else if ((Field[x - (Player1 ? 1 : -1), y - 1] is Pawn && Field[x - (Player1 ? 1 : -1), y - 1]?.Player1 != Player1))
                        return false;
                }
                catch (IndexOutOfRangeException) { }
            #endregion
            #region King
            for (int k = -1; k < 2; k++)
                for (int l = -1; l < 2; l++)
                    try
                    {
                        if (Field[x + k, y + l] is King && Field[x + k, y + l]?.Player1 != Player1)
                            return false;
                    }
                    catch (IndexOutOfRangeException) { }
            #endregion
            return true;
        }



        public override char ToChar()
        {
            return Player1 ? 'K' : 'k';
        }
    }
}
