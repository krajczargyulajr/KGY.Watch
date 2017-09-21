using System;

namespace KGY.Watch.Conway.Stages
{
    public class GridStage : IStage
    {
        public bool IsInitialized { get; set; }

        public bool IsAlive { get; private set; } = false;

        public uint Width { get; set; }

        public uint Height { get; set; }

        public bool[,] Cells { get; private set; }

        public void Initialize()
        {
            if (Width == 0 || Height == 0)
            {
                throw new Exception("Dimensions are not set properly");
            }

            Cells = new bool[Width, Height];

            IsInitialized = true;
        }

        public void Clear()
        {
            if (!IsInitialized) return;

            Clear(0, Width, 0, Height);
        }

        public void Clear(uint xFrom, uint xTo, uint yFrom, uint yTo)
        {
            if (!IsInitialized) return;

            for (var x = xFrom; x < xTo; ++x)
                for (var y = yFrom; y < yTo; ++y)
                    Cells[x, y] = false;
        }

        public int NeighborCount(uint x, uint y)
        {
            var neighborCount = 0;
            if (x > 0 && Cells[x - 1, y]) neighborCount++;
            if (x > 0 && y < Height - 1 && Cells[x - 1, y + 1]) neighborCount++;
            if (y < Height - 1 && Cells[x, y + 1]) neighborCount++;
            if (x < Width - 1 && y < Height - 1 && Cells[x + 1, y + 1]) neighborCount++;
            if (x < Width - 1 && Cells[x + 1, y]) neighborCount++;
            if (x < Width - 1 && y > 0 && Cells[x + 1, y - 1]) neighborCount++;
            if (y > 0 && Cells[x, y - 1]) neighborCount++;
            if (x > 0 && y > 0 && Cells[x - 1, y - 1]) neighborCount++;

            return neighborCount;
        }

        public bool GetCell(uint x, uint y)
        {
            return Cells[x, y];
        }

        public void SetCell(uint x, uint y, bool value)
        {
            Cells[x, y] = value;

            if (value) IsAlive = true;
        }

        public IStage Clone()
        {
            var clone = new GridStage()
            {
                Width = Width,
                Height = Height
            };
            clone.Initialize();

            return clone;
        }
    }
}
