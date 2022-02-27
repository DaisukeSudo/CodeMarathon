# https://atcoder.jp/contests/abc139/tasks/abc139_d

(
  parse.(BigInt, readline())
  |> n -> (
    n * (n - 1) / 2
  )
  |> BigInt
  |> println
)

# https://atcoder.jp/contests/abc139/submissions/29737501
