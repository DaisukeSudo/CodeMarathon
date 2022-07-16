// https://atcoder.jp/contests/typical90/tasks/typical90_p

let n = stdin.ReadLine() |> int
let a, b, c = stdin.ReadLine().Split() |> Array.map int |> Array.sortDescending |> fun x -> x.[0], x.[1], x.[2]

seq {
  for i in 0 .. (min (n / a) 9999) do
    let n1 = n - a * i
    for j in 0 .. (min (n1 / b) (9999 - i)) do
      let n2 = n1 - b * j
      if n2 % c = 0 then yield i + j + n2 / c
}
|> Seq.min
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/33255332
