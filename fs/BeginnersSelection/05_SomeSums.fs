// https://atcoder.jp/contests/abs/tasks/abc083_b

// stdin.ReadLine()
// |> fun x -> x.Split(' ')
// |> Array.map int
// |> fun x -> (x.[0], x.[1], x.[2])
// |> fun (n, a, b) -> 
//   [1..n]
//   |> List.filter (fun ni ->
//     ni.ToString()
//     |> fun x -> x.ToCharArray(0, x.Length)
//     |> Array.map (fun x -> x.ToString() |> int)
//     |> Array.sum
//     |> fun x -> x >= a && x <= b
//   )
//   |> List.sum
//   |> printfn "%d"

// ----------

// stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1], x.[2]) |> fun (n, a, b) ->  [1..n] |> List.filter (fun ni -> ni.ToString() |> fun x -> x.ToCharArray(0, x.Length) |> Array.map (fun x -> x.ToString() |> int) |> Array.sum |> fun x -> x >= a && x <= b ) |> List.sum |> printfn "%d"

// https://atcoder.jp/contests/abs/submissions/9414764

// ----------

// f [1; 2; 3]

// let f = fun (a :: b :: [c]) -> printfn "%d, %d, %d" a b c
// // warning FS0025: Incomplete pattern matches on this expression. For example, the value '[_;_;_;_]' may indicate a case not covered by the pattern(s).

// let f =
//   function
//   | a :: b :: [c] -> printfn "%d, %d, %d" a b c
//   | _ -> ()

// ----------

stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map int
|> fun x -> (x.[0], x.[1], x.[2])
|> fun (n, a, b) -> 
  [1..n]
  |> List.filter (
    fun ni ->
      ni.ToString()
      |> fun x -> x.ToCharArray(0, x.Length)
      |> Array.sumBy (fun x -> x.ToString() |> int)
      |> fun x -> x >= a && x <= b
  )
  |> List.sum
  |> printfn "%d"
