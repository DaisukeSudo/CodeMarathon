// https://atcoder.jp/contests/typical90/tasks/typical90_j

let n = stdin.ReadLine() |> int
let memo = Array.init 2 (fun _ -> Array.create (n + 1) 0)

[1..n] |> List.iter (fun i ->
  let c, p = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
  [0..1] |> List.iter (fun j ->
    memo.[j].[i] <- memo.[j].[i - 1] + if c - 1 = j then p else 0
  )
)

let q = stdin.ReadLine() |> int

[1..q] |> List.iter (fun _ ->
  let l, r = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
  memo
  |> Array.map (fun m -> m.[r] - m.[l - 1] |> string)
  |> String.concat " "
  |> stdout.WriteLine
)

// https://atcoder.jp/contests/typical90/submissions/32268182
