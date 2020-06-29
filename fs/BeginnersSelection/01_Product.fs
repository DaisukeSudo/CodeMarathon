// https://atcoder.jp/contests/abs/tasks/abc086_a

// stdin.ReadLine()
// |> fun x -> x.Split(' ')
// |> Array.map (int >> (&&&) 1)
// |> Array.reduce (fun a b -> a &&& b)
// |> fun x -> (if x = 1 then "Odd" else "Even")
// |> printfn "%s"

// ----------

// stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map (int >> (&&&) 1) |> Array.reduce (fun a b -> a &&& b) |> fun x -> (if x = 1 then "Odd" else "Even") |> printfn "%s"

// https://atcoder.jp/contests/abs/submissions/9414632

// ----------

stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Seq.map (int >> (&&&) 1)
|> Seq.reduce (&&&)
|> fun x -> (if x = 1 then "Odd" else "Even")
|> printfn "%s"
