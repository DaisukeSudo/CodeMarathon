// https://atcoder.jp/contests/typical90/tasks/typical90_cb

let n, d = stdin.ReadLine().Split() |> fun x -> int x.[0], int x.[1]
let a = stdin.ReadLine().Split() |> Array.map int64

(0L, [0L .. (1L <<< n) - 1L]) ||> List.fold (fun ans i ->
  let mutable bit = 0L
  let mutable conditions = 0L
  for j in [0 .. n - 1] do
    if (i >>> j) &&& 1L = 1L then
      bit <- bit ||| a.[j]
      conditions <- conditions + 1L

  let mutable freeDigits = 0
  for j in [0 .. d - 1] do
    if (bit >>> j) &&& 1L = 0L then
      freeDigits <- freeDigits + 1

  ans + (1L <<< freeDigits) * (if conditions % 2L = 0L then 1L else -1L)
)
|> stdout.WriteLine

// https://atcoder.jp/contests/typical90/submissions/48246255
