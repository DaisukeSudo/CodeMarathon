// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=ALDS1_10_B

stdin.ReadLine ()
|> int
|> fun n ->
  (
    Array.init n (fun _ -> stdin.ReadLine () |> fun x -> x.Split ' ' |> Array.map int |> fun x -> (x.[0], x.[1])) |> Array.unzip,
    Array.init n (fun _ -> Array.zeroCreate n)
  )
  |> fun ((mss : int [], mes : int []), memo : int [] []) ->
    [1..(n - 1)]
    |> List.map (
      fun i ->
        [for j in 0..(n - i - 1) -> (j, j + i)]
        |> List.map (
          fun (s, e) ->
            [for k in s..(e - 1) -> ((s, k), (k + 1, e))]
            |> List.map (
              fun ((s1, e1), (s2, e2)) ->
                mss.[s1] * mes.[e1] * mes.[e2] + memo.[s1].[e1] + memo.[s2].[e2]
            )
            |> List.min
            |> fun v -> memo.[s].[e] <- v
        )
    )
    |> fun _ -> memo.[0] |> Array.last
|> printfn "%A"


// ----------

// i | [(j, j + i)]
// --
// 1 | [(0, 1); (1, 2); (2, 3); (3, 4); (4, 5)]
// 2 | [(0, 2); (1, 3); (2, 4); (3, 5)]
// 3 | [(0, 3); (1, 4); (2, 5)]
// 4 | [(0, 4); (1, 5)]
// 5 | [(0, 5)]

// (s, e)
// (0, 5)
// --
// (0, 0) * (1, 5)
// (0, 1) * (2, 5)
// (0, 2) * (3. 5)
// (0, 3) * (4, 5)
// (0, 4) * (5, 5)

// ----------

// input:
// 6
// 30 35
// 35 15
// 15 5
// 5 10
// 10 20
// 20 25

// memo:
// [|
//   [|0; 15750; 7875; 9375; 11875; 15125|]
//   [|0;     0; 2625; 4375;  7125; 10500|]
//   [|0;     0;    0;  750;  2500;  5375|]
//   [|0;     0;    0;    0;  1000;  3500|]
//   [|0;     0;    0;    0;     0;  5000|]
//   [|0;     0;    0;    0;     0;     0|]
// |]

// output:
// 15125
