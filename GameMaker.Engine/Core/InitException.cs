namespace GameMaker.Engine
{
    //初始化异常
    [Serializable()]
    public class InitException : Exception
    {
        public InitException() : base() { }
        public InitException(string message) : base(message) { }
        public InitException(string message, Exception inner) : base(message, inner) { }
    }
}
