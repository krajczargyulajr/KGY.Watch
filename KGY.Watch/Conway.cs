using System;

namespace KGY.Watch
{
    public class Conway
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private bool[,] cells;

        public bool[,] Cells { get { return cells; } }

        public Conway(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            cells = new bool[width, height];
        }

        public void Turn()
        {
            Random r = new Random();
            var isDisasterTime = r.Next(100) > 90;

            if (isDisasterTime)
            {
                IntroduceDisaster();
            }
            else
            {
                for (var x = 0; x < Width; ++x)
                {
                    for (var y = 0; y < Height; ++y)
                    {
                        var neighborCount = NeighborCount(x, y);
                        if (cells[x, y])
                        {
                            if (neighborCount < 2 || neighborCount > 3)
                            {
                                cells[x, y] = false;
                            }
                        }
                        else
                        {
                            if (neighborCount == 3)
                            {
                                cells[x, y] = true;
                            }
                        }
                    }
                }
            }
        }

        public void IntroduceDisaster()
        {
            var rSeed = new Random(DateTime.Now.Second).Next(100);

            if (rSeed < 30) Console.WriteLine("Lucky bastard");
            else if (rSeed < 60) ABomb();
            else if (rSeed < 95) Meteor();
            else Napalm();
        }

        private void Clear()
        {
            Clear(0, Width, 0, Height);
        }

        private void Clear(int xFrom, int xTo, int yFrom, int yTo)
        {
            for (var x = xFrom; x < xTo; ++x)
                for (var y = yFrom; y < yTo; ++y)
                    cells[x, y] = false;
        }

        private void ABomb()
        {
            Console.WriteLine("ABomb");
            var r = new Random();
            int xLower = r.Next(Width);
            int xUpper = r.Next(xLower, Width);
            int yLower = r.Next(Height);
            int yUpper = r.Next(yLower, Height);

            Clear(xLower, xUpper, yLower, yUpper);
        }

        private void Meteor()
        {
            Console.WriteLine("Meteor");
            var r = new Random();
            int startX = r.Next(Width);
            int startY = r.Next(Height);
            int endX = r.Next(Width);
            int endY = r.Next(Height);
            double length = Math.Sqrt(Math.Pow((startX - endX), 2) + Math.Pow((startY - endY), 2));
            int size = (int)Math.Ceiling(length / 10);

            double dx = endX - startX;
            double dy = endY - startY;

            int steps;
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                steps = (int)Math.Abs(dx);
            }
            else
            {
                steps = (int)Math.Abs(dy);
            }

            double Xincrement = dx / steps;
            double Yincrement = dy / steps;

            double x = startX;
            double y = startY;
            for (int i = 0; i < steps; ++i)
            {
                x = x + Xincrement;
                y = y + Yincrement;
                cells[(int)Math.Ceiling(x), (int)Math.Ceiling(y)] = false;
            }
        }

        private void Napalm()
        {
            Console.WriteLine("Napalm");
            Clear();
            RandomSeed();
        }

        private int NeighborCount(int x, int y)
        {
            var neighborCount = 0;
            if (x > 0 && cells[x - 1, y]) neighborCount++;
            if (x > 0 && y < Height - 1 && cells[x - 1, y + 1]) neighborCount++;
            if (y < Height - 1 && cells[x, y + 1]) neighborCount++;
            if (x < Width - 1 && y < Height - 1 && cells[x + 1, y + 1]) neighborCount++;
            if (x < Width - 1 && cells[x + 1, y]) neighborCount++;
            if (x < Width - 1 && y > 0 && cells[x + 1, y - 1]) neighborCount++;
            if (y > 0 && cells[x, y - 1]) neighborCount++;
            if (x > 0 && y > 0 && cells[x - 1, y - 1]) neighborCount++;

            return neighborCount;
        }

        public void RandomSeed()
        {
            Random r = new Random(DateTime.Now.Millisecond);

            for (var x = 0; x < Width; ++x)
            {
                for (var y = 0; y < Height; ++y)
                {
                    var rnext = r.Next(10);
                    if (rnext > 8) cells[x, y] = true;
                    else cells[x, y] = false;
                }
            }
        }
    }

    public enum DisasterType
    {
        ABomb = 0,
        Napalm = 1,
        Meteor = 2
    }
}
