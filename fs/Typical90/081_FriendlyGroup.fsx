// https://atcoder.jp/contests/typical90/tasks/typical90_cc

let n, k = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let ab = Array.init n (fun _ -> stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1])

let imos = Array.init 5001 (fun _ -> Array.create 5001 0)

ab |> Array.iter (fun (a, b) ->
  imos.[a].[b] <- imos.[a].[b] + 1
)
[0 .. 5000] |> List.iter (fun a -> [1 .. 5000] |> List.iter (fun b ->
  imos.[a].[b] <- imos.[a].[b - 1] + imos.[a].[b]
))
[1 .. 5000] |> List.iter (fun a -> [0 .. 5000] |> List.iter (fun b ->
  imos.[a].[b] <- imos.[a - 1].[b] + imos.[a].[b]
))

let mutable ans = 0
let d = k + 1
[0 .. 5000 - d] |> List.iter (fun a -> [0 .. 5000 - d] |> List.iter (fun b ->
  ans <- max ans (imos.[a].[b] - imos.[a + d].[b] - imos.[a].[b + d] + imos.[a + d].[b + d])
))

ans |> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/48484079
