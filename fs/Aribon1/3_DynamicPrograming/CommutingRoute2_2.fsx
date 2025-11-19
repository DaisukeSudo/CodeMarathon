// https://atcoder.jp/contests/joi2010yo/tasks/joi2010yo_e

let MOD = 100000

let memoize (f: 'a -> 'b): 'a -> 'b =
  let m = System.Collections.Generic.Dictionary<'a, 'b>()
  fun a ->
    match m.TryGetValue(a) with
    | true, v -> v
    | false, _ ->
      let b = f a
      m.Add(a, b)
      b

let rec count i j k =
  match (i, j, k) with
  | _ when i < 0 || j < 0 -> 0
  | (1, 0, 0) -> 1
  | (0, 1, 1) -> 1
  | (_, _, 0) ->
    (
      countM (i - 1) j 0 +
      if i >= 2 then countM (i - 2) j 1 else 0
    ) % MOD
  | (_, _, 1) ->
    (
      countM i (j - 1) 1 +
      if j >= 2 then countM i (j - 2) 0 else 0
    ) % MOD
  | _ -> failwith "unexpected"

and countM = memoize count

let w, h = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
(
  (countM (h - 1) w 1) +
  (countM h (w - 1) 0) 
) % MOD
|> stdout.WriteLine

// https://atcoder.jp/contests/joi2010yo/submissions/71074151 TLE
