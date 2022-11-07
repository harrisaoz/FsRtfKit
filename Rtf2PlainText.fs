﻿module FsRtfKit.Rtf2PlainText

let tryExtractPlainText (richText: string) =
    let box = new System.Windows.Forms.RichTextBox()
    try
        box.Rtf <- richText
        Some box.Text
    with
        | :? System.OutOfMemoryException ->
            reraise()
        | _ ->
            None

/// Attempt to extract the plain text content of the provided document text,
/// assuming that the input document text is in Rich Text Format.
/// If the extraction fails, then the original text is returned unchanged.
/// If the extraction succeeds, then the extracted plain text is returned.
let rtf2Text (docText: string) =
    tryExtractPlainText docText
    |> function
        | None -> docText
        | Some plainText -> plainText
