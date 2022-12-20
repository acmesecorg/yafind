# yafind

Yafind (Yet another find).

Find plains for a hash using arbitrarily defined algorithms.

## Syntax 

Work in progress

```
md5(plain)
md5(salt.plain)
md5(md5(plain))
```

where plain and salt and md5 are predefined keywords
`.` is a concatenation operator
`(` and `)` is a precedance operator

Given the lines above, the following c# code functions would be generated
```
  byte[] Func1(string plain)
  {
    return md5(plain);
  }
  
  byte[] Func2(string plain, string salt)
  {
    return md5(salt+plain);
  }
  
  byte Func3(string plain)
  {
    return md5(md5(plain))
  }
  
  string Execute(string plain, string salt)
  {
  }
 ```
