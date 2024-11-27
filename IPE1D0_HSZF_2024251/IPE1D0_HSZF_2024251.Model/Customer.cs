using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace IPE1D0_HSZF_2024251.Model
{
    [XmlRoot("Customer")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [XmlElement("Id")]
        public int Id { get; set; }

        [Required]
        [XmlElement("Name")]
        public string Name { get; set; } = string.Empty;

        [XmlElement("Balance")]
        public float Balance { get; set; }
    }
}
