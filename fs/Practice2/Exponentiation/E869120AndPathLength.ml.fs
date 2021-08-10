// https://atcoder.jp/contests/s8pc-1/tasks/s8pc_1_e

let p = 1_000_000_007L

let readInput =
  fun () ->
    (
      (stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]),
      (stdin.ReadLine().Split() |> Array.map int64),
      (stdin.ReadLine().Split() |> Array.map int)
    )

let power =
  ([| fun _ _ -> 0L |])
  |> fun (fnc: (int64 -> int64 -> int64) []) ->
    (
      fun (m: int64) (n: int64) ->
        match n with
        | _ when n = 0L -> 1L
        | _ when n % 2L = 0L -> fnc.[0] m (n / 2L) |> (fun x -> x * x % p)
        | _ -> m * fnc.[0] m (n - 1L) % p
    )
    |> fun fn -> (fnc.[0] <- fn) |> fun () -> fn

let accCost =
  fun (arr: int64 []) ->
    Array.zeroCreate (arr.Length)
    |> fun ret ->
      [ 1..arr.Length - 1 ]
      |> List.iter (
        fun i ->
          power arr.[i - 1] arr.[i]
          |> fun x -> ret.[i] <- ret.[i - 1] + x
      )
      |> fun () -> [[| 0L |]; ret] |> Array.concat

let totalCost =
  fun (acc: int64 [], route: int []) ->
    [route; [| 1 |]]
    |> Array.concat
    |> Array.fold (
      fun (cost, i1) i2 ->
        (acc.[i1] - acc.[i2])
        |> abs
        |> (+) cost
        |> fun x -> (x % p, i2)
    ) (0L, 1)
    |> fst

()
|> readInput
|> fun (_, a1, a2) -> (a1 |> accCost, a2)
|> totalCost
|> printfn "%d"

// https://atcoder.jp/contests/s8pc-1/submissions/24935541
