// https://atcoder.jp/contests/typical90/tasks/typical90_a

(
  (stdin.ReadLine().Split() |> fun x -> int x.[1]),
  (stdin.ReadLine() |> int),
  (stdin.ReadLine().Split() |> Array.map int)
)
|> fun (l, k, ns) ->
  (* 答え（最小の長さ）を二分探索 *)
  ([|-1|], [|l + 1|]) |> fun (li, ri) ->                  (* 範囲は li 以上, ri 未満 *)
    Seq.initInfinite id
    |> Seq.takeWhile (fun _ -> ri.[0] - li.[0] > 1)       (* 範囲が１以上あれば探索 *)
    |> Seq.map (fun _ -> li.[0] + (ri.[0] - li.[0]) / 2)  (* 範囲内の真ん中の値（左寄せ） *)
    |> Seq.iter (fun m ->
      (* 最大何個の『長さ m 以上のピース』に分割できるか *)
      ((0, 0), ns) ||> Array.fold (fun (p, c) a ->
        if a - p >= m && l - a >= m (* 前回の位置からの差分 および 残りの長さが m 以上か *)
        then (a, c + 1)   (* カットする *)
        else (p, c)       (* カットしない *)
      )
      |> fun (_, c) ->
        if c >= k         (* k 以上個の『長さ m 以上のピース』に分割できるか *)
        then li.[0] <- m  (* より長くてもよいかもしれない *)
        else ri.[0] <- m  (* より短くなければならない *)
    )
    |> fun _ -> li.[0]
|> printfn "%d"

// https://atcoder.jp/contests/typical90/submissions/30768178
