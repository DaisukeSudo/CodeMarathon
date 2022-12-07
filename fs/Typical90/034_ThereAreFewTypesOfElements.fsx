// https://atcoder.jp/contests/typical90/tasks/typical90_ah

open System.Collections.Generic

let n, k = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let xs = stdin.ReadLine().Split()

let dict = new Dictionary<string, int>()

let add key =
  let p, v = dict.TryGetValue key
  if p then dict.Remove key |> ignore
  dict.Add (key, v + 1)
  not p

let sub key =
  let v = dict.GetValueOrDefault (key, 0)
  dict.Remove key |> ignore
  let p = v > 1
  if p then dict.Add (key, v - 1)
  not p

let rec subLoop s =
  let xs = xs.[s]
  if sub xs
  then s + 1
  else subLoop (s + 1)

let mutable s = 0 // 開始位置
let mutable l = 0 // 最大の長さ

[0 .. n - 1] |> List.iter (fun i ->
  if add xs.[i] && dict.Count > k then
    s <- subLoop s
  l <- max l (i - s + 1)
)

l |> stdout.WriteLine 

// https://atcoder.jp/contests/typical90/submissions/37076723
