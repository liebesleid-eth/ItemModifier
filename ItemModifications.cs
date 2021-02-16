using System.ComponentModel;
using System.Xml.Serialization;

namespace ItemModifier
{
    public class ItemModifications
    {        
        [XmlAttribute]
        public ushort Id { get; set; }

        public Modification[] Modifications { get; set; }

        public class Modification
        {
            public Modification() { }

            public Modification(string name, string value)
            {
                Name = name;
                Value = value;
            }

            [XmlAttribute]
            public string Name { get; set; }
            [XmlAttribute]
            public string Value { get; set; }
        }
    }
}
