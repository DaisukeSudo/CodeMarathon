let unionFindTree n =
  let mutable cnt = n
  let parent = [| 0..n - 1 |]
  let rank = Array.zeroCreate n

  let rec findRoot x =
    if parent.[x] = x then x
    else
      let root = findRoot parent.[x]
      parent.[x] <- root  // path compression
      root

  let unite a b =
    let rA = findRoot a
    let rB = findRoot b
    if rA <> rB then
      // union by rank
      if rank.[rA] < rank.[rB] then
        parent.[rA] <- rB
      elif rank.[rA] > rank.[rB] then
        parent.[rB] <- rA
      else
        parent.[rB] <- rA
        rank.[rA] <- rank.[rA] + 1
      cnt <- cnt - 1
    (rA, rB)

  let same a b = (findRoot a) = (findRoot b)

  let count () = cnt

  (unite, same, count)

// ----

let unite, same, count = unionFindTree 5
unite 0 1
unite 2 3
unite 3 4
same 1 2 |> printfn "same 1 2 : %A"
same 2 4 |> printfn "same 2 4 : %A"
count () |> printfn "count    : %A"
