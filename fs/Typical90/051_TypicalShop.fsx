// https://atcoder.jp/contests/typical90/tasks/typical90_ay

let n, k, p = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1], int64 x.[2]
let a = stdin.ReadLine().Split() |> Array.map int64

let toMultiValueMap (list : ('a * 'b) list) =
  List.fold (fun a (k, v) ->
    a |> Map.add k (v :: (Map.tryFind k a |> Option.defaultValue []))
  ) Map.empty list

let search a =
  let dp = [| [(0, 0L)] |]
  a |> Seq.iter (fun x ->
    dp.[0] |> Seq.iter (fun (c, v) ->
      if c < k && v + x <= p then
        dp.[0] <- (c + 1, v + x) :: dp.[0]
    )
  )
  dp.[0]
  |> toMultiValueMap
  |> Map.map (fun _ v -> v |> Seq.sort |> Seq.toArray)

let upperBound<'a when 'a : comparison> (arr: 'a array) (v: 'a) =
  let mutable li = 0
  let mutable ri = arr.Length - 1
  while li <= ri do
    let mi = (li + ri) / 2
    if arr.[mi] > v then
      ri <- mi - 1
    else
      li <- mi + 1
  li

let g1 = search a.[0 .. n / 2 - 1] 
let g2 = search a.[n / 2 ..]

g1 |> Map.toList |> List.sumBy (fun (i, a1) ->
  g2 |> Map.tryFind (k - i) |> Option.fold (fun _ a2 ->
    a1 |> Array.sumBy (((-) p) >> upperBound a2 >> int64)
  ) 0L
)
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/40995890
