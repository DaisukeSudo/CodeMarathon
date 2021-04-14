# https://atcoder.jp/contests/abs/tasks/abc085_b

defmodule Main, do:
  def main(), do:
    IO.read(:line)
    |> String.trim
    |> String.to_integer
    |> (& (1..&1)).()
    |> Enum.map(fn _ -> IO.read(:line) end)
    |> MapSet.new
    |> MapSet.size
    |> IO.puts

# https://atcoder.jp/contests/abs/submissions/21759385
