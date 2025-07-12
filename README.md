# EncFile Tools

---

### Usage:

1. Drag and drop the file (multiple allowed) onto the exe icon.

2. The software will automatically execute and display the processed file to the console.
 
3. The files with the same name that are added under the original file are the processed files.

> (Note: Most new files will end with '~1'.)

1. Most programs: Repeat the above to restore the file.

> (Note: In 'Details', programs marked with (R) can be manipulated in this way, while those marked with (N) are described separately.)

**WARNING: Before processing a file, you need to check whether there are duplicate files. The software does not detect and instead directly overwrites the write. This results in file errors.**

**Only files are supported as command line arguments! Input file paths are not supported.**

### Details:

`reverse'X'`

The Reverse'X' program reverses the bytes of the file.('X' is the number of bytes in each reverse order.) (R)

> (Note: 'reverseulongsubtract' program will do ulong subtraction first, and then a reverse order.)

`ulongsubtract`

Perform a ulong subtraction of the file bytes. (R)

`unevensplit`
Split the input file unevenly. (N)

> (Note: This program can only split files, not revert. But 7-zip does.)

`xor`
XOR the entire file. (R)

> (Note: You need to provide the correct key manually. If the key is incorrect, you will get an incorrect file.)

`encodefilenameb'X'`

Encode the file name (including the suffix). ('X' = 16 or 64) (N)

> (Note: The software does not support decoding file names. You need to use other software to decode.)
> (Note: The '/' in the Base64 file name will be replaced with '_'.)

`renamewith'X'`

Calculate the 'X' value of the file and rename the file to this value.Keep the file suffix.('X' is the algorithm.)

`evenlydistributed+ / evenlydistributed-`

Divide the document evenly into two copies (not commom volumes). (R,'-' For splitting, '+'for merging)

> (Distribution example:
> Original document: 123456
> New File A: 135
> New File B: 246)

It is also possible to merge two files of the exact same size together. Restore files using '-' software.

> (Note:You should manually rename the file so that the last character of one file is A and the other is B.)
> (Note:If the two imported files are not the same size, the output file will be incorrect.)

`fakeheaderzip+ / fakeheaderzip-`

Add random data to the ZIP/RAR/7z archive so that it is not detected by file detection programs such as trid.(R,'-' For splitting, '+'for merging)

The '+' program only provides intelligent addition of random data, and cannot customize the data content. However, the '-' program will output to the .header file regardless of what data is added when processing.

`searchhex`

Search for a specified HEX string or a normal string in the file. and output a new file that starts with the position of that string.(N)

> (Note: Only one location can be searched. If you want to search for more than one, work on the files yourself before continuing.)
> (Note: If the input string does not look like a HEX string, it will be treated as a normal string. Otherwise, it is treated as a HEX string. If you want to search for normal strings that look like HEX, you need to manually convert them to true HEX strings first.)

`decodeb64file`

Decode from Base64 text file.(R)


---

### C# Source:

The .cs file with the same name as the .exe is the source code of the corresponding program.

---
