using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using System.Xml.Serialization;

namespace IPE1D0_HSZF_2024251.Model
{
    [XmlRoot("Customer")]
    public class Customer
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; } = string.Empty;

        [XmlElement("Balance")]
        public float Balance { get; set; }
    }
}
