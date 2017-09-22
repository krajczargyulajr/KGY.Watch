using KGY.Watch.Conway.Stages;

namespace KGY.Watch.Conway
{
    public interface IGame
    {
        IStage Stage { get; set; }

        void Initialize();
    }
}
