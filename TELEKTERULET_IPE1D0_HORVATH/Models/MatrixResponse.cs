namespace TELEKTERULET_IPE1D0_HORVATH.Models
{
    public class MatrixResponse
    {
        public int[][] Matrix { get; set; } = null!;
        public double Epszilon { get; set; }

        public List<Cellindex> MaxArea { get; set; } = new List<Cellindex>();

        public int MaxAreaSize => MaxArea.Count;
    }

    public class Cellindex
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public Cellindex(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
