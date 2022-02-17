﻿using cs_games.exceptions;

namespace cs_games.chess
{
    public class Chess : Game
    {
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
                new Pawn(Field, i, 1, true);
                new Pawn(Field, i, 6, false);
            }
            new Rook(Field, 0, 0, true);
            new Rook(Field, 7, 0, true);
            new Rook(Field, 0, 7, false);
            new Rook(Field, 7, 7, false);
            new Knight(Field, 1, 0, true);
            new Knight(Field, 6, 0, true);
            new Knight(Field, 1, 7, true);
            new Knight(Field, 6, 7, true);
            new Bishop(Field, 2, 0, true);
            new Bishop(Field, 5, 0, true);
            new Bishop(Field, 2, 7, true);
            new Bishop(Field, 5, 7, true);
            new Queen(Field, 3, 0, true);
            new Queen(Field, 3, 7, false);
            new King(Field, 4, 0, true);
            new King(Field, 4, 7, false);
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
        public Rook(GameField<Chess> field, int x, int y, bool player1) : base(field, y, x, player1) { HasMoved = false; }

        public override void MoveTo(int x, int y)
        {
            if (X == x ^ Y == y)
            {
                for (int i = X, j = Y; i != x && j != y; i += Math.Sign(x - i), j += Math.Sign(y - j))
                {
                    if (Field[i, j] != null)
                        throw new IllegalMoveException($"Auf ({i}|{j}) steht eine Figur");
                }
                if (Field[x, y]?.Player1 == Player1)
                    throw new IllegalMoveException("Man kann seine eigene Figur nicht Schlagen");
                Field[X, Y] = null;
                X = x;
                Y = y;
                Field[x, y] = null;
                Field[x, y] = this;
                HasMoved = true;
            }
            else
                throw new IllegalMoveException("Der Turm kann sich nur gerade in einer Reihe bzw. Spalte bewegen.");
        }

