// https://atcoder.jp/contests/typical90/tasks/typical90_ai

let n   = stdin.ReadLine() |> int
let es  = [1 .. n - 1] |> List.map (fun _ -> stdin.ReadLine().Split() |> Array.map (int >> ((+) -1)) |> fun x -> x.[0], x.[1])
let q   = stdin.ReadLine() |> int
let vss = [1 .. q] |> List.map (fun _ -> stdin.ReadLine().Split() |> Array.tail |> Array.map (int >> ((+) -1)))

if n > 5000 || q > 5000 then failwith "give up"

let adjacents = Array.create n Set.empty in
  es |> Seq.iter (fun (s, e) ->
    adjacents.[s] <- adjacents.[s] |> Set.add e
    adjacents.[e] <- adjacents.[e] |> Set.add s
  )

vss |> List.map (fun vs ->
  let memo = Array.create n 0
  vs |> Array.iter (fun v -> memo.[v] <- memo.[v] + 1)

  let rec loop pos pre =
    adjacents.[pos]
    |> Seq.filter (fun i -> i <> pre)
    |> Seq.iter (fun i ->
      loop i pos
      memo.[pos] <- memo.[pos] + memo.[i]
    )
  loop vs.[0] -1

  [0 .. n - 1]
  |> Seq.filter (fun i -> i <> vs.[0] && memo.[i] <> 0)
  |> Seq.length
)
|> List.iter stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/37280394
