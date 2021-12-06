using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode21
{
    class Bingo
    {
        private List<Board> boards;
        private List<int> numbers;

        public Bingo(List<string> numbers, List<string> lines)
        {
            this.numbers = numbers.Select(val => Int32.Parse(val)).ToList();
            this.boards = new();

            List<List<GridItem>> boardItems = new();
            foreach(var line in lines)
            {
                if(line.Equals(""))
                {
                    boards.Add(new Board(boardItems));
                    boardItems.Clear();
                    continue;
                }
                else
                {
                    var split = line.Trim().Split(' ').ToList().Where(val => val != "").Select(val => new GridItem(Int32.Parse(val))).ToList();
                    boardItems.Add(split);
                }                
            }
            boards.Add(new Board(boardItems));
        }

        public void GetFirstWinner()
        {
            foreach(var number in numbers)
            {
                Console.WriteLine(number);
                foreach (Board board in boards)
                {                    
                    board.Mark(number);
                    if (board.CheckGrid())
                    {
                        Console.WriteLine(board.GetScore(number));
                        Console.ReadLine();
                    }
                }
            }
        }

        public void GetLastWinner()
        {
            Board lastWinner = null;
            int lastWinningNumber = -1;
            List<Board> winners = new();
            foreach(var number in numbers)
            {
                Console.WriteLine(number);
                foreach (Board board in boards)
                {                    
                    board.Mark(number);
                    if (board.CheckGrid())
                    {
                        lastWinner = board;
                        lastWinningNumber = number;
                        winners.Add(board);
                    }
                    boards = boards.Where(val => !winners.Contains(val)).ToList();

                }
            }
            Console.WriteLine(lastWinner.GetScore(lastWinningNumber));
        }
    }


    class Board
    {
        List<List<GridItem>> rows;

        public Board(List<List<GridItem>> rows)
        {
            this.rows = new(rows);
        }

        public void Mark(int number)
        {
            foreach(var row in this.rows)
            {
                foreach(GridItem item in row)
                {
                    if(item.Value == number)
                    {
                        Console.WriteLine($"Marked {number}");
                        item.IsMarked = true;
                    }
                }
            }
        }

        public bool CheckGrid()
        {
            return CheckRows() || CheckColumns();
        }

        public bool CheckRows()
        {
            foreach (var row in rows)
            {
                var marked = true;
                foreach(var item in row)
                {
                    if (!item.IsMarked)
                    {
                        marked = false;
                    }
                }
                if (marked)
                {
                    return true;
                }
            };
            return false;
        }

        public bool CheckColumns()
        {
            for(int i = 0; i < rows[0].Count; i++)
            {
                var marked = true;
                for(int j = 0; j < rows.Count; j++)
                {
                    if (!rows[j][i].IsMarked)
                    {
                        marked = false;
                    }
                }
                if (marked)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetScore(int winningNumber)
        {
            var sum = 0;
            foreach(var row in rows)
            {
                foreach(var item in row)
                {
                    if (!item.IsMarked)
                    {
                        sum += item.Value;
                    }
                }
            }
            return sum * winningNumber;
        }
    }


    class GridItem
    {
        int value;
        bool isMarked = false;

        public GridItem(int value)
        {
            this.Value = value;
        }

        public bool IsMarked { get => isMarked; set => isMarked = value; }
        public int Value { get => value; private set => this.value = value; }
    }
}