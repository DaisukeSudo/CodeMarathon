// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=DPL_1_B

stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1])
|> fun (n, tw) ->
  Array.init n (fun _ -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1]))
  |> Array.append [| (0, 0) |]
  |> Array.unzip
  |> fun (vs : int [], ws : int [])  ->
    Array.init (n + 1) (fun _ -> Array.create (tw + 1) 0)
    |> fun (memo: int [] []) ->
      [1..n]
      |> List.map (
        fun i ->
          [0..tw]
          |> List.map (
            fun w ->
              (if w >= ws.[i] then memo.[i - 1].[w - ws.[i]] + vs.[i] else 0)
              |> max memo.[i - 1].[w]
              |> fun v -> memo.[i].[w] <- v
          )
      )
      |> fun _ -> memo.[n].[tw]
|> printfn "%d"


// input:
// 4 5
// 4 2
// 5 2
// 2 1
// 8 3

// memo:
// [|
//   [|0; 0; 0; 0; 0; 0|];
//   [|0; 0; 4; 4; 4; 4|];
//   [|0; 0; 5; 5; 9; 9|];
//   [|0; 2; 5; 7; 9; 11|];
//   [|0; 2; 5; 8; 10; 13|];
// |]

// output:
// 13
