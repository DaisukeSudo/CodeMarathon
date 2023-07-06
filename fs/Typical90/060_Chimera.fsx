// https://atcoder.jp/contests/typical90/tasks/typical90_bh

let lowerBound<'a when 'a : comparison> (arr: 'a array) (v: 'a) =
  let mutable li = 0
  let mutable ri = arr.Length - 1
  while li <= ri do
    let mi = li + (ri - li) / 2
    if arr.[mi] >= v then
        ri <- mi - 1
    else
        li <- mi + 1
  li

// ----

let n = stdin.ReadLine() |> int
let a = stdin.ReadLine().Split() |> Array.map (int >> ((+) -1))

let p  = Array.create n System.Int32.MaxValue in
  let dp = Array.create n System.Int32.MaxValue
  let mutable cnt = 0
  for i in seq { 0 .. n - 1 } do
    let pos = lowerBound dp a.[i]
    dp.[pos] <- a.[i]
    p.[i] <- pos + 1
    if pos = cnt then cnt <- cnt + 1

let q  = Array.create n System.Int32.MaxValue in
  let dp = Array.create n System.Int32.MaxValue
  let mutable cnt = 0
  for i in seq { n - 1 .. (-1) .. 0 } do
    let pos = lowerBound dp a.[i]
    dp.[pos] <- a.[i]
    q.[i] <- pos + 1
    if pos = cnt then cnt <- cnt + 1

(0, seq { 0 .. n - 1 })
||> Seq.fold (fun a i -> max a (p.[i] + q.[i] - 1))
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/43286195
