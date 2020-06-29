// https://atcoder.jp/contests/abs/tasks/abc085_b

stdin.ReadLine()
|> int
|> fun x -> [1..x]
|> List.map (fun _ -> stdin.ReadLine())
|> List.distinct
|> List.length
|> printfn "%d"

// ----------

stdin.ReadLine() |> int |> fun x -> [1..x] |> List.map (fun _ -> stdin.ReadLine()) |> List.distinct |> List.length |> printfn "%d" 

// https://atcoder.jp/contests/abs/submissions/9414793
