module Filefixer
open System.IO

let rec replaceFrontMatter lines =
    match lines with
    | "(*---" :: xs -> "---" :: (replaceFrontMatter xs)
    | "---*)" :: xs -> "---" :: (replaceFrontMatter xs)
    | x :: xs -> x :: (replaceFrontMatter xs)
    | [] -> []

[<EntryPoint>]
let main argv =
    let cwd = Directory.GetCurrentDirectory()
    let postsPath = Path.Combine(cwd, argv.[0])
    let filesToFix = Directory.EnumerateFiles(postsPath, "*.html")
    filesToFix
    |> Seq.map (fun filename -> (filename, File.ReadLines(filename) |> List.ofSeq))
    |> Seq.map (fun (filename, lines) -> (filename, replaceFrontMatter lines |> Seq.ofList))
    |> Seq.iter (fun (filename, lines) -> File.WriteAllLines(filename, lines))
    printfn "%A" filesToFix
    0 // return an integer exit code
