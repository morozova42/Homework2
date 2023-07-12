namespace WebClient
{
	public class Response<T>
	{
		public int Result { get; set; }
		public string Error { get; set; }
		public T Data { get; set; }
	}
}
