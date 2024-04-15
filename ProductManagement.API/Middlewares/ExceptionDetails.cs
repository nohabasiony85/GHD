namespace ProductManagement.API.Middlewares;

public record ExceptionDetails(
    int Status,
    string Type,
    string Title,
    string Detail,
    IEnumerable<object>? Errors);