# https://atcoder.jp/contests/abs/tasks/arc089_a

use Bitwise

defmodule Main, do:
  def main(), do:
    IO.read(:line)
    |> String.trim
    |> String.to_integer
    |> (fn n -> (1..n) end).()
    |> Enum.map(fn _ ->
      IO.read(:line)
      |> String.trim
      |> String.split(" ")
      |> Enum.map(&String.to_integer/1)
    end)
    |> Enum.reduce([0, 0, 0], fn n, p ->
      if p !== :none
        && apply(fn t1, x1, y1, t2, x2, y2 ->
          (t2 &&& 1) === ((x2 + y2) &&& 1)
            && ((t2 - t1) >= (abs(x2 - x1) + abs(y2 - y1)))
        end, p ++ n),
      do: n, else: :none
    end)
    |> (& if &1 !== :none, do: "Yes", else: "No").()
    |> IO.puts

# https://atcoder.jp/contests/abs/submissions/21781453
