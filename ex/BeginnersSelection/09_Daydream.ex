# https://atcoder.jp/contests/abs/tasks/arc065_a

defmodule Main, do:
  def main(), do:
    IO.read(:line)
    |> String.trim
    |> (fn s ->
      ["eraser", "erase", "dreamer", "dream"]
      |> Enum.reduce([s], fn word, acc ->
        Enum.flat_map(acc, & String.split(&1, word, trim: true))
      end)
    end).()
    |> Enum.count
    |> (& if &1 === 0, do: "YES", else: "NO").()
    |> IO.puts

# https://atcoder.jp/contests/abs/submissions/21781265
