// https://atcoder.jp/contests/s8pc-1/tasks/s8pc_1_e

let p = 1_000_000_007L

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
  fun (arr: seq<int64>) ->
    arr
    |> Seq.tail
    |> Seq.fold (fun (acc, i1) i2 ->
      ((Seq.head acc + power i1 i2) :: acc, i2)
    ) ([0L], Seq.head arr)
    |> fst
    |> List.rev
    |> fun acc -> 0L::acc
    |> List.toArray

let totalCost =
  fun (acc: int64 []) (route: seq<int>) ->
    [route; Seq.singleton 1]
    |> Seq.concat
    |> Seq.fold (
      fun (cost, i1) i2 ->
        (acc.[i1] - acc.[i2])
        |> abs
        |> (+) cost
        |> fun x -> (x % p, i2)
    ) (0L, 1)
    |> fst

(
  (stdin.ReadLine() |> ignore),
  (stdin.ReadLine().Split() |> Array.map int64) |> accCost |> totalCost,
  (stdin.ReadLine().Split() |> Array.map int)
)
|> fun (_, solve, route) -> route |> solve
|> printfn "%d"

// https://atcoder.jp/contests/s8pc-1/submissions/24986629
