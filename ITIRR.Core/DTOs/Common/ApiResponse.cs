namespace ITIRR.Services.DTOs.Common
{
    public class ResponseMeta
    {
        public bool IsResponse { get; set; }
        public bool IsSuccess { get; set; }
        public string ServerMsg { get; set; } = string.Empty;
        public string? Error { get; set; } // only on server crash
    }

    public class ApiResponse<T>
    {
        public ResponseMeta Data { get; set; } = new();
        public T? Payload { get; set; }

        // ── Success: record fetched / login / list ──
        public static ApiResponse<T> Success(T payload, string message = "Success")
            => new()
            {
                Data = new ResponseMeta { IsResponse = true, IsSuccess = true, ServerMsg = message },
                Payload = payload
            };

        // ── Created ──
        public static ApiResponse<T> Created(T payload)
            => new()
            {
                Data = new ResponseMeta { IsResponse = true, IsSuccess = true, ServerMsg = "Record Created Successfully" },
                Payload = payload
            };

        // ── Updated ──
        public static ApiResponse<T> Updated(T payload)
            => new()
            {
                Data = new ResponseMeta { IsResponse = true, IsSuccess = true, ServerMsg = "Record Updated Successfully" },
                Payload = payload
            };

        // ── Deleted ──
        public static ApiResponse<T> Deleted()
            => new()
            {
                Data = new ResponseMeta { IsResponse = true, IsSuccess = true, ServerMsg = "Record Deleted Successfully" },
                Payload = default
            };

        // ── Not found ──
        public static ApiResponse<T> NotFound(string message = "No Record Found")
            => new()
            {
                Data = new ResponseMeta { IsResponse = true, IsSuccess = false, ServerMsg = message },
                Payload = default
            };

        // ── Validation failed ──
        public static ApiResponse<T> ValidationFailed(string message = "Validation Failed")
            => new()
            {
                Data = new ResponseMeta { IsResponse = true, IsSuccess = false, ServerMsg = message },
                Payload = default
            };

        // ── Unauthorized ──
        public static ApiResponse<T> Unauthorized(string message = "Unauthorized")
            => new()
            {
                Data = new ResponseMeta { IsResponse = true, IsSuccess = false, ServerMsg = message },
                Payload = default
            };

        // ── Server error ──
        public static ApiResponse<T> ServerError(string error = "Internal Server Error")
            => new()
            {
                Data = new ResponseMeta
                {
                    IsResponse = true,
                    IsSuccess = false,
                    ServerMsg = "Internal Server Error",
                    Error = error
                },
                Payload = default
            };

        // ── Generic fail ──
        public static ApiResponse<T> Fail(string message)
            => new()
            {
                Data = new ResponseMeta { IsResponse = true, IsSuccess = false, ServerMsg = message },
                Payload = default
            };
    }
}