// https://atcoder.jp/contests/abs/tasks/abc086_a

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine
      .split(" ")
      .map(_.toInt)
      .map(_ & 1)
      .reduce((a, x) => a & x)
      |> ((x) => if (x == 1) "Odd" else "Even")
      |> println
  )
}

// https://atcoder.jp/contests/abs/submissions/19753942
