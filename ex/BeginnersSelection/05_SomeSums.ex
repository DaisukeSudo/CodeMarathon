# https://atcoder.jp/contests/abs/tasks/abc083_b

defmodule Main, do:
  def main(), do:
    IO.read(:line)
    |> String.trim
    |> String.split(" ")
    |> Enum.map(&String.to_integer/1)
    |> (fn t -> apply(fn n, a, b ->
      (1..n)
      |> Enum.filter(fn i ->
        i
        |> Integer.to_string
        |> String.split("", trim: true)
        |> Enum.map(&String.to_integer/1)
        |> Enum.sum
        |> (& a <= &1 && &1 <= b).()
      end)
      |> Enum.sum
    end, t) end).()
    |> IO.puts

# https://atcoder.jp/contests/abs/submissions/21757684
