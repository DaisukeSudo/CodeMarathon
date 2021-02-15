// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=1611

let stdin = new System.IO.StreamReader("Darumaotoshi.input")

Seq.initInfinite (fun _ -> stdin.ReadLine ())
|> Seq.takeWhile ((<>) "0")
|> Seq.fold (
  fun (i, ns, wss) s ->
    if i % 2 = 0
    then (s            |> int           |> fun n ->  (i + 1, n :: ns, wss))
    else (s.Split(' ') |> Array.map int |> fun ws -> (i + 1, ns, ws :: wss))
) (0, [], [])
|> fun (_, ns, wss) -> (List.zip ns wss)
|> List.rev
|> List.iter (fun (n, ws) ->
  (Array.init n (fun _ -> Array.create n 0))
  |> fun memo ->
    seq {
      for i in 1..n do
      for j in 0..(n - i - 1) do
      yield (i, j, j + i)
    }
    |> Seq.iter (fun (i, s, e) ->
      if (ws.[s] - ws.[e] |> abs |> ((>=) 1))
        && (memo.[s + 1].[e - 1] = i - 1)
      then
        memo.[s].[e] <- i + 1
      else
        seq { for k in s..(e - 1) -> ((s, k), (k + 1, e)) }
        |> Seq.iter (fun ((s1, e1), (s2, e2)) ->
          (memo.[s1].[e1] + memo.[s2].[e2])
          |> max memo.[s].[e]
          |> fun x -> memo.[s].[e] <- x
        )
    )
  |> fun _ -> memo.[0].[n - 1]
  |> printfn "%d"
)
