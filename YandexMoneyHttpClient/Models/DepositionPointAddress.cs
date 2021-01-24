using System.Xml.Serialization;

namespace YandexMoneyHttpClient.Models
{
    [XmlType(TypeName = "address")]
    public class DepositionPointAddress
    {
        [XmlElement(ElementName = "region")]
        public string Region;
        [XmlElement(ElementName = "regionType")]
        public string RegionType;
        [XmlElement(ElementName = "street")]
        public string Street;
        [XmlElement(ElementName = "streetType")]
        public string StreetType;
        [XmlElement(ElementName = "house")]
        public string House;
        [XmlElement(ElementName = "building")]
        public string Building;
        [XmlElement(ElementName = "buildingType")]
        public string BuildingType;
        [XmlElement(ElementName = "houseType")]
        public string HouseType;
    }
}