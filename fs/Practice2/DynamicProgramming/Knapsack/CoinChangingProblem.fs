// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=DPL_1_A

(
  (stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1])),
  (stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> Array.append [| 0 |])
)
|> fun ((n, m), cs) ->
  Array.init (m + 1) (fun _ -> Array.create (n + 1) 50_000)
  |> fun (memo: int [] []) ->
    (memo.[0].[0] <- 0)
    |> fun () ->
      [1..m]
      |> List.map (
        fun i ->
          [0..n]
          |> List.map (
            fun t ->
              match t with
              | 0 -> 0
              | _ when t >= cs.[i] ->
                memo.[i].[t - cs.[i]] + 1 |> min memo.[i - 1].[t]
              | _ -> memo.[i - 1].[t]
              |> fun v -> memo.[i].[t] <- v
          )
      )
      |> fun _ -> memo.[m].[n]
|> printfn "%d"


// input:
// 9 3
// 1 2 3

// memo:
// [|
//   [|0; N/A; N/A; N/A; N/A; N/A; N/A; N/A; N/A; N/A|];
//   [|0; 1; 2; 3; 4; 5; 6; 7; 8; 9|];
//   [|0; 1; 1; 2; 2; 3; 3; 4; 4; 5|];
//   [|0; 1; 1; 1; 2; 2; 2; 3; 3; 3|]
// |]

// output:
// 3


// input:
// 15 6
// 1 2 7 8 12 50

// memo:
// [|
//   [|0; N/A; N/A; N/A; N/A; N/A; N/A; N/A; N/A; N/A; N/A; N/A; N/A; N/A; N/A; N/A|];
//   [|0; 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12; 13; 14; 15|];
//   [|0; 1; 1; 2; 2; 3; 3; 4; 4; 5; 5; 6; 6; 7; 7; 8|];
//   [|0; 1; 1; 2; 2; 3; 3; 1; 2; 2; 3; 3; 4; 4; 2; 3|];
//   [|0; 1; 1; 2; 2; 3; 3; 1; 1; 2; 2; 3; 3; 4; 2; 2|];
//   [|0; 1; 1; 2; 2; 3; 3; 1; 1; 2; 2; 3; 1; 2; 2; 2|];
//   [|0; 1; 1; 2; 2; 3; 3; 1; 1; 2; 2; 3; 1; 2; 2; 2|]
// |]

// output:
// 2


// immutable version (TLE)

(
  (stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int |> fun x -> (x.[0], x.[1])),
  (stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int)
)
|> fun ((n, m), cs) ->
  [0..(m - 1)]
  |> List.fold (
    fun (memo1 : int []) i ->
      [| 0..n |]
      |> Array.fold (
        fun (memo2 : int []) t ->
          match t with
          | 0 -> 0
          | _ when t >= cs.[i] -> memo2.[t - cs.[i]] + 1 |> min memo1.[t]
          | _ -> memo1.[t]
          |> Array.create 1
          |> Array.append memo2
      ) [||]
  ) (Array.create n 50_000 |> Array.append [| 0 |])
  |> Array.last
|> printfn "%d"
