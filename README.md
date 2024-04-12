# ZipLogCli

ZipLogClie or ZL is a command-line interface (CLI) tool written in C# to simplify zipping files while providing options to delete them afterward. It offers various features to streamline the zipping process and manage file deletion according to user preferences.

## Table of Contents

- [ZipLogCli](#ziplogcli)
  - [Table of Contents](#table-of-contents)
  - [Lates Version and download](#lates-version-and-download)
  - [Features](#features)
    - [File Patterns](#file-patterns)
  - [Installation](#installation)
  - [Usage](#usage)
    - [Arguments](#arguments)
    - [Options](#options)
  - [Examples](#examples)
    - [Zip file one and delete them afterward](#zip-file-one-and-delete-them-afterward)
    - [Zip files using a patter to allocate them](#zip-files-using-a-patter-to-allocate-them)
    - [Add files to an existing zip file](#add-files-to-an-existing-zip-file)
    - [Zip files and skip confirmations](#zip-files-and-skip-confirmations)
  - [Acknowledgments](#acknowledgments)
  - [Authors](#authors)
  - [License](#license)

## Lates Version and download

- ZipLogCli V1.0.0 [https://github.com/proteo5/ZipLogCli/releases/tag/v1.0.0](https://github.com/proteo5/ZipLogCli/releases/tag/v1.0.0)

## Features

- Zip files based on file name.
- Zip files based on file pattern.
- Optional delete files to be added to the zip file.
- Add files to existing zip file.
- Delete existing zip file to override it their content.
- Specify the output file name.

### File Patterns

File patterns are a way to specify multiple files using wildcard characters. These wildcard characters allow you to match filenames based on certain patterns rather than specifying each file individually. Here are the common wildcard characters used in file patterns:

- `*` (asterisk): Matches zero or more characters in a filename. For example, `*.txt` matches all files with the `.txt` extension.
- `?` (question mark): Matches any single character in a filename. For example, `file?.txt` matches `file1.txt`, `fileA.txt`, etc.
- `[ ]` (square brackets): Matches any single character within the specified range or set. For example, `[abc].txt` matches `a.txt`, `b.txt`, or `c.txt`.

File patterns provide flexibility when dealing with a large number of files, especially when their names follow a certain pattern or format. They allow you to specify files using a concise and efficient syntax, reducing the need for manual enumeration of filenames.

## Installation

To install ZL on a Windows operating system, follow these steps:

1. Build the ZL executable (`zl.exe`) from the source code or download the lates build on the releases section.

2. Once you have the executable, copy the `zl.exe` file to a folder of your choice on your computer. For example, you might create a folder named `ZL` in your user directory (`C:\Users\YourUsername\ZL`) and place the executable there.

3. Next, add the path to the folder containing `zl.exe` to the system environment variable called "Path". This allows Windows to locate and execute the `zl.exe` command from any directory.

    - Open the Start menu, type "environment variables" and select "Edit the system environment variables".
    - In the System Properties window, click on the "Environment Variables..." button.
    - In the Environment Variables window, under "System variables", select the "Path" variable and click on the "Edit..." button.
    - Click on the "New" button and add the path to the folder containing `zl.exe` (e.g., `C:\Users\YourUsername\ZL`).
    - Click "OK" on all windows to save the changes.

4. To verify that ZL is installed correctly, open a new command prompt window and type `zl --help`. If installed properly, this should display the help message for ZL.

Now, you can use the `zl` command from any directory on your Windows system.

## Usage

```
zl file [options]

zl pattern [options]
```

### Arguments

- `files`: Name or pattern of the files to be zipped. **(Required)**

### Options

- `-d, --delete`: Delete files after being zipped.
- `-y, --skip-zip-confirmation`: Skip confirmation before zipping.
- `-z, --skip-delete-confirmation`: Skip confirmation before deleting files.
- `-s, --skip-all-confirmations`: Skip all confirmation prompts.
- `-a, --add-to-existing-zip-file`: Add files to an existing zip file; otherwise, delete the existing zip file.
- `-o, --output-file-name <String>`: Specify the output file name.
- `-h, --help`: Show help message.
- `--version`: Show version information.

## Examples

### Zip file one and delete them afterward

```bash
zl myFile.txt -d
```

The output zip file will be `myFiletxt.zip`

### Zip files using a patter to allocate them

```bash
zl myFile* 
```

The output zip file will be `myFile.zip`

### Add files to an existing zip file

```bash
zl myFile* -a -o myOldFiles.zip
```

The files will be added to the file `myOldFiles.zip`. 
if the output file doesn't exist it will be created.

### Zip files and skip confirmations

```bash
zl myFile* -y
```
Skip add files confirmation.

```bash
zl myFile* -d -z 
```
Delete files and and skip delete confirmation

```bash
zl myFile* -s
```
Skip add and delete confirmations

## Acknowledgments

* Thanks to [Mayuki](https://github.com/mayuki) for your project Cocona [https://github.com/mayuki/Cocona](https://github.com/mayuki/Cocona), doing the console app with your framework was a breeze.

## Authors

- Alfredo Pinto Molina (aka proteo5) [https://github.com/proteo5](https://github.com/proteo5)

## License
This project is licensed under the MIT License.

Copyright 2024 Alfredo Pinto Molina

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.