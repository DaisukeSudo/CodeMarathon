// https://atcoder.jp/contests/abc006/tasks/abc006_4

let lowerBound<'a when 'a : comparison> (v: 'a) (arr: 'a array) =
  let mutable li, ri = 0, arr.Length - 1
  while li <= ri do
    let mi = li + (ri - li) / 2
    if arr.[mi] >= v then ri <- mi - 1 else li <- mi + 1
  li

// ----

let n = stdin.ReadLine() |> int
let c = Array.init n (fun _ -> stdin.ReadLine() |> int)

let dp = Array.init n (fun _ -> System.Int32.MaxValue)
c |> Array.iter (fun x -> dp.[dp |> lowerBound x] <- x)

dp
|> lowerBound System.Int32.MaxValue
|> fun x -> n - x
|> stdout.WriteLine

// https://atcoder.jp/contests/abc006/submissions/62933723
