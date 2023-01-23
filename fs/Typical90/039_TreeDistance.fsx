// https://atcoder.jp/contests/typical90/tasks/typical90_am

let n = stdin.ReadLine() |> int
let es = [| 1 .. n - 1 |] |> Array.map (fun _ -> stdin.ReadLine().Split() |> Array.map (int >> ((+) -1)) |> fun x -> x.[0], x.[1])

let g = Array.create n [] in
  es |> Seq.iter (fun (s, e) ->
    g.[s] <- e :: g.[s]
    g.[e] <- s :: g.[e]
  )

let dp = Array.create n 1L

let rec dfs ci pi =
  g.[ci]
  |> Seq.filter ((<>) pi)
  |> Seq.iter (fun ni ->
    dfs ni ci
    dp.[ci] <- dp.[ci] + dp.[ni]
  )

dfs 0 -1

let n = n |> int64

es
|> Seq.sumBy (fun (s, e) ->
  let r = min dp.[s] dp.[e]
  r * (n - r)
)
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/38285415
