namespace Wolfpack.Business.Models;

public enum ServiceResultCode
{
    Ok = 200,
    Created = 201,
    NotFound = 404,
    Conflict = 409,
    ValidationError = 8000,
}