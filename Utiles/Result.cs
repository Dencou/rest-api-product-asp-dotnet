using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiApiRestTest.Utiles
{
   public class Result<T>{
    public bool Success { get; }
    public T? Data { get; }
    public string? Error { get; }

    protected Result(bool success, T? data, string? error)
    {
        Success = success;
        Data = data;
        Error = error;
    }

    public static Result<T> Ok(T data)
    {
        return new Result<T>(true, data, null);
    }

    public static Result<T> Fail(string error)
    {
        return new Result<T>(false, default, error);
    }
}
}