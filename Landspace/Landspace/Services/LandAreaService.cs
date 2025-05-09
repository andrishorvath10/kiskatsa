using Landspace.Models;

namespace Landspace.Services
{
    public class LandAreaService
    {
        public LandAreaResponse CalculateLargestArea(LandAreaRequest request)
        {
            var matrix = request.Matrix;
            var epsilon = request.Epsilon;
            int n = matrix.Length;
            
            var response = new LandAreaResponse
            {
                Matrix = matrix,
                Epsilon = epsilon
            };

            if (n == 0)
            {
                return response;
            }

            bool[,] visited = new bool[n, n];
            List<CellIndex> largestArea = new List<CellIndex>();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (!visited[i, j])
                    {
                        List<CellIndex> currentArea = new List<CellIndex>();
                        FindContiguousArea(matrix, visited, i, j, epsilon, currentArea);

                        if (currentArea.Count > largestArea.Count)
                        {
                            largestArea = currentArea;
                        }
                    }
                }
            }

            response.LargestAreaIndices = largestArea;
            return response;
        }

        // BFS (Breadth-First Search)
        private void FindContiguousArea(int[][] matrix, bool[,] visited, int startRow, int startCol, double epsilon, List<CellIndex> area)
        {
            int n = matrix.Length;
            
            Queue<CellIndex> queue = new Queue<CellIndex>();
            queue.Enqueue(new CellIndex(startRow, startCol));
            
            visited[startRow, startCol] = true;
            area.Add(new CellIndex(startRow, startCol));
            
            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, 1, 0, -1 };
            
            while (queue.Count > 0)
            {
                CellIndex current = queue.Dequeue();
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
                            area.Add(new CellIndex(newRow, newCol));
                            
                            queue.Enqueue(new CellIndex(newRow, newCol));
                        }
                    }
                }
            }
        }
    }
} 