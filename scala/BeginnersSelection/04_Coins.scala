// https://atcoder.jp/contests/abs/tasks/abc087_b

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    (
      io.StdIn.readLine.toInt,
      io.StdIn.readLine.toInt,
      io.StdIn.readLine.toInt,
      io.StdIn.readLine.toInt
    )
    |> ((a: Int, b: Int, c: Int, x: Int) =>
      (0 to a.min(x / 500))
        .map((i: Int) =>
          (0 to b.min((x - 500 * i) / 100))
            .filter((j: Int) => x - 500 * i - 100 * j <= 50 * c)
            .length
        )
        .sum
    ).tupled
    |> println
  )
}

// https://atcoder.jp/contests/abs/submissions/19755674
