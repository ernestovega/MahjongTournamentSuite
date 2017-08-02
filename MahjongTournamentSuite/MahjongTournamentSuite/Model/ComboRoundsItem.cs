namespace MahjongTournamentSuite.Model
{
    class ComboRoundsItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public ComboRoundsItem(string text, string value)
        {
            Text = text;
            Value = value;
        }
    }
}
