namespace ConwaysGameOfLife
{
    using System;
    using System.Text;

    //Any live cell with fewer than two live neighbours dies, as if caused by under-population.
    //Any live cell with two or three live neighbours lives on to the next generation.
    //Any live cell with more than three live neighbours dies, as if by overcrowding.
    //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
    public class Start
    {
        private const string LIVE_CELL = "X";
        private const string DEAD_CELL = " ";
        private static StringBuilder sb;
        private static int rows;
        private static int cols;
        private static bool[,] world;
        private static bool[,] nextGen;
        private static int gameSpeed;

        static void Main(string[] args)
        {
            InitialConfig();

            SetInitialPattern();

            RunGame();
        }

        private static void RunGame()
        {
            while (true)
            {
                nextGen = new bool[rows, cols];

                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        int neighbours = CheckNeighbourCount(row, col);

                        //check live cells
                        if (world[row, col])
                        {
                            if (neighbours < 2 || neighbours > 3)
                            {
                                nextGen[row, col] = false;
                            }
                            else // neighbours == 3
                            {
                                nextGen[row, col] = true;
                            }
                        }
                        else //check dead cells
                        {
                            if (neighbours == 3)
                            {
                                nextGen[row, col] = true;
                            }
                        }

                        sb.Append(nextGen[row, col] ? LIVE_CELL : DEAD_CELL);
                    }
                    sb.AppendLine();
                }

                world = nextGen;

                Console.WriteLine(sb.ToString()); //draw
                sb.Clear();

                System.Threading.Thread.Sleep(gameSpeed); //wait before clearing console so we can see
                Console.Clear(); 
            }
        }

        //uncomment if you want to try it
        private static void SetInitialPattern()
        {
            //fun starts here
            for (int i = 0; i < cols; i++)
            {
                world[10, i] = true;
            }

            //blink
            //world[4, 25] = true;
            //world[4, 26] = true;
            //world[4, 27] = true;
            //world[5, 26] = true;
            //world[5, 27] = true;
            //world[5, 28] = true;

            //blink
            //world[4, 25] = true;
            //world[4, 26] = true;
            //world[5, 25] = true;
            //world[5, 26] = true;
            //world[6, 27] = true;
            //world[6, 28] = true;
            //world[7, 27] = true;
            //world[7, 28] = true;

            //still life
            //world[4, 25] = true;
            //world[4, 26] = true;
            //world[5, 25] = true;
            //world[5, 26] = true;

            //still life
            //world[4, 25] = true;
            //world[4, 26] = true;
            //world[5, 25] = true;
            //world[5, 27] = true;
            //world[6, 26] = true;
        }

        private static void InitialConfig()
        {
            sb = new StringBuilder();
            rows = 20;
            cols = 50;
            gameSpeed = 100;
            world = new bool[rows, cols];
        }

        private static int CheckNeighbourCount(int row, int col)
        {
            var neighbourCount = 0;
            for (int currentRow = (row - 1); currentRow <= (row + 1); currentRow++)
            {
                if (currentRow >= 0 && currentRow < rows)
                {
                    for (int currentCol = (col - 1); currentCol <= (col + 1); currentCol++)
                    {
                        //skip self
                        if (currentRow == row && currentCol == col)
                        {
                            continue;
                        }
                        
                        if (currentCol >= 0 && currentCol < cols)
                        {
                            if (world[currentRow, currentCol])
                            {
                                neighbourCount++;
                            }
                        }
                    }
                }
            }

            return neighbourCount;
        }
    }
}
