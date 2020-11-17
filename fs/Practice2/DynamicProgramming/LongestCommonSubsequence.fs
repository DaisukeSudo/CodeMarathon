// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=ALDS1_10_C

stdin.ReadLine()
|> int
|> fun q -> Array.init q (fun _ -> (stdin.ReadLine() |> Seq.toArray, stdin.ReadLine() |> Seq.toArray))
|> Array.map (
  fun (xs : char [], ys : char []) ->
    (
      (Array.create (ys.Length + 1) 0),
      (Array.create 123 0)
    )
    |> fun (memo : int [], xcmap : int []) ->
      xs
      |> Array.map (
        fun x ->
          (
            xcmap.[(int x)],
            [| 0 |]
          )
          |> fun (xc, yc) ->
            ys
            |> Array.mapi (
              fun i y ->
                (
                  if yc.[0] >= 0 && x = y
                  then
                    (
                      if xc = yc.[0]
                      then
                        (
                          (xcmap.[(int x)] <- xcmap.[(int x)] + 1),
                          (yc.[0] <- -1)
                        )
                        |> fun _ -> 1
                      else
                        (yc.[0] <- yc.[0] + 1)
                        |> fun _ -> 0
                    )
                  else 0
                )
                |> fun p ->
                  memo.[i + 1] <- memo.[i + 1] |> max (memo.[i] + p) 
            )
      )
      |> fun _ -> memo.[ys.Length]
)
|> Array.fold (fun acc x -> acc + (string x) + "\n") ""
|> printfn "%s"
