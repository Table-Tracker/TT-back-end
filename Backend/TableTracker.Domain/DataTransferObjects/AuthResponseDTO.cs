namespace TableTracker.Domain.DataTransferObjects
{
    public class AuthResponseDTO
    {
        public bool IsAuthSuccessful { get; set; }

        public string ErrorMessage { get; set; }

        public string Token { get; set; }

        public VisitorDTO User { get; set; }
    }
}
