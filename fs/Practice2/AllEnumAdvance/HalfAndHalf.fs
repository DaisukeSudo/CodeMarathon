// https://atcoder.jp/contests/abc095/tasks/arc096_a

stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map int
|> fun x -> (x.[0], x.[1], x.[2], x.[3], x.[4])
|> fun (a, b, c, x, y) ->
  [
    a * x + b * y
    (
      if x > y
      then a * (x - y) + c * 2 * y
      else b * (y - x) + c * 2 * x
    )
    c * 2 * (max x y)
  ]
  |> Seq.min
|> printfn "%d"

// https://atcoder.jp/contests/abc095/submissions/11211590
