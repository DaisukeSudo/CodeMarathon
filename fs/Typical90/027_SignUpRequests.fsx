// https://atcoder.jp/contests/typical90/tasks/typical90_aa

open System.Collections.Generic;

let n = stdin.ReadLine() |> int
let ss = seq { for _ in 1 .. n -> stdin.ReadLine() }

((1, [], HashSet<string>()), ss) ||> Seq.fold (fun (i, acc, reg) s ->
  if reg.Contains s
  then
    (i + 1, acc, reg)
  else
    reg.Add s |> ignore
    (i + 1, i :: acc, reg)
)
|> fun (_, acc, _) -> acc
|> List.rev
|> List.iter stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/35462637
