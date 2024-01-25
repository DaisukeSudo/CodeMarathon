// https://atcoder.jp/contests/typical90/tasks/typical90_cg

let k = stdin.ReadLine() |> int64

let divisors =
  Seq.initInfinite (int64 >> ((+) 1L))
  |> Seq.takeWhile (fun i -> i * i <= k)
  |> Seq.filter (fun i -> k % i = 0L)
  |> Seq.collect (fun i -> [i; k / i])
  |> Seq.distinct
  |> Seq.sort
  |> Seq.toList

(0L, divisors)
||> List.fold (fun ans a ->
  divisors
  |> List.filter (fun b ->
    (a <= b) && ((k / a) % b = 0L) && (b <= k / a / b)
  )
  |> List.length
  |> (int64 >> (+) ans)
)
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/49642289
