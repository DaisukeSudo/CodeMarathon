// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=ALDS1_4_B&lang=ja

// 10 ** 9               = 1_000_000_000
// System.Int32.MaxValue = 2_147_483_647

// log2        100_000 = 16.60964047 ≒ 17
// log2  1_000_000_000 = 29.89735285 ≒ 30

(
  stdin.ReadLine() |> ignore,
  stdin.ReadLine().Split(' ') |> Seq.map int |> Seq.toList,
  stdin.ReadLine() |> ignore,
  stdin.ReadLine().Split(' ') |> Seq.map int |> Seq.toList
)
|> fun (_, ss, _, ts) ->
  (System.Math.Log((ss |> List.length |> double), 2.) |> ceil |> int)
  |> fun limit ->
    ts
    |> List.filter (
      fun x ->
        [1..limit]
        |> List.fold (
          fun (list, status) _ ->
            if status
            then ([], status)
            else (
              ((list |> List.length) / 2)
              |> fun half ->
                list.[half]
                |> fun v ->
                  match x with
                  | _ when x = v -> ([], true)
                  | _ when x < v -> (list.[..(half - 1)], status)
                  | _            -> (list.[(half + 1)..], status)
            )
        ) (ss, false)
        |> fun (_, status) -> status
    )
  |> List.length
|> printfn "%d"

// ----

// [1..1000] |> List.map string |> String.concat " " |> printfn "%s"
