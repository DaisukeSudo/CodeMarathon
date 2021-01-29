// https://atcoder.jp/contests/abs/tasks/abc081_a

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine
      .map(_.toString)
      .map(_.toInt)
      .sum
      |> println
  )
}

// https://atcoder.jp/contests/abs/submissions/19754090
