namespace E_Commerce_Website.Data.Enum
{
    public class EnumResponse
    {
        public enum StatusResponse
        {
            Success = 1,
            Failed = 2,
            NotFound = 3,
            AlreadyExist = 4

        }

        public class ResultResponse
        {
            public StatusResponse Result { get; set; }
            public string? Message { get; set; }
        }
    }
}