        public override List<int[]> GetMoves()
        {
            List<int[]> result = new();
            for (int i = X; i < 8; i++)
            {
                result.Add(new int[] { i, Y });
                if (Field[i, Y] != null)
                    break;
            }
            for (int i = X; i >= 0; i--)
            {
                result.Add(new int[] { i, Y });
                if (Field[i, Y] != null)
                    break;
            }
            for (int i = Y; i < 8; i++)
            {
                result.Add(new int[] { X, i });
                if (Field[X, i] != null)
                    break;
            }
            for (int i = Y; i >= 0; i--)
            {
                result.Add(new int[] { i, Y });
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
        public Knight(GameField<Chess> field, int x, int y, bool player1) : base(field, y, x, player1) { }

        public override void MoveTo(int x, int y)
        {
            if ((Math.Abs(X - x) == 2 && Math.Abs(Y - y) == 1) || (Math.Abs(X - x) == 1 && Math.Abs(Y - y) == 2))
            {
                if (Field[x, y]?.Player1 == Player1)
                    throw new IllegalMoveException("Man kann seine eigene Figur nicht Schlagen");
                Field[X, Y] = null;
                X = x;
                Y = y;
                Field[x, y] = null;
                Field[x, y] = this;
            }
            else
                throw new IllegalMoveException("Der Springer kann sich nur 2 Schritte in eine Richtung und 1 Schritt zur Seite bewegen.");
        }

        public override List<int[]> GetMoves()
        {
            List<int[]> result = new();
            for (int i = -2; i < 3; i++)
                for (int j = -2; j < 3; j++)
                    if (!(Math.Abs(i) == Math.Abs(j)) && i != 0 && j != 0 && 0 <= X + i && 0 <= Y + j && 8 > X + i && 8 > Y + j)
                        result.Add(new int[] { X + i, Y + j });
            return result;
        }

        public override char ToChar()
        {
            return Player1 ? 'S' : 's';
        }
    }

    internal class Bishop : GameFigure<Chess>
    {
        public Bishop(GameField<Chess> field, int x, int y, bool player1) : base(field, y, x, player1) { }

        public override void MoveTo(int x, int y)
        {
            if (Math.Abs(X - x) == Math.Abs(Y - y))
            {
                for (int i = X, j = Y; i != x && j != y; i += Math.Sign(x - i), j += Math.Sign(y - j))
                {
                    if (Field[i, j] != null)
                        throw new IllegalMoveException($"Auf ({i}|{j}) steht eine Figur");
                }
                if (Field[x, y]?.Player1 == Player1)
                    throw new IllegalMoveException("Man kann seine eigene Figur nicht Schlagen");
                Field[X, Y] = null;
                X = x;
                Y = y;
                Field[x, y] = null;
                Field[x, y] = this;
            }
            else
                throw new IllegalMoveException("Der Läufer kann sich nur diagonal bewegen.");
        }

        public override List<int[]> GetMoves()
        {
            List<int[]> result = new();
            for (int i = X, j = Y; i < 8 && j < 8; i++, j++)
            {
                result.Add(new int[] { i, j });
                if (Field[X, i] != null)
                    break;
            }
            for (int i = X, j = Y; i < 8 && j >= 0; i++, j--)
            {
                result.Add(new int[] { i, j });
                if (Field[X, i] != null)
                    break;
            }
            for (int i = X, j = Y; i >= 0 && j < 8; i--, j++)
            {
                result.Add(new int[] { i, j });
                if (Field[X, i] != null)
                    break;
            }
            for (int i = X, j = Y; i >= 0 && j >= 0; i--, j--)
            {
                result.Add(new int[] { i, j });
                if (Field[X, i] != null)
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
        public Queen(GameField<Chess> field, int x, int y, bool player1) : base(field, y, x, player1) { }

        public override void MoveTo(int x, int y)
        {
            if (Math.Abs(X - x) == Math.Abs(Y - y) || (X == x ^ Y == y))
            {
                for (int i = X, j = Y; i != x && j != y; i += Math.Sign(x - i), j += Math.Sign(y - j))
                {
                    if (Field[i, j] != null)
                        throw new IllegalMoveException($"Auf ({i}|{j}) steht eine Figur");
                }
                if (Field[x, y]?.Player1 == Player1)
                    throw new IllegalMoveException("Man kann seine eigene Figur nicht Schlagen");
                Field[X, Y] = null;
                X = x;
                Y = y;
                Field[x, y] = null;
                Field[x, y] = this;
            }
            else
                throw new IllegalMoveException("Der Läufer kann sich nur diagonal bewegen.");
        }

        public override List<int[]> GetMoves()
        {
            List<int[]> result = new();
            #region inLine
            for (int i = X; i < 8; i++)
            {
                result.Add(new int[] { i, Y });
                if (Field[i, Y] != null)
                    break;
            }
            for (int i = X; i >=0; i--)
            {
                result.Add(new int[] { i, Y });
                if (Field[i, Y] != null)
                    break;
            }
            for (int i = Y; i < 8; i++)
            {
                result.Add(new int[] { X, i });
                if (Field[X, i] != null)
                    break;
            }
            for (int i = Y; i >= 0; i--)
            {
                result.Add(new int[] { i, Y });
                if (Field[X, i] != null)
                    break;
            }
            #endregion
            #region diagonal
            for (int i = X, j = Y; i < 8 && j < 8; i++, j++)
            {
                result.Add(new int[]{ i, j });
                if(Field[X, i] != null)
                    break;
            }
            for (int i = X, j = Y; i < 8 && j >=0; i++, j--)
            {
                result.Add(new int[] { i, j });
                if (Field[X, i] != null)
                    break;
            }
            for (int i = X, j = Y; i>=0 && j < 8; i--, j++)
            {
                result.Add(new int[] { i, j });
                if (Field[X, i] != null)
                    break;
            }
            for (int i = X, j = Y; i >= 0&& j >= 0; i--, j--)
            {
                result.Add(new int[] { i, j });
                if (Field[X, i] != null)
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
        private static GameField<Chess>? LastField { get; set; }
        public Pawn(GameField<Chess> field, int x, int y, bool player1) : base(field, y, x, player1) { }

        public override void MoveTo(int x, int y)
        {
            if (GetMoves().Contains(new int[] { x, y }))
            {
                Field[X, Y] = null;
                Field[x, y] = this;
                X= x;
                Y= y;
                if (Y == 7 || Y == 0)
                {
                    Field[X, Y] = null;
                    Field[X, Y] = new Queen(Field, X, Y, Player1);
                }
            }
        }

        public override char ToChar()
        {
            return Player1 ? 'B' : 'b';
        }

        public override List<int[]> GetMoves()
        {
            List<int[]> list = new();
            if (Field[X, Y + (Player1 ? 1 : -1)] == null)
                list.Add(new int[] { X, Y + (Player1 ? 1 : -1) });
            if (Field[X + 1, Y + (Player1 ? 1 : -1)] != null)
                list.Add(new int[] { X + 1, Y + (Player1 ? 1 : -1) });
            if (Field[X - 1, Y + (Player1 ? 1 : -1)] != null)
                list.Add(new int[] { X - 1, Y + (Player1 ? 1 : -1) });
            if (Y == (Player1 ? 1 : 6) && Field[X, Y + (Player1 ? 1 : -1)] == null && Field[X, Y + (Player1 ? 2 : -2)] == null)
                list.Add(new int[] { X, Y + (Player1 ? 2 : -2) });
            if ((Field[X + 1, Y] is Pawn) && (LastField?[X + 1, Y - (Player1 ? 2 : -2)] == Field[X + 1, Y]))
                list.Add(new int[] { X + 1, Y + (Player1 ? 1 : -1) });
            if ((Field[X - 1, Y] is Pawn) && (LastField?[X - 1, Y - (Player1 ? 2 : -2)] == Field[X - 1, Y]))
                list.Add(new int[] { X - 1, Y + (Player1 ? 1 : -1) });
            return list;
        }
    }

    internal class King : GameFigure<Chess>
    {
        private bool _hasMoved;
        public bool HasMoved { get; set; }

        public King(GameField<Chess> field, int x, int y, bool player1) : base(field, y, x, player1) { HasMoved = false; }

        public override void MoveTo(int x, int y)
        {
            if (Math.Abs(X - x) <= 1 && Math.Abs(Y - y) <= 1)
            {
                if (Field[x, y]?.Player1 == Player1)
                    throw new IllegalMoveException("Man kann seine eigene Figur nicht Schlagen");
                Field[X, Y] = null;
                X = x;
                Y = y;
                Field[x, y] = null;
                Field[x, y] = this;
                HasMoved = true;
            }
        }

        public override List<int[]> GetMoves()
        {
            List<int[]> ret = new();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (!(Field[i, j]?.Player1 == Player1))
                    {
                        if (IsAllowed(X + i, Y + j))
                            ret.Add(new int[] { X + i, Y + j });
                    }

                }
            }
            if (!HasMoved && IsAllowed(X, Y))
            {
                if (Field[0, Y] is Rook rook0)
                {
                    if (Field[1, Y] == null && Field[2, Y] == null && Field[3, Y] == null && rook0.HasMoved)
                        ret.Add(new int[] { 2, Y });
                }
                if (Field[7, Y] is Rook rook7)
                {
                    if (Field[6, Y] == null && Field[5, Y] == null && rook7.HasMoved)
                        ret.Add(new int[] { 6, Y });
                }
            }
            return ret;
        }

        private bool IsAllowed(int x, int y)
        {
            #region Rook and Queen
            for (int k = x; k < 8; k++)
                if (Field[k, y] != null)
                {
                    if ((Field[k, y] is Rook || Field[k, y] is Queen) && Field[k, y]?.Player1 != Player1)
                        return false;
                }
            for (int k = x; k >= 0; k--)
                if (Field[k, y] != null)
                {
                    if ((Field[k, y] is Rook || Field[k, y] is Queen) && Field[k, y]?.Player1 != Player1)
                        return false;
                }
            for (int k = y; k < 8; k++)
                if (Field[x, k] != null)
                {
                    if ((Field[x, k] is Rook || Field[x, k] is Queen) && Field[x, k]?.Player1 != Player1)
                        return false;
                }
            for (int k = y; k >= 0; k--)
                if (Field[x, k] != null)
                {
                    if ((Field[x, k] is Rook || Field[x, k] is Queen) && Field[x, k]?.Player1 != Player1)
                        return false;
                }
            #endregion
            #region Bishop and Queen
            for (int k = y, l = x; k < 8 && l < 8; k++, l++)
                if (Field[l, k] != null)
                    if ((Field[l, k] is Queen || Field[l, k] is Bishop) && Field[l, k]?.Player1 != Player1)
                        return false;
            for (int k = y, l = x; k >= 0 && l < 8; k--, l++)
                if (Field[l, k] != null)
                    if ((Field[l, k] is Queen || Field[l, k] is Bishop) && Field[l, k]?.Player1 != Player1)
                        return false;
            for (int k = y, l = x; k < 8 && l >= 0; k++, l--)
                if (Field[l, k] != null)
                    if ((Field[l, k] is Queen || Field[l, k] is Bishop) && Field[l, k]?.Player1 != Player1)
                        return false;
            for (int k = y, l = x; k >= 0 && l >= 0; k--, l--)
                if (Field[l, k] != null)
                    if ((Field[l, k] is Queen || Field[l, k] is Bishop) && Field[l, k]?.Player1 != Player1)
                        return false;
            #endregion
            #region Knight
            for (int k = -2; k < 3; k++)
                for (int l = -2; l < 3; l++)
                {
                    if (Math.Abs(k) == Math.Abs(l) || k == 0 || l == 0)
                        break;
                    if (Field[l, k] is Knight)
                    {
                        return false;
                    }
                }
            #endregion
            #region Pawn
            if (Player1)
                if ((Field[x + 1, y + 1] is Pawn && Field[x + 1, y + 1]?.Player1 != Player1) || (Field[x - 1, y + 1] is Pawn && Field[x - 1, y + 1]?.Player1 != Player1))
                    return false;
                else
                if ((Field[x + 1, y - 1] is Pawn && Field[x + 1, y - 1]?.Player1 != Player1) || (Field[x - 1, y - 1] is Pawn && Field[x - 1, y - 1]?.Player1 != Player1))
                    return false;
            #endregion
            #region King
            for (int k = -1; k < 2; k++)
                for (int l = -1; l < 2; l++)
                    if (Field[x + k, y + l] is King)
                        return false;
            #endregion
            return true;
        }



        public override char ToChar()
        {
            return Player1 ? 'K' : 'k';
        }
    }
}
