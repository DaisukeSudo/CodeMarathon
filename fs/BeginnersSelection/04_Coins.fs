// https://atcoder.jp/contests/abs/tasks/abc087_b

// (stdin.ReadLine(), stdin.ReadLine(), stdin.ReadLine(), stdin.ReadLine())
// |> fun (a, b, c, x) -> (a |> int, b|> int, c|> int, x|> int)
// |> fun (a, b, c, x) -> 
//   [0..(min a (x / 500))]
//   |> List.map (fun i ->
//     [0..(min b ((x - 500 * i) / 100))]
//     |> List.filter (fun j -> x - 500 * i - 100 * j <= 50 * c)
//     |> List.length
//   )
//   |> List.sum
//   |> printf "%d"

// ----------

// (stdin.ReadLine(), stdin.ReadLine(), stdin.ReadLine(), stdin.ReadLine()) |> fun (a, b, c, x) -> (a |> int, b|> int, c|> int, x|> int) |> fun (a, b, c, x) ->  [0..(min a (x / 500))] |> List.map (fun i -> [0..(min b ((x - 500 * i) / 100))] |> List.filter (fun j -> x - 500 * i - 100 * j <= 50 * c) |> List.length) |> List.sum |> printf "%d"

// https://atcoder.jp/contests/abs/submissions/9416476

// ----------

(stdin.ReadLine(), stdin.ReadLine(), stdin.ReadLine(), stdin.ReadLine())
|> fun (a, b, c, x) -> (a |> int, b|> int, c|> int, x|> int)
|> fun (a, b, c, x) -> 
  [0..(min a (x / 500))]
  |> List.sumBy (fun i ->
    [0..(min b ((x - 500 * i) / 100))]
    |> List.filter (fun j -> x - 500 * i - 100 * j <= 50 * c)
    |> List.length
  )
  |> printf "%d"
