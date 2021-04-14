# https://atcoder.jp/contests/abs/tasks/abc085_c

defmodule Main, do:
  def main(), do:
    IO.read(:line)
    |> String.trim
    |> String.split(" ")
    |> Enum.map(&String.to_integer/1)
    |> (& [Enum.at(&1, 0), Enum.at(&1, 1) / 1000]).()
    |> (fn t -> apply(fn n, y ->
      (0..n)
      |> Enum.find_value({:none}, fn a ->
        if (y - n - 9 * a)
          |> (& floor(&1 / 4) == ceil(&1 / 4) && &1 >= 0 && n - a - &1 / 4 >= 0).(),
        do: {:some, a}
      end)
      |> (fn
        {:some, a} ->
          ((y - n - 9 * a) / 4)
          |> trunc
          |> (fn b -> "#{a} #{b} #{n - a - b}" end).()
        {:none} ->
          "-1 -1 -1"
      end).()
    end, t) end).()
    |> IO.puts

# https://atcoder.jp/contests/abs/submissions/21763790
