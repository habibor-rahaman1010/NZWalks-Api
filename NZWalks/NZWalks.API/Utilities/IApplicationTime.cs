namespace NZWalks.API.Utilities
{
    public interface IApplicationTime
    {
        public DateTime GetCurrentTime();
        public DateTime GetUtcNowTime();
    }
}
