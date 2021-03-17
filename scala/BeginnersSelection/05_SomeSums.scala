// https://atcoder.jp/contests/abs/tasks/abc083_b

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine
      .split(" ")
      .map(_.toInt)
      |> ((x) => (x(0), x(1), x(2)))
      |> ((n: Int, a: Int, b: Int) =>
        (1 to n)
          .filter((ni) =>
            ni.toString
              .map(_.toString)
              .map(_.toInt)
              .sum
              |> (x => a <= x && x <= b)
          )
          .sum
      ).tupled
      |> println
  )
}

// https://atcoder.jp/contests/abs/submissions/19771583
