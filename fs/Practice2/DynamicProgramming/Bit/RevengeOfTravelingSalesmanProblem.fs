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
                  (vx, t, d, time, memo.[vx ^^^ (1 <<< t)].[s]);
                  (vx, s, d, time, memo.[vx ^^^ (1 <<< s)].[t]);
                ]
          };
          seq {
            for vx in [(1 <<< n) - 1] do
            for (s, t, d, time) in graph do
            if s = 0
            then yield (0, 0, d, time, memo.[vx].[t])
          };
        ]
        |> Seq.concat
        |> Seq.filter (
          fun (vx, x, d, time, (prevCost, prevCount)) ->
            prevCost <> System.Int64.MaxValue && prevCost + d <= time
        )
        |> Seq.iter (
          fun (vx, x, d, time, (prevCost, prevCount)) ->
            (memo.[vx].[x], (prevCost + d))
            |> fun ((accCost, accCount), curCost) ->
              match vx with
              | _ when curCost < accCost -> (curCost, prevCount)
              | _ when curCost = accCost -> (accCost, accCount + prevCount)
              | _ -> (accCost, accCount)
            |> fun v -> memo.[vx].[x] <- v
        )
      |> fun _ -> memo.[0].[0]
|> fun (cost, count) -> if count = 0L then "IMPOSSIBLE" else sprintf "%d %d" cost count
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

// https://atcoder.jp/contests/s8pc-1/submissions/20482836
