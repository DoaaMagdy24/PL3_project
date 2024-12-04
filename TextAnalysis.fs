module TextAnalysis

open System
open System.Text.RegularExpressions

let countWords (text: string) =
    let words = text.Split([| ' '; '\t'; '\n'; '\r' |], StringSplitOptions.RemoveEmptyEntries)
    words.Length

let countSentences (text: string) =
    let sentences = text.Split([| '.'; '?'; '!' |], StringSplitOptions.RemoveEmptyEntries)
    sentences.Length

let countParagraphs (text: string) =
    let paragraphs = text.Split([| "\n"; "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    paragraphs.Length

let countCharacters (text: string) =
    text.Length

let countVowels (text: string) =
    let vowels = "aeiou"
    text.ToLower()
    |> Seq.filter (fun c -> vowels.Contains(c))
    |> Seq.length

let calculateWordFrequency (text: string) =
    let cleanText = text.ToLower().Replace(",", "").Replace(".", "").Replace("?", "").Replace("!", "")
    let words = cleanText.Split([| ' '; '\t'; '\n'; '\r' |], StringSplitOptions.RemoveEmptyEntries)
    words
    |> Seq.groupBy id
    |> Seq.map (fun (word, occurrences) -> word, Seq.length occurrences)
    |> Seq.sortByDescending snd 

let measureTextReadability (text: string) =
    let sentences = 
        Regex.Split(text, @"[.!?]")
        |> Array.map (fun s -> s.Trim())
        |> Array.filter (fun s -> s <> "")
    
    let words = text.Split([|' '; '\t'; '\n'; '\r'|], StringSplitOptions.RemoveEmptyEntries)
    
    let avgSentenceLength = 
        if sentences.Length > 0 then 
            float words.Length / float sentences.Length 
        else 
            0
    avgSentenceLength


let analyzeText (text: string) =
    if String.IsNullOrWhiteSpace(text) then
        "Please enter or load some text for analysis."
    else
        let wordCount = countWords text
        let sentenceCount = countSentences text
        let paragraphCount = countParagraphs text
        let characterCount = countCharacters text
        let vowelCount = countVowels text
        let wordFrequencies = calculateWordFrequency text |> Seq.take 5 // أخذ أكثر 5 كلمات تكرارًا
        let readability = measureTextReadability text
        let topWords = 
            wordFrequencies
            |> Seq.map (fun (word, freq) -> sprintf "        %s : %d" word freq)
            |> String.concat "\n"
        sprintf """
        ============================
            Text Analysis Report
        ============================

        📄 Word Count       : %d
        ✒️  Sentence Count   : %d
        📑 Paragraph Count  : %d
        🔡 Character Count  : %d
        🅰️  Vowel Count     : %d
        🏆 Top Words        : %s
        📖 Avg Sentence Length : %.2f

        ============================
        """ wordCount sentenceCount paragraphCount characterCount vowelCount topWords readability

