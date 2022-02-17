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
        private Skin<Dame> skin = new DameChineseSkin();
        public override string IMG
        {
            get => skin.getIMG(_player1, this);
        }

        public override string Name
        {
            get => "Pawn";
        }

        public DameFigure(GameField<Dame> field, int x, int y, bool player1) : base(field, x, y, player1) { }

        public override bool CanMove()
        {
            if (Player1)
            {
                // unten
                return false;
            }
            else
            {
                // oben
                return false;
            }
        }

        public override void MoveTo(int x, int y)
        {
            throw new NotImplementedException();
        }

        public override char ToChar()
        {
            return (_player1) ? 'x' : 'o';
        }
    }

    public class DameChineseSkin : Skin<Dame>
    {
        public override string getIMG(bool player1, GameFigure<Dame> figure)
        {
            return base.getIMG(player1, figure) + "dame/chinese_" + ((player1) ? "1" : "2") + ".png";
        }
    }

    public class DameChineseColoredSkin : Skin<Dame>
    {
        public override string getIMG(bool player1, GameFigure<Dame> figure)
        {
            return base.getIMG(player1, figure) + "dame/chinese_colored_" + ((player1) ? "1" : "2") + ".png";
        }
    }
}