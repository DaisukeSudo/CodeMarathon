// https://atcoder.jp/contests/typical90/tasks/typical90_bz

let n, m = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let ab = Array.init m (fun _ -> stdin.ReadLine().Split() |> Array.map (int >> ((+) -1)) |> fun x -> x.[0], x.[1])

// adjacencies
let g = Array.create n [] in
  ab |> Array.iter (fun (a, b) ->
    g.[a] <- b :: g.[a]
    g.[b] <- a :: g.[b]
  )

((0, 0), g) ||> Array.fold (fun (i, ans) es ->
  let satisfied = es |> List.filter ((>) i) |> List.length = 1
  (
    i + 1,
    ans + if satisfied then 1 else 0
  )
)
|> snd
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/47528122
