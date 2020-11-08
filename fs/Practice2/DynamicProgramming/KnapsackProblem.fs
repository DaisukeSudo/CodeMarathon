// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=DPL_1_C

stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1])
|> fun (n, tw) ->
  Array.init n (fun _ -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1]))
  |> Array.append [| (0, 0) |]
  |> Array.unzip
  |> fun (vs : int [], ws : int []) ->
    Array.init (n + 1) (fun _ -> Array.create (tw + 1) 0)
    |> fun (memo: int [] []) ->
      [1..n]
      |> List.map (
        fun i ->
          [0..tw]
          |> List.map (
            fun w ->
              (if w >= ws.[i] then memo.[i].[w - ws.[i]] + vs.[i] else 0)
              |> max memo.[i - 1].[w]
              |> fun v -> memo.[i].[w] <- v
          )
      )
      |> fun _ -> memo.[n].[tw]
|> printfn "%d"


// input:
// 4 8
// 4 2
// 5 2
// 2 1
// 8 3

// memo:
// [|
//   [|0; 0; 0; 0; 0; 0; 0; 0; 0|];
//   [|0; 0; 4; 4; 8; 8; 12; 12; 16|];
//   [|0; 0; 5; 5; 10; 10; 15; 15; 20|];
//   [|0; 2; 5; 7; 10; 12; 15; 17; 20|];
//   [|0; 2; 5; 8; 10; 13; 16; 18; 21|]
// |]

// output:
// 21


// immutable version (TLE)

stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1])
|> fun (n, tw) ->
  Array.init n (fun _ -> stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1]))
  |> Array.unzip
  |> fun (vs : int [], ws : int []) ->
    [0..(n - 1)]
    |> List.fold (
      fun (memo1 : int []) i ->
        [| 0..tw |]
        |> Array.fold (
          fun (memo2 : int []) w ->
            (if w >= ws.[i] then memo2.[w - ws.[i]] + vs.[i] else 0)
            |> max memo1.[w]
            |> Array.create 1
            |> Array.append memo2
        ) [||]
    ) (Array.create (tw + 1) 0)
    |> Array.last
|> printfn "%d"
