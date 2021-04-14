# https://atcoder.jp/contests/abs/tasks/abc086_a

use Bitwise

defmodule Main, do:
  def main(), do:
    IO.read(:line)
    |> String.trim
    |> String.split(" ")
    |> Enum.map(&String.to_integer/1)
    |> Enum.map(& &1 &&& 1)
    |> Enum.reduce(& &1 &&& &2)
    |> (& if &1 == 1, do: 'Odd', else: 'Even').()
    |> IO.puts

# https://atcoder.jp/contests/abs/submissions/21739209
