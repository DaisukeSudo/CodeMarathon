// https://atcoder.jp/contests/abc128/tasks/abc128_c

stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map float
|> fun x -> (x.[0], x.[1])
|> fun (n, m) ->
  (
    [0.0..(2. ** n - 1.)]
    |> List.map int,
    [1.0..m]
    |> List.map (fun _ -> stdin.ReadLine() |> fun x -> x.Split(' ').[1..] |> Array.map (int >> (+) -1))
    |> List.mapi (fun i x -> i, x),
    stdin.ReadLine() |> fun x -> x.Split(' ') |> Array.map int
  )
|> fun (ns, ms, ps) ->
  ns
  |> Seq.filter (
    fun switches ->
      ms
      |> Seq.forall (
        fun (i, si) -> (si |> Seq.sumBy (fun sii -> switches >>> sii &&& 1)) % 2 = ps.[i]
      )
  )
|> Seq.length
|> printfn "%d"

// https://atcoder.jp/contests/abc128/submissions/13015013
