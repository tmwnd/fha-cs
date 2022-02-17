namespace cs_games.data_objects
{
    public class GameButton<G>
    where G : Game
    {
        public GameButton(Button button, GameFigure<G>? figure)
        {
            if (figure == null)
                return;

            button.Enabled = figure.CanMove();
            button.BackgroundImage = Image.FromFile(figure.IMG);

            button.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}