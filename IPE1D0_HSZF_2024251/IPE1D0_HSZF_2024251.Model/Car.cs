using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IPE1D0_HSZF_2024251.Model
{
    [XmlRoot("Car")]
    public class Car
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Model")]
        public string Model { get; set; } = string.Empty;

        [XmlElement("TotalDistance")]
        public float TotalDistance { get; set; }

        [XmlElement("DistanceSinceLastMaintenance")]
        public float DistanceSinceLastMaintenance { get; set; }
    }
}
