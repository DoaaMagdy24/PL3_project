module TextAnalysis

open System

let countWords (text: string) =
    let words = text.Split([| ' '; '\t'; '\n'; '\r' |], StringSplitOptions.RemoveEmptyEntries)
    words.Length

let countSentences (text: string) =
    let sentences = text.Split([| '.'; '?'; '!' |], StringSplitOptions.RemoveEmptyEntries)
    sentences.Length

let countParagraphs (text: string) =
    let paragraphs = text.Split([| '\n' |], StringSplitOptions.RemoveEmptyEntries)
    paragraphs.Length

let countCharacters (text: string) =
    text.Length

let countVowels (text: string) =
    let vowels = "aeiou"
    text.ToLower()
    |> Seq.filter (fun c -> vowels.Contains(c))
    |> Seq.length

let analyzeText (text: string) =
    if String.IsNullOrWhiteSpace(text) then
        "Please enter or load some text for analysis."
    else
        let wordCount = countWords text
        let sentenceCount = countSentences text
        let paragraphCount = countParagraphs text
        let characterCount = countCharacters text
        let vowelCount = countVowels text
        sprintf """
        ============================
            Text Analysis Report
        ============================

        📄 Word Count       : %d
        ✒️  Sentence Count   : %d
        📑 Paragraph Count  : %d
        🔡 Character Count  : %d
        🅰️  Vowel Count     : %d

        ============================
        """ wordCount sentenceCount paragraphCount characterCount vowelCount