namespace KGY.Watch.Conway.Stages
{
    public interface IStage
    {
        uint Width { get; }

        uint Height { get; }

        void Initialize();

        void Clear();

        void Clear(uint xFrom, uint xTo, uint yFrom, uint yTo);

        IStage Clone();

        bool GetCell(uint x, uint y);

        void SetCell(uint x, uint y, bool value);

        int NeighborCount(uint x, uint y);
    }
}
