namespace Infrastructure.LoadingPipeline
{
    public struct LoadingResult
    {
        public bool Succeess;
        public string Error;

        public LoadingResult(bool success, string error = "")
        {
            Succeess = success;
            Error = error;
        }
    }
}
