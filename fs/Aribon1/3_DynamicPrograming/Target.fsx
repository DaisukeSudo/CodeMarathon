// https://atcoder.jp/contests/tdpc/tasks/tdpc_target

let lowerBound<'a when 'a : comparison> (v: 'a) (arr: 'a array) =
  let mutable li, ri = 0, arr.Length - 1
  while li <= ri do
    let mi = li + (ri - li) / 2
    if arr.[mi] >= v then ri <- mi - 1 else li <- mi + 1
  li

// ----

let n  = stdin.ReadLine() |> int
let xr = Array.init n (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1])

let dp = Array.init n (fun _ -> System.Int32.MaxValue)
xr
|> Seq.map (fun (x, r) -> x + r, x - r)
|> Seq.sort
|> Seq.map (snd >> ((*) -1))
|> Seq.iter (fun x -> dp.[dp |> lowerBound x] <- x)

dp
|> lowerBound System.Int32.MaxValue
|> stdout.WriteLine

// https://atcoder.jp/contests/tdpc/submissions/63430128
