# https://atcoder.jp/contests/abs/tasks/abc081_b

use Bitwise

defmodule Main, do:
  def main(), do:
    [IO.read(:line), IO.read(:line)]
    |> Enum.at(1)
    |> String.trim
    |> String.split(" ")
    |> Enum.map(&String.to_integer/1)
    |> Enum.reduce(& &1 ||| &2)
    |> (& Integer.to_string(&1, 2)).()
    |> String.split("")
    |> Enum.filter(& &1 != "")
    |> Enum.reverse
    |> Enum.find_index(& &1 === "1")
    |> IO.puts

# https://atcoder.jp/contests/abs/submissions/21756387
