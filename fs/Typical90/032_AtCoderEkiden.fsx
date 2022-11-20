// https://atcoder.jp/contests/typical90/tasks/typical90_af

let countOnes x =
  let mutable a = x 
  let mutable c = 0 
  while a > 0 do
    if a &&& 1 = 1 then c <- c + 1
    a <- a >>> 1
  c

// ----

let n = stdin.ReadLine() |> int
let ass = [| 1 .. n |] |> Array.map (fun _ -> stdin.ReadLine().Split() |> Array.map int)
let m = stdin.ReadLine() |> int
let xys = [| 1 .. m |] |> Array.map (fun _ -> stdin.ReadLine().Split() |> Array.map (int >> (+) -1) |> fun x -> x.[0], x.[1])

let rumors = Array.init n (fun _ -> Array.create n false) in
  xys |> Array.iter (fun (x, y) ->
    rumors.[x].[y] <- true
    rumors.[y].[x] <- true
  )

let memo = Array.init n (fun _ -> Array.create (1 <<< n) System.Int32.MaxValue)

[0 .. n - 1] |> List.iter (fun i ->
  memo.[i].[1 <<< i] <- ass.[i].[0]               // 各選手が第１区のとき
)

[1 .. (1 <<< n) - 1] |> List.iter (fun i ->       // 現在の組合せ
  // (i, System.Convert.ToString(i, 2)) |> printfn "i: %A"
  [0 .. min (n - 1) i]
  |> List.filter (fun j -> i >>> j &&& 1 = 1)     // 現在の選手
  |> List.filter (fun j -> memo.[j].[i] <> System.Int32.MaxValue)
  |> List.iter (fun j ->
    // (j, (1 <<< j)) |> printfn "  j: %A"
    [0 .. n - 1]
    |> List.filter (fun k -> i >>> k &&& 1 = 0)   // 次の選手
    |> List.filter (fun k -> not rumors.[j].[k])  // 噂があればスキップ
    |> List.iter (fun k ->
      // (k, (1 <<< k)) |> printfn "    k: %A"
      let d = i |> countOnes                      // 現在の区間
      let c = ass.[k].[d]                         // 該当のタイム
      let i2 = i ||| (1 <<< k)                    // 次の組合せ
      // printfn "      i2: %A, v: %A" i2 (memo.[j].[i] + c)
      memo.[k].[i2] <- min memo.[k].[i2] (memo.[j].[i] + c)
    )
  )
)

// memo |> Array.iter (printfn "%1000A")

memo
|> Array.map Array.last
|> Array.min
|> fun x -> if x = System.Int32.MaxValue then -1 else x
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/36667321
