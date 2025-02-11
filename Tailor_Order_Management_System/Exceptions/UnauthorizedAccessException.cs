namespace Tailor_Order_Management_System.Exceptions
{
    public class UnauthorizedAccessException:Exception
    {
        public UnauthorizedAccessException(string message) : base(message)
        {
        }
    }
}
