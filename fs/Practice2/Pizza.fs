// https://atcoder.jp/contests/joi2009ho/tasks/joi2009ho_b

// d: 2 <= d <= 10 ** 9 | 環状線の長さ
// n: 2 <= n <= 10 ** 5 | 店舗の数
// m: 1 <= m <= 10 ** 4 | 注文の数

// System.Math.Log(150000., 2.) ≒ 18
// (8, 3, 2, [3; 1], [4; 6])

// d: 0 1 2 3 4 5 6 7 0 1 2 3 4 5 6 7
// d: 0 1 2 3 4 5 6 7 8 9 A B C D E F
// n: x x . x . . . . x x . x . . . .
// m:         o   o  

(
  stdin.ReadLine() |> int,
  stdin.ReadLine() |> int,
  stdin.ReadLine() |> int
)
|> fun (d, n, m) ->
  (
    0::([1..(n - 1)] |> List.map (fun _ -> stdin.ReadLine() |> int)),
        [1..m]       |> List.map (fun _ -> stdin.ReadLine() |> int)
  )
  |> fun (ns, ms) ->
    ns @ (ns |> List.filter (fun x -> x <= d / 2) |> List.map (fun x -> x + d))
    |> List.sort
    |> List.toArray
    |> fun ns2 ->
      (System.Math.Log((ns2 |> Array.length |> double), 2.) |> ceil |> int)
      |> fun limit ->
        ms
        |> List.sumBy (
          fun x ->
            [1..limit]
            |> List.fold (
              fun (li, ri) _ ->
                if li = ri
                then (li, ri)
                else (
                  li + (ri - li) / 2
                  |> fun hi ->
                    (
                      ns2.[hi],
                      ns2.[hi + 1]
                    )
                    |> fun (lv, gv) -> 
                      match x with
                      | _ when x = lv               -> (hi    , hi    )
                      | _ when x = gv               -> (hi + 1, hi + 1)
                      | _ when x < lv               -> (li    , hi    )
                      | _ when x > gv               -> (hi + 1, ri    )
                      | _ when (x - lv) < (gv - x)  -> (hi    , hi    )
                      | _                           -> (hi + 1, hi + 1)
                )
            ) (0, (ns2 |> Array.length) - 1)
            |> fun (i, _) -> ns2.[i]
            |> (-) x
            |> abs
        )
|> printfn "%d"

// https://atcoder.jp/contests/joi2009ho/submissions/15106856
