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
            // throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }

    internal class Rook : GameFigure<Chess>
    {
        public Rook(GameField<Chess> field, int x, int y, bool player1) : base(field, x, y, player1) { }

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
                X = x;
                Y = y;
                Field[x, y] = null;
                Field[x, y] = this;
            }
            else
                throw new IllegalMoveException("Der Turm kann sich nur gerade in einer Reihe bzw. Spalte bewegen.");
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
            if ((Math.Abs(X - x) == 2 && Math.Abs(Y - y) == 1) || (Math.Abs(X - x) == 1 && Math.Abs(Y - y) == 2))
            {
                if (Field[x, y]?.Player1 == Player1)
                    throw new IllegalMoveException("Man kann seine eigene Figur nicht Schlagen");
                X = x;
                Y = y;
                Field[x, y] = null;
                Field[x, y] = this;
            }
            else
                throw new IllegalMoveException("Der Springer kann sich nur 2 Schritte in eine Richtung und 1 Schritt zur Seite bewegen.");
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
            if (Math.Abs(X - x) == Math.Abs(Y - y))
            {
                for (int i = X, j = Y; i != x && j != y; i += Math.Sign(x - i), j += Math.Sign(y - j))
                {
                    if (Field[i, j] != null)
                        throw new IllegalMoveException($"Auf ({i}|{j}) steht eine Figur");
                }
                if (Field[x, y]?.Player1 == Player1)
                    throw new IllegalMoveException("Man kann seine eigene Figur nicht Schlagen");
                X = x;
                Y = y;
                Field[x, y] = null;
                Field[x, y] = this;
            }
            else
                throw new IllegalMoveException("Der Läufer kann sich nur diagonal bewegen.");
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
            if (Math.Abs(X - x) == Math.Abs(Y - y) || (X == x ^ Y == y))
            {
                for (int i = X, j = Y; i != x && j != y; i += Math.Sign(x - i), j += Math.Sign(y - j))
                {
                    if (Field[i, j] != null)
                        throw new IllegalMoveException($"Auf ({i}|{j}) steht eine Figur");
                }
                if (Field[x, y]?.Player1 == Player1)
                    throw new IllegalMoveException("Man kann seine eigene Figur nicht Schlagen");
                X = x;
                Y = y;
                Field[x, y] = null;
                Field[x, y] = this;
            }
            else
                throw new IllegalMoveException("Der Läufer kann sich nur diagonal bewegen.");
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
        public Pawn(GameField<Chess> field, int x, int y, bool player1) : base(field, x, y, player1) { }

        public override void MoveTo(int x, int y)
        {
            if ((y == Y + 1 && Player1) || (y == Y - 1 && !Player1))
            {
                if (Field[x, y] != null)
                    throw new IllegalMoveException($"Auf ({x}|{y}) steht eine Figur");
                X = x;
                Y = y;
                Field[x, y] = this;
            }
            else if ((y == 4 && Y == 2 && Player1) || (y == 5 && Y == 7 && !Player1))
            {
                if (Field[x, y] != null || Field[x, Y + Math.Sign(y - Y)] != null)
                    throw new IllegalMoveException($"Auf ({x}|{y}) steht eine Figur");
                X = x;
                Y = y;
                Field[x, y] = this;
            }
            else if ((y == Y + 1 && Math.Abs(X - x) == 1 && Player1) || (y == Y - 1 && Math.Abs(X - x) == 1 && !Player1))
            {
                if ((Field[x, Y] is Pawn) && (LastField?[x, Y - 2 * Math.Sign(y - Y)] == Field[x, Y]))
                {
                    Field[x, Y] = null;
                    X = x;
                    Y = y;
                    Field[x, y] = this;
                }
                if (Field[x, y]?.Player1 != this.Player1)
                    throw new IllegalMoveException($"Man kann weder eigene Figuren noch leere Felder schlagen.");
                X = x;
                Y = y;
                Field[x, y] = this;
            }
        }

        public override char ToChar()
        {
            return Player1 ? 'B' : 'b';
        }
    }
}
