// https://atcoder.jp/contests/typical90/tasks/typical90_h

let p = 1_000_000_007;
let mutable a = 0
let mutable t = 0
let mutable c = 0
let mutable o = 0
let mutable d = 0
let mutable e = 0
let mutable r = 0

let count x =
  match x with
  | 'a' -> a <- (a + 1)
  | 't' -> t <- (t + a) % p
  | 'c' -> c <- (c + t) % p
  | 'o' -> o <- (o + c) % p
  | 'd' -> d <- (d + o) % p
  | 'e' -> e <- (e + d) % p
  | 'r' -> r <- (r + e) % p
  | _ -> ()

stdin.ReadLine() |> ignore
stdin.ReadLine() |> Seq.iter count
stdout.WriteLine r

// https://atcoder.jp/contests/typical90/submissions/31917285
