// https://atcoder.jp/contests/typical90/tasks/typical90_bc

let combinations arr k =
  if k = 0 then Seq.empty else
    let rec loop arr k =
      seq {
        match (Seq.length arr) with
        | _ when k = 0 -> yield Seq.empty
        | l when l = k -> yield arr
        | _ ->
          let h = Seq.head arr
          let ts = Seq.tail arr
          let a1 = loop ts (k - 1) |> Seq.map (fun x -> Seq.append (Seq.singleton h) x)
          let a2 = loop ts k
          yield! a1
          yield! a2
      }
    loop arr k

// combinations [1; 2; 3; 4; 5] 3 ;;

// ----

let _, p, q = stdin.ReadLine().Split() |> Array.map int64 |> fun x -> x.[0], x.[1], x.[2]
let a = stdin.ReadLine().Split() |> Array.map int64 |> Array.toList

combinations a 5
|> Seq.map(Seq.reduce ( * ))
|> Seq.filter (fun x -> x % p = q)
|> Seq.length
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/41884802
