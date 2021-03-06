// https://atcoder.jp/contests/s8pc-1/tasks/s8pc_1_g

(fun () -> stdin.ReadLine () |> fun x -> x.Split ' ')
|> fun readLine ->
  readLine ()
  |> Array.map int
  |> fun arr -> (arr.[0], arr.[1])
  |> fun (n, m) ->
    (
      List.init m (fun _ ->
        readLine ()
        |> fun arr -> (int arr.[0] - 1, int arr.[1] - 1, int64 arr.[2], int64 arr.[3])
      )
      |> List.map (fun (s, t, d, time) -> if s < t then (s, t, d, time) else (t, s, d, time)),
      Array.init (1 <<< n) (fun _ -> Array.create n (System.Int64.MaxValue, 0L))
    )
    |> fun (graph, memo) ->
      (memo.[1].[0] <- (0L, 1L))
      |> fun () ->
        [
          seq {
            for vx in 1..2..(1 <<< n) - 1 do
            for (s, t, d, time) in graph do
            if vx >>> s &&& 1 = 1 && vx >>> t &&& 1 = 1
            then
              yield!
                [
                  (vx, t, vx ^^^ (1 <<< t), s, d, time);
                  (vx, s, vx ^^^ (1 <<< s), t, d, time);
                ]
          };
          seq {
            for (s, t, d, time) in graph do
            if s = 0
            then yield (0, 0, (1 <<< n) - 1, t, d, time)
          };
        ]
        |> Seq.concat
        |> Seq.iter (
          fun (i, j, i2, j2, d, time) ->
            memo.[i2].[j2]
            |> fun (preV, preC) ->
              if preV <> System.Int64.MaxValue && preV + d <= time
              then (
                (memo.[i].[j], (preV + d))
                |> fun ((accV, accC), curV) ->
                  match i with
                  | _ when curV < accV -> (curV, preC)
                  | _ when curV = accV -> (accV, accC + preC)
                  | _ -> (accV, accC)
                |> fun x -> memo.[i].[j] <- x
              )
        )
      |> fun _ -> memo.[0].[0]
|> fun (v, c) -> if c = 0L then "IMPOSSIBLE" else sprintf "%d %d" v c
|> printfn "%s"

// input:
// 3 3
// 1 2 1 6
// 2 3 2 6
// 3 1 3 6

// memo:
// [|
//   [| (6, 2); (-, 0); (-, 0) |] // 0: 000
//   [| (0, 1); (-, 0); (-, 0) |] // 1: 001
//   [| (-, 0); (-, 0); (-, 0) |] // 2: 010
//   [| (-, 0); (1, 1); (-, 0) |] // 3: 011
//   [| (-, 0); (-, 0); (-, 0) |] // 4: 100
//   [| (-, 0); (-, 0); (3, 1) |] // 5: 101
//   [| (-, 0); (-, 0); (-, 0) |] // 6: 110
//   [| (-, 0); (5, 1); (3, 1) |] // 7: 111
// |]

// output:
// 6 2

// https://atcoder.jp/contests/s8pc-1/submissions/20735572
