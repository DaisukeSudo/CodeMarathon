// https://atcoder.jp/contests/typical90/tasks/typical90_ac

// Range Updated Query
let ruq operate e n =
  let n2 =
    let mutable x = 1 in while n > x do x <- x * 2
    x

  let memo = Array.create (n2 * 2 - 1) e
  let temp = Array.create (n2 * 2 - 1) e

  let eval i =
    if temp.[i] <> e then (
      if i < n2 - 1 then (
        temp.[i * 2 + 1] <- temp.[i]
        temp.[i * 2 + 2] <- temp.[i]
      )
      memo.[i] <- temp.[i]
      temp.[i] <- e
    ) 

  // 範囲 [a, b) を値 x に更新する
  let update a b x =
    let rec update i l r =
      eval i
      match i with
      | _ when a <= l && r <= b ->
        temp.[i] <- x
        eval i
      | _ when a < r && l < b ->
        update (i * 2 + 1) l ((l + r) / 2)
        update (i * 2 + 2) ((l + r) / 2) r
        memo.[i] <- operate memo.[i * 2 + 1] memo.[i * 2 + 2]
      | _ -> ()

    update 0 0 n2

  // 範囲 [a, b) の値を取得する
  let query a b =
    let rec query i l r =
      eval i
      match i with
      | _ when r <= a || b <= l -> e
      | _ when a <= l && r <= b -> memo.[i]
      | _ ->
        let vl = query (i * 2 + 1) l ((l + r) / 2)
        let vr = query (i * 2 + 2) ((l + r) / 2) r
        operate vl vr

    query 0 0 n2

  (update, query)

// ----

let w, n = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let lrs = seq { for _ in 1 .. n -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0] - 1, x.[1] }

let update, query = ruq max 0 w

lrs
|> Seq.map (fun (l, r) ->
  let x = (query l r) + 1
  update l r x
  x
)
|> Seq.iter stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/35991614
