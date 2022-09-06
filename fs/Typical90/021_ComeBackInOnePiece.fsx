// https://atcoder.jp/contests/typical90/tasks/typical90_u

let scc n es =

  let direct n es =
    let f = Array.create n []
    let r = Array.create n []
    es
    |> Seq.iter (fun (s, e) ->
      f.[s] <- e :: f.[s]
      r.[e] <- s :: r.[e])
    (f, r)

  let dfs (dest: int list array) (canUse: int -> bool) (doUse: int -> unit) (onFinished: int -> unit) (v0: int) =
    let rec loop v =
      if canUse v then
        doUse v
        dest.[v] |> List.filter canUse |> List.iter loop
        onFinished v
    loop v0

  let forward, reverse = direct n es

  let t = (
    let mutable acc = []
    let used = Array.create n false
    [0 .. n - 1] |> List.iter (
      dfs forward
        (fun x -> not used.[x])
        (fun x -> used.[x] <- true)
        (fun x -> acc <- x :: acc)
    )
    acc
  )

  let ret = (
    let mutable acc = []
    let mutable g = []
    let used = Array.create n false
    t |> List.iter (fun i ->
      g <- []
      dfs reverse
        (fun x -> not used.[x])
        (fun x -> used.[x] <- true)
        (fun x -> g <- x :: g)
        i
      acc <- g :: acc
    )
    acc
  )

  ret

// ----------

let inline combination2 n = int64 n * (int64 n - 1L) / 2L

// ----------

let n, m = stdin.ReadLine().Split() |> Array.map int |> fun x -> x.[0], x.[1]
let es = [for _ in 1 .. m -> stdin.ReadLine().Split() |> Array.map (int >> (+) -1) |> fun x -> x.[0], x.[1]]

scc n es |> Seq.sumBy (Seq.length >> combination2)
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/34645251

// ----------

// let n: int = 22
// let m: int = 50
// let es: (int * int) list =
//   [(20, 14); (21, 16); (17, 14); (13, 18); (10, 12); (10, 1); (4, 20);
//    (14, 17); (20, 0); (18, 21); (8, 16); (19, 3); (17, 20); (20, 7); (4, 6);
//    (3, 5); (8, 6); (14, 0); (13, 19); (21, 12); (19, 1); (6, 18); (6, 20);
//    (15, 21); (4, 2); (7, 20); (3, 1); (5, 18); (8, 14); (1, 19); (0, 19);
//    (14, 16); (18, 5); (9, 2); (10, 7); (12, 15); (1, 11); (0, 6); (6, 16);
//    (4, 21); (2, 16); (2, 20); (6, 19); (17, 9); (13, 0); (5, 21); (11, 7);
//    (6, 2); (1, 5); (8, 11)]

// scc n es
// |> List.filter (Seq.length >> ((<) 0))
// |> printfn "%A"
// ;;
