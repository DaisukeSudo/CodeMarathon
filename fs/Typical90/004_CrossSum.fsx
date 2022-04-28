// https://atcoder.jp/contests/typical90/tasks/typical90_d

let h, w = stdin.ReadLine().Split() |> fun x -> (int x.[0], int x.[1]) in
let arr  = Array.init h (fun _ -> stdin.ReadLine().Split() |> Array.map int) in

let sumH = arr |> Array.map Array.sum in
let sumW = Array.init w (fun j -> Array.init h (fun i -> arr.[i].[j]) |> Array.sum) in
let res  = Array.init h (fun i -> Array.init w (fun j -> sumH.[i] + sumW.[j] - arr.[i].[j])) in

res
|> (Array.map (Array.map string >> String.concat " ") >> String.concat "\n")
|> printfn "%s"

// https://atcoder.jp/contests/typical90/submissions/31317246
// https://atcoder.jp/contests/typical90/submissions/31317254
