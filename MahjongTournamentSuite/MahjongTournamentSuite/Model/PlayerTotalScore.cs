namespace MahjongTournamentSuitePresentationLayer.TableManager
{
    public class PlayerTablePoints
    {
        public enum Seats
        {
            EAST,
            SOUTH,
            WEST,
            NORTH
        }

        public Seats Seat { get; set; }

        public int Score { get; set; }

        public string Points { get; set; }

        public PlayerTablePoints() { }

        public PlayerTablePoints(Seats seat, int score)
        {
            Seat = seat;
            Score = score;
            Points = string.Empty;
        }
    }
}