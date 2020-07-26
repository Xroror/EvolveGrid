using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    class Grid
    {
        private List<List<int>> GridContainer = new List<List<int>>();
        private int EvolutionCount = 0;
        private int Xtar = 0;
        private int Ytar = 0;
        private int GreenGeneration = 0;

        public Grid(List<List<int>> GridInput, int XtarInp, int YtarInp, int EvolCountInp){
            GridContainer = GridInput;
            Xtar = XtarInp;
            Ytar = YtarInp;
            EvolutionCount = EvolCountInp;
            CheckForGreen();//check initial grid if it is a green generation
            EvolveGrid();
        }

        private void CheckForGreen() 
        {
            if (GridContainer[Xtar][Ytar] == 1)
            {
                GreenGeneration++;
            }
        }

        private void EvolveGrid()
        {
            for(int e = 0;e < EvolutionCount; e++)
            {
                List<List<int>> NextGrid = new List<List<int>>();

                for (int i = 0; i < GridContainer.Count; i++)
                {
                    List<int> TempGridRow = new List<int>();

                    for (int j = 0; j < GridContainer[i].Count; j++)
                    {
                        List<int> NeighbourList = new List<int>();

                        int GreenCount = 0;
                        NeighbourList = FindNeighbours(i, j);
                        for (int k = 0; k < NeighbourList.Count; k++)
                        {
                            if (NeighbourList[k] == 1)
                            {
                                GreenCount++;
                            }
                        }

                        if (GridContainer[i][j] == 0 && (GreenCount == 3 || GreenCount == 6))
                        {
                            TempGridRow.Add(1);
                        }
                        if (GridContainer[i][j] == 0 && (GreenCount != 3 && GreenCount != 6))
                        {
                            TempGridRow.Add(0);
                        }
                        if (GridContainer[i][j] == 1 && (GreenCount != 2 && GreenCount != 3 && GreenCount != 6))
                        {
                            TempGridRow.Add(0);
                        }
                        if (GridContainer[i][j] == 1 && (GreenCount == 2 || GreenCount == 3 || GreenCount == 6))
                        {
                            TempGridRow.Add(1);
                        }

                    }
                    NextGrid.Add(TempGridRow);
                }

                GridContainer.Clear();
                GridContainer = NextGrid;
                CheckForGreen(); // check for a green target after each evolution

            }
            Console.WriteLine("Green Generations:");
            Console.WriteLine(GreenGeneration);

        }

        private List<int> FindNeighbours(int tarX, int tarY)
        {
            List<int> Neighbours = new List<int>();

            for (int i = -1; i < 2; i++)
            {
                if ((tarX + i) < 0 || tarX + i > GridContainer.Count - 1)
                {
                    continue;
                }

                for (int j = -1; j < 2; j++)
                {
                    if ((tarY + j) < 0 || tarY + j > GridContainer[0].Count - 1)
                    {
                        continue;
                    }
                    if (tarX + i == tarX && tarY + j == tarY)
                    {
                        continue;
                    }
                    Neighbours.Add(GridContainer[tarX + i][tarY + j]);
                }

            }

            return Neighbours;
        }

    }



    class UserInput
    {

        private  List<List<int>> TempGridCont = new List<List<int>>();

        public UserInput()
        {
            Console.WriteLine("Enter Grid Size X, Y:");

            string InputGridSize = Console.ReadLine();
            string[] GridSize = InputGridSize.Split(',');

            int Xsize = Convert.ToInt32(GridSize[0]);
            int Ysize = Convert.ToInt32(GridSize[1]);

            GetGridInfo(Xsize, Ysize);
            GetLastArgs();
        }

        private  void GetLastArgs()
        {
            Console.WriteLine("Enter target cell X and Y followed by Generation count.");
            Console.WriteLine("ex.: 2, 1, 10");
            string LastArg = Console.ReadLine();
            string[] LastArgList = LastArg.Split(',');
            int Ytar = Int32.Parse(LastArgList[0]);
            int Xtar = Int32.Parse(LastArgList[1]);
            int Generations = Int32.Parse(LastArgList[2]);
            Grid myGrid = new Grid(TempGridCont, Xtar, Ytar, Generations);
        }

        private  void GetGridInfo(int Xsize, int Ysize)
        {
            for (int i = 0; i < Ysize; i++)
            {
                Console.WriteLine($"Enter the info for row:{i+1}");
                string GridLine = Console.ReadLine();
                if (GridLine.Length > Xsize || GridLine.Length < Xsize)
                {
                    Console.WriteLine("Last line contained too many or too few charecters, it will be ignored;");
                    i--;
                }
                else
                {
                    GenerateGrid(GridLine);
                }
               
            }

        }

        private  void GenerateGrid(string GridLine)
        {
            List<int> TempList = new List<int>();
            for (int i = 0; i < GridLine.Length; i++)
            {
                int TempInt = (int)Char.GetNumericValue(GridLine[i]);
                TempList.Add(TempInt);
            }
            TempGridCont.Add(TempList);
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {

            UserInput User1 = new UserInput();
            Console.ReadKey();
        }
    }
}
