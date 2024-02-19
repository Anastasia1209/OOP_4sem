using System;

namespace Laba6_7.Products
{
    internal class Product
    {
        private string _name;
        private DeviceType _deviceType;
        private string _deviceDescription;
        private string _FotoUrl;
        private string _description;
        private int _cost;
        private Guid _id;


        public Product(string name, DeviceType deviceType, string fotoUrl, string description, int cost)
        {
            Name = name;
            _deviceType = deviceType;

            switch (_deviceType)
            {
                case DeviceType.Smartphone: DeviceTypeString = "Телефон"; break;
                case DeviceType.Tablet: DeviceTypeString = "Планшет"; break;
                case DeviceType.Laptop: DeviceTypeString = "Ноутбук"; break;
            }

            FotoUrl = fotoUrl;
            Description = description;
            Cost = cost;
            Id = new Guid();
        }

        public string Name { get => _name; set => _name = value; }
        public string FotoUrl { get => _FotoUrl; set => _FotoUrl = value; }
        public string Description { get => _description; set => _description = value; }
        public int Cost { get => _cost; set => _cost = value; }
        public Guid Id { get => _id; set => _id = value; }
        public string DeviceTypeString { get => _deviceDescription; set => _deviceDescription = value; }

        public DeviceType DeviceType
        {
            get => _deviceType;
            set => _deviceType = value;
        }
    }
}
