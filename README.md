# What is base64
Base64 is an encryption method which converts human readable (UTF-8 encoded) text into the base64 decoded format.
The result is just some random letters, but with decoding them, they become human readable again.

# Why should I use that?
Not that many reasons, but examples:
  + Storing links of "pages you dont want to be found by other" inside a txt file, encoding it, done.
  + Creating a password list and decoding it so another can't read it so easily
  
Keep in mind, that everybody who knows that the letter garbage is Base64 can easily encode it again, so this method is only safe,
if you know who probably messes around with.

# Usage of this tool
It's simple, look:
```
  b64.exe encode/decode -i=path/to/input/file -o=path/to/output/file
```
