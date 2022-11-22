module FsRtfKit.Spec.Rtf2PlainText

open Xunit
open FsCheck
open FsCheck.Xunit

open FsRtfKit.Rtf2PlainText
open FsRtfKit.Spec.RtfGen

[<Property>]
let ``Transformation of an invalid document yields the input document`` () =
    let invalidRtfText = "invalid"

    rtf2Text invalidRtfText = invalidRtfText

[<Property>]
let ``In general, arbitrary text is not valid RTF`` (NonEmptyString inputText) =
    Option.ofObj inputText
    |> Option.bind tryExtractPlainText
    |> Option.isNone

[<Property>]
let ``Empty text is valid RTF`` () = tryExtractPlainText "" |> Option.isSome

[<Fact>]
let ``The transformation to plain text of RTF generated from simple text should be the original simple text`` () =
    Prop.forAll arbPrintableString
    <| fun (PrintableString simpleText) ->
        makeRtf simpleText
        |> Option.bind tryExtractPlainText
        |> function
            | Some plainText -> plainText = simpleText
            | None -> false // should not happen
