using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace IPE1D0_HSZF_2024251.Model
{
    [XmlRoot("Car")]
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [XmlElement("Id")]
        public int Id { get; set; }

        [Required]
        [XmlElement("Model")]
        public string Model { get; set; } = string.Empty;

        [XmlElement("TotalDistance")]
        public float TotalDistance { get; set; }

        [XmlElement("DistanceSinceLastMaintenance")]    
        public float DistanceSinceLastMaintenance { get; set; }
    }
}
