﻿namespace MahjongTournamentSuite.ViewModel
{
    public class ComboItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public ComboItem(string text, string value)
        {
            Text = text;
            Value = value;
        }
    }
}
