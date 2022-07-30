using Assets.CodeBase.Data;

namespace Assets.CodeBase.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}
