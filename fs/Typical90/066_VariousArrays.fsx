// https://atcoder.jp/contests/typical90/tasks/typical90_bn

let n  = stdin.ReadLine() |> int
let lr = Array.init n (fun _ -> stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1])

seq {
  for i in 0 .. n - 2 do
  for j in i + 1 .. n - 1 -> (lr.[i], lr.[j])
}
|> Seq.sumBy (fun ((il, ir), (jl, jr)) ->
  seq {
    for k in il .. ir do
    for l in jl .. jr -> (if k > l then 1 else 0)
  }
  |> Seq.fold (fun (cnt, all) x -> (cnt + x, all + 1)) (0, 0)
  |> fun (cnt, all) -> float cnt / float all
)
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/44888702
