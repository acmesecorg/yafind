# yafind

Yafind (Yet another find).

Find plains for a hash using arbitrarily defined algorithms.

## Syntax 

Work in progress

md5(plain)
md5(salt.plain)

where plain and salt and md5 are predefined keywords
. is a concatenation operator
( and ) is a precedance operator

Given the lines above, the following c# code functions would be generated

  string Func1(string plain)
  {
    return md5(plain);
  }
  
  string Func2(string plain, string salt)
  {
    return md5(salt+plain);
  }
  
  string Execute(string plain, string salt)
  {
  }
