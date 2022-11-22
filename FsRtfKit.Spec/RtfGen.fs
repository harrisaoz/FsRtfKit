module FsRtfKit.Spec.RtfGen

open System.Globalization
open FsCheck

let makeRtf text =
    let box =
        new System.Windows.Forms.RichTextBox()

    try
        box.Text <- text
        Some box.Rtf
    with
    | _ -> None

type PrintableString = PrintableString of string

let arbPrintableString =
    let onlyPrintable (NonEmptyString s) =
        String.forall (not << System.Char.IsControl) s
    Arb.Default.NonEmptyString().Generator
    |> Gen.filter onlyPrintable
    |> Gen.map (fun (NonEmptyString s) -> PrintableString s)
    |> Arb.fromGen
