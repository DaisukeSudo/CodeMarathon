// https://atcoder.jp/contests/abc083/tasks/arc088_a

// let x, y = stdin.ReadLine().Split() |> fun x -> int64 x.[0], int64 x.[1]

// let v = log (float y / float x) / log 2.
// let ans = int (floor v) + 1

// ans |> stdout.WriteLine

// https://atcoder.jp/contests/abc083/submissions/57213836


let x, y = stdin.ReadLine().Split() |> Array.map int64 |> fun x -> x.[0], x.[1]
Seq.initInfinite (pown 2L >> ((*) x)) |> Seq.takeWhile ((>=) y) |> Seq.length
|> stdout.WriteLine

// https://atcoder.jp/contests/abc083/submissions/57216837
