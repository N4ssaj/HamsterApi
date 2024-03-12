

namespace HamsterApi.Core.Common;

public class Result<TValue>
{
    private readonly TValue? _value;

    public Error Error { get; }

    public TValue Value=>IsSuccess
        ? _value! : throw new InvalidOperationException("The value of a failure result");

    public bool IsSuccess { get; }

    public bool Failure => !IsSuccess;

    private Result(TValue value)
        => (IsSuccess, _value, Error) = (true, value, Error.None);

    private Result(Error error)
        =>(IsSuccess, _value, Error)=(false,default,error);


    public static implicit operator Result<TValue>(TValue value)
        => new(value);

    public static implicit operator Result<TValue>(Error error)
        => new(error);
}
