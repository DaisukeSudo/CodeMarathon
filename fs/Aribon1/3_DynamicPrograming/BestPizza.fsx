// https://atcoder.jp/contests/joi2012yo/tasks/joi2012yo_c

let n = stdin.ReadLine() |> int
let a, b = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let c = stdin.ReadLine() |> int
let d = Array.init n (fun _ -> stdin.ReadLine() |> int)

d
|> Array.sortDescending
|> Array.fold (fun (k, s, ans) x ->
  let k = k + 1
  let s = s + x
  (k, s, max ans ((c + s) / (a + k * b)))
) (0, 0, 0)
|> fun (_, _, ans) -> ans
|> stdout.WriteLine

// https://atcoder.jp/contests/joi2012yo/submissions/71228357
