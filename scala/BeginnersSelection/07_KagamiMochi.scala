// https://atcoder.jp/contests/abs/tasks/abc085_b

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine.toInt
      |> ((n) =>
        (1 to n)
          .map(_ => io.StdIn.readLine)
          .distinct
          .length
      )
      |> println
  )
}

// https://atcoder.jp/contests/abs/submissions/19785957
