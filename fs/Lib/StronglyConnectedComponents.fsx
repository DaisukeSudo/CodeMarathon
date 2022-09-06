// Strongly Connected Components

let scc n es =

  let direct n es =
    let f = Array.create n []
    let r = Array.create n []
    es
    |> Seq.iter (fun (s, e) ->
      f.[s] <- e :: f.[s]
      r.[e] <- s :: r.[e])
    (f, r)

  // direct 7 [(0, 1); (1, 2); (2, 0); (2, 3); (3, 4); (4, 3); (4, 5); (4, 6)] ;;
  // > ([|[1]; [2]; [3; 0]; [4]; [6; 5; 3]; []; []|], [|[2]; [0]; [1]; [4; 2]; [3]; [4]; [4]|])

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

// scc 7 [(0, 1); (1, 2); (2, 0); (2, 3); (3, 4); (4, 3); (4, 5); (4, 6)] ;;
// > [[6]; [5]; [4; 3]; [1; 2; 0]]
