// https://atcoder.jp/contests/abs/tasks/abc088_b

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine
      |> (_ => io.StdIn.readLine
        .split(" ")
        .map(_.toInt)
        .sorted
        .reverse
        .zipWithIndex
        .fold((0, 0)) { case ((a, b), (x, i)) =>
          if (i % 2 == 0)
            (a + x, b)
          else
            (a, b + x)
        }
      )
      |> { case (a, b) => a - b }
      |> println
  )
}

// https://atcoder.jp/contests/abs/submissions/19772466
