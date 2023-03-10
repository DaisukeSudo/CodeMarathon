// https://atcoder.jp/contests/typical90/tasks/typical90_at

let p = 46
let g = 3

let n = stdin.ReadLine() |> int
let a = Array.init g (fun _ -> stdin.ReadLine().Split() |> Array.map int)

let memo = Array.init g (fun _ -> Array.create p 0L) in
  [0 .. n - 1] |> List.iter (fun i ->
    a
    |> Seq.map (fun r -> r.[i])
    |> Seq.map (fun x -> x % p)
    |> Seq.iteri (fun i x -> memo.[i].[x] <- memo.[i].[x] + 1L)
  )

[0 .. p - 1] |> List.collect (fun i ->
  [0 .. p - 1] |> List.map (fun j ->
    [i; j; (p * 2 - i - j) % p]
    |> Seq.mapi (fun i x -> memo.[i].[x])
    |> Seq.reduce ( * )
  )
)
|> List.sum
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/39576192
