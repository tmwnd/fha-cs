namespace CS_Games.Dame
{
    public class Dame : IGame
    {
        private GameField<Dame> _field;

        public Dame(GameField<Dame> field)
        {
            _field = field;
            Init();
        }

        public void Init()
        {
            // TODO better init
            for (int i = 0; i < 8; i += 2)
            {
                new DameFigure(_field, 0, i, false);
                new DameFigure(_field, 1, i + 1, false);
                new DameFigure(_field, 2, i, false);
                new DameFigure(_field, 5, i + 1, true);
                new DameFigure(_field, 6, i, true);
                new DameFigure(_field, 7, i + 1, true);
            }
        }

        public override String ToString()
        {
            return _field.ToString();
        }
    }

    public class DameFigure : GameFigure<Dame>
    {
        public DameFigure(GameField<Dame> field, int x, int y, bool player1) : base(field, x, y, player1) { }

        public override char ToChar()
        {
            return (_player1) ? 'x' : 'o';
        }

        public override void MoveTo()
        {
            throw new NotImplementedException();
        }
    }
}