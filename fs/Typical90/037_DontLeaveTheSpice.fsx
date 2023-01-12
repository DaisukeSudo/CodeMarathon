// https://atcoder.jp/contests/typical90/tasks/typical90_ak

let rmq (e: int64) (f: int64 -> int64 -> int64) n =
  let size =
    let mutable x = 1 in while x <= n do x <- x * 2
    x

  let dat = Array.create (size * 2) e

  let update p x =
    let mutable pos = p + size
    dat.[pos] <- x
    while pos >= 2 do
      pos <- pos >>> 1
      dat.[pos] <- f dat.[pos * 2] dat.[pos * 2 + 1]

  let query l r =
    let rec loop l r a b u =
      match u with
      | _ when l <= a && b <= r -> dat.[u]
      | _ when r <= a || b <= l -> e
      | _ ->
        let v1 = loop l r a ((a + b) >>> 1) (u * 2)
        let v2 = loop l r ((a + b) >>> 1) b (u * 2 + 1)
        f v1 v2
    loop l r 0 size 1

  (update, query)

// ----

let e = -1L
let rangeMax = rmq e max

let w, n = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let lrvs = [| 1 .. n |] |> Array.map (fun _ -> stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1], int64 x.[2])

let dp = Array.init (n + 1) (fun _ -> Array.create (w + 1) e)
let update, query = Array.init (n + 1) (fun _ -> rangeMax (w + 2)) |> Array.unzip

dp.[0].[0] <- 0L
update.[0] 0 0L

[1 .. n] |> List.iter (fun i ->
  let l, r, v = lrvs.[i - 1]
  [0 .. w] |> List.iter (fun j ->
    dp.[i].[j] <- dp.[i - 1].[j]
  )
  [0 .. w] |> List.iter (fun j ->
    let cl = max 0 (j - r)
    let cr = max 0 (j - l + 1)
    if cl <> cr then
      let v2 = query.[i - 1] cl cr
      if v2 <> e then
        dp.[i].[j] <- max dp.[i].[j] (v + v2)
  )
  [0 .. w] |> List.iter (fun j ->
    update.[i] j dp.[i].[j]
  )
)

dp.[n].[w] |> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/37955210
