// https://atcoder.jp/contests/typical90/tasks/typical90_ar

let n, q = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let a    = stdin.ReadLine().Split() |> Array.map int
let txy  = [1 .. q] |> List.map (fun _ -> stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1] - 1, x.[2] - 1)

let mutable s = 0
let shift () = s <- (s + 1) % n
let index x = (n + x - s) % n

let swap x y =
  let x2 = index x
  let y2 = index y
  let z  = a.[x2]
  a.[x2] <- a.[y2]
  a.[y2] <- z

let mutable ans = []
let put x =
  let x2 = index x
  ans <- a.[x2] :: ans

txy |> List.iter (fun (t, x, y) ->
  match t with
  | 1 -> swap x y
  | 2 -> shift ()
  | 3 -> put x
  | _ -> failwith "unexpected value"
)

ans |> List.rev |> List.iter (stdout.WriteLine)

// https://atcoder.jp/contests/typical90/submissions/39004527
