let countUniqueWords (text: string) : int =
  text.Split([|' '; '\n'; '\r'; '.', ','; ';'; ':'; '!'; '?'; '"'; '\''|], StringSplitOptions.RemoveEmptyEntries)
  |> Array.map (fun word -> word.Trim().ToLower())
  |> Set.ofArray
  |> Set.count