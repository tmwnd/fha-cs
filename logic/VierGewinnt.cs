namespace cs_games.vier_gewinnt
{
    public class VierGewinnt : Game
    {
        private GameField<VierGewinnt> _field;
        public GameField<VierGewinnt> Field
        {
            get => _field;
        }

        public override string Name { get => "VierGewinnt"; }
        public override int Width { get => _field.Width; }
        public override int Height { get => _field.Height; }

        public VierGewinnt()
        {
            _field = new GameField<VierGewinnt>(8, 6);
        }

        public VierGewinnt(GameField<VierGewinnt> field)
        {
            _field = field;
        }

        public override void Init() { }

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
}