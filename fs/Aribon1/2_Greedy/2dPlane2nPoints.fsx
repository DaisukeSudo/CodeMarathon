// https://atcoder.jp/contests/abc091/tasks/arc092_a

let n = stdin.ReadLine() |> int
let ab = Array.init n (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1])
let cd = Array.init n (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1])

(
  ab |> Array.sortByDescending snd,
  cd |> Array.sortBy fst
)
||> Array.fold (fun acc (c, d) ->
  match acc |> Array.tryFind (fun (a, b) -> a < c && b < d) with
  | Some x -> acc |> Array.filter ((<>) x)
  | None   -> acc
) 
|> Array.length
|> (((*) -1) >> ((+) n))
|> stdout.WriteLine

// https://atcoder.jp/contests/abc091/submissions/58081241
