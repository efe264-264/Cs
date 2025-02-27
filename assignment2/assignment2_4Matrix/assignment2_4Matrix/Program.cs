namespace assignment2_4Matrix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[][] matrix1 = {
            new int[] {1, 2, 3, 4},
            new int[] {5, 1, 2, 3},
            new int[] {9, 5, 1, 2}
            };
            int[][] matrix2 = {
            new int[] {1, 5, 3, 4},
            new int[] {5, 1, 2, 3},
            new int[] {9, 5, 1, 2}
            };

            Console.Write(IsTopliMatrix(matrix1));

            Console.Write('\n');
            Console.Write(IsTopliMatrix(matrix2));
        }



        static bool IsTopliMatrix(int[][] matrix)
            {
                int row = matrix.Length;
                int col = matrix[0].Length;

                for (int i = 0; i < row-1; i++)
                {
                    
                    for (int j = 0; j < row-1; j++)
                    {
                        if (matrix[i][j] != matrix[i + 1][j + 1])
                            return false;
                    }
                }
                return true;
            }
        }
    }

