// https://atcoder.jp/contests/sumitrust2019/tasks/sumitb2019_e

stdin.ReadLine()
|> fun _ -> stdin.ReadLine().Split() |> Array.map int
|> Array.fold (fun (r, abc) x ->
  abc
  |> Seq.filter ((=) x)
  |> Seq.length
  |> int64
  |> fun c -> 
    abc
    |> Seq.tryFindIndex ((=) x)
    |> fun i ->
      match i with
      | Some i -> (r * c % 1_000_000_007L, Array.concat [ abc.[0..i - 1]; [| abc.[i] + 1 |]; abc.[i + 1..] ])
      | None -> (0L, [||])
) (1L, Array.create 3 0)
|> fst
|> printfn "%d"

// https://atcoder.jp/contests/sumitrust2019/submissions/30219032
