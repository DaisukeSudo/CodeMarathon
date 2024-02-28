// https://atcoder.jp/contests/typical90/tasks/typical90_ck

let n, k = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1] in if k > 0 then failwith "give up"
let a = stdin.ReadLine().Split() |> Array.map int64
let p = 1_000_000_007L

[0 .. n - 2]
|> List.filter (fun i -> a.[i] <= a.[i + 1])
|> List.length
|> fun x -> [0 .. x - 1]
|> List.fold (fun ans _ -> ans * 2L % p) 1L
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/50703746
