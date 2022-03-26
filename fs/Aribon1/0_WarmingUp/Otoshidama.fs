// https://atcoder.jp/contests/abc085/tasks/abc085_c

// 10 * a
//  5 * b
//  1 * c
//  n 枚
//  y 円

// 10 * a + 5 * b + c = y
//      a +     b + c = n
//  9 * a + 4 * b     = y - n

// b = (y - n - 9 * a) / 4
// c = n - a - b

stdin.ReadLine()
|> fun x -> x.Split(' ')
|> Array.map int
|> fun x -> (x.[0], x.[1] / 1000)
|> fun (n, y) ->
  [0..n]
  |> Seq.tryFind (fun a ->
    (y - n - 9 * a) % 4 = 0 &&
       (y - n - 9 * a) >= 0 &&
       n - a - (y - n - 9 * a) / 4 >= 0
  )
  |> fun a ->
    match a with
    | Some a ->
      (y - n - 9 * a) / 4
      |> fun b -> sprintf "%d %d %d" a b (n - a - b)
    | None -> "-1 -1 -1"
|> printfn "%s"

// https://atcoder.jp/contests/abc085/submissions/11467368
