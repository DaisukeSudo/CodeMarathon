// https://atcoder.jp/contests/typical90/tasks/typical90_bu

let n = stdin.ReadLine() |> int
let c = stdin.ReadLine().Split() |> Array.map char
let ab = Array.init (n - 1) (fun _ -> stdin.ReadLine().Split() |> Array.map (int >> ((+) -1)) |> fun x -> x.[0], x.[1])

let p = 1_000_000_007L

let g = Array.create n [] in
  ab |> Seq.iter (fun (a, b) ->
    g.[a] <- b :: g.[a]
    g.[b] <- a :: g.[b]
  )

let rec dfs cur prev =
  g.[cur]
  |> List.filter ((<>) prev)
  |> List.fold (fun (ab1, a1, b1) next ->
    let ab2, a2, b2 = dfs next cur
    (
      (a1 * (b2 + ab2) +
       b1 * (a2 + ab2) + 
       ab1 * (a2 + b2 + ab2 * 2L)) % p,
      (a1 * (a2 + ab2)) % p,
      (b1 * (b2 + ab2)) % p
    )
  ) (
    if c.[cur] = 'a'
    then (0L, 1L, 0L)
    else (0L, 0L, 1L)
  )
  
let ans, _, _ = dfs 0 -1
ans |> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/46469130
