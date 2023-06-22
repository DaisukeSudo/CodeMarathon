// https://atcoder.jp/contests/typical90/tasks/typical90_bf

let buttonA x =
  let rec loop num =
    if num = 0 then 0 else
      num % 10 + loop (num / 10)
  (x + loop x) % 100000

let solve n k =
  let used = Array.create 100000 -1 in used.[n] <- 0
  let memo = Array.create 100000 -1 in memo.[0] <- n

  let rec loop i x =
    let i2 = i + 1
    let x2 = buttonA x
    if (int64 i2) = k then
      x2
    else if used.[x2] <> -1 then
      let s = used.[x2]
      memo.[int ((k - (int64 s)) % int64 (i2 - s)) + s]
    else
      used.[x2] <- i2
      memo.[i2] <- x2
      loop i2 x2
  loop 0 n

stdin.ReadLine().Split()
|> fun x -> (int x.[0], int64 x.[1])
||> solve
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/42823514
