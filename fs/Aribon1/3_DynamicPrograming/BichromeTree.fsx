// https://atcoder.jp/contests/arc083/tasks/arc083_c

open System
open System.Collections.Generic

let n = stdin.ReadLine() |> int
let p = if n = 1 then (stdin.ReadLine() |> fun _ -> [||]) else stdin.ReadLine().Split() |> Array.map (int >> ((+) -1))
let x = stdin.ReadLine().Split() |> Array.map int

let g = Array.create n [] in
  p |> Array.iteri (fun i p ->
    g.[p] <- (i + 1) :: g.[p]
  )

let dp = Array.create n 0

let rec compute u =
  if u < 0 then true else
    ((0, Set.singleton 0), g.[u]) ||> Seq.fold (fun (s1, r1) v ->
      (HashSet<int>(), r1) ||> Seq.fold (fun r2 c ->
        r2.Add(c + x.[v]) |> ignore  // 同色
        r2.Add(c + dp.[v]) |> ignore // 異色
        r2
      )
      |> Set.ofSeq
      |> fun r2 -> (s1 + x.[v] + dp.[v], r2)
    )
    |> fun (s1, reachable) ->
      seq { x.[u] .. -1 .. 0 }
      |> Seq.tryFind reachable.Contains
      |> fun s2 ->
        match s2 with
        | Some s2 ->
          dp.[u] <- s1 - s2
          compute (u - 1)
        | None ->
          false

if compute (n - 1) then "POSSIBLE" else "IMPOSSIBLE"
|> stdout.WriteLine

// https://atcoder.jp/contests/arc083/submissions/69976024
