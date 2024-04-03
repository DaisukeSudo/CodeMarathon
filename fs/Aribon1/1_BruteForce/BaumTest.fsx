// https://atcoder.jp/contests/arc037/tasks/arc037_b

let n, m = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let uv = Array.init m (fun _ -> stdin.ReadLine().Split() |> Array.map (int >> ((+) -1)) |> fun x -> x.[0], x.[1])

let visited = Array.create n false

let es = Array.create n [] in
  uv |> Seq.iter (fun (a, b) ->
    es.[a] <- b :: es.[a]
    es.[b] <- a :: es.[b]
  )

let rec dfs current parent =
  if visited.[current] then false
  else
    visited.[current] <- true
    (true, es.[current]) ||> List.fold (fun acc adjacent ->
      if adjacent <> parent then acc && (dfs adjacent current)
      else acc
    )

(0, [0 .. n - 1]) ||> List.fold (fun acc i ->
  if not visited.[i] && dfs i -1 then acc + 1 else acc
)
|> stdout.WriteLine

// https://atcoder.jp/contests/arc037/submissions/51989648
