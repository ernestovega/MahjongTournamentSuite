using System;

namespace MahjongTournamentSuite.Model
{
    class Tournament
    {
        public int Id
        {
            get
            {
                return Id;
            }

            set
            {
                Id = value;
            }
        }

        public string Name
        {
            get
            {
                return Name;
            }

            set
            {
                Name = value;
            }
        }

        public DateTime CreationDate
        {
            get
            {
                return CreationDate;
            }

            set
            {
                CreationDate = value;
            }
        }

        public Tournament(int id, string name, DateTime creationDate)
        {
            Id = id;
            Name = name;
            CreationDate = creationDate;
        }
    }
}
