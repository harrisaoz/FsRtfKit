# Purpose

Facilitate extraction of plain text from Rich Text Format documents.

# How

Rely on the Windows.Forms.RichTextBox component, which is distributed on Windows (only) as part of .net framework (up to
4.x), .net core (2 and 3) and .NET (5+), to effect the extraction of plain text.

# Limitations

This library can only be used on Windows.  To implement similar functionality on other platforms, you could look into
whether the RichTextBox component is available for Mono.

# Target Framework (Rationale)

The .net framework version 4.8 is targeted in order to facilitate use in the MsSqlServer CLR - albeit unsupported due to
all the dependencies being unsupported in the SQL Server CLR.

# Deployment to MS SQL Server CLR

In order to create an assembly in Sql Server for a derivative library, the following DLL dependencies must be loaded as
assemblies.

.Net Framework libraries:

- System.Windows.Forms
- System.Drawing
- System.Runtime.Serialization.Formatters.Soap

Libraries produced by this project:
- FSharp.Core
- FsRtfKit

Simply copy these from the Microsoft.NET references folder (SYSTEM_DRIVE\Windows\Microsoft.NET\Framework[64]\<version>)
and the bin\Release\net48 folder (FSharp.Core.dll and FsRtfKit.dll) to a single folder (referred to below as
<dll-folder>) on the database server that is accessible by the Sql Server instance, then execute the following:

```tsql
-- noinspection SqlNoDataSourceInspectionForFile
create assembly FsRtfKit from '<dll-folder>/FsRtfKit.dll' with permission_set = unsafe;
```

At this point, you can create and load an assembly that uses FsRtfKit. The following is a demonstration of the syntax,
assuming deployment of a module ```DerivativeModule.Conversion``` with a function ```asPlainText``` to be used as an
external function in schema [demo].

```tsql
-- noinspection SqlNoDataSourceInspectionForFile
create assembly DerivModAssembly from '<dll-folder>/DerivativeModule.dll' with permission_set = unsafe;

create function [demo].asPlainText(@rtfText nvarchar(max))
returns nvarchar(max)
as external name DerivModAssembly.[DerivativeModule.Conversion].asPlainText;
```
