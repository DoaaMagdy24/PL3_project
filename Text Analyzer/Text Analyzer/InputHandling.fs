module InputHandling

open System
open System.IO

// Function to load text from a file
let loadTextFromFile (filePath: string) =
    try
        File.ReadAllText(filePath)
    with
        | :? FileNotFoundException -> "File not found."
        | ex -> sprintf "An error occurred: %s" ex.Message