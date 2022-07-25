// https://atcoder.jp/contests/typical90/tasks/typical90_q

let imos2 n lrs =
  let memo = Array.init n (fun _ -> Array.create n 0)

  let inline inc i j = memo.[i].[j] <- memo.[i].[j] + 1
  let inline dec i j = memo.[i].[j] <- memo.[i].[j] - 1

  lrs |> List.iter (fun (l, r) ->
    // if l > 0 then
    //   inc (l + 1) 0
    //   dec (l + 1) r
    //   dec r 0
    if r < n - 1 then
      inc (l + 1) (r + 1)
      dec r (r + 1)
  )

  let inline slideW (i, j) = memo.[i].[j] <- memo.[i].[j] + memo.[i].[j - 1]
  let inline slideH (i, j) = memo.[i].[j] <- memo.[i].[j] + memo.[i - 1].[j]

  seq {for i in 0 .. n - 1 do for j in 1 .. n - 1 -> (i, j)} |> Seq.iter (slideW)
  seq {for i in 1 .. n - 1 do for j in 0 .. n - 1 -> (i, j)} |> Seq.iter (slideH)

  memo

// ----

let n, m = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]

let lrs =
  [1 .. m]
  |> List.map (fun _ -> stdin.ReadLine().Split() |> Array.map (int >> (+) -1) |> fun x -> x.[0], x.[1])
  |> List.filter (fun (l, r) -> r - l > 1 && (l > 0 || r < n))

let memo = imos2 n lrs

lrs
|> List.sumBy (fun (i, j) -> memo.[i].[j])
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/33515807
