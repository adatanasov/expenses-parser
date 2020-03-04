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
            { "EDDY S BAKE", Category.Food },
            { "Такса", Category.BankTax },
            { "ЙОРЯНА СПОРТ КОМПЪНИ ЕООД", Category.Sport },
            { "VITANIYA", Category.Health },
            { "METRO BALAN", Category.Transport },
            { "Revolut", Category.ToDelete },
            { "Прогрес", Category.ToDelete },
            { "ЗАХРАНВАНЕ", Category.CommonAccount },
            { "Теглене на АТМ", Category.ToDelete },
        };

        private Dictionary<string, string> commonMap = new Dictionary<string, string>()
        {
            { "Такса", Category.BankTaxBG },
            { "Т-са Плащ.", Category.BankTaxBG },
            { "Анатолий", Category.RentBG },
            { "LIDL", Category.FoodBG },
            { "BILLA", Category.FoodBG },
            { "SHELL", Category.GasBG },
            { "LUKOIL", Category.GasBG },
            { " OMV ", Category.GasBG },
            { "LADA GROUP", Category.GasBG },
            { "ZOO", Category.CatsBG },
            { "ML.1 AKVADIZAYN", Category.CatsBG},
            { "#DM ", Category.HomeItemsBG },
            { "NETFLIX", Category.NetflixBG },
            { "Spotify", Category.SpotifyBG },
            { "Revolut", Category.ToDelete },
            { "Период.плащ.", Category.ToDelete },
            { "Получен кредитен превод", Category.ToDelete},
            { "FANTASTICO", Category.FoodBG },
            { "KINO ARENA", Category.FoodBG },
            { "SITI STANDART", Category.FacilityBG },
            { "TOPLOFIKATSIA SOFIA", Category.ToploBG},
            { "KASHTATA NA DZHIKOV", Category.RestaurantBG},
            { "RESTAURANT TOZI ONZI", Category.RestaurantBG},
            { "KITCHEN SI", Category.RestaurantBG}
        };

        private Dictionary<string, string> revolutMap = new Dictionary<string, string>()
        {
            { "Top-Up by *", Category.ToDelete },
            { "Cash at ", Category.ToDelete },
            { "3dpos Etapgroup.com", Category.HolidayBG },
            { "ЙОРЯНА СПОРТ КОМПЪНИ ЕООД", Category.Sport },
            { "Telenor", Category.Phone },
            { "Patreon", Category.Entertainment }
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
                case Type.Revolut:
                    return this.revolutMap;
                default: 
                    throw new ArgumentException("Invalid type for CategoryChooser.");
            }
        }
    }
}