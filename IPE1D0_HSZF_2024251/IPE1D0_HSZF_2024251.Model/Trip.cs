using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace IPE1D0_HSZF_2024251.Model
{
    [XmlRoot("Trip")]
    public class Trip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [XmlElement("Id")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Car")]
        [XmlElement("CarId")]
        public int CarId { get; set; }

        [Required]
        [ForeignKey("Customer")]
        [XmlElement("CustomerId")]
        public int CustomerId { get; set; }

        [XmlElement("Distance")]
        public float Distance { get; set; }

        [XmlElement("Cost")]
        public float Cost { get; set; }

        public event EventHandler<string> OnInsufficientBalance;
        public event EventHandler<string> OnTripCompleted;

        public void NotifyInsufficientBalance(string message)
        {
            OnInsufficientBalance?.Invoke(this, message);
        }

        public void NotifyTripCompleted(string message)
        {
            OnTripCompleted?.Invoke(this, message);
        }
    }
}
