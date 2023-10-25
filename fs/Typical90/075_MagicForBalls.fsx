// https://atcoder.jp/contests/typical90/tasks/typical90_bw

let factorize n =
  let mutable x = n
  let mutable ans = []
  let z = n |> double |> sqrt |> ceil |> int64
  for i in 2L :: [3L .. 2L .. z] do
    let rec loop () =
      if x % i = 0L then
        x <- x / i
        ans <- i :: ans
        loop ()
    loop ()
  if x <> 1L then ans <- x :: ans
  ans

let leftmost n =
  let mutable x = n
  let mutable pos = 0
  while x > 0 do
    pos <- pos + 1
    x <- x >>> 1
  pos

stdin.ReadLine() |> int64
|> factorize
|> List.length |> leftmost
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/46924974
