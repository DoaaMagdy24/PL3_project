module Output

open System
open TextAnalysis

let analyzeText (text: string) =
    match text with
    | null | "" -> "Please enter or load some text for analysis."
    | _ ->
        let wordCount = countWords text
        let sentenceCount = countSentences text
        let paragraphCount = countParagraphs text
        let characterCount = countCharacters text
        let vowelCount = countVowels text
        let wordFrequencies = calculateWordFrequency text |> Seq.take 5 
        let readability = measureTextReadability text
        let words = text.Split([| ' '; '\t'; '\n'; '\r' |], StringSplitOptions.RemoveEmptyEntries) |> List.ofArray
        let shortestWords = findShortestWords words |> String.concat ", " 
        let longestWords = findLongestWords words |> String.concat ", " 
        let unquieWords = countUniqueWords text
        let topWords = 
            wordFrequencies
            |> Seq.map (fun (word, freq) -> sprintf "        %s : %d" word freq)
            |> String.concat "\n"
        sprintf """
        ============================
            Text Analysis Report
        ============================

         Word Count       : %d
         Sentence Count   : %d
         Paragraph Count  : %d
         Character Count  : %d
         Vowel Count      : %d
         Top Words        : %s
         Avg Sentence Length : %.2f
         Shortest Words   : %s
         Longest Words    : %s
         Unique Words     : %d

        ============================
        """ wordCount sentenceCount paragraphCount characterCount vowelCount topWords readability shortestWords longestWords unquieWords

