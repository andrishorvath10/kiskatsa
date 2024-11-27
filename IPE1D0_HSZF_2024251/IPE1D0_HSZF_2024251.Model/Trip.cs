using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IPE1D0_HSZF_2024251.Model
{
    [XmlRoot("Trip")]
    public class Trip
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("CarId")]
        public int CarId { get; set; }

        [XmlElement("CustomerId")]
        public int CustomerId { get; set; }

        [XmlElement("Distance")]
        public float Distance { get; set; }

        [XmlElement("Cost")]
        public float Cost { get; set; }
    }
}
