namespace movie.Playloads.Responses
{
    public class ResponsesObject<T>
    {
        public int status {  get; set; }
        public string message { get; set; }
        public T Data { get; set; }
        public ResponsesObject() { }
        public ResponsesObject(int status, string message, T data)
        {
            this.status = status;
            this.message = message;
            Data = data;
        }
        public ResponsesObject<T>ResponsesSucsess( string message,T data)
        {
            return new ResponsesObject<T> (StatusCodes.Status200OK,message,data);   
        }
        public ResponsesObject<T> ResponsesErr(int status,string message, T data)
        {
            return new ResponsesObject<T>(status, message, data);
        }
        

    }
}
