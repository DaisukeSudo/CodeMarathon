// https://atcoder.jp/contests/joi2017yo/tasks/joi2017yo_d

stdin.ReadLine()
|> fun x -> x.Split()
|> Array.map int
|> fun arr -> (arr.[0], arr.[1])
|> fun (n, m) ->
  (
    Array.init n (fun _ -> stdin.ReadLine() |> int |> (+) -1),
    Array.create m 0,
    Array.init m (fun _ -> Array.create (n + 1) 0),
    Array.create (1 <<< m) (0, 0)
  )
  |> fun (items, cs, vss, memo) ->
    items
    |> Array.mapi (
      fun i x ->
        (cs.[x] <- cs.[x] + 1),
        ([0..m - 1] |> List.iter (fun j -> vss.[j].[i + 1] <- cs.[j]))
    )
    |> fun _ ->
      [1..(1 <<< m) - 1]
      |> List.iter (
        fun i ->
          [0..m - 1]
          |> List.filter (fun j -> i &&& (1 <<< j) = (1 <<< j))
          |> List.map (
            fun j ->
              (memo.[i ^^^ (1 <<< j)], cs.[j], vss.[j])
              |> fun ((pc, pv), c, vs) ->
                (c - vs.[pc + c] + vs.[pc])
                |> fun v -> (pc + c, pv + v)
          )
          |> List.minBy (fun (c, v) -> v)
          |> fun x -> memo.[i] <- x
      )
    |> fun _ -> memo.[(1 <<< m) - 1] |> snd
|> printfn "%d"

// ----------

// input:
// 12 4
// 1
// 3
// 2
// 4
// 2
// 1
// 2
// 3
// 1
// 1
// 3
// 4

// cs:
// [|4; 3; 3; 2|]

// vss:
// [|
//   [|0; 1; 1; 1; 1; 1; 2; 2; 2; 3; 4; 4; 4|]
//   [|0; 0; 0; 1; 1; 2; 2; 3; 3; 3; 3; 3; 3|]
//   [|0; 0; 1; 1; 1; 1; 1; 1; 2; 2; 2; 3; 3|]
//   [|0; 0; 0; 0; 1; 1; 1; 1; 1; 1; 1; 1; 2|]
// |]

// memo:
// [|
//   (0, 0)
//   (4, 3)
//   (3, 2)
//   (7, 4)
//   (3, 2)
//   (7, 5)
//   (6, 4)
//   (10, 6)
//   (2, 2)
//   (6, 5)
//   (5, 3)
//   (9, 5)
//   (5, 3)
//   (9, 5)
//   (8, 5)
//   (12, 7)
// |]

// output:
// 7

// https://atcoder.jp/contests/joi2017yo/submissions/21127477
