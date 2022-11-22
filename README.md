# Purpose

Facilitate extraction of plain text from Rich Text Format documents.

# Implementation Notes

Rely on the Windows.Forms.RichTextBox component, which is distributed on Windows (only) as part of .net framework (up to
4.x), .net core (2 and 3) and .NET (5+), to effect the extraction of plain text.

# Limitations

This library can only be used on Windows. To implement similar functionality on other platforms, you could adapt this to
work with the RichTextBox from Mono.

# Usage

```f# script
#r "nuget: FsRtfKit"
open FsRtfKit.Rtf2PlainText

let onNone () = ...
let onPlainText (plainText: string) = ...
let ``Function that's agnostic to whether the input is plain text or RTF`` (anyText: string) = ...

let inputRtf = System.IO.File.ReadAllText "input.rtf"

tryExplainPlainText inputRtf |> function
    | None -> onNone ()
    | Some plainText -> onPlainText plainText

rtf2Text inputRtf
|> ``Function that's agnostic to whether the input is plain text or RTF`` inputRtf
```
# Run Tests

```powershell
dotnet test
```

# Build (for Release)

```powershell
dotnet build -c Release
```

# Package

```powershell
dotnet pack -c Release
```

# Deploy

```powershell
dotnet nuget push FsRtfKit\bin\Release\FsRtfKit.version.nupkg
```
