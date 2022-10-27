// Range Minimam Query
let rmq operate e n =
  let n =
    let mutable x = 1 in while n > x do x <- x * 2
    x

  let memo = Array.create (n * 2 - 1) e

  // 位置 i を値 x に更新する
  let update i x =
    let mutable i = n - 1 + i
    memo.[i] <- x
    while i > 0 do
      i <- (i - 1) / 2
      memo.[i] <- operate memo.[i * 2 + 1] memo.[i * 2 + 2]

  // 範囲 [a, b) の値を取得する
  let query a b =
    let rec query i l r =
      match i with
      | _ when r <= a || b <= l -> e
      | _ when a <= l && r <= b -> memo.[i]
      | _ ->
        let vl = query (i * 2 + 1) l ((l + r) / 2)
        let vr = query (i * 2 + 2) ((l + r) / 2) r
        operate vl vr

    query 0 0 n

  (update, query)

// ----

let rmqTest =
  printfn "rmq test"
  let update, query = rmq min System.Int32.MaxValue 8
  [0..7] |> List.iter (fun i -> update i (i + 1))
  // [|
  //   1;
  //   1;          5;
  //   1;    3;    5;    7;
  //   1; 2; 3; 4; 5; 6; 7; 8;
  // |]
  [1..8] |> List.map (fun x -> query 0 x) |> printfn "%A"
  // [1; 1; 1; 1; 1; 1; 1; 1]
  [0..7] |> List.map (fun x -> query x 8) |> printfn "%A"
  // [1; 2; 3; 4; 5; 6; 7; 8]
  [0..7] |> List.map (fun x -> query x (x + 1)) |> printfn "%A"
  // [1; 2; 3; 4; 5; 6; 7; 8]

// ----

// Range Updated Query
let ruq operate e n =
  let n =
    let mutable x = 1 in while n > x do x <- x * 2
    x

  let memo = Array.create (n * 2 - 1) e
  let temp = Array.create (n * 2 - 1) e

  let eval i =
    if temp.[i] <> e then (
      if i < n - 1 then (
        temp.[i * 2 + 1] <- temp.[i]
        temp.[i * 2 + 2] <- temp.[i]
      )
      memo.[i] <- temp.[i]
      temp.[i] <- e
    ) 

  // 範囲 [a, b) を値 x に更新する
  let update a b x =
    let rec update i l r =
      eval i
      match i with
      | _ when a <= l && r <= b ->
        temp.[i] <- x
        eval i
      | _ when a < r && l < b ->
        update (i * 2 + 1) l ((l + r) / 2)
        update (i * 2 + 2) ((l + r) / 2) r
        memo.[i] <- operate memo.[i * 2 + 1] memo.[i * 2 + 2]
      | _ -> ()

    update 0 0 n

  // 範囲 [a, b) の値を取得する
  let query a b =
    let rec query i l r =
      eval i
      match i with
      | _ when r <= a || b <= l -> e
      | _ when a <= l && r <= b -> memo.[i]
      | _ ->
        let vl = query (i * 2 + 1) l ((l + r) / 2)
        let vr = query (i * 2 + 2) ((l + r) / 2) r
        operate vl vr

    query 0 0 n

  (update, query)

// ----

let ruqTest =
  printfn "ruq test"
  let update, query = ruq min System.Int32.MaxValue 8
  [0..7] |> List.iter (fun i -> update i 8 (i + 1))
  // [|
  //   1;
  //   1;          5;
  //   1;    3;    5;    7;
  //   1; 2; 3; 4; 5; 6; 7; 8;
  // |]
  [1..8] |> List.map (fun x -> query 0 x) |> printfn "%A"
  // [1; 1; 1; 1; 1; 1; 1; 1]
  [0..7] |> List.map (fun x -> query x 8) |> printfn "%A"
  // [1; 2; 3; 4; 5; 6; 7; 8]
  [0..7] |> List.map (fun x -> query x (x + 1)) |> printfn "%A"
  // [1; 2; 3; 4; 5; 6; 7; 8]
