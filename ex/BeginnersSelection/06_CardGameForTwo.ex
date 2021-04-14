# https://atcoder.jp/contests/abs/tasks/abc088_b

defmodule Main, do:
  def main(), do:
    [IO.read(:line), IO.read(:line)]
    |> Enum.at(1)
    |> String.trim
    |> String.split(" ")
    |> Enum.map(&String.to_integer/1)
    |> Enum.sort(& &1 > &2)
    |> Enum.reduce([0, 0, 0], fn x, t -> apply(fn a, b, i ->
      if rem(i, 2) === 0,
        do:   [a + x, b, i + 1],
        else: [a, b + x, i + 1]
    end, t) end)
    |> (& Enum.at(&1, 0) - Enum.at(&1, 1)).()
    |> IO.puts

# https://atcoder.jp/contests/abs/submissions/21758902
