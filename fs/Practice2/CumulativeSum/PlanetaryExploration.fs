// https://atcoder.jp/contests/joi2011ho/tasks/joi2011ho1

(
  (stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1])),
  (stdin.ReadLine() |> int)
)
|> fun ((m, n), k) ->
  (
    ([1..m] |> List.map (fun _ -> stdin.ReadLine() |> Seq.toArray)),
    Array.init (m + 1) (fun _ -> Array.create (n + 1) 0L)
  )
  |> fun (jois, acc) ->
    [0..m - 1] |> List.iter (fun i -> [0..n - 1] |> List.iter (fun j ->
      (
        match jois.[i].[j] with
        | 'J' -> 10_000_000L
        | 'O' -> 1L
        | _ -> 0L
      )
      |> fun x -> acc.[i + 1].[j + 1] <- x + acc.[i].[j + 1] + acc.[i + 1].[j] - acc.[i].[j]
    ))
    |> fun () ->
      [1..k] |> List.iter (fun _ ->
        stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0] - 1, x.[1] - 1, x.[2], x.[3])
        |> fun (a, b, c, d) ->
          (acc.[c].[d] + acc.[a].[b] - acc.[c].[b] - acc.[a].[d])
          |> fun x ->
            (
              (c - a) * (d - b),
              x / 10_000_000L |> int,
              x % 10_000_000L |> int
            )
        |> fun (total, j, o) -> printfn "%d %d %d" j o (total - j - o)
      )

// https://atcoder.jp/contests/joi2011ho/submissions/27007424
// https://atcoder.jp/contests/joi2011ho/submissions/27007540
