using System;
using System.Collections.Generic;

namespace expenses_parser
{
    public class CategoryChooser
    {
        private Type type;

        private Dictionary<string, string> personalMap = new Dictionary<string, string>()
        {
            { "EDIS BAKERY", Category.Food },
            { "EDDYS BAKE", Category.Food },
            { "Такса", Category.BankTax },
            { "ЙОРЯНА СПОРТ КОМПЪНИ ЕООД", Category.Health },
            { "VITANIYA", Category.Health },
            { "METRO BALAN", Category.Transport },
            { "Revolut", Category.ToDelete },
            { "Прогрес", Category.ToDelete }
        };

        private Dictionary<string, string> commonMap = new Dictionary<string, string>()
        {
            { "Такса", Category.BankTax },
            { "Т-са Плащ.", Category.BankTax },
            { "Анатолий", Category.RentBG },
            { "LIDL", Category.FoodBG },
            { "BILLA", Category.FoodBG },
            { "SHELL", Category.GasBG },
            { "LUKOIL", Category.GasBG },
            { " OMV ", Category.GasBG },
            { "LADA GROUP", Category.GasBG },
            { "ZOO", Category.CatsBG },
            { "#DM ", Category.HomeItemsBG },
            { "NETFLIX", Category.NetflixBG },
            { "Spotify", Category.SpotifyBG },
            { "Revolut", Category.ToDelete },
            { "Период.плащ.", Category.ToDelete }
        };

        public CategoryChooser(Type type)
        {
            this.type = type;
        }

        public string GetCategory(string note)
        {
            foreach (var mapping in this.GetMap())
            {
                if (note.ToLowerInvariant().Contains(mapping.Key.ToLowerInvariant()))
                {
                    return mapping.Value;
                }
            }

            return string.Empty;
        }

        private Dictionary<string, string> GetMap()
        {
            switch (this.type)
            {
                case Type.Personal:
                    return this.personalMap;
                case Type.Common:
                    return this.commonMap;
                default: 
                    throw new ArgumentException("Invalid type for CategoryChooser.");
            }
        }
    }
}