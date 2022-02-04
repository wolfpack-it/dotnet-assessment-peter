namespace Wolfpack.Business.Models;

public enum ServiceResultCode
{
    Ok = 200,
    NotFound = 404,
    Conflict = 409,
    ValidationError = 8000,
}