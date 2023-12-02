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
            { "Balan", Category.Transport },
            { "Revolut", Category.ToDelete },
            { "Прогрес", Category.ToDelete },
            { "ЗАХРАНВАНЕ", Category.CommonAccount },
            { "Теглене на АТМ", Category.ToDelete },
            { "H&M", Category.ClothesShoes },
            { "VITOSHA BG777 SOFIYA", Category.ClothesShoes },
            { "Babino Selo", Category.Food },
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
            { "FABRIKA DAGA", Category.RestaurantBG},
            { "KITCHEN SI", Category.RestaurantBG},
            { "MEDEYA MLADOST", Category.ChildBG},
            { "APTEKA SOFARMACY", Category.ChildBG},
            { "APTEKA MIRABEL", Category.ChildBG},
            { "DKTS VITA", Category.ChildBG},
            { "SOFARMASI", Category.ChildBG},
            { "MDL CIBALAB", Category.ChildBG},
            { "SOPHARMACY", Category.ChildBG},
            { "HEPI DOSTAVKA", Category.RestaurantBG},
            { "SOFIISKA VODA", Category.WaterBG},
            { "RESTAURANT DOBREVSKI", Category.RestaurantBG},
            { "PURLENKITE", Category.RestaurantBG},
            { "NASLADA MLADOST", Category.FoodBG }
        };

        private Dictionary<string, string> payseraMap = new Dictionary<string, string>()
        {
            { "Такса", Category.BankTaxBG },
            { "Т-са Плащ.", Category.BankTaxBG },
            { "card fee", Category.BankTaxBG },
            { "Анатолий", Category.RentBG },
            { "LIDL", Category.FoodBG },
            { "BILLA", Category.FoodBG },
            { "FIDI-43", Category.FoodBG },
            { "SHELL", Category.GasBG },
            { "EKO BG", Category.GasBG },
            { "PETROL AD", Category.GasBG },
            { "LUKOIL", Category.GasBG },
            { " OMV ", Category.GasBG },
            { "LADA GROUP", Category.GasBG },
            { "ZOO", Category.CatsBG },
            { "ML.1 AKVADIZAYN", Category.CatsBG},
            { "#DM ", Category.HomeItemsBG },
            { "Dm 061", Category.HomeItemsBG },
            { "NETFLIX", Category.NetflixBG },
            { "Spotify", Category.SpotifyBG },
            { "Revolut", Category.ToDelete },
            { "Период.плащ.", Category.ToDelete },
            { "Получен кредитен превод", Category.ToDelete},
            { "FANTASTICO", Category.FoodBG },
            { "FANTASTIKO", Category.FoodBG },
            { "Dar Mladost", Category.FoodBG },
            { "KINO ARENA", Category.FoodBG },
            { "SITI STANDART", Category.FacilityBG },
            { "TOPLOFIKATSIA SOFIA", Category.ToploBG},
            { "KASHTATA NA DZHIKOV", Category.RestaurantBG},
            { "RESTAURANT TOZI ONZI", Category.RestaurantBG},
            { "FABRIKA DAGA", Category.RestaurantBG},
            { "KITCHEN SI", Category.RestaurantBG},
            { "HEPINES EOOD", Category.RestaurantBG},
            { "NEDELYA MLADOST", Category.RestaurantBG},
            { "DOMINOS.BG", Category.RestaurantBG},
            { "EDDYS", Category.RestaurantBG},
            { "DOSTAVKA* ORDER", Category.RestaurantBG},
            { "ALEGRA 84", Category.RestaurantBG},
            { "MEDEYA MLADOST", Category.ChildBG},
            { "APTEKA SOFARMACY", Category.ChildBG},
            { "APTEKA MIRABEL", Category.ChildBG},
            { "DKTS VITA", Category.ChildBG},
            { "SOFARMASI", Category.ChildBG},
            { "MDL CIBALAB", Category.ChildBG},
            { "SOPHARMACY", Category.ChildBG},
            { "FURISTO", Category.ChildBG},
            { "PEPKO", Category.ChildBG},
            { "Pharma Center", Category.ChildBG},
            { "ADZHIBADEM SITI KLINIK", Category.ChildBG},
            { "HEPI DOSTAVKA", Category.RestaurantBG},
            { "SOFIISKA VODA", Category.WaterBG},
            { "DOBREVSKI", Category.RestaurantBG},
            { "PURLENKITE", Category.RestaurantBG},
            { "NASLADA MLADOST", Category.FoodBG },
            { "Magazin Naslada", Category.FoodBG },
            { "GLOVOAPP", Category.RestaurantBG},
            { "EVROPA-VN", Category.FoodBG },
            { "Dm 103", Category.HomeItemsBG },
            { "S PLYUS S-S", Category.FoodBG },
            { "BAKALIN", Category.FoodBG },
            { "Dm 025", Category.HomeItemsBG },
            { "LORENS 2010", Category.FoodBG },
            { "Glovo", Category.RestaurantBG},
            { "HIT HIPERMARKET", Category.FoodBG },
            { "BOZMOV", Category.FoodBG },
            { "Ciccione", Category.RestaurantBG },
            { "REKORD 2004", Category.FoodBG },
            { "JE T AIME", Category.RestaurantBG },
            { "PARK BOBY  and  KELLY", Category.ChildBG },
            { "OSK LOZENEZ EAD", Category.ChildBG },
            { "DM BULGARIA EOOD", Category.HomeItemsBG },
        };

        private Dictionary<string, string> revolutMap = new Dictionary<string, string>()
        {
            { "Top-Up by *", Category.ToDelete },
            { "Cash at ", Category.ToDelete },
            { "3dpos Etapgroup.com", Category.HolidayBG },
            { "ЙОРЯНА СПОРТ КОМПЪНИ ЕООД", Category.Sport },
            { "Telenor", Category.Phone },
            { "Patreon", Category.Entertainment },
            { "Steamgames", Category.Entertainment },
            { "Google *services", Category.Entertainment },
            { "google Play Ap", Category.Entertainment},
            { "Heroku", Category.Entertainment},
            { "Eddy's", Category.Food },
            { "Eddys", Category.Food },
            { "breaktimejsc", Category.Food },
            { "BreakTime", Category.Food },
            { "Restaurant", Category.Food },
            { "Metro", Category.Transport },
            { "webportal.sofi", Category.Transport},
            { "Spotify", Category.CommonAccount },
            { "TRANSFER Current To BGN Personal Vault", Category.Savings},
            { "taxime", Category.Taxi},
            { "Babino Selo", Category.Food},
            { "Sofia Transit Dsk Mobi", Category.Transport },
            { "Citygate Sofia Transit", Category.Transport },
            { "Sofia Transit", Category.Transport },
            { "Breaktime Progres", Category.Food},
            { "Ag And Co Ood", Category.Alcohol},
            { "Cantina 22", Category.Food },
            { "dominos.bg", Category.EatingOut },
            { "Krisna Ood", Category.Food },
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
                case Type.Paysera:
                    return this.payseraMap;
                default:
                    throw new ArgumentException("Invalid type for CategoryChooser.");
            }
        }
    }
}