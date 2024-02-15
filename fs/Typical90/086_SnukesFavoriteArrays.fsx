// https://atcoder.jp/contests/typical90/tasks/typical90_ch

let n, q = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let xyzw = Array.init q (fun _ ->
  stdin.ReadLine().Split() |> fun x -> int x.[0] - 1, int x.[1] - 1, int x.[2] - 1, int64 x.[3]
)

let p = 1_000_000_007

[0 .. 60] |> List.map (fun i ->
  [0 .. 1 <<< n] |> List.filter (fun bits ->
    xyzw |> Array.forall (fun (x, y, z, w) ->
      ((bits >>> x ||| bits >>> y ||| bits >>> z) &&& 1) = int (w >>> i &&& 1L)
    )
  )
  |> List.length
)
|> List.reduce (fun ans x -> ans * x % p)
|> stdout.WriteLine
