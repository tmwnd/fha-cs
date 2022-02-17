namespace cs_games.tic_tac_toe
{
    public class TicTacToe : Game
    {
        private GameField<TicTacToe> _field;
        public GameField<TicTacToe> Field
        {
            get => _field;
        }

        public override string Name { get => "TicTacToe"; }
        public override int Width { get => _field.Width; }
        public override int Height { get => _field.Height; }

        public TicTacToe()
        {
            _field = new GameField<TicTacToe>(3);
        }

        public TicTacToe(GameField<TicTacToe> field)
        {
            _field = field;
        }

        public override void Init() { }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}