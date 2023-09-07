let unionFindTree n =
  let mutable cnt = n
  let tbl = [| 0..n - 1 |]

  let findTree x =
    let mutable next = true
    Seq.initInfinite ignore
    |> Seq.takeWhile (fun () -> next)
    |> Seq.scan (fun c _ ->
      let parent = tbl.[c]
      if parent = c then next <- false
      parent
    ) x
    |> Seq.rev
    |> Seq.toList

  let findRoot =
    findTree >> (fun tree ->
      let root = tree |> List.head
      tree |> List.tail |> List.iter (fun x -> tbl.[x] <- root)
      root
    )

  let updateRoot newRoot =
    findTree >> (fun tree ->
      let oldRoot = tree |> Seq.head
      tree |> List.iter (fun x -> tbl.[x] <- newRoot)
      oldRoot
    )

  let unite a b =
    let rA = findRoot a
    let rB = updateRoot rA b
    if (rA <> rB) then cnt <- cnt - 1
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
