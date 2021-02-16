using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ItemModifier
{
    public class ItemModifierConfiguration : IRocketPluginConfiguration
    {
        public List<ItemModifications> Items { get; set; }

        public void LoadDefaults()
        {
            Items = new List<ItemModifications>()
            {
                new ItemModifications()
                {
                    Id = 363,
                    Modifications = new ItemModifications.Modification[]
                    {
                        new ItemModifications.Modification("isInvulnerable", "true"),
                        new ItemModifications.Modification("structureDamage", "300"),
                        new ItemModifications.Modification("barricadeDamage", "300"),
                        new ItemModifications.Modification("ammoMax", "255")
                    }
                }
            };
        }
    }
}
