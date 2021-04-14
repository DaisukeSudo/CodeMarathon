# ----- 基本 -----

defmodule Main, do:
  def main(), do:
    IO.read(:line)
    |> String.trim
    |> String.to_integer
    |> (fn x -> x end).()
    |> (& &1 * &1).()
    |> IO.puts


# ----- Enum -----

(for x <- 1..9, do: x * 2)
|> IO.inspect
|> Enum.filter(& rem(&1, 3) == 0)
|> IO.inspect
|> Enum.map(& &1 / 3)
|> IO.inspect
|> Enum.map(&trunc/1)
|> IO.inspect
|> Enum.reduce(0, &+/2)
|> IO.inspect

# [2, 4, 6, 8, 10, 12, 14, 16, 18]
# [6, 12, 18]
# [2.0, 4.0, 6.0]
# [2, 4, 6]
# 12
