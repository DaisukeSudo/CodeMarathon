# https://atcoder.jp/contests/abs/tasks/abc087_b

defmodule Main, do:
  def main(), do:
    [IO.read(:line), IO.read(:line), IO.read(:line), IO.read(:line)]
    |> Enum.map(&String.trim/1)
    |> Enum.map(&String.to_integer/1)
    |> (fn t -> apply(fn a, b, c, x ->
      (0..Enum.min([a, floor(x / 500)]))
      |> Enum.map(fn i ->
        (0..Enum.min([b, floor((x - 500 * i) / 100)]))
        |> Enum.filter(fn j -> x - 500 * i - 100 * j <= 50 * c end)
        |> Enum.count
      end)
      |> Enum.sum
    end, t) end).()
    |> IO.puts

# https://atcoder.jp/contests/abs/submissions/21757220
