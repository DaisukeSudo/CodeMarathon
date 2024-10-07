// https://atcoder.jp/contests/tdpc/tasks/tdpc_contest

let _ = stdin.ReadLine() |> int
let p = stdin.ReadLine().Split() |> Array.map int

(Set.empty.Add 0, p) ||> Array.fold (fun dp p ->
  dp |> Set.map ((+) p) |> Set.union dp
)
|> Set.count
|> stdout.WriteLine

// https://atcoder.jp/contests/tdpc/submissions/58542372
