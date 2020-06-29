// https://atcoder.jp/contests/abs/tasks/abc081_a

// stdin.ReadLine()
// |> fun x -> x.ToCharArray(0, x.Length)
// |> Array.map (fun x -> x.ToString() |> int)
// |> Array.reduce (fun a b -> a + b)
// |> printfn "%d"

// ----------

// stdin.ReadLine() |> fun x -> x.ToCharArray(0, x.Length) |> Array.map (fun x -> x.ToString() |> int) |> Array.reduce (fun a b -> a + b) |> printfn "%d"

// https://atcoder.jp/contests/abs/submissions/9414694

// ----------

stdin.ReadLine()
|> Seq.sumBy (string >> int)
|> printfn "%d"
