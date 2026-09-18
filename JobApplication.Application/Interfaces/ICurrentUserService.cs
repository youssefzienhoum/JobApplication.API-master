namespace JobApplication.Application.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        bool IsAuthenticated { get; }
        bool IsRecruiter { get; }
        bool IsCandidate { get; }
    }
}
