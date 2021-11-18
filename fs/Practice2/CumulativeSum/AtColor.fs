// https://atcoder.jp/contests/abc014/tasks/abc014_3

(
  (stdin.ReadLine() |> int),
  Array.create (1_000_000 + 2) 0L
)
|> fun (n, acc) ->
  [1..n]
  |> List.map (fun _ ->  stdin.ReadLine().Split() |> Array.map int |> fun x -> (x.[0], x.[1]))
  |> List.iter (fun (a, b) ->
    [
      acc.[a]     <- acc.[a]     + 1L;
      acc.[b + 1] <- acc.[b + 1] - 1L
    ]
    |> ignore
  )
  |> fun () ->
    [1..1_000_000]
    |> List.iter (fun i -> acc.[i] <- acc.[i] + acc.[i - 1])
  |> fun () ->
    acc |> Array.max
|> printfn "%d"

// https://atcoder.jp/contests/abc014/submissions/27313385
