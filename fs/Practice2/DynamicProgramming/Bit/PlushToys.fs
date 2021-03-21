// https://atcoder.jp/contests/joi2017yo/tasks/joi2017yo_d

stdin.ReadLine()
|> fun x -> x.Split()
|> Array.map int
|> fun arr -> (arr.[0], arr.[1])
|> fun (n, m) ->
  (
    Array.init n (fun _ -> stdin.ReadLine() |> int |> (+) -1),
    Array.create (1 <<< m) (0, 0)
  )
  |> fun (items, memo) ->
    items
    |> Seq.groupBy id
    |> Seq.fold (
      fun acc (i, v) ->
        Array.concat [ acc.[0..(i - 1)]; [| v |> Seq.length |]; acc.[(i + 1)..] ]
    ) (Array.create m 0)
    |> fun counts ->
      [1..(1 <<< m) - 1]
      |> Seq.iter (
        fun i ->
          [0..m - 1]
          |> List.filter (fun j -> i &&& (1 <<< j) = (1 <<< j))
          |> List.map (
            fun j ->
              (
                memo.[i ^^^ (1 <<< j)],
                counts.[j]
              )
              |> fun ((pc, pv), c) ->
                items.[pc..pc + c - 1]
                |> Array.filter ((<>) j)
                |> Array.length
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
