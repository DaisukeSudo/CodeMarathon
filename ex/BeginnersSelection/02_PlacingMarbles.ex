# https://atcoder.jp/contests/abs/tasks/abc081_a

defmodule Main, do:
  def main(), do:
    IO.read(:line)
    |> String.trim
    |> String.split("", trim: true)
    |> Enum.map(&String.to_integer/1)
    |> Enum.sum
    |> IO.puts

# https://atcoder.jp/contests/abs/submissions/21757655
