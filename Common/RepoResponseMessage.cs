namespace Common
{
    public class RepoResponseMessage<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
        public T Data { get; set; }
    }
}