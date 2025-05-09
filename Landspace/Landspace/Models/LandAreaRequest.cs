namespace Landspace.Models
{
    public class LandAreaRequest
    {
        public int[][] Matrix { get; set; } = null!;
        public double Epsilon { get; set; }
    }
} 