# Yet Another Find

Find plains for a hash using arbitrarily defined algorithms.

## Usage

`yafind algorithm hash:salt:plain`

## Syntax 

The following is the types of syntax that yafind supports:

```
md5(plain)
md5(salt.plain)
md5(md5(plain))
md5(md5(plain)+md5(salt)))
```

where `plain` and `salt` and md5 are predefined, `.` is a concatenation operator `(` and `)` is a precedance operator. The code is transformed to a syntactically correct c# statement and compiled on the fly for optimal performance.

To achieve this, following bootsrap c# code is generated and the `Plain` and `Salt` are injected into the process:
```
  var source = args[0].Replace('.', '+');

  var builder = new StringBuilder();
  builder.AppendLine("Bytes Func1(string plain, string salt)");
  builder.AppendLine("{");
  builder.AppendLine($"return {source};");
  builder.AppendLine("}");
  builder.AppendLine("return Func1(Plain, Salt);");
 ```
 
These primitives (e.g. for the md5 algorithm) are also required as support structures and functions and are injected into the global namespace of the script. They utilize the underlying equivaklent present in the .net frameowrk, or can utilize custom implementations of functions if required.
 
 ```
public static Bytes md5(string value)
{
  var bytes = Encoding.UTF8.GetBytes(value);
  return new Bytes(_md5.ComputeHash(bytes));
}

public static Bytes md5(Bytes value)
{
  return new Bytes(_md5.ComputeHash(value.GetValue()));
}
```
  
The `Bytes` type is created so that the + operator can be overloaded to be used between strings and bytes interchangably.
```
//Create a custom type so that we can use the + operator against byte arrays
public readonly struct Bytes
{
  private readonly byte[] _bytes;

  public Bytes(byte[] value)
  {
    _bytes = value;
  }

  public byte[] GetValue()
  {
    return _bytes;
  }

  public static Bytes operator +(Bytes a, Bytes b)
  {
    var first = a.GetValue();
    var second = b.GetValue();

    byte[] ret = new byte[first.Length + second.Length];
    Buffer.BlockCopy(first, 0, ret, 0, first.Length);
    Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
    return new Bytes(ret);
  }
}
```
