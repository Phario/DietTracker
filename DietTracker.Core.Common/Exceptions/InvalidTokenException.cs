using System.Net;

namespace DietTracker.Core.Common.Exceptions;

public class InvalidTokenException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) 
: ExceptionBase(message, statusCode);