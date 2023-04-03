namespace Domain.Common.Exception
{
	public class NotFoundException : System.Exception
	{
		public NotFoundException(string message) : base(message)
		{
		}
	}
}
