// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=DPL_1_D

stdin.ReadLine ()
|> int
|> fun n ->
  (
    List.init n (fun _ -> stdin.ReadLine () |> int),
    Array.create (n + 1) -1
  )
  |> fun (ns, memo) ->
    ns
    |> List.fold (
      fun len x ->
        if memo.[len] < x
        then
          (memo.[len + 1] <- x)
          |> fun _ -> len + 1
        else
          memo
          |> Array.findIndex ((<=) x)
          |> fun i -> memo.[i] <- x
          |> fun _ -> len
    ) 0
|> printfn "%d"

// input:
// 5
// 5
// 1
// 3
// 2
// 4

// memo:
// [|-; 1; 2; 4; -; -|]

// output:
// 3
