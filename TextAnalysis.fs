module TextAnalysis

open System
open System.Text.RegularExpressions

let countWords (text: string) =
    let words = 
        text.Split([| ' '; '\t'; '\n'; '\r' |], StringSplitOptions.RemoveEmptyEntries)
    words.Length

let countSentences (text: string) =
    let pattern = @"(?<=[.!?])\s+"  // This matches sentence-ending punctuation followed by whitespace.
    let sentences = 
        Regex.Split(text, pattern)
        |> List.ofArray
        |> List.filter (fun s -> not (String.IsNullOrWhiteSpace(s))) // Remove empty sentences
    sentences.Length

let countParagraphs (text: string) =
    let paragraphs = 
        text.Split([| "\n"; "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
        |> List.ofArray
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
    let words = 
        cleanText.Split([| ' '; '\t'; '\n'; '\r' |], StringSplitOptions.RemoveEmptyEntries)
        |> List.ofArray
    words
    |> Seq.groupBy id
    |> Seq.map (fun (word, occurrences) -> word, Seq.length occurrences)
    |> Seq.sortByDescending snd 

let measureTextReadability (text: string) =
    let sentences = 
        Regex.Split(text, @"[.!?]")
        |> Seq.map (fun s -> s.Trim())
        |> Seq.filter (fun s -> s <> "")
    
    let words = 
        text.Split([|' '; '\t'; '\n'; '\r'|], StringSplitOptions.RemoveEmptyEntries)
        |> Seq.ofArray
    
    let avgSentenceLength = 
        if Seq.isEmpty sentences then 0.0
        else float (Seq.length words) / float (Seq.length sentences)
    avgSentenceLength
    
let findShortestWords words =
    match words with
    | [] -> [] 
    | _ ->
        let uniqueWords = words |> List.distinct 
        let minLength = uniqueWords |> List.minBy String.length |> String.length
        uniqueWords
        |> List.filter (fun word -> String.length word = minLength)
        |> List.truncate 3

let findLongestWords words =
    match words with
    | [] -> [] 
    | _ ->
        let uniqueWords = words |> List.distinct 
        let maxLength = uniqueWords |> List.maxBy (fun word -> String.length word) |> String.length
        uniqueWords
        |> List.filter (fun word -> String.length word = maxLength)
        |> List.truncate 3 

let countUniqueWords (text: string) : int =
    let pattern = @"\w+" 
    Regex.Matches(text, pattern)
    |> Seq.map (fun m -> m.Value.ToLower()) 
    |> Set.ofSeq 
    |> Set.count 