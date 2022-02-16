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
        public override int Width { get => _field.Width; }
        public override int Height { get => _field.Height; }

        public Dame() { _field = new GameField<Dame>(); }

        public Dame(GameField<Dame> field)
        {
            _field = field;
        }

        public override void Init()
        {
            for (int i = 0; i < 8; i += 2)
            {
                foreach (int j in new int[] { 0, 1, 2, 5, 6, 7 })
                    new DameFigure(_field, j, i + ((j % 2 == 1) ? 1 : 0), j > 2);
            }
        }

        public override string ToString()
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

        public override void MoveTo(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}