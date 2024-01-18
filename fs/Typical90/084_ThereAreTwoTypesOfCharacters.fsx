// https://atcoder.jp/contests/typical90/tasks/typical90_cf

let n = stdin.ReadLine() |> int
let s = stdin.ReadLine() + "-"

let mutable l, r = 0, -1
let mutable o, x = 0, 0

let moveR () =
  r <- r + 1
  if s.[r] = 'o' then o <- o + 1 else x <- x + 1
let moveL () =
  if s.[l] = 'o' then o <- o - 1 else x <- x - 1
  l <- l + 1

moveR ()

let mutable ans = 0L
while r < n do
  if o = 0 || x = 0 then
    moveR ()
  else
    ans <- ans + int64 (n - r)
    moveL ()

ans |> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/49385878
