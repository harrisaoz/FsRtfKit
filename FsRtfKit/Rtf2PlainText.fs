module FsRtfKit.Rtf2PlainText

/// Attempt to obtain a plain-text transformation of input text assumed to be
/// in Rich Text Format.
/// If a valid transformation is not obtained, then None is returned.
/// If a valid transformation to plain text is obtained, then that
/// 'Some ``plain text``' transformation is returned.
let tryExtractPlainText (richText: string) =
    let box =
        new System.Windows.Forms.RichTextBox()

    try
        box.Rtf <- richText
        Some box.Text
    with
    | :? System.OutOfMemoryException -> reraise ()
    | _ -> None

/// Attempt to obtain a plain-text transformation of input text assumed to be
/// in Rich Text Format.
/// If no valid transformation is obtained, then the original text is returned unchanged.
/// If a valid transformation is obtained, then that transformation is returned.
let rtf2Text (docText: string) =
    tryExtractPlainText docText
    |> function
        | None -> docText
        | Some plainText -> plainText
