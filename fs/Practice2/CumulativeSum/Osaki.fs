// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=2013

Seq.initInfinite (fun _ -> stdin.ReadLine())
|> Seq.takeWhile ((<>) "0")
|> Seq.fold (fun (n, i, lines) s ->
  if n = i
  then s |> int |> fun n -> (n, 0, (Array.create n "") :: lines)
  else (lines.[0].[i] <- s) |> fun () -> (n, i + 1, lines)
) (0, 0, [])
|> fun (_, _, x) -> x
|> List.rev
|> List.map (fun times ->
  Array.create (86_400 + 1) 0L |> fun acc ->
    times
    |> Array.iter (fun t ->
        t.Split()
        |> Array.map (fun s -> s.Split(':') |> Array.map int |> fun x ->
          x.[0] * 60 * 60 + x.[1] * 60 + x.[2]
        )
        |> fun x -> (x.[0], x.[1])
        |> fun (a, b) ->
          [
            acc.[a] <- acc.[a] + 1L;
            acc.[b] <- acc.[b] - 1L
          ]
          |> ignore
    )
    |> fun () -> [1..86_400] |> List.iter (fun i -> acc.[i] <- acc.[i] + acc.[i - 1])
    |> fun () -> acc
  |> Array.max
  |> printfn "%d"
)
