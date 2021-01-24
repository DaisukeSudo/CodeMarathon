// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=ALDS1_10_C

stdin.ReadLine()
|> int
|> fun q -> Array.init q (fun _ -> (stdin.ReadLine() |> Seq.toArray, stdin.ReadLine() |> Seq.toArray))
|> Array.map (
  fun (xs : char [], ys : char []) ->
    (Array.create (ys.Length + 1) 0)
    |> fun (memo : int []) ->
      xs
      |> Array.map (
        fun x ->
          [| false |]
          |> fun (used : bool []) ->
            ys
            |> Array.mapi (
              fun i y ->
                match y with
                | _ when memo.[i] < memo.[i + 1] -> (used.[0] <- false)
                | _ when not used.[0] && x = y   -> (used.[0] <- true) |> fun () -> memo.[i + 1] <- memo.[i] + 1
                | _ when memo.[i] = memo.[i + 1] -> (used.[0] <- false)
                | _ -> memo.[i + 1] <- memo.[i]
          )
      )
      |> fun _ -> memo.[ys.Length]
)
|> Array.fold (fun acc x -> acc + (string x) + "\n") ""
|> printfn "%s"


// intput:
// abcbdab
// bdcababb

// memo:
// [|'b'; 'd'; 'c'; 'a'; 'b'; 'a'; 'b'; 'b'|]
// 'a': [|0; 0; 0; 0; 1; 1; 1; 1; 1|]
// 'b': [|0; 1; 1; 1; 1; 2; 2; 2; 2|]
// 'c': [|0; 1; 1; 2; 2; 2; 2; 2; 2|]
// 'b': [|0; 1; 1; 2; 2; 3; 3; 3; 3|]
// 'd': [|0; 1; 2; 2; 2; 3; 3; 3; 3|]
// 'a': [|0; 1; 2; 2; 3; 3; 4; 4; 4|]
// 'b': [|0; 1; 2; 2; 3; 4; 4; 5; 5|]

// output:
// 5
