namespace Landspace.Models
{
    public class LandAreaResponse
    {
        public int[][] Matrix { get; set; } = null!;
        public double Epsilon { get; set; }
        public List<CellIndex> LargestAreaIndices { get; set; } = new List<CellIndex>();
        public int LargestAreaSize => LargestAreaIndices.Count;
    }

    public class CellIndex
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public CellIndex(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
} 