using TELEKTERULET_IPE1D0_HORVATH.Models;

namespace TELEKTERULET_IPE1D0_HORVATH.Service
{
    public class LandAreaService
    {
        public MatrixResponse CalculateLargestArea(MatrixRequest matrixRequest)
        {
            var matrix = matrixRequest.Matrix;
            var epsilon = matrixRequest.Epszilon;
            int n = matrix.Length;

            var response = new MatrixResponse
            {
                Matrix = matrix,
                Epszilon = epsilon
            };

            if (n == 0)
            {
                return response;
            }

            bool[,] visited = new bool[n, n];
            List<Cellindex> largestArea = new List<Cellindex>();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (!visited[i, j])
                    {
                        List<Cellindex> currentArea = new List<Cellindex>();
                        FindContiguousArea(matrix, visited, i, j, epsilon, currentArea);

                        if (currentArea.Count > largestArea.Count)
                        {
                            largestArea = currentArea;
                        }
                    }
                }
            }

            response.MaxArea = largestArea;
            return response;
        }
        private void FindContiguousArea(int[][] matrix, bool[,] visited, int startRow, int startCol, double epsilon, List<Cellindex> area)
        {
            int n = matrix.Length;

            Queue<Cellindex> queue = new Queue<Cellindex>();
            queue.Enqueue(new Cellindex(startRow, startCol));

            visited[startRow, startCol] = true;
            area.Add(new Cellindex(startRow, startCol));

            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, 1, 0, -1 };

            while (queue.Count > 0)
            {
                Cellindex current = queue.Dequeue();
                int currentHeight = matrix[current.Row][current.Col];

                for (int i = 0; i < 4; i++)
                {
                    int newRow = current.Row + dx[i];
                    int newCol = current.Col + dy[i];

                    if (newRow >= 0 && newRow < n && newCol >= 0 && newCol < n && !visited[newRow, newCol])
                    {
                        int newHeight = matrix[newRow][newCol];

                        if (Math.Abs(newHeight - currentHeight) <= epsilon)
                        {
                            visited[newRow, newCol] = true;
                            area.Add(new Cellindex(newRow, newCol));

                            queue.Enqueue(new Cellindex(newRow, newCol));
                        }
                    }
                }
            }
        }
    }
}
