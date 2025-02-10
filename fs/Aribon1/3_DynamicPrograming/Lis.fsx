// https://atcoder.jp/contests/chokudai_S001/tasks/chokudai_S001_h

let lowerBound<'a when 'a : comparison> (v: 'a) (arr: 'a array) =
  let mutable li, ri = 0, arr.Length - 1
  while li <= ri do
    let mi = li + (ri - li) / 2
    if arr.[mi] >= v then ri <- mi - 1 else li <- mi + 1
  li

// ----

let n = stdin.ReadLine() |> int
let a = stdin.ReadLine().Split() |> Array.map int

let dp = Array.init n (fun _ -> System.Int32.MaxValue)

a |> Array.iter (fun x ->
  let i = dp |> lowerBound x
  dp.[i] <- x
)

dp |> lowerBound System.Int32.MaxValue
|> stdout.WriteLine

// https://atcoder.jp/contests/chokudai_S001/submissions/62628938
